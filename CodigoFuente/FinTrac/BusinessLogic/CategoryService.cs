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
            if (workspace.Categories.Contains(category))
            {
                throw new CategoryAlreadyExistsException();
            }
            try
            {
                workspace.Categories.Add(category);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            _database.SaveChanges();
        }

        public Category Get(Workspace workspace, string name)
        {
            User user = _database.Users.Where(x => x.Workspaces.Contains(workspace)).FirstOrDefault<User>();

            if (user == null)
            {
                return null;
            }
            Workspace workspaceToFind = user.Workspaces.FirstOrDefault(x => x.ID == workspace.ID);
            if (workspaceToFind == null)
            {
                return null;
            }
            Category category = workspaceToFind.Categories.FirstOrDefault(x => x.Name == name);
            if (category == null)
            {
                return null;
            }
            return category;
        }

        public void Delete(Workspace workspace, Category category)
        {
            var user = _database.Users.Where(x => x.Workspaces.Contains(workspace)).FirstOrDefault<User>();
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            var targetWorkspace = user.Workspaces.Find(x => x.ID == workspace.ID);
            if (targetWorkspace == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var account in targetWorkspace.Accounts.Where(a => a.Transactions.Count > 0))
            {
                var transaction = account.Transactions.Find(x => x.Category == category);
                if (transaction != null)
                {
                    throw new CategoryHasTransactionsException();
                }
            }
            try
            {
                workspace.Categories.Remove(category);
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
                    var user = _database.Users.Where(x => x.Workspaces.Contains(workspace)).FirstOrDefault<User>();
                    if (user == null)
                    {
                        throw new ArgumentNullException();
                    }
                    var targetWorkspace = user.Workspaces.Find(x => x.ID == workspace.ID);
                    if (targetWorkspace == null)
                    {
                        throw new ArgumentNullException();
                    }
                    var accountWithTransactions = targetWorkspace.Accounts.Find(x => x.Transactions.Count > 0);
                    if (accountWithTransactions == null)
                    {
                        targetWorkspace.Categories.Find(x => x.Name == categoryToUpdate).Name = updatedCategory.Name;
                        return;
                    }
                    foreach (var account in targetWorkspace.Accounts.Where(a => a.Transactions.Count > 0))
                    {
                        var transaction = account.Transactions.Find(x => x.Category.Name == updatedCategory.Name);
                        if (transaction != null)
                        {
                            if (Get(workspace, categoryToUpdate).Status != updatedCategory.Status || Get(workspace, categoryToUpdate).Type != updatedCategory.Type)
                            {
                                throw new CategoryHasTransactionsException("No se puede cambiar el tipo ni el estado a una categoría que tiene transacciones");
                            }
                            else
                            {
                                targetWorkspace.Categories.Find(x => x.Name == categoryToUpdate).Name = updatedCategory.Name;
                            }
                        }
                    }
                    Category category = Get(workspace, updatedCategory.Name);
                    category.Status = updatedCategory.Status;
                    category.Type = updatedCategory.Type;
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