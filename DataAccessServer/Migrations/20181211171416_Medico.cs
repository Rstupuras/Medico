using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseServer.Migrations
{
    public partial class Medico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Username = table.Column<string>(maxLength: 15, nullable: true),
                    Password = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Medicaments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsPrescribed = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicaments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Username = table.Column<string>(maxLength: 15, nullable: true),
                    Password = table.Column<string>(maxLength: 15, nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Username = table.Column<string>(maxLength: 15, nullable: true),
                    Password = table.Column<string>(maxLength: 15, nullable: true),
                    MainDoctorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_MainDoctorID",
                        column: x => x.MainDoctorID,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorID = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    PatientID = table.Column<int>(nullable: false),
                    IsViewed = table.Column<bool>(nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientID = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    IsSent = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    PharmacyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Pharmacies_PharmacyID",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicamentID = table.Column<int>(nullable: true),
                    PatientId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    DateTimeFrom = table.Column<DateTime>(nullable: false),
                    DateTimeTo = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medicaments_MedicamentID",
                        column: x => x.MedicamentID,
                        principalTable: "Medicaments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicamentID = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Items_Medicaments_MedicamentID",
                        column: x => x.MedicamentID,
                        principalTable: "Medicaments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorID",
                table: "Appointments",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientID",
                table: "Appointments",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Username",
                table: "Doctors",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MedicamentID",
                table: "Items",
                column: "MedicamentID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderID",
                table: "Items",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PatientID",
                table: "Orders",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PharmacyID",
                table: "Orders",
                column: "PharmacyID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MainDoctorID",
                table: "Patients",
                column: "MainDoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Username",
                table: "Patients",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_Username",
                table: "Pharmacies",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicamentID",
                table: "Prescriptions",
                column: "MedicamentID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Medicaments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
