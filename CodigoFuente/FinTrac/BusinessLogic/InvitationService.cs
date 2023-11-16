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
		private readonly FintracContext _database;
		public InvitationService(FintracContext database)
		{
			this._database = database;
		}

		public void Add(Invitation invitation)
		{
			if (invitation.Admin != invitation.Workspace.UserAdmin)
			{
				throw new InvitationOnlyUserAdmin();
			}
			if (_database.Users.Where(x => x.Email == invitation.UserToInvite.Email) == null)
			{
				throw new InvalidUserException();
			}
			if (_database.Users.Where(x => x.Email == invitation.UserToInvite.Email).FirstOrDefault<User>().RecievedInvitations.Contains(invitation))
			{
				throw new InvitationAlreadyExistsException();
			}
			if (_database.Users.Where(x => x.Email == invitation.UserToInvite.Email).FirstOrDefault<User>().Workspaces.Contains(invitation.Workspace))
			{
				throw new UserAlreadyInWorkspaceException();
			}
			try
			{
				_database.Users.Where(x => x.Email == invitation.UserToInvite.Email).FirstOrDefault<User>().RecievedInvitations.Add(invitation);
			}
			catch (Exception exception)
			{
				throw exception;
			}

			_database.SaveChanges();

		}

		public Invitation Get(int ID)
		{
			Invitation invitation = null;
			var user = _database.Users.Where(x => x.RecievedInvitations.Any(x => x.ID == ID)).FirstOrDefault<User>();
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
					_database.Users.Where(x => x == invitationToDelete.UserToInvite).FirstOrDefault<User>().RecievedInvitations.Remove(invitationToDelete);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}

			_database.SaveChanges();
		}

		public void AcceptInvitation(int ID)
		{
			try
			{
				Invitation invitationAccepted = this.Get(ID);
				if (invitationAccepted != null)
				{
					User user = invitationAccepted.UserToInvite;
					_database.Users.Where(x => x == user).FirstOrDefault<User>().Workspaces.Add(invitationAccepted.Workspace);
					_database.SaveChanges();
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