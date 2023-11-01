<<<<<<< HEAD
using BusinessLogic;
using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

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
			Workspace workspace = new Workspace(useradmin, "Test");

			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);
			Assert.AreEqual(workspace, useradmin.WorkspaceList.First(x => x == workspace));

		}
		[TestMethod]
		[ExpectedException(typeof(WorkspaceAlreadyExistsException))]
		public void AddWorkspaceAlreadyExists()
		{
			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			Workspace workspace = new Workspace(useradmin, "Test");

			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);
			_service.Add(useradmin, workspace);

		}

		[TestMethod]
		public void GetWorkspace()
		{
			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			Workspace workspace = new Workspace(useradmin, "Test");

			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);

			Assert.AreEqual(workspace, _service.Get(workspace.ID));
		}

		[TestMethod]
		public void UpdateWorkspaceName()
		{
			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			Workspace workspace = new Workspace(useradmin, "Test");
			_service.Add(useradmin, workspace);

			_service.UpdateName(workspace, "Nuevo Workspace");
			Assert.AreEqual("Nuevo Workspace", workspace.Name);
		}


		[TestMethod]
		public void DeleteWorkspaceWithOtherUsers()
		{
			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			User otherUser = new User { Name = "Other", LastName = "Test", Email = "test@a.com", Password = "12345678909" };
			Workspace workspace = new Workspace(useradmin, "Test");

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

		[TestMethod]
		public void ListGuestUsers()
		{

			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };

			User user1 = new User { Name = "User1", LastName = "User1", Email = "user1@user.com", Password = "12345678909" };
			User user2 = new User { Name = "User2", LastName = "User2", Email = "user2@user.com", Password = "12345678909" };
			User user3 = new User { Name = "User3", LastName = "User3", Email = "user3@user.com", Password = "12345678909" };

			Workspace workspace = new Workspace(useradmin, "Test");

			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);

			_userService.Add(user1);
			_service.Add(user1, workspace);

			_userService.Add(user2);
			_service.Add(user2, workspace);

			_userService.Add(user3);
			_service.Add(user3, workspace);

			List<User> expected = new List<User> { useradmin, user1, user2, user3 };
			List<User> listUser = _service.ListGuestUsers(workspace);

			CollectionAssert.AreEqual(expected, listUser);

		}
	}
=======
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
            _service.Add(useradmin, workspace);

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
>>>>>>> develop
}


