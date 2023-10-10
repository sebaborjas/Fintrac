using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class AccountService
    {
        private readonly MemoryDatabase _memorryDatabase;

        public AccountService(MemoryDatabase memorryDatabase)
        {
            _memorryDatabase = memorryDatabase;
        }
    }
}
