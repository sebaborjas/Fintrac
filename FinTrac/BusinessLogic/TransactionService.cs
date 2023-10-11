using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

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
            throw new NotImplementedException();
        }

        public void Duplicate(Account account, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Transaction Get(Account account, string title)
        {
            throw new NotImplementedException();
        }

        public Transaction Modify(string title, DateTime date, Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
