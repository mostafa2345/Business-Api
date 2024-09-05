using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class commentOnetoone1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_appUserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36621533-3e24-4606-95da-dee25d26c12a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f231b6d6-0d22-4e78-94ec-9ff82c0517fe");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "appUserId",
                table: "Comments",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_appUserId",
                table: "Comments",
                newName: "IX_Comments_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ebb0424-fae4-4d4f-b0a6-24dbba36b88a", null, "User", "USER" },
                    { "a20b1959-213b-4fbf-a6e5-db70e6bb019e", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId",
                table: "Comments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ebb0424-fae4-4d4f-b0a6-24dbba36b88a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a20b1959-213b-4fbf-a6e5-db70e6bb019e");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Comments",
                newName: "appUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AppUserId",
                table: "Comments",
                newName: "IX_Comments_appUserId");

            migrationBuilder.AlterColumn<string>(
                name: "appUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36621533-3e24-4606-95da-dee25d26c12a", null, "User", "USER" },
                    { "f231b6d6-0d22-4e78-94ec-9ff82c0517fe", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_appUserId",
                table: "Comments",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
