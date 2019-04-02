using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseServer.Migrations
{
    public partial class Medico1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medicaments_MedicamentID",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "MedicamentID",
                table: "Prescriptions",
                newName: "MedicamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_MedicamentID",
                table: "Prescriptions",
                newName: "IX_Prescriptions_MedicamentId");

            migrationBuilder.AlterColumn<int>(
                name: "MedicamentId",
                table: "Prescriptions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Pharmacies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Doctors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medicaments_MedicamentId",
                table: "Prescriptions",
                column: "MedicamentId",
                principalTable: "Medicaments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medicaments_MedicamentId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "MedicamentId",
                table: "Prescriptions",
                newName: "MedicamentID");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_MedicamentId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_MedicamentID");

            migrationBuilder.AlterColumn<int>(
                name: "MedicamentID",
                table: "Prescriptions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medicaments_MedicamentID",
                table: "Prescriptions",
                column: "MedicamentID",
                principalTable: "Medicaments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
