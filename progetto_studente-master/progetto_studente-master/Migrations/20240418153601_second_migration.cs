using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace progetto_studente.Migrations
{
    /// <inheritdoc />
    public partial class second_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nomeStudente",
                table: "Studenti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nomeCorso",
                table: "Corsi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nomeStudente",
                table: "Studenti");

            migrationBuilder.DropColumn(
                name: "nomeCorso",
                table: "Corsi");
        }
    }
}
