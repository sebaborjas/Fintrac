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
		private readonly FintracContext _database;
		public ExchangeService(FintracContext database)
		{
			this._database = database;
		}

		public void Add(Workspace workspace, Exchange exchange)
		{
			if (exchange.CurrencyValue <= 0)
			{
				throw new ArgumentException(("El valor de la moneda debe ser mayor a 0"));
			}
			if (workspace.Exchanges.Contains(exchange))
			{
				throw new ExchangeAlreadyExistsException();
			}
			try
			{
				workspace.Exchanges.Add(exchange);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			_database.SaveChanges();
		}

		public void Delete(Workspace workspace, Exchange exchange)
		{
			var user = _database.Users.FirstOrDefault(x => x.Workspaces.Contains(workspace));
			if (user == null)
			{
				throw new ExchangeNotFoundException();
			}
			var targetWorkspace = user.Workspaces.FirstOrDefault(x => x.ID == workspace.ID);
			if (targetWorkspace == null)
			{
				throw new ExchangeNotFoundException();

			}
			var accountWithTransactions = targetWorkspace.Accounts.FirstOrDefault(x => x.Transactions.Count > 0 && x.Currency == exchange.Currency);
			if (accountWithTransactions == null)
			{
				workspace.Exchanges.Remove(exchange);
				return;
			}
			var transaction = accountWithTransactions.Transactions.FirstOrDefault(x => x.CreationDate >= exchange.Date && x.Currency == exchange.Currency);
			if (transaction != null)
			{
				throw new ExchangeHasTransactionsException();
			}
			try
			{
				workspace.Exchanges.Remove(exchange);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			_database.SaveChanges();
		}

		public Exchange Get(ExchangeQueryParameters exchange)
		{
			Workspace workspace = exchange.Workspace;
			User user = _database.Users.FirstOrDefault(x => x.Workspaces.Contains(workspace));
			if (user == null)
			{
				return null;
			}
			var workspaceFounded = user.Workspaces.FirstOrDefault(w => w.ID == exchange.Workspace.ID);

			if (workspace == null)
			{
				return null;
			}
			Exchange exchangeToReturn = workspace.Exchanges.FirstOrDefault(x => x.Date == exchange.Date && x.Currency == exchange.CurrencyType);
			if (exchangeToReturn == null)
			{
				return null;
			}
			return exchangeToReturn;
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
			_database.SaveChanges();
		}
	}
}