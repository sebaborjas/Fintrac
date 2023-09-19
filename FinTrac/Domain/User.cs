using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        private string _name;
        private string _email;
        private string _password;
        public string Name {
            get
            {
                return _name;
            }
            set 
            {
                if (value == "") 
                {
                    throw new ArgumentException("No se admiten campos vacíos");
                } else
                {
                    _name = value;
                }
            } 
        }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { 
            get 
            { 
                return _email; 
            } 
            set 
            {
                if(value == "")
                {
                    throw new ArgumentException("No se adminten campos vacios");
                } else
                {
                    _email = value;
                }
            } }
        public string Password {
            get 
            {
                return _password;
            }
            set
            {
                if(value == "")
                {
                    throw new ArgumentException("No se admiten campos vacios");
                } else
                {
                    _password = value;
                }
            } 
        }
    }
}
