using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace TestDomain
{
	[TestClass]
	public class TestGoalsReport
	{
		private GoalsReport goalsReport;
		private Workspace workSpace;
		private List<Transaction> transactionList;
		private Account account;
		private Goal goal;

		[TestInitialize]
		public void SetUp()
		{
			User user = new User { Email = "test@test.com", Name = "Test" , LastName = "Test", Password = "1234567890"};
			
			workSpace = new Workspace (user, "TestWorkspace");

			user.WorkspaceList.Add (workSpace);

			

			account = new PersonalAccount { Name = "TestPersonalAccount", Currency = CurrencyType.UYU, WorkSpace = workSpace, StartingAmount = 20000 };

			Category category = new Category {Name = "Cosas personales", Status = CategoryStatus.Active, Type = CategoryType.Cost, Workspace = workSpace };
			
			transactionList = new List<Transaction>();

			Transaction firstTransaction = new Transaction { Title = "Gasto 1", Amount = 2000, Account = account, Category = category, Currency = CurrencyType.UYU};
			Transaction secondTransaction = new Transaction { Title = "Gasto 2", Amount = 5000, Account = account, Category = category, Currency = CurrencyType.UYU };
			Transaction thirdTransaction = new Transaction { Title = "Gasto 3", Amount = 1000, Account = account, Category = category, Currency = CurrencyType.UYU };

			transactionList.Add(firstTransaction);
			transactionList.Add(secondTransaction);
			transactionList.Add(thirdTransaction);

			account.TransactionList = transactionList;

			workSpace.AccountList.Add (account);

			goal = new Goal { Title = "Ahorro", MaxAmount = 10000, Workspace = workSpace };
			goal.Categories.Add(category);


			goalsReport = new GoalsReport { Currency = CurrencyType.UYU, WorkSpace = workSpace };
		}
		
		[TestMethod]
		public void CorrectDefinedAmount()
		{
			double amount = 4000;
			goalsReport.DefinedAmount = amount;

			Assert.AreEqual(amount, goalsReport.DefinedAmount);
		}
		
		[TestMethod]
		public void CorrectAmountSpent()
		{
			double amount = 9000;
			goalsReport.AmountSpent = amount;

			Assert.AreEqual(amount, goalsReport.AmountSpent);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NegativeDefinedAmount()
		{
			double negativeAmount = -1000;
			goalsReport.DefinedAmount = negativeAmount;

		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NegativeAmountSpent()
		{
			double negativeAmount = -1000;
			goalsReport.AmountSpent = negativeAmount;

		}

		[TestMethod]
		public void GoalAchieved()
		{
			double definedAmount = 10000;
			double amountSpent = 5000;

			goalsReport.DefinedAmount = definedAmount;
			goalsReport.AmountSpent = amountSpent;
			
			goalsReport.CalculateReport();

			Assert.IsTrue(goalsReport.GoalAchieved);
		}

		[TestMethod]
		public void GoalNotAchieved()
		{
			double definedAmount = 9000;
			double amountSpent = 9500;

			goalsReport.DefinedAmount = definedAmount;
			goalsReport.AmountSpent = amountSpent;

			goalsReport.CalculateReport();

			Assert.IsFalse(goalsReport.GoalAchieved);
		}

		[TestMethod]
		public void GoalAchievedDefindedAndSpentEqual()
		{
			double definedAmount = 5000;
			double amountSpent = 5000;

			goalsReport.DefinedAmount = definedAmount;
			goalsReport.AmountSpent = amountSpent;

			goalsReport.CalculateReport();

			Assert.IsTrue(goalsReport.GoalAchieved);
		}
	}
}
