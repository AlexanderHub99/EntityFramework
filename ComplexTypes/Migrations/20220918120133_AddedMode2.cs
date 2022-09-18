using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplexTypes.Migrations
{
    public partial class AddedMode2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profile_Surname",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile_Surname",
                table: "Users");
        }
    }
}
