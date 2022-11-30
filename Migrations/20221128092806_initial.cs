using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoViviendasFelipe.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Viviendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Direccion = table.Column<string>(type: "TEXT", nullable: true),
                    Propietario = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viviendas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameUsuario = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FechaInicio = table.Column<string>(type: "TEXT", nullable: true),
                    FechaFin = table.Column<string>(type: "TEXT", nullable: true),
                    ViviendaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Viviendas_ViviendaId",
                        column: x => x.ViviendaId,
                        principalTable: "Viviendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Viviendas",
                columns: new[] { "Id", "Description", "Direccion", "Name", "Propietario" },
                values: new object[,]
                {
                    { 1, "Casa en las afueras muy grande", "Málaga, Marbella, Calle Lopez, n20", "Casa de Campo", "Luis Gómez" },
                    { 2, "Piso en el centro de malaga para turistas", "Málaga, Málaga, Calle Granada, n30", "Piso en Ciudad", "José Pérez" }
                });

            migrationBuilder.InsertData(
                table: "Reservas",
                columns: new[] { "Id", "FechaFin", "FechaInicio", "NameUsuario", "ViviendaId" },
                values: new object[,]
                {
                    { 1, "15-10-2022", "10-10-2022", "Juan Gonzalez", 1 },
                    { 2, "15-10-2022", "10-10-2022", "Javi Ruiz", 2 },
                    { 3, "27-11-2022", "20-11-2022", "Juan Gonzalez", 2 },
                    { 4, "14-11-2022", "11-11-2022", "Alberto Muñoz", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ViviendaId",
                table: "Reservas",
                column: "ViviendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Viviendas");
        }
    }
}
