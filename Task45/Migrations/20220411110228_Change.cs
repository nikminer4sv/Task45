using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task45.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__messages",
                table: "_messages");

            migrationBuilder.RenameTable(
                name: "_messages",
                newName: "Messages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "_messages");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "_messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK__messages",
                table: "_messages",
                column: "Id");
        }
    }
}
