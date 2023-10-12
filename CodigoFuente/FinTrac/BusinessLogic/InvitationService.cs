using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;

namespace BusinessLogic
{
    public class InvitationService
    {
        private readonly MemoryDatabase _memoryDatabase;

        public InvitationService(MemoryDatabase memoryDatabase)
        {
            this._memoryDatabase = memoryDatabase;
        }

        public void Add(Invitation invitation)
        {
            if (invitation.Admin != invitation.Workspace.UserAdmin)
            {
                throw new InvitationOnlyUserAdmin();
            }
            if (_memoryDatabase.Users.Find(x => x.Email == invitation.UserToInvite.Email) == null)
            {
                throw new InvalidUserException();
            }
            if (_memoryDatabase.Users.Find(x => x.Email == invitation.UserToInvite.Email).RecievedInvitations.Contains(invitation))
            {
                throw new InvitationAlreadyExistsException();
            }
            if (_memoryDatabase.Users.Find(x => x.Email == invitation.UserToInvite.Email).WorkspaceList.Contains(invitation.Workspace))
            {
                throw new UserAlreadyInWorkspaceException();
            }
            try
            {
                _memoryDatabase.Users.Find(x => x.Email == invitation.UserToInvite.Email).RecievedInvitations.Add(invitation);
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        public Invitation Get(int ID)
        {
            Invitation invitation = null;
            var user = _memoryDatabase.Users.Find(x => x.RecievedInvitations.Any(x => x.ID == ID));
            if (user != null)
            {
                invitation = user.RecievedInvitations.Find(x => x.ID == ID);
                if (invitation == null)
                {
                    throw new ElementNotFoundException("No se encontró la invitación");
                }
                return invitation;
            }
            throw new ElementNotFoundException("No se encontró la invitación");

        }

        public void Delete(int ID)
        {
            try
            {
                Invitation invitationToDelete = Get(ID);
                if (invitationToDelete != null)
                {
                    _memoryDatabase.Users.Find(x => x == invitationToDelete.UserToInvite).RecievedInvitations.Remove(invitationToDelete);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void AcceptInvitation(int ID)
        {
            try
            {
                Invitation invitationAccepted = this.Get(ID);
                if (invitationAccepted != null)
                {
                    User user = invitationAccepted.UserToInvite;
                    _memoryDatabase.Users.Find(x => x == user).WorkspaceList.Add(invitationAccepted.Workspace);
                    this.Delete(ID);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

    }

}