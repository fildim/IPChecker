using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPChecker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COUNTRIES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TWO_LETTER_CODE = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false),
                    THREE_LETTER_CODE = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IP_ADDRESSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COUNTRY_ID = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPADDRESSES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IPADDRESSES_COUNTRIES",
                        column: x => x.COUNTRY_ID,
                        principalTable: "COUNTRIES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRIES_NAME",
                table: "COUNTRIES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRIES_THREELETTERCODE",
                table: "COUNTRIES",
                column: "THREE_LETTER_CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRIES_TWOLETTERCODE",
                table: "COUNTRIES",
                column: "TWO_LETTER_CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IP_ADDRESSES_COUNTRY_ID",
                table: "IP_ADDRESSES",
                column: "COUNTRY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_IPADDRESSES_IP",
                table: "IP_ADDRESSES",
                column: "IP",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IP_ADDRESSES");

            migrationBuilder.DropTable(
                name: "COUNTRIES");
        }
    }
}
