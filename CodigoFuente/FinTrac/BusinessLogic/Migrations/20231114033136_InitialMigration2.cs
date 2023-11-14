using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Goal_GoalId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_GoalId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "GoalId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "CategoryGoal",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    GoalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGoal", x => new { x.CategoriesId, x.GoalId });
                    table.ForeignKey(
                        name: "FK_CategoryGoal_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGoal_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGoal_GoalId",
                table: "CategoryGoal",
                column: "GoalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGoal");

            migrationBuilder.AddColumn<int>(
                name: "GoalId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_GoalId",
                table: "Category",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Goal_GoalId",
                table: "Category",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "Id");
        }
    }
}
