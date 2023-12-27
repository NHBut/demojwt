using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTREF.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "UserRefreshToken",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserRefreshToken",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshToken_Username",
                table: "UserRefreshToken",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRefreshToken_User_Username",
                table: "UserRefreshToken",
                column: "Username",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRefreshToken_User_Username",
                table: "UserRefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_UserRefreshToken_Username",
                table: "UserRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "UserRefreshToken",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserRefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Name");
        }
    }
}
