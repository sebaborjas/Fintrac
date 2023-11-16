using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserWorkspac2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Users_UserToInviteId",
                table: "Invitation");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Users_UserToInviteId",
                table: "Invitation",
                column: "UserToInviteId",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Users_UserToInviteId",
                table: "Invitation");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Users_UserToInviteId",
                table: "Invitation",
                column: "UserToInviteId",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
