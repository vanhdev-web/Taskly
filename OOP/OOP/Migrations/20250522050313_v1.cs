using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOP.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Projects_projectID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_projectID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "projectID",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "projectID",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_projectID",
                table: "Users",
                column: "projectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Projects_projectID",
                table: "Users",
                column: "projectID",
                principalTable: "Projects",
                principalColumn: "projectID");
        }
    }
}
