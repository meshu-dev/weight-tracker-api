using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeightTracker.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    ShortName = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: true),
                    UnitId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeighIns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeighIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeighIns_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Standard" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Kilograms", "kg" },
                    { 2, "Pounds", "lbs" },
                    { 3, "Stone", "st" },
                    { 4, "Stone & Pounds", "st lbs" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "RoleId", "UnitId" },
                values: new object[] { 1, "harmeshuppal@gmail.com", "Mesh", "Uppal", "ACOG+F5CQHf58fHvRpkgMdky7Fz1ZhIe/wPKXPbSO4chsdbKZ924fseQpUm3RtrQLw==", 1, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "RoleId", "UnitId" },
                values: new object[] { 2, "test@gmail.com", "Test", "Man", "ADZ13lG0RywDCqHtM3n6mtzigFJBND2QFE0frSsqa6gchP/ECB7FuUq9hhAvMYLZQg==", 2, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "RoleId", "UnitId" },
                values: new object[] { 3, "test2@gmail.com", "Tester", "Two", "AIsrTVzGzE+UoMu6jgn5fJdIJYj1TqIxMBW9pZOijvS0RajQmFY9bo6rpOJjq94YhA==", 2, 2 });

            migrationBuilder.InsertData(
                table: "WeighIns",
                columns: new[] { "Id", "Date", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 9, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "144" },
                    { 2, new DateTime(2019, 9, 14, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "144" },
                    { 4, new DateTime(2019, 9, 21, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "143" },
                    { 6, new DateTime(2019, 9, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "141.4" },
                    { 7, new DateTime(2019, 10, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "141" },
                    { 8, new DateTime(2019, 10, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "140" },
                    { 9, new DateTime(2019, 10, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "139.4" },
                    { 10, new DateTime(2019, 10, 26, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "138.2" },
                    { 12, new DateTime(2019, 11, 2, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "138.4" },
                    { 13, new DateTime(2019, 11, 9, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "137.6" },
                    { 14, new DateTime(2019, 11, 16, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "136.4" },
                    { 15, new DateTime(2019, 11, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "136" },
                    { 3, new DateTime(2019, 9, 10, 9, 30, 0, 0, DateTimeKind.Unspecified), 3, "160" },
                    { 5, new DateTime(2019, 9, 23, 9, 30, 0, 0, DateTimeKind.Unspecified), 3, "159" },
                    { 11, new DateTime(2019, 10, 30, 9, 30, 0, 0, DateTimeKind.Unspecified), 3, "158.5" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UnitId",
                table: "Users",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WeighIns_UserId",
                table: "WeighIns",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeighIns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
