using BusinessLogic;
using Domain.DataTypes;
using Domain;
using Domain.Exceptions;

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
            MemoryDatabase _database = new MemoryDatabase();
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
        public void AddGoalCategoriesListEmpty() {

            _service.Add(_goal.Workspace, _goal);
        }
    }
}
