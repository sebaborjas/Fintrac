using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public class UserService
    {
        private readonly FintracContext _database;

        public UserService(FintracContext database)
        {
            this._database = database;
        }

        public void Add(User u)
        {
            if (_database.Users.Any(x => x.Email == u.Email))
            {
                throw new UserAlreadyExistsException();
            }
            try
            {
		        Workspace defaultWorkspace = new Workspace { UserAdmin = u, Name = $"Espacio personal de {u.Name} {u.LastName}" };
                u.WorkspaceList.Add(defaultWorkspace);
                _database.Users.Add(u);
            }   
            catch (Exception exception)
            {
				throw exception;
                
            }
            _database.SaveChanges();
        }

		public User Get(string email)
        {
            return _database.Users.Where(u => u.Email == email).FirstOrDefault<User>();
            }

		public User UpdateUser(String name, String email, String lastName, String address, String password)
		{
			User userToUpdate = _database.Users.First(x => x.Email == email);

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
            _database.SaveChanges();

			return userToUpdate;
		}


		public void DeleteUser(User user)
        {
            _database.Users.Remove(user);
            _database.SaveChanges();
        }

        public bool Login(string email, string password)
        {
            User validUser = _database.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault<User>();

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