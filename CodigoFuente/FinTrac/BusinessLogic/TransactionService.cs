using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;
using Domain.DataTypes;

namespace BusinessLogic
{
    public class TransactionService
    {
        private readonly MemoryDatabase _memoryDatabase;

        public TransactionService(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public void Add(Account account, Transaction transaction)
        {
            if (account.TransactionList.Contains(transaction))
            {
                throw new TransactionAlreadyExistsException();
            }
            var user = _memoryDatabase.Users.FirstOrDefault(x => x.WorkspaceList.Contains(account.WorkSpace));
            if (user != null)
            {
                var targetWorkspace = user.WorkspaceList.FirstOrDefault(x => x.ID == account.WorkSpace.ID);
                if (targetWorkspace != null)
                {
                    var exchange = targetWorkspace.ExchangeList.Find(x => x.Date <= transaction.CreationDate && x.Currency == transaction.Currency);
                    if (exchange == null && transaction.Currency != CurrencyType.UYU)
                    {
                        throw new ExchangeNotFoundException();
                    }
                }
            }

            try
            {
                account.TransactionList.Add(transaction);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Duplicate(Account account, Transaction transaction)
        {
            Transaction duplicatedTransaction = new Transaction
            {
                Title = transaction.Title,
                Amount = transaction.Amount,
                CreationDate = DateTime.Today,
                Currency = transaction.Currency,
                Category = transaction.Category,
                Account = transaction.Account
            };
            account.TransactionList.Add(duplicatedTransaction);
        }

        public Transaction Get(Account account, int transactionID)
        {
            return _memoryDatabase.Users.Find(x => x.WorkspaceList.Contains(account.WorkSpace)).WorkspaceList.Find(x => x.ID == account.WorkSpace.ID).AccountList.Find(x => x == account).TransactionList.Find(x => x.ID == transactionID);
        }

        public void Modify(Transaction transaction, string title, double amount)
        {
            Transaction transactionToUpdate = Get(transaction.Account, transaction.ID);
            transactionToUpdate.Amount = amount;
            transactionToUpdate.Title = title;
        }
    }
}
