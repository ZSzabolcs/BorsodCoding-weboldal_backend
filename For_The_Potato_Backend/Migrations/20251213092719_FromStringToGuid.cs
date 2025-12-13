using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace For_The_Potato_Backend.Migrations
{
    /// <inheritdoc />
    public partial class FromStringToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                oldCollation: "utf8_general_ci")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "user")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "save")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "user",
                type: "datetime(6)",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "'''0001-01-01 00:00:00.000000'''",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 6,
                oldDefaultValueSql: "'0001-01-01 00:00:00.000000'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "user",
                type: "datetime(6)",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "'''0001-01-01 00:00:00.000000'''",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 6,
                oldDefaultValueSql: "'0001-01-01 00:00:00.000000'");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "longtext",
                nullable: true,
                defaultValueSql: "'NULL'",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "user",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "save",
                type: "datetime(6)",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "'''0001-01-01 00:00:00.000000'''",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 6,
                oldDefaultValueSql: "'0001-01-01 00:00:00.000000'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "save",
                type: "datetime(6)",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "'''0001-01-01 00:00:00.000000'''",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 6,
                oldDefaultValueSql: "'0001-01-01 00:00:00.000000'");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "save",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.CreateTable(
                name: "__efmigrationshistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ProductVersion = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MigrationId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__efmigrationshistory");

            migrationBuilder.AlterDatabase(
                collation: "utf8_general_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "user")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "save")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "user",
                type: "datetime(6)",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "'0001-01-01 00:00:00.000000'",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 6,
                oldDefaultValueSql: "'''0001-01-01 00:00:00.000000'''");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "user",
                type: "datetime(6)",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "'0001-01-01 00:00:00.000000'",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 6,
                oldDefaultValueSql: "'''0001-01-01 00:00:00.000000'''");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldDefaultValueSql: "'NULL'");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "user",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "save",
                type: "datetime(6)",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "'0001-01-01 00:00:00.000000'",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 6,
                oldDefaultValueSql: "'''0001-01-01 00:00:00.000000'''");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModDate",
                table: "save",
                type: "datetime(6)",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "'0001-01-01 00:00:00.000000'",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 6,
                oldDefaultValueSql: "'''0001-01-01 00:00:00.000000'''");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "save",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36);
        }
    }
}
