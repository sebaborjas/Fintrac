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
        private Exchange exchange;

        [TestInitialize]
        public void SetUp()
        {
            MemoryDatabase _database = new MemoryDatabase();
            _service = new ExchangeService(_database);
            _workspaceService = new WorkspaceService(_database);
            _userService = new UserService(_database);

            _user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };
            _workspace = new Workspace(_user, "Test");

            _userService.Add(_user);
            _workspaceService.Add(_user, _workspace);

            exchange = new Exchange
            {
                Date = DateTime.Today,
                DollarValue = 38.4,
                Workspace = _workspace,
            };
        }

        [TestMethod]
        public void AddExchange()
        {
            _service.Add(exchange.Workspace, exchange);
            Assert.AreEqual(exchange, _service.Get(exchange.Workspace, exchange.Date));
        }

        [TestMethod]
        [ExpectedException(typeof(ExchangeAlreadyExistsException))]
        public void AddDuplicateExchange()
        {
            _service.Add(exchange.Workspace, exchange);
            _service.Add(exchange.Workspace, exchange);
        }

        [TestMethod]
        public void GetExchange()
        {
            _service.Add(exchange.Workspace, exchange);
            Assert.AreEqual(exchange, _service.Get(exchange.Workspace, exchange.Date));
        }

        [TestMethod]
        public void UpdateExchange()
        {
            _service.Add(exchange.Workspace, exchange);
            double newDollarValue = 40;
            _service.Update(exchange.Workspace, exchange, newDollarValue);
            Assert.AreEqual(newDollarValue, _service.Get(exchange.Workspace, exchange.Date).DollarValue);
        }

        [TestMethod]
        public void DeleteExchangeNoTransactionInWorkspace()
        {
            _service.Add(exchange.Workspace, exchange);
            _service.Delete(exchange.Workspace, exchange);
            Assert.IsNull(_service.Get(exchange.Workspace, exchange.Date));
        }

        [TestMethod]
        [ExpectedException (typeof(ExchangeHasTransactionsException))]
        public void DeleteExchangeHaveTransactionInWorkspace()
        {
            _service.Add(exchange.Workspace, exchange);
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
            Transaction transaction = new Transaction
            {
                Title = "Test",
                CreationDate = DateTime.Today,
                Amount = 100,
                Currency = CurrencyType.UYU,
                Category = category,
                Account = personalAccount,
            };
            personalAccount.TransactionList.Add(transaction);
            _workspace.AccountList.Add(personalAccount);
            _service.Delete(exchange.Workspace, exchange);

        }
    }
}
