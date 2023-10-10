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
            category = new Category
            {
                Name = "Test"
            };
        }

        [TestMethod]
        public void EqualsCategories()
        {
            Category otherCategory = new Category
            {
                Name = "Test"
            };
            Assert.AreEqual(category, otherCategory);
        }

        [TestMethod]
        public void NotEqualsCategories()
        {
            Category otherCategory = new Category
            {
                Name = "Home"
            };
            Assert.AreNotEqual(category, otherCategory);
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
        public void TestCategoryCreationDateBefore()
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            category.CreationDate = yesterday;
            Assert.AreEqual(yesterday, category.CreationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCategoryCreationDateAfter()
        {
            category.CreationDate = DateTime.Today.AddDays(1);
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
