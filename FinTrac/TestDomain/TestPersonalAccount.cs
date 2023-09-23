using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.DataTypes;
using Domain.Excepciones;

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
			Assert.AreEqual(Currency.UYU, account.Currency);
		}
		[TestMethod]
		public void ChangeCurrencyToUSD()
		{
			account.Currency = Currency.USD;
			Assert.AreEqual(Currency.USD, account.Currency);
		}

		[TestMethod]
		public void SetWorkSpace()
		{
			Object workSpace = new Object();
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
		public void StaringAmountCero()
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
	}
}
