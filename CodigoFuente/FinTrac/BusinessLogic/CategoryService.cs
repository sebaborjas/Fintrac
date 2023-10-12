﻿using System;
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
                    var user = _memoryDatabase.Users.Find(x => x.WorkspaceList.Contains(workspace));
                    if (user != null)
                    {
                        var targetWorkspace = user.WorkspaceList.Find(x => x.ID == workspace.ID);
                        if (targetWorkspace != null)
                        {
                            var accountWithTransactions = targetWorkspace.AccountList.Find(x => x.TransactionList.Count > 0);
                            if (accountWithTransactions != null)
                            {
                                var transaction = accountWithTransactions.TransactionList.Find(x => x.Category == Get(workspace, categoryToUpdate));
                                if (transaction != null)
                                {
                                    if (Get(workspace, categoryToUpdate).Status != updatedCategory.Status || Get(workspace, categoryToUpdate).Type != updatedCategory.Type)
                                    {
                                        throw new CategoryHasTransactionsException("No se puede cambiar el tipo ni el estado a una categoría que tiene transacciones");
                                    }
                                    accountWithTransactions.TransactionList.Find(x => x.Category == Get(workspace, categoryToUpdate)).Category = updatedCategory;
                                }
                            }
                        }
                        targetWorkspace.CategoryList.Find(x => x.Name == categoryToUpdate).Name = updatedCategory.Name;
                    }
                }
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}