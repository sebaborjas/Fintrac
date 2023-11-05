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
        private readonly FintracContext _database;

        public AccountService(FintracContext database)
        {
            _database = database;
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

            _database.SaveChanges();
        }

        public Account Get(Workspace workspace, string name)
        {
            return _database.Users.Where(x => x.WorkspaceList.Contains(workspace)).FirstOrDefault<User>().WorkspaceList.Find(x => x.ID == workspace.ID).AccountList.Find(x => x.Name == name);
            
        }

        public void Modify(string oldName, Account accountModified)
        {
            try 
            {
                Account oldAccount = Get(accountModified.WorkSpace, oldName);

                Account newAccountAlreadyExists = Get(accountModified.WorkSpace, accountModified.Name);
                if (oldName != accountModified.Name && newAccountAlreadyExists != null)
                {
                    throw new AccountAlreadyExistsException();
                }

				oldAccount.Update(accountModified);
                
            }
            catch(Exception exception) 
            {
                throw exception;
            }

            _database.SaveChanges();
        }

        public void Delete(Workspace workspace, string name)
        {
            try 
            {
                Account account = Get(workspace, name);
                _database.Users.First(x => x.WorkspaceList.Contains(workspace)).WorkspaceList.First(x => x.ID == workspace.ID).AccountList.Remove(account);
            } catch(Exception exception) 
            {
                throw exception;
            }

            _database.SaveChanges();
        }
    }
}
