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

			Workspace defaultWorkspace = new Workspace{UserAdmin = user, Name = $"{user.Name} {user.LastName}"};
			user.Workspaces.Add(defaultWorkspace);
			Users.Add(user);

			Exchange exchange = new Exchange { Workspace = defaultWorkspace, Date = DateTime.Today.AddDays(-5),Currency = CurrencyType.USD, CurrencyValue = 35 };
			defaultWorkspace.Exchanges.Add(exchange);

			Account account = new PersonalAccount { Name = "TestPersonalAccount", Currency = CurrencyType.UYU, WorkSpace = defaultWorkspace, StartingAmount = 20000 };
			Account accountUSD = new PersonalAccount { Name = "TestPersonalAccountUSD", Currency = CurrencyType.USD, WorkSpace = defaultWorkspace, StartingAmount = 20000 };
			Account creditCardAccount = new CreditCard
			{
				BankName = "Santander",
				LastDigits = "1234",
				AvailableCredit = 10000,
				DeadLine = 26,
				Name = "Credit Santander",
				Currency = CurrencyType.UYU,
				WorkSpace = defaultWorkspace
			};

			Account creditCardAccount2 = new CreditCard
			{
				BankName = "Santander2",
				LastDigits = "5678",
				AvailableCredit = 5000,
				DeadLine = 18,
				Name = "Credit Santander2",
				Currency = CurrencyType.UYU,
				WorkSpace = defaultWorkspace
			};

			Category category = new Category { Name = "Cosas personales", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = defaultWorkspace };
			Category categoryHome = new Category { Name = "Hogar", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = defaultWorkspace };
			defaultWorkspace.Categories.Add(category);
			defaultWorkspace.Categories.Add(categoryHome);

			List<Transaction> transactionList = new List<Transaction>();

			Transaction firstTransaction = new Transaction { Title = "Gasto 1", Amount = 2000, Account = account, Category = category, Currency = CurrencyType.UYU };
			Transaction secondTransaction = new Transaction { Title = "Gasto 2", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };
			Transaction thirdTransaction = new Transaction { Title = "Gasto 3", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };

			transactionList.Add(firstTransaction);
			transactionList.Add(secondTransaction);
			transactionList.Add(thirdTransaction);

			account.Transactions = transactionList;

			defaultWorkspace.Accounts.Add(account);

			List<Transaction> transactionListUSD = new List<Transaction>();

			Transaction firstTransactionUSD = new Transaction { Title = "Gasto 1", Amount = 20, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };
			Transaction secondTransactionUSD = new Transaction { Title = "Gasto 2", Amount = 35, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome, };
			Transaction thirdTransactionUSD = new Transaction { Title = "Gasto 3", Amount = 40, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };

			transactionListUSD.Add(firstTransactionUSD);
			transactionListUSD.Add(secondTransactionUSD);
			transactionListUSD.Add(thirdTransactionUSD);

			accountUSD.Transactions = transactionListUSD;

			defaultWorkspace.Accounts.Add(accountUSD);

			List<Transaction> transactionListCreditCard = new List<Transaction>();

			Transaction firstTransactionCreditCard = new Transaction { Title = "Gasto 4", Amount = 2000, Account = creditCardAccount, Category = category, Currency = CurrencyType.UYU };
			Transaction secondTransactionCreditCard = new Transaction { Title = "Gasto 5", Amount = 1000, Account = creditCardAccount, Category = category, Currency = CurrencyType.UYU };
			Transaction thirdTransactionCreditCard = new Transaction { Title = "Gasto 6", Amount = 1000, Account = creditCardAccount, Category = category, Currency = CurrencyType.UYU };

			transactionListCreditCard.Add(firstTransactionCreditCard);
			transactionListCreditCard.Add(secondTransactionCreditCard);
			transactionListCreditCard.Add(thirdTransactionCreditCard);

			creditCardAccount.Transactions = transactionListCreditCard;


			defaultWorkspace.Accounts.Add(creditCardAccount);
			defaultWorkspace.Accounts.Add(creditCardAccount2);

			Goal goal = new Goal { Title = "Ahorro", MaxAmount = 10000, Workspace = defaultWorkspace };
			goal.Categories.Add(category);
			goal.Categories.Add(categoryHome);
			defaultWorkspace.Goals.Add(goal);

			Goal goal2 = new Goal { Title = "Gastos", MaxAmount = 15000, Workspace = defaultWorkspace };
			goal2.Categories.Add(categoryHome);
			defaultWorkspace.Goals.Add(goal2);

			Goal goal3 = new Goal { Title = "Comida", MaxAmount = 5000, Workspace = defaultWorkspace };
			goal3.Categories.Add(category);
			defaultWorkspace.Goals.Add(goal3);
		}


	}

}