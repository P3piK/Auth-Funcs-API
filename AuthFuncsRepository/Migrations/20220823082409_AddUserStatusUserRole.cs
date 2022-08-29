using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthFuncsRepository.Migrations
{
    public partial class AddUserStatusUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Users",
                newName: "StatusId");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4128), "User" },
                    { 2, new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4131), "Superuser" },
                    { 3, new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4133), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "UserStatuses",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4017), "Active" },
                    { 2, new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4049), "Inactive" },
                    { 3, new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4051), "PasswordReset" },
                    { 4, new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4053), "NotConfirmed" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Modified", "RoleId", "StatusId" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4148), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserStatuses_StatusId",
                table: "Users",
                column: "StatusId",
                principalTable: "UserStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserStatuses_StatusId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StatusId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Users",
                newName: "Status");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Modified", "Status" },
                values: new object[] { new DateTime(2022, 8, 22, 16, 5, 37, 192, DateTimeKind.Local).AddTicks(39), 0 });
        }
    }
}
