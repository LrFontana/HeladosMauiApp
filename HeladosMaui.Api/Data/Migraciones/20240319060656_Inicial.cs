using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeladosMaui.Api.Data.Migraciones
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Helados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteNombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ClienteEmail = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ClienteDireccion = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PrecioTotal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ContraseñaSalt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContraseñaHash = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpcionesDeHelados",
                columns: table => new
                {
                    HeladoId = table.Column<int>(type: "int", nullable: false),
                    Sabor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Agregados = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionesDeHelados", x => new { x.HeladoId, x.Sabor, x.Agregados });
                    table.ForeignKey(
                        name: "FK_OpcionesDeHelados_Helados_HeladoId",
                        column: x => x.HeladoId,
                        principalTable: "Helados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleOrdenes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenId = table.Column<long>(type: "bigint", nullable: false),
                    HeladoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Sabor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Agregados = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrecioTotal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleOrdenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleOrdenes_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Helados",
                columns: new[] { "Id", "Imagen", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_0.jpg", "Vanilla Delight", 60.25 },
                    { 2, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_1.jpg", "ChocoBerry Bliss", 70.5 },
                    { 3, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_2.jpg", "Minty Cookie Crunch", 8.75 },
                    { 4, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_3.jpg", "Strawberry Dream", 60.950000000000003 },
                    { 5, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_4.jpg", "Cookie Dough Extravaganza", 90.200000000000003 },
                    { 6, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_5.jpg", "Caramel Swirl Symphony", 70.75 },
                    { 7, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_6.jpg", "Peanut Butter Paradise", 80.5 },
                    { 8, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_7.jpg", "Mango Tango Tango", 90.799999999999997 },
                    { 9, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_8.jpg", "Hazelnut Heaven", 80.950000000000003 },
                    { 10, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_9.jpg", "Blueberry Bliss Bonanza", 70.200000000000003 },
                    { 11, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_10.jpg", "Toffee Twist Temptation", 70.950000000000003 },
                    { 12, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_11.jpg", "Rocky Road Revelry", 90.5 },
                    { 13, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_12.jpg", "Passionfruit Paradise", 80.75 },
                    { 14, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_13.jpg", "Cotton Candy Carnival", 70.200000000000003 },
                    { 15, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_14.jpg", "Lemon Sorbet Serenity", 60.899999999999999 },
                    { 16, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_15.jpg", "Maple Pecan Pleasure", 80.25 },
                    { 17, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_16.jpg", "Raspberry Ripple Rhapsody", 70.450000000000003 },
                    { 18, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_17.jpg", "Almond Joyful Jubilee", 90.950000000000003 },
                    { 19, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_18.jpg", "Blue Lagoon Bliss", 80.5 },
                    { 20, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_19.jpg", "Coconut Caramel Carnival", 70.799999999999997 }
                });

            migrationBuilder.InsertData(
                table: "OpcionesDeHelados",
                columns: new[] { "Agregados", "HeladoId", "Sabor" },
                values: new object[,]
                {
                    { "Chocolate Sauce", 1, "Default" },
                    { "Whipped Cream", 1, "Default" },
                    { "Default", 1, "Vanilla" },
                    { "Default", 2, "Chocolate" },
                    { "Sprinkles", 2, "Default" },
                    { "Default", 2, "Strawberry" },
                    { "Cherries", 3, "Default" },
                    { "Chocolate Sauce", 3, "Default" },
                    { "Default", 3, "Mint Chocolate Chip" },
                    { "Sprinkles", 4, "Default" },
                    { "Whipped Cream", 4, "Default" },
                    { "Default", 4, "Strawberry" },
                    { "Default", 5, "Cookie Dough" },
                    { "Cherries", 5, "Default" },
                    { "Chocolate Sauce", 5, "Default" },
                    { "Cherries", 6, "Default" },
                    { "Chocolate Sauce", 6, "Default" },
                    { "Default", 6, "Vanilla" },
                    { "Default", 7, "Chocolate" },
                    { "Sprinkles", 7, "Default" },
                    { "Whipped Cream", 7, "Default" },
                    { "Default", 8, "Cookie Dough" },
                    { "Sprinkles", 8, "Default" },
                    { "Default", 8, "Strawberry" },
                    { "Chocolate Sauce", 9, "Default" },
                    { "Whipped Cream", 9, "Default" },
                    { "Default", 9, "Mint Chocolate Chip" },
                    { "Default", 10, "Chocolate" },
                    { "Cherries", 10, "Default" },
                    { "Default", 10, "Strawberry" },
                    { "Cherries", 11, "Default" },
                    { "Whipped Cream", 11, "Default" },
                    { "Default", 11, "Vanilla" },
                    { "Default", 12, "Chocolate" },
                    { "Chocolate Sauce", 12, "Default" },
                    { "Default", 12, "Mint Chocolate Chip" },
                    { "Sprinkles", 13, "Default" },
                    { "Whipped Cream", 13, "Default" },
                    { "Default", 13, "Strawberry" },
                    { "Default", 14, "Cookie Dough" },
                    { "Cherries", 14, "Default" },
                    { "Chocolate Sauce", 14, "Default" },
                    { "Sprinkles", 15, "Default" },
                    { "Default", 15, "Strawberry" },
                    { "Default", 15, "Vanilla" },
                    { "Default", 16, "Chocolate" },
                    { "Whipped Cream", 16, "Default" },
                    { "Default", 16, "Mint Chocolate Chip" },
                    { "Default", 17, "Cookie Dough" },
                    { "Chocolate Sauce", 17, "Default" },
                    { "Default", 17, "Strawberry" },
                    { "Cherries", 18, "Default" },
                    { "Sprinkles", 18, "Default" },
                    { "Default", 18, "Vanilla" },
                    { "Default", 19, "Chocolate" },
                    { "Whipped Cream", 19, "Default" },
                    { "Default", 19, "Strawberry" },
                    { "Chocolate Sauce", 20, "Default" },
                    { "Sprinkles", 20, "Default" },
                    { "Default", 20, "Mint Chocolate Chip" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleOrdenes_OrdenId",
                table: "DetalleOrdenes",
                column: "OrdenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleOrdenes");

            migrationBuilder.DropTable(
                name: "OpcionesDeHelados");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Helados");
        }
    }
}
