﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain.DataTypes;
using Domain;
using Domain.Exceptions;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestCategoryService
    {
        private CategoryService _service;
        private WorkspaceService _workspaceService;
        private UserService _userService;
        private Workspace _workspace;
        private User _user;
        private Category _category;

        [TestInitialize]
        public void SetUp()
        {
            MemoryDatabase _database = new MemoryDatabase();
            _service = new CategoryService(_database);
            _workspaceService = new WorkspaceService(_database);
            _userService = new UserService(_database);
            _user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };
            _workspace = new Workspace(_user, "Test");
            _userService.Add(_user);
            _workspaceService.Add(_user, _workspace);
            _category = new Category
            {
                CreationDate = DateTime.Today.AddDays(-5),
                Name = "Test",
                Workspace = _workspace,
                Type = CategoryType.Income,
                Status = CategoryStatus.Active
            };
        }


        [TestMethod]
        public void AddCategory()
        {
            _service.Add(_category.Workspace, _category);
            Assert.AreEqual(_category, _service.Get(_category.Workspace, _category.Name));
        }

        [TestMethod]
        public void GetCategory()
        {
            _service.Add(_category.Workspace, _category);
            Assert.AreEqual(_category, _service.Get(_category.Workspace, _category.Name));
        }

        [TestMethod]
        public void DeleteCategoryNoTransactionInWorkspace()
        {
            _service.Add(_category.Workspace, _category);
            _service.Delete(_category.Workspace, _category);
            Assert.IsNull(_service.Get(_category.Workspace, _category.Name));
        }

        [TestMethod]
        [ExpectedException(typeof(CategoryHasTransactionsException))]
        public void DeleteExchangeHaveTransactionInWorkspace()
        {
            _service.Add(_category.Workspace, _category);
            Account personalAccount = new PersonalAccount
            {
                Name = "Test",
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            Transaction transaction = new Transaction
            {
                Title = "Test",
                CreationDate = DateTime.Today,
                Amount = 100,
                Currency = CurrencyType.UYU,
                Category = _category,
                Account = personalAccount,
            };
            personalAccount.TransactionList.Add(transaction);
            _workspace.AccountList.Add(personalAccount);
            _service.Delete(_category.Workspace, _category);
        }
    }
}

