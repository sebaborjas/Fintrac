using Domain;
using BusinessLogic;
using Domain.Exceptions;
using Domain.DataTypes;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestWorkspaceService
    {
        private WorkspaceService _service;
        private UserService _userService;
        private MemoryDatabase newMemory;
        [TestInitialize]
        public void SetUp()
        {
            newMemory = new MemoryDatabase();
            _service = new WorkspaceService(newMemory);
            _userService = new UserService(newMemory);

        }
        [TestMethod]
        public void AddWorkspace()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");

            _userService.Add(useradmin);
            _service.Add(useradmin, workspace);
            Assert.AreEqual(workspace, useradmin.WorkspaceList.First(x => x == workspace));

        }
        [TestMethod]
        [ExpectedException(typeof(WorkspaceAlreadyExistsException))]
        public void AddWorkspaceAlreadyExists()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");

            _userService.Add(useradmin);
            _service.Add(useradmin, workspace);
            _service.Add(useradmin, workspace);

        }

        [TestMethod]
        public void GetWorkspace()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");

            _userService.Add(useradmin);
            _service.Add(useradmin,workspace);

            Assert.AreEqual(workspace, _service.Get(workspace.ID));
        }

        [TestMethod]
        public void UpdateWorkspaceName()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");
            _service.Add(useradmin, workspace);

            _service.UpdateName(workspace, "Nuevo Workspace");
            Assert.AreEqual("Nuevo Workspace", workspace.Name);
        }


        [TestMethod]
        public void DeleteWorkspaceWithOtherUsers()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            User otherUser = new User { Name = "Other", LastName = "Test", Email = "test@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");

            _userService.Add(useradmin);
            _userService.Add(otherUser);
            _service.Add(useradmin, workspace);
            _service.Add(otherUser, workspace);

            _service.DeleteWorkspace(workspace);

            Assert.AreEqual(workspace.UserAdmin, otherUser);
        }

        [TestMethod]
        public void ListAllTransactions()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");


            Account personalAccount = new PersonalAccount
            {
                Name = "Test",
                CreationDate = DateTime.Today.AddDays(-5),
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            Account creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = 1234,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Credit Santander",
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            Category category = new Category
            {
                Name = "Test",
                CreationDate = DateTime.Today.AddDays(-10),
                Status = CategoryStatus.Active,
                Workspace = workspace,
                Type = CategoryType.Income
            };

            Transaction transaction1 = new Transaction
            {
                Title = "TransactionTest",
                Account = creditCardAccount,
                Category = category,
                CreationDate = DateTime.Today.AddDays(-1),
                Amount = 100,
                Currency = CurrencyType.UYU,
            };

            Transaction transaction2 = new Transaction
            {
                Title = "TransactionTest",
                Account = personalAccount,
                Category = category,
                CreationDate = DateTime.Today.AddDays(-1),
                Amount = 100,
                Currency = CurrencyType.UYU,
            };
            creditCardAccount.TransactionList.Add(transaction1);
            personalAccount.TransactionList.Add(transaction2);
            workspace.AccountList.Add(creditCardAccount);
            workspace.AccountList.Add(personalAccount);
            workspace.CategoryList.Add(category);

            _userService.Add(useradmin);
            _service.Add(useradmin, workspace);

            int totalTransaction = _service.ListAllTransactionsAllAcounts(workspace).Count;
            Assert.AreEqual(2, totalTransaction);

        }
    }
}


