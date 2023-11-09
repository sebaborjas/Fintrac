using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;

namespace BusinessLogic
{
    public class ExchangeService
    {
        private readonly MemoryDatabase _memoryDatabase;

        public ExchangeService(MemoryDatabase memoryDatabase)
        {
            this._memoryDatabase = memoryDatabase;
        }

        public void Add(Workspace workspace, Exchange exchange)
        {
            if (exchange.CurrencyValue <= 0)
            {
                throw new ArgumentException(("El valor de la moneda debe ser mayor a 0"));
            }
            if (workspace.ExchangeList.Contains(exchange))
            {
                throw new ExchangeAlreadyExistsException();
            }
            try
            {
                workspace.ExchangeList.Add(exchange);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Delete(Workspace workspace, Exchange exchange)
        {
            var user = _memoryDatabase.Users.FirstOrDefault(x => x.WorkspaceList.Contains(workspace));
            if (user != null)
            {
                var targetWorkspace = user.WorkspaceList.FirstOrDefault(x => x.ID == workspace.ID);
                if (targetWorkspace != null)
                {
                    var accountWithTransactions = targetWorkspace.AccountList.FirstOrDefault(x => x.TransactionList.Count > 0);
                    if (accountWithTransactions != null)
                    {
                        var transaction = accountWithTransactions.TransactionList.FirstOrDefault(x => x.CreationDate >= exchange.Date && x.Currency == exchange.Currency);
                        if (transaction != null)
                        {
                            throw new ExchangeHasTransactionsException();
                        }
                    }
                }
            }

            try
            {
                workspace.ExchangeList.Remove(exchange);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Exchange Get(ExchangeQueryParameters exchange)
        {
            var result = _memoryDatabase.Users.FirstOrDefault(user => user.WorkspaceList.Contains(exchange.Workspace));

            if (result != null)
            {
                var workspace = result.WorkspaceList.FirstOrDefault(w => w.ID == exchange.Workspace.ID);

                if (workspace != null)
                {
                    return workspace.ExchangeList.FirstOrDefault(x => x.Date == exchange.Date && x.Currency == exchange.CurrencyType);
                }
            }

            return null;
        }

        public void Update(Workspace workspace, Exchange exchange, double newCurrencyValue)
        {
            try
            {
                if (newCurrencyValue <= 0)
                {
                    throw new ArgumentException(("El valor de la moneda debe ser mayor a 0"));
                }
                ExchangeQueryParameters exchangeToGet = new ExchangeQueryParameters
                {
                    CurrencyType = exchange.Currency,
                    Workspace = exchange.Workspace,
                    Date = exchange.Date
                };

                Exchange exchangeToUpdate = Get(exchangeToGet);
                if (exchangeToUpdate == null)
                {
                    throw new ElementNotFoundException("No existe un cambio para esa fecha");
                }

                exchange.CurrencyValue = newCurrencyValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}