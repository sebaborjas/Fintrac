using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;

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
			category.Name = "";
		}

		[TestMethod]
		public void TestCategoryName()
		{
			category.Name = "Category Name";
			Assert.AreEqual("Category Name", category.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCategoryCreationDateBeforeNow()
		{
			category.CreationDate = DateTime.Now.AddDays(-1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCategoryCreationDateAfterNow()
		{
			category.CreationDate = DateTime.Now.AddDays(1);
		}

		[TestMethod]
		public void TestCategoryCreationDate()
		{
			category.CreationDate = DateTime.Today;
			Assert.AreEqual(DateTime.Today, category.CreationDate);
		}

		[TestMethod]
		public void TestCategoryStatusActive()
		{
			category.Status = Domain.DataTypes.CategoryStatus.Active;
			Assert.AreEqual(Domain.DataTypes.CategoryStatus.Active, category.Status);
		}

		[TestMethod]
		public void TestCategoryStatusInactive()
		{
			category.Status = Domain.DataTypes.CategoryStatus.Inactive;
			Assert.AreEqual(Domain.DataTypes.CategoryStatus.Inactive, category.Status);
		}

		[TestMethod]
		public void TestCategoryTypeIncome()
		{
			category.Type = Domain.DataTypes.CategoryType.Income;
			Assert.AreEqual(Domain.DataTypes.CategoryType.Income, category.Type);
		}

		[TestMethod]
		public void TestCategoryTypeCost()
		{
			category.Type = Domain.DataTypes.CategoryType.Cost;
			Assert.AreEqual(Domain.DataTypes.CategoryType.Cost, category.Type);
		}
	}
}
