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
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<CreditCard>().ToTable("CreditCards");
            modelBuilder.Entity<PersonalAccount>().ToTable("PersonalAccounts");

            modelBuilder.Entity<User>().HasKey(x => x.Email);
            modelBuilder.Entity<Workspace>().HasKey(x => x.ID);

            modelBuilder.Entity<UserWorkspace>().HasKey(uw => new { uw.UserId, uw.WorkspaceId });

            modelBuilder.Entity<UserWorkspace>()
                .HasOne(uw => uw.User)
                .WithMany(u => u.UserWorkspace)
                .HasForeignKey(uw => uw.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserWorkspace>()
                .HasOne(uw => uw.Workspace)
                .WithMany(w => w.UserWorkspace)
                .HasForeignKey(uw => uw.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workspace>()
                .HasMany(w => w.Users)
                .WithMany(u => u.Workspaces)
                .UsingEntity<UserWorkspace>(
                    j => j.HasOne(uw => uw.User)
                          .WithMany(u => u.UserWorkspace)
                          .HasForeignKey(uw => uw.UserId)
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne(uw => uw.Workspace)
                          .WithMany(w => w.UserWorkspace)
                          .HasForeignKey(uw => uw.WorkspaceId)
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasKey(uw => new { uw.UserId, uw.WorkspaceId })
                );

            modelBuilder.Entity<Workspace>()
                .HasOne(workspace => workspace.UserAdmin);

            modelBuilder.Entity<User>()
                .HasMany(u => u.RecievedInvitations)
                .WithOne(i => i.UserToInvite)
                .HasForeignKey(i => i.UserToInviteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Workspace)
                .WithMany().HasForeignKey(w => w.ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Admin)
                .WithMany().HasForeignKey(i => i.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany().HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Goal>().HasKey(g => g.Id);

            modelBuilder.Entity<CategoryGoal>()
                .HasKey(cg => new { cg.CategoryId, cg.GoalId });

            modelBuilder.Entity<CategoryGoal>()
                .HasOne(cg => cg.Category)
                .WithMany(c => c.GoalCategory)
                .HasForeignKey(cg => cg.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryGoal>()
                .HasOne(cg => cg.Goal)
                .WithMany(g => g.GoalCategory)
                .HasForeignKey(cg => cg.GoalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Goal>()
                .HasMany(g => g.Categories)
                .WithMany(c => c.Goals)
                .UsingEntity<CategoryGoal>(
                    j => j.HasOne(cg => cg.Category)
                          .WithMany(c => c.GoalCategory)
                          .HasForeignKey(cg => cg.CategoryId)
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne(cg => cg.Goal)
                          .WithMany(g => g.GoalCategory)
                          .HasForeignKey(cg => cg.GoalId)
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasKey(cg => new { cg.CategoryId, cg.GoalId })
                );
        }
    }
}
