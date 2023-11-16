using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersWorkspaces_Workspace_WorkspaceListID",
                table: "UsersWorkspaces");

            migrationBuilder.RenameColumn(
                name: "WorkspaceListID",
                table: "UsersWorkspaces",
                newName: "WorkspacesID");

            migrationBuilder.RenameIndex(
                name: "IX_UsersWorkspaces_WorkspaceListID",
                table: "UsersWorkspaces",
                newName: "IX_UsersWorkspaces_WorkspacesID");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersWorkspaces_Workspace_WorkspacesID",
                table: "UsersWorkspaces",
                column: "WorkspacesID",
                principalTable: "Workspace",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersWorkspaces_Workspace_WorkspacesID",
                table: "UsersWorkspaces");

            migrationBuilder.RenameColumn(
                name: "WorkspacesID",
                table: "UsersWorkspaces",
                newName: "WorkspaceListID");

            migrationBuilder.RenameIndex(
                name: "IX_UsersWorkspaces_WorkspacesID",
                table: "UsersWorkspaces",
                newName: "IX_UsersWorkspaces_WorkspaceListID");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersWorkspaces_Workspace_WorkspaceListID",
                table: "UsersWorkspaces",
                column: "WorkspaceListID",
                principalTable: "Workspace",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
