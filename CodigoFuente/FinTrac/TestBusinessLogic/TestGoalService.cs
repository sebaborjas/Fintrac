using BusinessLogic;
using Domain.DataTypes;
using Domain;
using Domain.Exceptions;
using System.Xml;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestGoalService
    {
        private GoalService _service;
        private CategoryService _categoryService;
        private WorkspaceService _workspaceService;
        private UserService _userService;
        private Workspace _workspace;
        private User _user;
        private Category _category;
        private Goal _goal;

        [TestInitialize]
        public void SetUp()
        {
            FintracContext _database = TestContextFactory.CreateContext();
            _service = new GoalService(_database);
            _workspaceService = new WorkspaceService(_database);
            _userService = new UserService(_database);
            _categoryService = new CategoryService(_database);
            _user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };
            _workspace = new Workspace(_user, "Test");
            _userService.Add(_user);
            _workspaceService.Add(_user, _workspace);
            _category = new Category
            {
                CreationDate = DateTime.Today.AddDays(-5),
                Name = "Test",
                Workspace = _workspace,
                Type = CategoryType.Income,
                Status = CategoryStatus.Active
            };
            _categoryService.Add(_category.Workspace, _category);
            _goal = new Goal
            {
                Title = "Test",
                MaxAmount = 1000,
                Workspace = _workspace
            };
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyCategoryListException))]
        public void AddGoalCategoriesListEmpty()
        {

            _service.Add(_goal.Workspace, _goal);
        }

        [TestMethod]
        public void AddGoalCategoriesListNotEmpty()
        {
            Category categoryCostActive = new Category
            {
                Name = "Test1",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            Category categoryCostInactive = new Category
            {
                Name = "Test2",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Inactive,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            List<Category> categoriesToAdd = new List<Category>()
            {
                _category,
                categoryCostActive,
                categoryCostInactive

            };
            List<Category> expected = new List<Category> { categoryCostActive };
            _service.AddCategoryToGoal(_goal, categoriesToAdd);
            _service.Add(_goal.Workspace, _goal);
            CollectionAssert.AreEqual(expected, _goal.Categories);
        }

        [TestMethod]
        public void DeleteGoal()
        {
            _goal.Categories.Add(_category);
            _service.Add(_workspace, _goal);
            _service.Delete(_workspace, _goal);
            Assert.IsNull(_service.Get(_workspace, _goal.Title));
        }

        [TestMethod]
        public void UpdateGoalValid()
        {
            Category categoryCostActive = new Category
            {
                Name = "Test1",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            Category categoryCostInactive = new Category
            {
                Name = "Test2",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Inactive,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            List<Category> categoriesToAdd = new List<Category>()
            {
                _category,
                categoryCostActive,
                categoryCostInactive

            };

            _service.AddCategoryToGoal(_goal, categoriesToAdd);
            _service.Add(_goal.Workspace, _goal);

            Goal newGoal = new Goal
            {
                Title = "Home",
                MaxAmount = 1500,
                Workspace = _workspace
            };
            Category categoryCostActive2 = new Category
            {
                Name = "Test1",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            Category categoryCostActive3 = new Category
            {
                Name = "Test2",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            List<Category> categoriesToAdd2 = new List<Category>()
            {
                categoryCostActive2,
                categoryCostActive3
            };
            List<Category> expected = new List<Category> { categoryCostActive2, categoryCostActive3 };
            _service.AddCategoryToGoal(newGoal, categoriesToAdd2);
            _service.Update(_goal, newGoal);
            Assert.AreEqual(newGoal.Title, _goal.Title);
            Assert.AreEqual(newGoal.MaxAmount, _goal.MaxAmount);
            CollectionAssert.AreEqual(expected, _goal.Categories);
        }

        [TestMethod]
        [ExpectedException(typeof(GoalAlreadyExistsException))]
        public void UpdateGoalAlreadyExist()
        {
            Category categoryCostActive = new Category
            {
                Name = "Test1",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            Category categoryCostInactive = new Category
            {
                Name = "Test2",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Inactive,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            List<Category> categoriesToAdd = new List<Category>()
            {
                _category,
                categoryCostActive,
                categoryCostInactive

            };

            _service.AddCategoryToGoal(_goal, categoriesToAdd);
            _service.Add(_goal.Workspace, _goal);

            Goal newGoal = new Goal
            {
                Title = "Home",
                MaxAmount = 1500,
                Workspace = _workspace
            };
            Category categoryCostActive2 = new Category
            {
                Name = "Test1",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            Category categoryCostActive3 = new Category
            {
                Name = "Test2",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            List<Category> categoriesToAdd2 = new List<Category>()
            {
                categoryCostActive2,
                categoryCostActive3
            };
            Goal alreadyExistGoal = new Goal
            {
                Title = "Test",
                MaxAmount = 1500,
                Workspace = _workspace
            };
            _service.AddCategoryToGoal(newGoal, categoriesToAdd2);
            _service.Add(_workspace,newGoal);
            _service.AddCategoryToGoal(alreadyExistGoal, categoriesToAdd2);
            _service.Update(newGoal, alreadyExistGoal);
        }

        [TestMethod]
        public void UpdateGoalEqualName()
        {
            Category categoryCostActive = new Category
            {
                Name = "Test1",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            Category categoryCostInactive = new Category
            {
                Name = "Test2",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Inactive,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            List<Category> categoriesToAdd = new List<Category>()
            {
                _category,
                categoryCostActive,
                categoryCostInactive

            };

            _service.AddCategoryToGoal(_goal, categoriesToAdd);
            _service.Add(_goal.Workspace, _goal);

            Goal newGoal = new Goal
            {
                Title = "Test",
                MaxAmount = 1500,
                Workspace = _workspace
            };
            Category categoryCostActive2 = new Category
            {
                Name = "Test1",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            Category categoryCostActive3 = new Category
            {
                Name = "Test2",
                Type = CategoryType.Cost,
                Status = CategoryStatus.Active,
                CreationDate = DateTime.Today.AddDays(-5),
                Workspace = _workspace
            };
            List<Category> categoriesToAdd2 = new List<Category>()
            {
                categoryCostActive2,
                categoryCostActive3
            };
            List<Category> expected = new List<Category> { categoryCostActive2, categoryCostActive3 };
            _service.AddCategoryToGoal(newGoal, categoriesToAdd2);
            _service.Update(_goal, newGoal);
            Assert.AreEqual(newGoal.Title, _goal.Title);
            Assert.AreEqual(newGoal.MaxAmount, _goal.MaxAmount);
            CollectionAssert.AreEqual(expected, _goal.Categories);
        }
    }
}
