using Microsoft.EntityFrameworkCore.Migrations;
using For_The_Potatoe_Backend;
using MySqlX.XDevAPI.CRUD;

#nullable disable

namespace For_The_Potatoe_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            using (StreamReader sr = new StreamReader("UserData.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');
                    migrationBuilder.InsertData(
                    table: "User",
                    columns: ["Id", "Name", "Password", "Date"],
                    values: [int.Parse(sor[0]), sor[1], sor[2], sor[3]]
                    );
                }

            }

            using (StreamReader sr = new StreamReader("SaveData.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(',');
                    migrationBuilder.InsertData(
                    table: "Save",
                    columns: ["Id", "Points", "Level", "Language", "Date", "UserId"],
                    values: [int.Parse(sor[0]), sor[1], sor[2], sor[3], sor[4], int.Parse(sor[0])]
                    );
                }
            }
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
