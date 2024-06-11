using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace progetto_studente.Migrations
{
    /// <inheritdoc />
    public partial class fixfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corsi_Corsi_CorsoId",
                table: "Corsi");

            migrationBuilder.DropIndex(
                name: "IX_Corsi_CorsoId",
                table: "Corsi");

            migrationBuilder.DropColumn(
                name: "CorsoId",
                table: "Corsi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorsoId",
                table: "Corsi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Corsi_CorsoId",
                table: "Corsi",
                column: "CorsoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Corsi_Corsi_CorsoId",
                table: "Corsi",
                column: "CorsoId",
                principalTable: "Corsi",
                principalColumn: "Id");
        }
    }
}
