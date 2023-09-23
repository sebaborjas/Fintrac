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
	public class TestCategory
	{
		Category category;

		[TestInitialize]
		public void Setup()
		{
			category = new Category();
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyFieldException))]
		public void TestCategoryNameEmpty()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCategoryNameNull()
		{
		}

		[TestMethod]
		public void TestCategoryName()
		{
		}

		
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCategoryNameAlreadyExists()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCategoryCreationDateNull()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCategoryCreationDateBeforeNow()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCategoryCreationDateAfterNow()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyFieldException))]
		public void TestCategoryCreationDateEmpty()
		{
		}

		[TestMethod]
		public void TestCategoryCreationDate()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCategoryStatusInvalid()
		{
		}

		[TestMethod]
		public void TestCategoryStatusActive()
		{
		}

		[TestMethod]
		public void TestCategoryStatusInactive()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCategoryStatusNull()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyFieldException))]
		public void TestCategoryStatusEmpty()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCategoryTypeInvalid()
		{
		}

		[TestMethod]
		public void TestCategoryTypeIncome()
		{
		}

		[TestMethod]
		public void TestCategoryTypeCost()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCategoryTypeNull()
		{
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyFieldException))]
		public void TestCategoryTypeEmpty()
		{
		}
	}
}
