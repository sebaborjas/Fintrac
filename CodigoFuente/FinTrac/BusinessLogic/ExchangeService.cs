using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            if (exchange.DollarValue <= 0)
            {
                throw new ArgumentException(("El valor del dolar debe ser mayor a 0"));
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
                        var transaction = accountWithTransactions.TransactionList.FirstOrDefault(x => x.CreationDate >= exchange.Date);
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

        public Exchange Get(Workspace workspace, DateTime dateTime)
        {
            return _memoryDatabase.Users.Find(x => x.WorkspaceList.Contains(workspace)).WorkspaceList.Find(x => x.ID == workspace.ID).ExchangeList.Find(x => x.Date == dateTime);

        }

        public void Update(Workspace workspace, Exchange exchange, double newDollarValue)
        {
            try
            {
                if(newDollarValue <= 0)
                {
                       throw new ArgumentException(("El valor del dolar debe ser mayor a 0"));
                }
                Exchange exchangeToUpdate = Get(workspace, exchange.Date);
                if (exchangeToUpdate == null)
                {
                    throw new ElementNotFoundException("No existe un cambio para esa fecha");
                }

                exchange.DollarValue = newDollarValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}