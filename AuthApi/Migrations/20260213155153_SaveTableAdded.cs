using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class SaveTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var filePath = "for_the_potato.sql";

            if (File.Exists(filePath))
            {
                var sql = File.ReadAllText(filePath);
                migrationBuilder.Sql(sql);
            }
            else
            {
                throw new FileNotFoundException("Nem található az SQL fájl a migrációhoz!");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
