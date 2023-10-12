using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Transactions;
using Domain;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestInvitation
    {
        public Workspace workspace;
        private User admin;
        private User userToInvite;

        [TestInitialize]
        public void Setup()
        {
            admin = new User();
            userToInvite = new User();
            workspace = new Workspace(admin, "Test");
        }

        [TestMethod]
        public void EqualsInvitations()
        {
            Invitation invitation = new Invitation
            {
                Admin = admin,
                Workspace = workspace,
                UserToInvite = userToInvite
            };
            Assert.AreEqual(invitation, invitation);
        }

        [TestMethod]
        public void NotEqualsInvitations()
        {
            Invitation invitation = new Invitation
            {
                Admin = admin,
                Workspace = workspace,
                UserToInvite = userToInvite
            };
            User userToInvite2 = new User();
            Invitation invitation2 = new Invitation
            {
                Admin = admin,
                Workspace = workspace,
                UserToInvite = userToInvite2
            };

            Assert.AreNotEqual(invitation, invitation2);
        }

        [TestMethod]
        public void SendInvitationUserToInviteNotNull()
        {
            Invitation invitation = new Invitation {
                Admin = admin,
                Workspace = workspace,
                UserToInvite = userToInvite
            };
            Assert.IsNotNull(userToInvite);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void SentInvitationUserToInviteNull()
        {
            Invitation invitation = new Invitation
            {
                Admin = admin,
                Workspace = workspace,
                UserToInvite = null
            };
            Assert.IsNull(userToInvite);
        }

        [TestMethod]
        [ExpectedException(typeof(InviteDifferentUserException))]
        public void SendInvitationEqualsUsers()
        {
            Invitation invitation = new Invitation
            {
                Admin = admin,
                Workspace = workspace,
                UserToInvite = admin
            };
        }

    }
}
