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
    public class User
    {
        private string _name;
        private string _lastName;
        private string _email;
        private string _password;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == "")
                {
                    throw new EmptyFieldException();
                }
                else
                {
                    _name = value;
                }
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value == "")
                {
                    throw new EmptyFieldException();
                }
                _lastName = value;
            }
        }
        public string? Address { get; set; }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value == "")
                {
                    throw new EmptyFieldException();
                }

                string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
                Regex regex = new(pattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("El email ingresado es invalido");
                }
                _email = value;

            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value == "")
                {
                    throw new EmptyFieldException();
                }
                if (value.Length < 10 || value.Length > 30)
                {
                    throw new ArgumentException("La contraseña debe tener entre 10 y 20 caracteres");
                }
                _password = value;
            }
        }

        public List<Workspace> WorkspaceList { get; set; } = new List<Workspace>();

        public List<Invitation> RecievedInvitations { get; set; } = new List<Invitation>(); 

        public override bool Equals(object? obj)
        {
            User user = (User)obj;
            return this.Email == user.Email;
        }
    }
}
