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
        private FintracContext newMemory;
        [TestInitialize]
        public void SetUp()
        {
            newMemory = TestContextFactory.CreateContext();
            _service = new WorkspaceService(newMemory);
            _userService = new UserService(newMemory);

        }
        [TestMethod]
        public void AddWorkspace()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace{ UserAdmin = useradmin, Name = "Nuevo Espacio" };

            _userService.Add(useradmin);
            _service.Add(useradmin, workspace);
            Assert.AreEqual(workspace, useradmin.WorkspaceList.First(x => x == workspace));

        }
        [TestMethod]
        [ExpectedException(typeof(WorkspaceAlreadyExistsException))]
        public void AddWorkspaceAlreadyExists()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace{ UserAdmin = useradmin, Name = $"Espacio personal de {useradmin.Name} {useradmin.LastName}" };

            _userService.Add(useradmin);
            _service.Add(useradmin, workspace);
            _service.Add(useradmin, workspace);

        }

        [TestMethod]
        public void GetWorkspace()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace{ UserAdmin = useradmin, Name = $"Espacio personal de {useradmin.Name} {useradmin.LastName}" };

            _userService.Add(useradmin);
            _service.Add(useradmin, workspace);

            Assert.AreEqual(workspace, _service.Get(workspace.ID));
        }

        [TestMethod]
        public void UpdateWorkspaceName()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace{ UserAdmin = useradmin, Name = $"Espacio personal de {useradmin.Name} {useradmin.LastName}" };
            _service.Add(useradmin, workspace);

            _service.UpdateName(workspace, "Nuevo Workspace");
            Assert.AreEqual("Nuevo Workspace", workspace.Name);
        }


        [TestMethod]
        public void DeleteWorkspaceWithOtherUsers()
        {
            User firstUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            User otherUser = new User { Name = "Other", LastName = "Test", Email = "test@a.com", Password = "12345678909" };
            Workspace newWorkspace = new Workspace { UserAdmin = firstUser, Name = "Nuevo WorkSpace" };

            _userService.Add(firstUser);
            _userService.Add(otherUser);


            //Esos metodos pasan el newWorkspace de la lista de firstUSer a la de otehrUser no lo agregan en ambos
            _service.Add(firstUser, newWorkspace);
            _service.Add(otherUser, newWorkspace);

            _service.DeleteWorkspace(newWorkspace);

            Assert.AreEqual(newWorkspace.UserAdmin, otherUser);
        }

        [TestMethod]
        public void ListAllTransactions()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace{ UserAdmin = useradmin, Name = $"Espacio personal de {useradmin.Name} {useradmin.LastName}" };


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
                LastDigits = "1234",
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
            List<Transaction> expected = new List<Transaction> { transaction1, transaction2 };
            List<Transaction> transactionList = _service.ListAllTransactionsAllAcounts(workspace);
            CollectionAssert.AreEqual(expected, transactionList);

        }
    }
}


