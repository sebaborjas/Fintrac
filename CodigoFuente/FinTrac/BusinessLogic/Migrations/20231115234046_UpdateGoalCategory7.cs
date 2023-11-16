using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGoalCategory7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Workspace",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAdminId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspace", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Workspace_Users_UserAdminId",
                        column: x => x.UserAdminId,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    WorkSpaceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Workspace_WorkSpaceID",
                        column: x => x.WorkSpaceID,
                        principalTable: "Workspace",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WorkspaceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Workspace_WorkspaceID",
                        column: x => x.WorkspaceID,
                        principalTable: "Workspace",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyValue = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    WorkspaceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exchange_Workspace_WorkspaceID",
                        column: x => x.WorkspaceID,
                        principalTable: "Workspace",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxAmount = table.Column<double>(type: "float", nullable: false),
                    WorkspaceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goal_Workspace_WorkspaceID",
                        column: x => x.WorkspaceID,
                        principalTable: "Workspace",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserToInviteId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Invitation_Users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitation_Users_UserToInviteId",
                        column: x => x.UserToInviteId,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitation_Workspace_ID",
                        column: x => x.ID,
                        principalTable: "Workspace",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersWorkspaces",
                columns: table => new
                {
                    UsersEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspaceListID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWorkspaces", x => new { x.UsersEmail, x.WorkspaceListID });
                    table.ForeignKey(
                        name: "FK_UsersWorkspaces_Users_UsersEmail",
                        column: x => x.UsersEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_UsersWorkspaces_Workspace_WorkspaceListID",
                        column: x => x.WorkspaceListID,
                        principalTable: "Workspace",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastDigits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableCredit = table.Column<double>(type: "float", nullable: false),
                    DeadLine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartingAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalAccounts_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryGoal",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    GoalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGoal", x => new { x.CategoryId, x.GoalId });
                    table.ForeignKey(
                        name: "FK_CategoryGoal_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGoal_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_WorkSpaceID",
                table: "Accounts",
                column: "WorkSpaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Category_WorkspaceID",
                table: "Category",
                column: "WorkspaceID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGoal_GoalId",
                table: "CategoryGoal",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_WorkspaceID",
                table: "Exchange",
                column: "WorkspaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_WorkspaceID",
                table: "Goal",
                column: "WorkspaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_AdminId",
                table: "Invitation",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_UserToInviteId",
                table: "Invitation",
                column: "UserToInviteId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkspaces_WorkspaceListID",
                table: "UsersWorkspaces",
                column: "WorkspaceListID");

            migrationBuilder.CreateIndex(
                name: "IX_Workspace_UserAdminId",
                table: "Workspace",
                column: "UserAdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGoal");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Exchange");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "PersonalAccounts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UsersWorkspaces");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Workspace");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
