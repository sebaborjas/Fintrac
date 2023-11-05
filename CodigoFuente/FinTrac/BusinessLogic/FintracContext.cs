using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public class FintracContext : DbContext
    {
        public bool isLoggedIn { get; set; } = false;
        public DbSet<User> Users { get; set; }
        public User currentUser { get; set; }
        public Account currentAccount { get; set; }
        public Workspace currentWorkspace { get; set; }
        public FintracContext(DbContextOptions<FintracContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }
}
