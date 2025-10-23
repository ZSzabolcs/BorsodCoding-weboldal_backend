using Microsoft.EntityFrameworkCore.Migrations;

namespace For_The_Potatoe_Backend.Migrations
{
    public class SetupData
    {
        public static void InsertDataToTables(ref MigrationBuilder migrationBuilder)
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
                    columns: ["UserId", "Points", "Level", "Language", "Date"],
                    values: [int.Parse(sor[0]), sor[1], sor[2], sor[3], sor[4]]
                    );
                }
            }
        }
    }
}
