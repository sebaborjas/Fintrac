using BusinessLogic;
using Domain.DataTypes;
using Domain;
using Domain.Exceptions;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestExchangeService
    {
        private ExchangeService _service;
        private WorkspaceService _workspaceService;
        private UserService _userService;

        private Workspace _workspace;
        private User _user;
        private Exchange _exchange;
        private ExchangeQueryParameters _exchangeQueryParameters;

        [TestInitialize]
        public void SetUp()
        {
			FintracContext _database = TestContextFactory.CreateContext();
			_service = new ExchangeService(_database);
            _workspaceService = new WorkspaceService(_database);
            _userService = new UserService(_database);

            _user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };
            _workspace = new Workspace{ UserAdmin = _user, Name = $"Espacio personal de {_user.Name} {_user.LastName}" };

            _userService.Add(_user);
            _workspaceService.Add(_user, _workspace);

            _exchange = new Exchange
            {
                Date = DateTime.Today,
                CurrencyValue = 38.4,
                Workspace = _workspace,
            };

            _exchangeQueryParameters = new ExchangeQueryParameters
            {
                Workspace = _exchange.Workspace,
                Date = _exchange.Date,
                CurrencyType = _exchange.Currency,
            };

        }

        [TestMethod]
        public void AddExchange()
        {
            _service.Add(_exchange.Workspace, _exchange);
            Assert.AreEqual(_exchange, _service.Get(_exchangeQueryParameters));
        }

        [TestMethod]
        [ExpectedException(typeof(ExchangeAlreadyExistsException))]
        public void AddDuplicateExchange()
        {
            _service.Add(_exchange.Workspace, _exchange);
            _service.Add(_exchange.Workspace, _exchange);
        }

        [TestMethod]
        public void GetExchange()
        {
            _service.Add(_exchange.Workspace, _exchange);
            Assert.AreEqual(_exchange, _service.Get(_exchangeQueryParameters));
        }

        [TestMethod]
        public void UpdateExchange()
        {
            _service.Add(_exchange.Workspace, _exchange);
            double newDollarValue = 40;
            _service.Update(_exchange.Workspace, _exchange, newDollarValue);
            Assert.AreEqual(newDollarValue, _service.Get(_exchangeQueryParameters).CurrencyValue);
        }

        [TestMethod]
        public void DeleteExchangeNoTransactionInWorkspace()
        {
            _service.Add(_exchange.Workspace, _exchange);
            _service.Delete(_exchange.Workspace, _exchange);
            Assert.IsNull(_service.Get(_exchangeQueryParameters));
        }

        [TestMethod]
        [ExpectedException (typeof(ExchangeHasTransactionsException))]
        public void DeleteExchangeHaveTransactionInWorkspace()
        {
            _service.Add(_exchange.Workspace, _exchange);
            Account personalAccount = new PersonalAccount
            {
                Name = "Test",
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };
            Category category = new Category
            {
                CreationDate = DateTime.Today.AddDays(-1),
                Name = "Test",
                Type = CategoryType.Income,
                Workspace = _workspace,
            };
            Transactions transaction = new Transactions
            {
                Title = "Test",
                CreationDate = DateTime.Today,
                Amount = 100,
                Currency = CurrencyType.UYU,
                Category = category,
                Account = personalAccount,
            };
            personalAccount.Transactions.Add(transaction);
            _workspace.Accounts.Add(personalAccount);
            _service.Delete(_exchange.Workspace, _exchange);

        }
    }
}
