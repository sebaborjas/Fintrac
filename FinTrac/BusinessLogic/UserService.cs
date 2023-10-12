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
    public class UserService
    {
        private readonly MemoryDatabase _memoryDatabase;

        public UserService(MemoryDatabase memoryDatabase)
        {
            this._memoryDatabase = memoryDatabase;
        }

        public void Add(User u)
        {
            if (_memoryDatabase.Users.Any(x => x.Email == u.Email))
            {
                throw new UserAlreadyExistsException();
            }
            try
            {
              Workspace defaultWorkspace = new Workspace(u, $"Personal {u.Name} {u.LastName}");
              u.WorkspaceList.Add(defaultWorkspace);
              _memoryDatabase.Users.Add(u);
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        public User Get(string email)
        {
            return _memoryDatabase.Users.Find(x => x.Email == email);
        }

		public User UpdateUser(String name, String email, String lastName, String address, String password)
		{
			User userToUpdate = _memoryDatabase.Users.First(x => x.Email == email);

			if (userToUpdate != null)
			{
				userToUpdate.Name = name;
				userToUpdate.LastName = lastName;
				userToUpdate.Address = address;
				userToUpdate.Password = password;
			}

			else
			{
				throw new InvalidUserException();
			}

			return userToUpdate;
		}


		public void DeleteUser(User user)
        {
            _memoryDatabase.Users.Remove(user);
        }

        public bool Login(string email, string password)
        {
            User validUser = _memoryDatabase.Users.Find(x => x.Email == email && x.Password == password);

            if (validUser != null)
            {
                return true;
            }
            else
            {
                throw new InvalidUserException();
            }


        }
    }
}