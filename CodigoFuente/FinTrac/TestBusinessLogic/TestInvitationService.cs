using Domain;
using BusinessLogic;
using Domain.Exceptions;
using Domain.DataTypes;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestInvitationService
    {
        private InvitationService _service;
        private WorkspaceService _workspaceService;
        private UserService _userService;
        private FintracContext newMemory;
        private User _useradmin;
        private User _userToInvite;
        private Workspace _workspace;
        private Invitation _invitation;
        private User _otherUser;

        [TestInitialize]
        public void SetUp()
        {
            newMemory =  TestContextFactory.CreateContext();
			_workspaceService = new WorkspaceService(newMemory);
            _userService = new UserService(newMemory);
            _service = new InvitationService(newMemory);

            _useradmin = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };
            _workspace = new Workspace{ UserAdmin = _useradmin, Name = $"Espacio personal de {_useradmin.Name} {_useradmin.LastName}" };
            _userService.Add(_useradmin);
            _workspaceService.Add(_useradmin, _workspace);
            _userToInvite = new User { Email = "invite@test.com", Name = "Pepe", LastName = "Test", Password = "12345678901" };

            _otherUser = new User { Email = "other@test.com", Name = "Pepe", LastName = "Test", Password = "12345678901" };
            _userService.Add(_otherUser);
        }

        [TestMethod]
        public void SendInvitation()
        {
            _invitation = new Invitation
            {
                Workspace = _workspace,
                Admin = _useradmin,
                UserToInvite = _userToInvite
            };
            _userService.Add(_userToInvite);
            _service.Add(_invitation);
            Assert.AreEqual(_invitation, _service.Get(_invitation.ID));
        }

        [TestMethod]
        [ExpectedException(typeof(InvitationAlreadyExistsException))]
        public void SendInvitationAlreadyExist()
        {
            _invitation = new Invitation
            {
                Workspace = _workspace,
                Admin = _useradmin,
                UserToInvite = _userToInvite
            };
            _userService.Add(_userToInvite);
            _service.Add(_invitation);
            _service.Add(_invitation);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitationOnlyUserAdmin))]
        public void SendInvitationUserNotAdmin()
        {
            _invitation = new Invitation
            {
                Workspace = _workspace,
                Admin = _userToInvite,
                UserToInvite = _otherUser,
            };
            _userService.Add(_userToInvite);
            _service.Add(_invitation);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyInWorkspaceException))]
        public void SendInvitationUserAlreadyInWorkspace()
        {

            _invitation = new Invitation
            {
                Workspace = _workspace,
                Admin = _useradmin,
                UserToInvite = _userToInvite,
            };
            _userToInvite.Workspaces.Add(_workspace);
            _userService.Add(_userToInvite);
            _service.Add(_invitation);
        }

        public void GetInvitation()
        {
            _invitation = new Invitation
            {
                Workspace = _workspace,
                Admin = _useradmin,
                UserToInvite = _userToInvite
            };
            _userService.Add(_userToInvite);
            _service.Add(_invitation);
            Assert.AreEqual(_invitation, _service.Get(_invitation.ID));
        }

        [TestMethod]
        [ExpectedException(typeof(ElementNotFoundException))]
        public void RejectInvitation()
        {
            _invitation = new Invitation
            {
                Workspace = _workspace,
                Admin = _useradmin,
                UserToInvite = _userToInvite
            };
            _userService.Add(_userToInvite);
            _service.Add(_invitation);
            _service.Delete(_invitation.ID);
            _service.Get(_invitation.ID);
        }

        [TestMethod]
        public void AcceptedInvitation()
        {
            _invitation = new Invitation
            {
                Workspace = _workspace,
                Admin = _useradmin,
                UserToInvite = _userToInvite
            };
            _userService.Add(_userToInvite);
            _service.Add(_invitation);
            _service.AcceptInvitation(_invitation.ID);
            Assert.IsTrue(_userToInvite.Workspaces.Contains(_workspace));
        }
    }
}