using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCharacters.Migrations
{
    public partial class seedingDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "FirstName", "Gender", "LastName", "Picture" },
                values: new object[] { 1, "Brad Pitt", "William Bradley", "male", " Pitt", "link" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
