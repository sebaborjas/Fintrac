using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace BusinessLogic
{
    public class AccountService
    {
        private readonly MemoryDatabase _memorryDatabase;

        public AccountService(MemoryDatabase memorryDatabase)
        {
            _memorryDatabase = memorryDatabase;
        }

        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public Account Get(string name)
        {
            throw new NotImplementedException();
        }

        public void Modify(string name, Account accountModified)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name)
        {
            throw new NotImplementedException();
        }
    }
}
