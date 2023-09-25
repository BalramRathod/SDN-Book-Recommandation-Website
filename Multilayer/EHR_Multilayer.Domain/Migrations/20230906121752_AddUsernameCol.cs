using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Multilayer.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Reviews");
        }
    }
}
