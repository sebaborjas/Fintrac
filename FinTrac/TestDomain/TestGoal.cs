using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Transactions;
using Domain;
using Domain.Exceptions;

namespace TestDomain
{
	[TestClass]
	public class TestGoal
	{
		Goal goal;

		[TestInitialize]
		public void Setup()
		{
			goal = new Goal();
		}

		[TestMethod]
		public void AddTitle()
		{
			goal.Title = "Test";
			Assert.AreEqual("Test", goal.Title);
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyFieldException))]
		public void EmptyTitleException()
		{
			goal.Title = "";
		}

		[TestMethod]
		public void AddMaxAmount()
		{
			goal.MaxAmount = 1000;
			Assert.AreEqual(1000, goal.MaxAmount);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidMaxAmountException()
		{
			goal.MaxAmount = 0;
		}

		// monto maximo negativo lanza excepcion
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NegativeMaxAmountException()
		{
			goal.MaxAmount = -1;
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestWorkSpaceNull()
		{
			goal.Workspace = null;
		}

		[TestMethod]
		public void TestWorkSpace()
		{
			Object workSpace = new Object();
			goal.Workspace = workSpace;
			Assert.AreEqual(workSpace, goal.Workspace);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCategoriesNull()
		{
			goal.Categories = null;
		}

		[TestMethod]
		public void TestCategories()
		{
			List<Category> categories = new List<Category>();
			goal.Categories = categories;
			Assert.AreEqual(categories, goal.Categories);
		}
	}
}
