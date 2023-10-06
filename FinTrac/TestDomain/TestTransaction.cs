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
*/
		[TestMethod]
		public void TestAddDecimalAmount()
		{
			transaction.Amount = 100.5;
			Assert.AreEqual(100.5, transaction.Amount);
		}
	}
}
