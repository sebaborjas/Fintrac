using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestTransactionService
    {
        private AccountService _service;
        private WorkspaceService _workspaceService;
        private UserService _userService;

        private Workspace _workspace;
        private User _user;
        private Account _account;

        [TestInitialize]
        public void SetUp()
        {
            MemoryDatabase _database = new MemoryDatabase();
            _service = new AccountService(_database);
            _workspaceService = new WorkspaceService(_database);
            _userService = new UserService(_database);

            _user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };

            _workspace = new Workspace(_user, "Test");

            _account = new PersonalAccount { Name = "Caja de ahorro", WorkSpace = _workspace, Currency = Domain.DataTypes.CurrencyType.UYU};

            _userService.Add(_user);
            _workspaceService.Add(_user, _workspace);
        }
    }
}
