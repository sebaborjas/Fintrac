using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
	public class TestPersonalAccount
	{
		private PersonalAccount account; 
		[TestInitialize]
		public void Setup()
		{
			account = new PersonalAccount();
		}
		[TestMethod]
		public void ValidName()
		{
			account.Name = "TestName";
			Assert.AreEqual("TestName", account.Name);
		}
		[TestMethod]
		[ExpectedException(typeof(EmptyFieldException))]
		public void EmptyNameException()
		{
			account.Name = "";
		}
		[TestMethod]
		public void DefaultCurrencyUYU()
		{
			Assert.AreEqual(CurrencyType.UYU, account.Currency);
		}
		[TestMethod]
		public void ChangeCurrencyToUSD()
		{
			account.Currency = CurrencyType.USD;
			Assert.AreEqual(CurrencyType.USD, account.Currency);
		}

		[TestMethod]
		public void SetWorkSpace()
		{
			User newUser = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
			Workspace workSpace = new Workspace(newUser, "Test");
			account.WorkSpace = workSpace;
			Assert.AreEqual(workSpace, account.WorkSpace);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmptyWorkSpace()
		{
			account.WorkSpace = null;
		}

		[TestMethod]
		public void ActualDateExpected()
		{
			DateTime actualDate = DateTime.Today;
			Assert.AreEqual(actualDate, account.CreationDate);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FutureDateException()
		{
			account.CreationDate =  DateTime.Now.AddDays(20);
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NegativeStartingAmount()
		{
			account.StartingAmount = -12;

		}

		[TestMethod]
		public void StaringAmountZero()
		{
			double amount = 0;
			account.StartingAmount = amount;
			Assert.AreEqual(amount, account.StartingAmount);
		}

		[TestMethod]
		public void PositveStaringAmount()
		{
			double amount = 3000;
			account.StartingAmount = amount;
			Assert.AreEqual(amount, account.StartingAmount);
		}

		[TestMethod]
		public void DecimalStaringAmount()
		{
			double amount = 50000.95;
			account.StartingAmount = amount;
			Assert.AreEqual(amount, account.StartingAmount);
		}

		[TestMethod]
        public void NotNullListOfTransactions()
		{
            Assert.IsNotNull(account.TransactionList);
        }
	}
}
