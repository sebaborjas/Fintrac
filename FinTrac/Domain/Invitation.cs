using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain
{
    public class Invitation
    {
        public Invitation()
        {
            ID = nextId;
            nextId++;
        }
        private static int nextId = 1;
        public int ID { get; private set; }
        private User _userToInvite;
        private User _admin;
        public Workspace Workspace { get; set; }
        public User Admin
        {
            get => _admin;
            set
            {
                if (value == UserToInvite)
                {
                    throw new InviteDifferentUserException();
                }
                _admin = value;
            }
        }

        public User UserToInvite
        {
            get => _userToInvite;
            set
            {
                if (value == null)
                {
                    throw new EmptyFieldException();
                }
                if (value == Admin)
                {
                    throw new InviteDifferentUserException();
                }
                _userToInvite = value;
            }
        }

        public override bool Equals(object? obj)
        {
            Invitation invitation = (Invitation)obj;
            return invitation.ID == this.ID ;
        }
    }
}