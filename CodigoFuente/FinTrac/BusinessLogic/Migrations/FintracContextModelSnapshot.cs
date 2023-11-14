﻿// <auto-generated />
using System;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessLogic.Migrations
{
    [DbContext(typeof(FintracContext))]
    partial class FintracContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryGoal", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("GoalId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "GoalId");

                    b.HasIndex("GoalId");

                    b.ToTable("CategoryGoal");
                });

            modelBuilder.Entity("Domain.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkSpaceID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkSpaceID");

                    b.ToTable("Accounts", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("WorkspaceID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Domain.Exchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<double>("CurrencyValue")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkspaceID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceID");

                    b.ToTable("Exchange");
                });

            modelBuilder.Entity("Domain.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("MaxAmount")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkspaceID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceID");

                    b.ToTable("Goal");
                });

            modelBuilder.Entity("Domain.Invitation", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<string>("AdminId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserToInviteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("AdminId");

                    b.HasIndex("UserToInviteId");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("Domain.Transactions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AccountId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Workspace", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAdminId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserAdminId");

                    b.ToTable("Workspace");
                });

            modelBuilder.Entity("UsersWorkspaces", b =>
                {
                    b.Property<string>("UsersEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WorkspaceListID")
                        .HasColumnType("int");

                    b.HasKey("UsersEmail", "WorkspaceListID");

                    b.HasIndex("WorkspaceListID");

                    b.ToTable("UsersWorkspaces");
                });

            modelBuilder.Entity("Domain.CreditCard", b =>
                {
                    b.HasBaseType("Domain.Account");

                    b.Property<double>("AvailableCredit")
                        .HasColumnType("float");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeadLine")
                        .HasColumnType("int");

                    b.Property<string>("LastDigits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CreditCards", (string)null);
                });

            modelBuilder.Entity("Domain.PersonalAccount", b =>
                {
                    b.HasBaseType("Domain.Account");

                    b.Property<double>("StartingAmount")
                        .HasColumnType("float");

                    b.ToTable("PersonalAccounts", (string)null);
                });

            modelBuilder.Entity("CategoryGoal", b =>
                {
                    b.HasOne("Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Goal", null)
                        .WithMany()
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Account", b =>
                {
                    b.HasOne("Domain.Workspace", "WorkSpace")
                        .WithMany("AccountList")
                        .HasForeignKey("WorkSpaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkSpace");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.HasOne("Domain.Workspace", "Workspace")
                        .WithMany("CategoryList")
                        .HasForeignKey("WorkspaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Domain.Exchange", b =>
                {
                    b.HasOne("Domain.Workspace", "Workspace")
                        .WithMany("ExchangeList")
                        .HasForeignKey("WorkspaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Domain.Goal", b =>
                {
                    b.HasOne("Domain.Workspace", "Workspace")
                        .WithMany("GoalList")
                        .HasForeignKey("WorkspaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Domain.Invitation", b =>
                {
                    b.HasOne("Domain.User", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Workspace", "Workspace")
                        .WithMany()
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.User", "UserToInvite")
                        .WithMany("RecievedInvitations")
                        .HasForeignKey("UserToInviteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("UserToInvite");

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Domain.Transactions", b =>
                {
                    b.HasOne("Domain.Account", "Account")
                        .WithMany("TransactionList")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Workspace", b =>
                {
                    b.HasOne("Domain.User", "UserAdmin")
                        .WithMany()
                        .HasForeignKey("UserAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAdmin");
                });

            modelBuilder.Entity("UsersWorkspaces", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersEmail")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Domain.Workspace", null)
                        .WithMany()
                        .HasForeignKey("WorkspaceListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.CreditCard", b =>
                {
                    b.HasOne("Domain.Account", null)
                        .WithOne()
                        .HasForeignKey("Domain.CreditCard", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.PersonalAccount", b =>
                {
                    b.HasOne("Domain.Account", null)
                        .WithOne()
                        .HasForeignKey("Domain.PersonalAccount", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Account", b =>
                {
                    b.Navigation("TransactionList");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("RecievedInvitations");
                });

            modelBuilder.Entity("Domain.Workspace", b =>
                {
                    b.Navigation("AccountList");

                    b.Navigation("CategoryList");

                    b.Navigation("ExchangeList");

                    b.Navigation("GoalList");
                });
#pragma warning restore 612, 618
        }
    }
}
