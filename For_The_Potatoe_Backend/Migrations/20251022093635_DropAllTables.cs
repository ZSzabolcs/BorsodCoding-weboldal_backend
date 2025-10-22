using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace For_The_Potatoe_Backend.Migrations
{
    /// <inheritdoc />
    public partial class DropAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Save");
            migrationBuilder.DropTable(name: "User");
        }
    }
}
