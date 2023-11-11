using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;
using Domain.Exceptions;

namespace BusinessLogic
{
    public class WorkspaceService
    {
        private readonly FintracContext _database;

        public WorkspaceService(FintracContext database)
        {
            this._database = database;
        }


        public void Add(User user, Workspace w)
        {
            if (user.WorkspaceList.Contains(w))
            {
                throw new WorkspaceAlreadyExistsException();
            }
            try
            {

                user.WorkspaceList.Add(w);
                _database.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }         
        }

        public Workspace Get(int ID)
        {

            return _database.Users.First(x => x.WorkspaceList.Any(x => x.ID == ID)).WorkspaceList.First(x => x.ID == ID);

        }

        public void UpdateName(Workspace workspace, string newName)
        {

            _database.Users.ToList().FindAll(x => x.WorkspaceList.Contains(workspace)).ForEach(x => x.WorkspaceList.Find(x => x.ID == workspace.ID).Name = newName);
            workspace.Name = newName;

            _database.SaveChanges();
        }

        public void DeleteWorkspace(Workspace workspace)
        {
            User oldUserAdmin = workspace.UserAdmin;

            List<User> users = _database.Users.ToList();

            int workspaceUsersCount = 0;

            foreach(User user in users)
            {
                foreach(Workspace currentWorkspace in user.WorkspaceList)
                {
                    if(currentWorkspace.ID == workspace.ID)
                    {
                        workspaceUsersCount++;
                    }
                }
            }

            

            if (workspaceUsersCount > 1)
            {
                User newUserAdmin = _database.Users.First(x => x.WorkspaceList.Contains(workspace) && x != oldUserAdmin);
                workspace.UserAdmin = newUserAdmin;
            }
            else
            {
                oldUserAdmin.WorkspaceList.Remove(workspace);
                _database.Users.First(x => x == oldUserAdmin).WorkspaceList.Remove(workspace);
                _database.SaveChanges();
            }
            
        }

        public List<Transaction> ListAllTransactionsAllAcounts(Workspace workspace)
        {
            List<Transaction> transactionList = new List<Transaction>();
            foreach (Account account in workspace.AccountList)
            {
                transactionList.AddRange(account.TransactionList);
            }
            return transactionList;
        }
    }

}
