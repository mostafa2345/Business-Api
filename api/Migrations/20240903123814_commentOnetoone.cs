using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class commentOnetoone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "817bb48b-f763-4490-abdc-21e019c715a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88eca2d6-f3a7-4ec2-88e4-58d2e35455fe");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "appUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36621533-3e24-4606-95da-dee25d26c12a", null, "User", "USER" },
                    { "f231b6d6-0d22-4e78-94ec-9ff82c0517fe", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_appUserId",
                table: "Comments",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_appUserId",
                table: "Comments",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_appUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_appUserId",
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

            migrationBuilder.DropColumn(
                name: "appUserId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "817bb48b-f763-4490-abdc-21e019c715a4", null, "Admin", "ADMIN" },
                    { "88eca2d6-f3a7-4ec2-88e4-58d2e35455fe", null, "User", "USER" }
                });
        }
    }
}
