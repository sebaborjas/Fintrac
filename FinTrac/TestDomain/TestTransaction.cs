using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Excepciones;

namespace TestDomain
{
	[TestClass]
	public class TestTransaction
	{
		Transaction transaction;

		[TestInitialize]
		public void TestInitialize()
		{
			transaction = new Transaction();
		}

		[TestMethod]
		public void TestAddTitle()
		{
			transaction.Title = "Title";
			Assert.AreEqual("Title", transaction.Title);
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyFieldException))]
		public void TestAddEmptyTitle()
		{
			transaction.Title = "";
		}

		[TestMethod]
		public void TestAddDate()
		{
			transaction.CreationDate = DateTime.Today;
			Assert.AreEqual(DateTime.Today, transaction.CreationDate);
		}

		[TestMethod]
		public void TestAddAmount()
		{
			transaction.Amount = 100;
			Assert.AreEqual(100, transaction.Amount);
		}

		//[TestMethod]
		//[ExpectedException(typeof(ArgumentException))]
		//public void TestAddNegativeAmount()
		//{
		//	transaction.Amount = -100;
		//}

		/*[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAddZeroAmount()
		{
			transaction.Amount = 0;
		}
*/
		[TestMethod]
		public void TestAddDecimalAmount()
		{
			transaction.Amount = 100.5;
			Assert.AreEqual(100.5, transaction.Amount);
		}

		/*
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAddNegativeDecimalAmount()
		{
			transaction.Amount = -100.5;
		}


		[TestMethod]
		public void TestAddCurrency()
		{
			transaction.Currency = "UYU";
			Assert.AreEqual("UYU", transaction.Currency);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAddInvalidCurrency()
		{
			transaction.Currency = "UY";
		}
		*/
	}
}
