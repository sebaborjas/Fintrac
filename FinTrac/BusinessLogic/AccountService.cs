using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;

namespace BusinessLogic
{
    public class AccountService
    {
        private readonly MemoryDatabase _memoryDatabase;

        public AccountService(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public void Add(Workspace workspace, Account account)
        {
            if (workspace.AccountList.Contains(account))
            {
                throw new AccountAlreadyExistsException();
            }
            try
            {
                workspace.AccountList.Add(account);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Account Get(Workspace workspace, string name)
        {
            return _memoryDatabase.Users.Find(x => x.WorkspaceList.Contains(workspace)).WorkspaceList.Find(x => x.ID == workspace.ID).AccountList.Find(x => x.Name == name);
            
        }

        public void Modify(string name, Account accountModified)
        {
            try 
            {
                Account account = Get(accountModified.WorkSpace, name);

                Account acccount = Get(accountModified.WorkSpace, accountModified.Name);
                if (acccount != null)
                {
                    throw new AccountAlreadyExistsException();
                }

                account.Update(accountModified);
                
            }
            catch(Exception exception) 
            {
                throw exception;
            }
        }

        public void Delete(Workspace workspace, string name)
        {
            try 
            {
                Account account = Get(workspace, name);
                _memoryDatabase.Users.First(x => x.WorkspaceList.Contains(workspace)).WorkspaceList.First(x => x.ID == workspace.ID).AccountList.Remove(account);
            } catch(Exception exception) 
            {
                throw exception;
            }
        }
    }
}
