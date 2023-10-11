using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;
using Domain.DataTypes;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestTransactionService
    {
        private TransactionService _service;
        private AccountService _accountService;
        private WorkspaceService _workspaceService;
        private UserService _userService;

        private Workspace _workspace;
        private User _user;
        private Account _account;
        private Category _category;
        private Transaction _transaction;

        [TestInitialize]
        public void SetUp()
        {
            MemoryDatabase _database = new MemoryDatabase();
            _service = new TransactionService(_database);
            _accountService = new AccountService(_database);
            _workspaceService = new WorkspaceService(_database);
            _userService = new UserService(_database);

            _user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };
            _workspace = new Workspace(_user, "Test");
            _account = new PersonalAccount { Name = "Caja de ahorro", WorkSpace = _workspace, Currency = CurrencyType.UYU};
            _category = new Category {Name = "Gastos casa", Status = CategoryStatus.Active, Type = CategoryType.Cost};
            _transaction = new Transaction { Account = _account, Amount = 100, Category = _category, Currency = CurrencyType.UYU, Title = "Test" };

            _userService.Add(_user);
            _workspaceService.Add(_user, _workspace);
            _accountService.Add(_workspace, _account);
        }

        [TestMethod]
        public void AddTransaction() 
        {
            _service.Add(_account,_transaction);
            Assert.AreEqual(_transaction, _service.Get(_account, _transaction.Title));
        }

        [TestMethod]
        public void DuplicateTransaction()
        {
            _service.Duplicate(_account, _transaction);

        }

        [TestMethod]
        public void ModifyTransaction() 
        {
            string oldTile = _transaction.Title;
            DateTime date = _transaction.CreationDate;
            _transaction.Title = "Test2";
            _transaction.Amount = 200;
            _service.Modify(oldTile, date, _transaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidModifyTransaction() { }

        [TestMethod]
        public void GetTransaction() { }
    }
}
