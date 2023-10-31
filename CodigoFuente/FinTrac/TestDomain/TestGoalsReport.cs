using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Transactions;
using Domain;
using Domain.Exceptions;

namespace TestDomain
{
	[TestClass]
	public class TestGoalsReport
	{
		GoalsReport goalsReport;

		[TestInitialize]
		public void SetUp()
		{
			goalsReport = new GoalsReport();
		}
		
		[TestMethod]
		public void CorrectDefinedAmount()
		{
			double amount = 15000;
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

			Assert.IsTrue(goalsReport.GoalAchieved);
		}

		[TestMethod]
		public void GoalNotAchieved()
		{
			double definedAmount = 9000;
			double amountSpent = 9500;

			goalsReport.DefinedAmount = definedAmount;
			goalsReport.AmountSpent = amountSpent;

			Assert.IsFalse(goalsReport.GoalAchieved);
		}

		[TestMethod]
		public void GoalAchievedDefindedAndSpentEqual()
		{
			double definedAmount = 5000;
			double amountSpent = 5000;

			goalsReport.DefinedAmount = definedAmount;
			goalsReport.AmountSpent = amountSpent;

			Assert.IsTrue(goalsReport.GoalAchieved);
		}
	}
}
