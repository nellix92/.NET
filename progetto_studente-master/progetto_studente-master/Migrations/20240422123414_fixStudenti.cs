using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace progetto_studente.Migrations
{
    /// <inheritdoc />
    public partial class fixStudenti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cognome",
                table: "Studenti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cognome",
                table: "Studenti");
        }
    }
}
