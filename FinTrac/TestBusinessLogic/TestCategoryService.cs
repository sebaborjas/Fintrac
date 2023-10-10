using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using BusinessLogic;
using Domain.Exceptions;
using Domain.DataTypes

namespace TestBusinessLogic
{
    [TestClass]
    public class TestCategoryService
    {
        public Workspace workspace;

        [TestInitialize]
        public void SetUp()
        {
            _service = new CategoryService(new MemoryDatabase());

        }
        [TestMethod]
        public void TestAddCategory()
        {
            User admin = new User
            {
                Name = "Test",
                LastName = "LastName",
                Address = "",
                Password = "1234567891",
                Email = "test@test.com"
            };
            workspace = new Workspace(admin, "Test");
            Category category = new Category
            {
                Name = "Test",
                Workspace = workspace,
                CreationDate = DateTime.Today,
                Status = CategoryStatus.Active,
                Type = CategoryType.Cost
            };
            workspace.CategoryList.Add(category);
            Assert.AreEqual(category, _service.Get(category.Name));

        }
    }
}
