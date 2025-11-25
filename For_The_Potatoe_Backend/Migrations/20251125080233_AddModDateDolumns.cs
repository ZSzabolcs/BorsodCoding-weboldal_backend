using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace For_The_Potatoe_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddModDateDolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "User",
                newName: "RegDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Save",
                newName: "RegDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModDate",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModDate",
                table: "Save",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModDate",
                table: "Save");

            migrationBuilder.RenameColumn(
                name: "RegDate",
                table: "User",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "RegDate",
                table: "Save",
                newName: "Date");
        }
    }
}
