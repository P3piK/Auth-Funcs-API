using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthFuncsRepository.Migrations
{
    public partial class Seedadminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Modified", "ModifierId", "Password", "Status" },
                values: new object[] { 1, "admin", new DateTime(2022, 8, 22, 16, 5, 37, 192, DateTimeKind.Local).AddTicks(39), null, "AQAAAAEAACcQAAAAEH7ClDMgufncQOTcKuYJUkXMrMFSegWBHFAQphveG7ph/NI3O86jRBbRnwtsqCGLNA==", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
