using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace progetto_studente.Migrations
{
    /// <inheritdoc />
    public partial class third_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studenti_Corsi_corsoId",
                table: "Studenti");

            migrationBuilder.RenameColumn(
                name: "corsoId",
                table: "Studenti",
                newName: "CorsoId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Studenti",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nomeStudente",
                table: "Studenti",
                newName: "Nome");

            migrationBuilder.RenameIndex(
                name: "IX_Studenti_corsoId",
                table: "Studenti",
                newName: "IX_Studenti_CorsoId");

            migrationBuilder.RenameColumn(
                name: "nomeCorso",
                table: "Corsi",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "corsoId",
                table: "Corsi",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Studenti_Corsi_CorsoId",
                table: "Studenti",
                column: "CorsoId",
                principalTable: "Corsi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studenti_Corsi_CorsoId",
                table: "Studenti");

            migrationBuilder.RenameColumn(
                name: "CorsoId",
                table: "Studenti",
                newName: "corsoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Studenti",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Studenti",
                newName: "nomeStudente");

            migrationBuilder.RenameIndex(
                name: "IX_Studenti_CorsoId",
                table: "Studenti",
                newName: "IX_Studenti_corsoId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Corsi",
                newName: "nomeCorso");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Corsi",
                newName: "corsoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studenti_Corsi_corsoId",
                table: "Studenti",
                column: "corsoId",
                principalTable: "Corsi",
                principalColumn: "corsoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
