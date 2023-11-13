using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataTypes;
using Domain;
using System.Security.Cryptography.X509Certificates;

namespace TestDomain
{
	[TestClass]
	public class TestCategoryReport
	{
		private CategoryReport categoryReportUYU; 
		private CategoryReport categoryReportUSD;
		private Workspace workSpace;
		private List<Transaction> transactionList;
		private List<Transaction> transactionListUSD;
		private Account account;
		private Account accountUSD;
		private Category category;
		private Category categoryHome;
		private Goal goal;

		[TestInitialize]
		public void SetUp()
		{
			User user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "1234567890" };

			workSpace = new Workspace(user, "TestWorkspace");

			user.WorkspaceList.Add(workSpace);
			Exchange exchange = new Exchange { Date = DateTime.Today.AddDays(-5), Currency = CurrencyType.USD, CurrencyValue = 35 };
			workSpace.ExchangeList.Add(exchange);

			account = new PersonalAccount { Name = "TestPersonalAccount", Currency = CurrencyType.UYU, WorkSpace = workSpace, StartingAmount = 20000 };
			accountUSD = new PersonalAccount { Name = "TestPersonalAccountUSD", Currency = CurrencyType.USD, WorkSpace = workSpace, StartingAmount = 20000 };

			category = new Category { Name = "Cosas personales", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = workSpace };
			categoryHome = new Category { Name = "Hogar", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = workSpace };

			transactionList = new List<Transaction>();

			Transaction firstTransaction = new Transaction { Title = "Gasto 1", Amount = 2000, Account = account, Category = category, Currency = CurrencyType.UYU };
			Transaction secondTransaction = new Transaction { Title = "Gasto 2", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };
			Transaction thirdTransaction = new Transaction { Title = "Gasto 3", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };

			transactionList.Add(firstTransaction);
			transactionList.Add(secondTransaction);
			transactionList.Add(thirdTransaction);

			account.TransactionList = transactionList;

			workSpace.AccountList.Add(account);

			transactionListUSD = new List<Transaction>();

			Transaction firstTransactionUSD = new Transaction { Title = "Gasto 1", Amount = 20, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };
			Transaction secondTransactionUSD = new Transaction { Title = "Gasto 2", Amount = 35, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome, };
			Transaction thirdTransactionUSD = new Transaction { Title = "Gasto 3", Amount = 40, Currency = CurrencyType.USD, Account = accountUSD, Category = categoryHome };

			transactionListUSD.Add(firstTransactionUSD);
			transactionListUSD.Add(secondTransactionUSD);
			transactionListUSD.Add(thirdTransactionUSD);

			accountUSD.TransactionList = transactionListUSD;

			workSpace.AccountList.Add(accountUSD);

			goal = new Goal { Title = "Ahorro", MaxAmount = 10000, Workspace = workSpace };
			goal.Categories.Add(category);
			goal.Categories.Add(categoryHome);

			categoryReportUYU = new CategoryReport { Currency = CurrencyType.UYU, WorkSpace = workSpace, Category = category, Month = (Month)DateTime.Today.Month };
			categoryReportUSD = new CategoryReport { Currency = CurrencyType.USD, WorkSpace = workSpace, Category = categoryHome, Month = (Month)DateTime.Today.Month };
		}

		[TestMethod]
		public void CalculateCategoryReportUYU() 
		{
			string categoryName = categoryReportUYU.Category.Name;
			double categorySpent = 4000;
			double amountSpent = 7325;
			string percentage = $"{Math.Round(categorySpent * 100 / amountSpent).ToString()} %";
			string reportOutput = $"{categoryName}: {categorySpent} => {percentage}";
			Assert.AreEqual(reportOutput, categoryReportUYU.CalculateReport());
		}

		[TestMethod]
		public void CalculateCategoryReportUSD()
		{
			string categoryName = categoryReportUSD.Category.Name;
			double categorySpent = 3325;
			double amountSpent = 7325;
			string percentage = $"{Math.Round(categorySpent * 100 / amountSpent).ToString()} %";
			string reportOutput = $"{categoryName}: {categorySpent} => {percentage}";
			Assert.AreEqual(reportOutput, categoryReportUSD.CalculateReport());
		}

		[TestMethod]
		public void CalculateCategoryReportMonthWithoutTransactions()
		{
			CategoryReport categoryReportJanuary = new CategoryReport { Currency = CurrencyType.UYU, WorkSpace = workSpace, Category = category, Month = Month.Enero };
			string categoryName = categoryReportJanuary.Category.Name;
			double categorySpent = 0;
			string percentage = "0 %";
			string reportOutput = $"{categoryName}: {categorySpent} => {percentage}";
			Assert.AreEqual(reportOutput, categoryReportJanuary.CalculateReport());
		}
	}
}
