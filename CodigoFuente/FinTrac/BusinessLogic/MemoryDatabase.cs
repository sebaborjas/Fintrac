using Domain;
using Domain.DataTypes;

namespace BusinessLogic
{
	public class MemoryDatabase
	{
		public bool isLoggedIn { get; set; } = false;
		public List<User> Users { get; set; } = new List<User>();
		public User currentUser { get; set; }
		public Account currentAccount { get; set; }
		public Workspace currentWorkspace { get; set; }
		public MemoryDatabase()
		{
			Users = new List<User>();
			addUsers();
		}

		public void addUsers()
		{
			User user = new User
			{
				Name = "Admin",
				LastName = "Test",
				Email = "admin@test.com",
				Password = "1234123412",
				Address = "123"
			};

			Workspace defaultWorkspace = new Workspace(user, $"{user.Name} {user.LastName}");
			user.WorkspaceList.Add(defaultWorkspace);
			Users.Add(user);

			Exchange exchange = new Exchange { Date = DateTime.Today.AddDays(-5), DollarValue = 35 };
			defaultWorkspace.ExchangeList.Add(exchange);

			Account account = new PersonalAccount { Name = "TestPersonalAccount", Currency = CurrencyType.UYU, WorkSpace = defaultWorkspace, StartingAmount = 20000 };
			Account accountUSD = new PersonalAccount { Name = "TestPersonalAccountUSD", Currency = CurrencyType.USD, WorkSpace = defaultWorkspace, StartingAmount = 20000 };

			Category category = new Category { Name = "Cosas personales", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = defaultWorkspace };
			Category categoryHome = new Category { Name = "Hogar", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = defaultWorkspace };
			defaultWorkspace.CategoryList.Add(category);
			defaultWorkspace.CategoryList.Add(categoryHome);
			List<Transaction> transactionList = new List<Transaction>();

			Transaction firstTransaction = new Transaction { Title = "Gasto 1", Amount = 2000, Account = account, Category = category, Currency = CurrencyType.UYU };
			Transaction secondTransaction = new Transaction { Title = "Gasto 2", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };
			Transaction thirdTransaction = new Transaction { Title = "Gasto 3", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };

			transactionList.Add(firstTransaction);
			transactionList.Add(secondTransaction);
			transactionList.Add(thirdTransaction);

			account.TransactionList = transactionList;

			defaultWorkspace.AccountList.Add(account);

			List<Transaction> transactionListUSD = new List<Transaction>();

			Transaction firstTransactionUSD = new Transaction { Title = "Gasto 1", Amount = 20, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };
			Transaction secondTransactionUSD = new Transaction { Title = "Gasto 2", Amount = 35, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome, };
			Transaction thirdTransactionUSD = new Transaction { Title = "Gasto 3", Amount = 40, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };

			transactionListUSD.Add(firstTransactionUSD);
			transactionListUSD.Add(secondTransactionUSD);
			transactionListUSD.Add(thirdTransactionUSD);

			accountUSD.TransactionList = transactionListUSD;

			defaultWorkspace.AccountList.Add(accountUSD);

			Goal goal = new Goal { Title = "Ahorro", MaxAmount = 10000, Workspace = defaultWorkspace };
			goal.Categories.Add(category);
			goal.Categories.Add(categoryHome);
			defaultWorkspace.GoalList.Add(goal);
		}

	}

}