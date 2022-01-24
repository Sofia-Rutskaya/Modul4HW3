using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Modul4HW3.Migrations
{
    public partial class CreateTableClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(27)", maxLength: 27, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ClientId", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "oafja[of@gmail.com", "Ellie", "Scottish", 1384920 },
                    { 2, "sreyw@gmail.com", "Matt", "Brutt", 37424724 },
                    { 3, "orywf@gmail.com", "Oliver", "Holland", 35864657 },
                    { 4, "okrdtf@gmail.com", "Hadderson", "Mallyis", 35737372 },
                    { 5, "dtktex@gmail.com", "Olya", "Ametya", 374876479 },
                    { 6, "srjuwyy@gmail.com", "Yacno", "Ugosgs", 323575 }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Budget", "ClientId", "Name", "StartedDate" },
                values: new object[,]
                {
                    { 1, 2314m, 1, "Project1", new DateTime(1998, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 23515m, 1, "Project3", new DateTime(2018, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1365135m, 2, "Project2", new DateTime(2011, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 135316m, 3, "Project4", new DateTime(2021, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1363153m, 4, "Project5", new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientId",
                table: "Project",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Client_ClientId",
                table: "Project",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Client_ClientId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Project_ClientId",
                table: "Project");

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Project");
        }
    }
}
