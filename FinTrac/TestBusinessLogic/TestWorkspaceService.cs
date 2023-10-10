using Domain;
using BusinessLogic;
using Domain.Exceptions;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestWorkspaceService
    {
        private WorkspaceService _service;
        [TestInitialize]
        public void SetUp()
        {
            _service = new WorkspaceService(new MemoryDatabase());

        }
        [TestMethod]
        public void AddWorkspace()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");
            _service.Add(workspace);
            Assert.AreEqual(workspace, _service.Get(workspace.ID));

        }
        [TestMethod]
        [ExpectedException(typeof(WorkspaceAlreadyExistsException))]
        public void AddWorkspaceAlreadyExists()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");
            _service.Add(workspace);
            _service.Add(workspace);
        }

        [TestMethod]
        public void GetWorkspace()
        {
            User useradmin = new User { Name = "Test", LastName = "Test", Email = "a@a.com", Password = "12345678909" };
            var workspace = new Workspace(useradmin, "Test");
            _service.Add(workspace);
            Assert.AreEqual(workspace, _service.Get(workspace.ID));
        }
    }
}
