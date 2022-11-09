using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.EntityFramework.Migrations
{
    public partial class AddDefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Email", "Login", "ModifiedOn", "Password" },
                values: new object[] { 1L, new DateTime(2022, 11, 8, 23, 47, 28, 636, DateTimeKind.Local).AddTicks(2491), null, "nikgusachenko@gmail.com", "Faraday", null, "1111" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
