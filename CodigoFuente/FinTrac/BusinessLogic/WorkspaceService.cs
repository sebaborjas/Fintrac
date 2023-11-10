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

            }
            catch (Exception exception)
            {
                throw exception;
            }

            _database.SaveChanges();
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
            if (_database.Users.Count(x => x.WorkspaceList.Contains(workspace)) > 1)
            {
                User newUserAdmin = _database.Users.First(x => x.WorkspaceList.Contains(workspace) && x != oldUserAdmin);
                workspace.UserAdmin = newUserAdmin;
            }
            else
            {
                oldUserAdmin.WorkspaceList.Remove(workspace);
                _database.Users.First(x => x == oldUserAdmin).WorkspaceList.Remove(workspace);
            }
            _database.SaveChanges();
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
