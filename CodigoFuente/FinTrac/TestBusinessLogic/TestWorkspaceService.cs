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
            Assert.AreEqual(workspace, useradmin.Workspaces.First(x => x == workspace));

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

			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);

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

            creditCardAccount.Transactions.Add(transaction1);
            personalAccount.Transactions.Add(transaction2);
            workspace.Accounts.Add(creditCardAccount);
            workspace.Accounts.Add(personalAccount);
            workspace.Categories.Add(category);

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

			Workspace workspace = new Workspace { UserAdmin = useradmin, Name = "Test" };

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

		[TestMethod]
		public void GetCreditCards()
		{
			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			User user1 = new User { Name = "User1", LastName = "User1", Email = "user1@user.com", Password = "12345678909" };

			Workspace workspace = new Workspace{ UserAdmin = useradmin, Name = "Test" };

			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);

			_userService.Add(user1);
			_service.Add(user1, workspace);

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

			Account creditCardAccount2 = new CreditCard
			{
				BankName = "Santander2",
				LastDigits = "5678",
				AvailableCredit = 5000,
				DeadLine = 18,
				Name = "Credit Santander2",
				Currency = CurrencyType.UYU,
				WorkSpace = workspace
			};

			workspace.Accounts.Add(creditCardAccount);
			workspace.Accounts.Add(personalAccount);
			workspace.Accounts.Add(creditCardAccount2);

			List<Account> expected = new List<Account> { creditCardAccount, creditCardAccount2 };
			List<CreditCard> creditCardList = _service.GetCreditCards(workspace);
			CollectionAssert.AreEqual(expected, creditCardList);
		}

		[TestMethod]
		public void GetPersonalAccounts()
		{
			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			User user1 = new User { Name = "User1", LastName = "User1", Email = "user1@user.com", Password = "12345678909" };

			Workspace workspace = new Workspace{ UserAdmin = useradmin, Name = "Test" };

			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);

			_userService.Add(user1);
			_service.Add(user1, workspace);

			Account personalAccount = new PersonalAccount
			{
				Name = "Test",
				CreationDate = DateTime.Today.AddDays(-5),
				StartingAmount = 1000,
				Currency = CurrencyType.UYU,
				WorkSpace = workspace
			};

			Account personalAccount2 = new PersonalAccount
			{
				Name = "Test2",
				CreationDate = DateTime.Today.AddDays(-4),
				StartingAmount = 2000,
				Currency = CurrencyType.UYU,
				WorkSpace = workspace
			};

			Account creditCardAccount = new CreditCard
			{
				BankName = "Santander2",
				LastDigits = "5678",
				AvailableCredit = 5000,
				DeadLine = 18,
				Name = "Credit Santander2",
				Currency = CurrencyType.UYU,
				WorkSpace = workspace
			};

			workspace.Accounts.Add(personalAccount);
			workspace.Accounts.Add(personalAccount2);
			workspace.Accounts.Add(creditCardAccount);

			List<Account> expected = new List<Account> { personalAccount, personalAccount2 };
			List<PersonalAccount> personalAccounts = _service.GetPersonalAccounts(workspace);
			CollectionAssert.AreEqual(expected, personalAccounts);
		}

		[TestMethod]
		public void GenerateGoalsReport()
		{
			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			User user1 = new User { Name = "User1", LastName = "User1", Email = "user1@user.com", Password = "12345678909" };

			Workspace workspace = new Workspace{ UserAdmin = useradmin, Name = "Test" };

			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);

			_userService.Add(user1);
			_service.Add(user1, workspace);

			Goal goal1 = new Goal
			{
				Title = "Goal 1",
				MaxAmount = 1000,
				Categories = new List<Category>(),
				Workspace = workspace
			}; Category category1 = new Category
			{
				Name = "Category 1",
				CreationDate = DateTime.Today.AddDays(-5),
				Status = CategoryStatus.Active,
				Workspace = workspace,
				Type = CategoryType.Cost
			};

			Category category2 = new Category
			{
				Name = "Category 2",
				CreationDate = DateTime.Today.AddDays(-5),
				Status = CategoryStatus.Active,
				Workspace = workspace,
				Type = CategoryType.Cost
			};

			goal1.Categories.Add(category1);
			goal1.Categories.Add(category2);

			Goal goal2 = new Goal
			{
				Title = "Goal 2",
				MaxAmount = 1000,
				Categories = new List<Category>(),
				Workspace = workspace
			};
			goal2.Categories.Add(category1);

			workspace.Goals.Add(goal1);
			workspace.Goals.Add(goal2);

			List<GoalsReport> goalsReport = _service.GenerateGoalsReport(workspace);
			Assert.IsTrue(goalsReport.Count == 2);
			Assert.IsNotNull(goalsReport);
		}

		[TestMethod]
		public void GenerateCategoryReports()
		{
			User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			Workspace workspace = new Workspace{ UserAdmin = useradmin, Name = "Test" };
			_userService.Add(useradmin);
			_service.Add(useradmin, workspace);

			Category category1 = new Category { Name = "Category 1", Workspace = workspace };
			Category category2 = new Category { Name = "Category 2", Workspace = workspace };
			workspace.Categories.Add(category1);
			workspace.Categories.Add(category2);

			List<CategoryReport> categoryReports = _service.GenerateCategoryReports(workspace, Month.Noviembre);

			Assert.IsNotNull(categoryReports);
			Assert.AreEqual(2, categoryReports.Count);

		}
    }
}
