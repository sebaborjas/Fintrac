using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestTransactionService
    {
        private TransactionService _service;
        private AccountService _accountService;
        private WorkspaceService _workspaceService;
        private UserService _userService;
        private ExchangeService _exchangeService;
		private CategoryService _categoryService;

		private Workspace _workspace;
        private User _user;
        private Account _account;
        private Category _category;
        private Transaction _transaction;
        private Exchange _exchange;

        [TestInitialize]
        public void SetUp()
        {
            FintracContext _database = TestContextFactory.CreateContext();
            _service = new TransactionService(_database);
            _accountService = new AccountService(_database);
            _workspaceService = new WorkspaceService(_database);
            _userService = new UserService(_database);
			_categoryService = new CategoryService(_database);
			_exchangeService = new ExchangeService(_database);

            _user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };
			_userService.Add(_user);

			_workspace = new Workspace { UserAdmin = _user, Name = $"Espacio personal de {_user.Name} {_user.LastName}" };
			_workspaceService.Add(_user, _workspace);

			_account = new PersonalAccount { Name = "Caja de ahorro", WorkSpace = _workspace, Currency = CurrencyType.UYU };
			_accountService.Add(_workspace, _account);

			_category = new Category { Name = "Gastos casa", Status = CategoryStatus.Active, Type = CategoryType.Cost };
            _categoryService.Add(_workspace, _category);

            _exchange = new Exchange { Date = DateTime.Today.AddDays(-2), CurrencyValue = 40, Workspace = _workspace };
			_exchangeService.Add(_workspace, _exchange);




		}

        [TestMethod]
        public void AddTransactionFoundExchange()
        {
			_transaction = new Transaction { Account = _account, Amount = 100, Category = _category, Currency = CurrencyType.UYU, Title = "Test" };
			_service.Add(_account, _transaction);
            Assert.AreEqual(_transaction, _service.Get(_account, _transaction.ID));
        }

        [TestMethod]
        [ExpectedException(typeof(ExchangeNotFoundException))]
        public void AddTransactionNotFoundExchange()
        {
			_transaction = new Transaction { Account = _account, Amount = 100, Category = _category, Currency = CurrencyType.USD, Title = "Test" };
			_exchangeService.Delete(_workspace, _exchange);
            _service.Add(_account, _transaction);
            Assert.AreEqual(_transaction, _service.Get(_account, _transaction.ID));
        }

        [TestMethod]
        public void DuplicateTransaction()
        {
			_transaction = new Transaction { Account = _account, Amount = 100, Category = _category, Currency = CurrencyType.UYU, Title = "Test" };
			_service.Add(_account, _transaction);
            _service.Duplicate(_account, _transaction);
            Assert.IsTrue(_account.Transactions.Count == 2);
        }

        [TestMethod]
        public void ModifyTransaction()
        {
			_transaction = new Transaction { Account = _account, Amount = 100, Category = _category, Currency = CurrencyType.UYU, Title = "Test" };
			_service.Add(_account, _transaction);
            string newTitle = "Transaccion editada";
            double newAmount = 200;
            _service.Modify(_transaction, newTitle, newAmount);
            Assert.AreEqual(newTitle, _transaction.Title);
            Assert.AreEqual(newAmount, _transaction.Amount);
        }


        [TestMethod]
        public void GetTransaction()
        {
			_transaction = new Transaction { Account = _account, Amount = 100, Category = _category, Currency = CurrencyType.UYU, Title = "Test" };
			_service.Add(_account, _transaction);
            Assert.AreEqual(_transaction, _service.Get(_account, _transaction.ID));
        }
    }
}
