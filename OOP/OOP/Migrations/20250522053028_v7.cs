using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Meetings_MeetingtaskID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MeetingtaskID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MeetingtaskID",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeetingtaskID",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MeetingtaskID",
                table: "Users",
                column: "MeetingtaskID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Meetings_MeetingtaskID",
                table: "Users",
                column: "MeetingtaskID",
                principalTable: "Meetings",
                principalColumn: "taskID");
        }
    }
}
