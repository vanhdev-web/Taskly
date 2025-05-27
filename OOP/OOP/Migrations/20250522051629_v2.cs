using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOP.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Meetings_MeetingID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MeetingID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MeetingID",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "MeetingtaskID",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeetingMemberManagement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeetingID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingMemberManagement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MeetingMemberManagement_Meetings_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meetings",
                        principalColumn: "taskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingMemberManagement_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_MeetingtaskID",
                table: "Users",
                column: "MeetingtaskID");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingMemberManagement_MeetingID",
                table: "MeetingMemberManagement",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingMemberManagement_UserID",
                table: "MeetingMemberManagement",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Meetings_MeetingtaskID",
                table: "Users",
                column: "MeetingtaskID",
                principalTable: "Meetings",
                principalColumn: "taskID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Meetings_MeetingtaskID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "MeetingMemberManagement");

            migrationBuilder.DropIndex(
                name: "IX_Users_MeetingtaskID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MeetingtaskID",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "MeetingID",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MeetingID",
                table: "Users",
                column: "MeetingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Meetings_MeetingID",
                table: "Users",
                column: "MeetingID",
                principalTable: "Meetings",
                principalColumn: "taskID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
