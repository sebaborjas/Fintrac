using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Domain;

namespace BusinessLogic
{
    public class MemoryDatabase
    {
        public bool isLoggedIn { get; set; } = false;
        public List<User> Users { get; set; }

        public MemoryDatabase()
        {
            Users = new List<User>();
            addUsers();
        }
     
        
        
        public void addUsers()
        {
            User otro = new User
            {
                Name = "Emiliano",
                LastName = "Marotta",
                Email = "etest@test.com",
                Password = "1234123412",
                Address = "123"
            };
            Users.Add(otro);
        }
    }

}
