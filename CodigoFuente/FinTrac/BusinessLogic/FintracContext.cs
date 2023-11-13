using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

            modelBuilder.Ignore<Report>();

            modelBuilder.Entity<Transaction>().HasOne(t => t.Category).WithMany().HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>().HasMany(a => a.TransactionList).WithOne(t => t.Account).HasForeignKey(t => t.AccountId);

			modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<CreditCard>().ToTable("CreditCards");
            modelBuilder.Entity<PersonalAccount>().ToTable("PersonalAccounts");


            modelBuilder.Entity<User>().HasKey(x => x.Email);

            modelBuilder.Entity<User>()
            .HasMany(u => u.WorkspaceList)
            .WithMany(w => w.Users)
			.UsingEntity<Dictionary<string, object>>(
			"UsersWorkspaces",
			j => j.HasOne<Workspace>().WithMany().OnDelete(DeleteBehavior.Cascade),
			j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.ClientCascade));


			modelBuilder.Entity<Workspace>()
                .HasOne(workspace => workspace.UserAdmin);

            modelBuilder.Entity<User>()
                .HasMany(u => u.RecievedInvitations)
                .WithOne(i => i.UserToInvite)
                .HasForeignKey(i => i.UserToInviteId);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Admin).WithMany()
                .HasForeignKey(i => i.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.UserToInvite)
                .WithMany()
                .HasForeignKey(i => i.UserToInviteId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
