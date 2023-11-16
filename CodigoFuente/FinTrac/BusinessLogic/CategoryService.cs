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
        private readonly FintracContext _database;

        public CategoryService(FintracContext database)
        {
            _database = database;
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

            _database.SaveChanges();
        }

        public Category Get(Workspace workspace, string name)
        {
            User user = _database.Users.Where(x => x.WorkspaceList.Contains(workspace)).FirstOrDefault<User>();

            if (user == null)
            {
                return null;
            }

            Workspace workspaceToFind = user.WorkspaceList.FirstOrDefault(x => x.ID == workspace.ID);

            if (workspaceToFind == null)
            {
                return null;
            }

            Category category = workspaceToFind.CategoryList.FirstOrDefault(x => x.Name == name);

            if (category == null)
            {
                return null;
            }

            return category;
        }

        public void Delete(Workspace workspace, Category category)
        {
            var user = _database.Users.Where(x => x.WorkspaceList.Contains(workspace)).FirstOrDefault<User>();
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            var targetWorkspace = user.WorkspaceList.Find(x => x.ID == workspace.ID);
            if (targetWorkspace == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var account in targetWorkspace.AccountList.Where(a => a.TransactionList.Count > 0))
            {
                var transaction = account.TransactionList.Find(x => x.Category == category);
                if (transaction != null)
                {
                    throw new CategoryHasTransactionsException();
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

            _database.SaveChanges();
        }

        public void Update(Workspace workspace, string categoryToUpdate, Category updatedCategory)
        {
            try
            {
                if (categoryToUpdate != updatedCategory.Name)
                {
                    Category category = Get(workspace, updatedCategory.Name);
                    if (category != null)
                    {
                        throw new CategoryAlreadyExistsException();
                    }
                }
                else
                {
                    var user = _database.Users.Where(x => x.WorkspaceList.Contains(workspace)).FirstOrDefault<User>();
                    if (user == null)
                    {
                        throw new ArgumentNullException();
                    }
                    var targetWorkspace = user.WorkspaceList.Find(x => x.ID == workspace.ID);
                    if (targetWorkspace == null)
                    {
                        throw new ArgumentNullException();
                    }
                    var accountWithTransactions = targetWorkspace.AccountList.Find(x => x.TransactionList.Count > 0);
                    if (accountWithTransactions == null)
                    {
                        targetWorkspace.CategoryList.Find(x => x.Name == categoryToUpdate).Name = updatedCategory.Name;
                        return;
                    }
                    foreach (var account in targetWorkspace.AccountList.Where(a => a.TransactionList.Count > 0))
                    {
                        var transaction = account.TransactionList.Find(x => x.Category == updatedCategory);
                        if (transaction != null)
                        {
                            throw new CategoryHasTransactionsException();
                        }
                    }
                    if (Get(workspace, categoryToUpdate).Status != updatedCategory.Status || Get(workspace, categoryToUpdate).Type != updatedCategory.Type)
                    {
                        throw new CategoryHasTransactionsException("No se puede cambiar el tipo ni el estado a una categoría que tiene transacciones");
                    }
                    targetWorkspace.CategoryList.Find(x => x.Name == categoryToUpdate).Name = updatedCategory.Name;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            _database.SaveChanges();
        }
    }
}