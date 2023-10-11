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
    public class CategoryService
    {
        private readonly MemoryDatabase _memoryDatabase;

        public CategoryService(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public void Add(Workspace workspace, Category category)
        {
            if (workspace.CategoryList.Contains(category))
            {
                throw new CategoryAlreadyExistsException();
            }
            try
            {
                workspace.CategoryList.Add(category);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Category Get(Workspace workspace, string name)
        {
            return _memoryDatabase.Users.Find(x => x.WorkspaceList.Contains(workspace)).WorkspaceList.Find(x => x.ID == workspace.ID).CategoryList.Find(x => x.Name == name);
        }

        public void Delete(Workspace workspace, Category category)
        {
            var user = _memoryDatabase.Users.Find(x => x.WorkspaceList.Contains(workspace));
            if (user != null)
            {
                var targetWorkspace = user.WorkspaceList.Find(x => x.ID == workspace.ID);
                if (targetWorkspace != null)
                {
                    var accountWithTransactions = targetWorkspace.AccountList.Find(x => x.TransactionList.Count > 0);
                    if (accountWithTransactions != null)
                    {
                        var transaction = accountWithTransactions.TransactionList.Find(x => x.Category == category);
                        if (transaction != null)
                        {
                            throw new CategoryHasTransactionsException();
                        }
                    }
                }
            }
            try
            {
                workspace.CategoryList.Remove(category);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}