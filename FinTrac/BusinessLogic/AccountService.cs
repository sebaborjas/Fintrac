//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain;
//using Domain.Exceptions;

//namespace BusinessLogic
//{
//    public class AccountService
//    {
//        private readonly MemoryDatabase _memoryDatabase;

//        public AccountService(MemoryDatabase memoryDatabase)
//        {
//            _memoryDatabase = memoryDatabase;
//        }

//        public void Add(Account account)
//        {
//            _memoryDatabase.Accounts.Add(account);
//        }

//        public Account Get(string name)
//        {
//            Account acccount = _memoryDatabase.Accounts.Find(x => x.Name == name);

//            if (acccount == null)
//            {
//                throw new ElementNotFoundException("La cuenta no se encunetra en el sistema");
//            }
//            return acccount;
//        }

//        public void Modify(string name, Account accountModified)
//        {
//            try 
//            {
//                Account account = Get(name);

//                Account acccount = _memoryDatabase.Accounts.Find(x => x.Name == accountModified.Name);
//                if (acccount != null)
//                {
//                    throw new AccountAlreadyExistsException();
//                }

//                account.Update(accountModified);
                
//            }
//            catch(Exception exception) 
//            {
//                throw exception;
//            }
//        }

//        public void Delete(string name)
//        {
//            try 
//            {
//                Account account = Get(name);
//                _memoryDatabase.Accounts.Remove(account);
//            } catch(Exception exception) 
//            {
//                throw exception;
//            }
//        }
//    }
//}
