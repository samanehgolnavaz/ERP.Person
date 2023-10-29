using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Person.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEnablePropToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enable",
                table: "People");
        }
    }
}
