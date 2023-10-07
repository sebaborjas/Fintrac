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
		public List<User> Users { get; set; }

		public MemoryDatabase ()
		{
			Users = new List<User>();
		}
	}	
}
