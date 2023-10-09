using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.DataTypes;
using Domain.Excepciones;

namespace TestDomain
{
	[TestClass]
	public class TestTransaction
	{
		Transaction transaction;
		Category category;

		[TestInitialize]
		public void TestInitialize()
		{
			transaction = new Transaction();
			category = new Category();
		}

		[TestMethod]
		public void TestTitle()
		{
			transaction.Title = "Test";
			Assert.AreEqual("Test", transaction.Title);
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyFieldException))]
		public void TestTitleEmpty()
		{
			transaction.Title = "";
		}

		[TestMethod]
		public void TestCreationDate()
		{
			transaction.CreationDate = DateTime.Today;
			Assert.AreEqual(DateTime.Today, transaction.CreationDate);
		}

		[TestMethod]
		public void TestCreationDateAnterior()
		{
			transaction.CreationDate = DateTime.Today.AddDays(-1);
			Assert.AreEqual(DateTime.Today.AddDays(-1), transaction.CreationDate);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCreationDatePosterior()
		{
			transaction.CreationDate = DateTime.Today.AddDays(1);
		}

		[TestMethod]
		public void TestAmount()
		{
			transaction.Amount = 100;
			Assert.AreEqual(100, transaction.Amount);
		}

		[TestMethod]
		public void TestAmountDecimal()
		{
			transaction.Amount = 100.5;
			Assert.AreEqual(100.5, transaction.Amount);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestAmountNegativo()
		{
			transaction.Amount = -100;
		}

		[TestMethod]
		public void TestCurrency()
		{
			transaction.Currency = CurrencyType.UYU;
			Assert.AreEqual(CurrencyType.UYU, transaction.Currency);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestCurrencyInvalida()
		{
			transaction.Currency = (CurrencyType)3;
		}
 
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestAccountNull()
		{
			transaction.Account = null;
		}

		[TestMethod]
		public void TestAccount()
		{
			Object account = new Object();
			transaction.Account = account;
			Assert.AreEqual(account, transaction.Account);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestWorkSpaceNull()
		{
			transaction.WorkSpace = null;
		}

		[TestMethod]
		public void TestWorkSpace()
		{
			Object workSpace = new Object();
			transaction.WorkSpace = workSpace;
			Assert.AreEqual(workSpace, transaction.WorkSpace);
		}

		[TestMethod]
		public void TestCategoryActive()
		{
			category.Status = CategoryStatus.Active;
			transaction.Category = category;
			Assert.AreEqual(CategoryStatus.Active, transaction.Category.Status);
		}

		[TestMethod] public void TestCategoryInactive()
		{
			category.Status = CategoryStatus.Inactive;
			transaction.Category = category;
		}

		[TestMethod]
		public void TestValidCategoryType()
		{
			category.Type = CategoryType.Cost;
			transaction.Category = category;
			Assert.AreEqual(CategoryType.Cost, transaction.Category.Type);
		}
	}
}
