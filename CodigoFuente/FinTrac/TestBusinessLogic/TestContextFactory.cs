using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace TestBusinessLogic
{
	public static class TestContextFactory
	{
		public static FintracContext CreateContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<FintracContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
			return new FintracContext(optionsBuilder.Options);
		}
	}
}
