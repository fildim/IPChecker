using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPChecker.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "IP_ADDRESSES",
                type: "char(15)",
                unicode: false,
                fixedLength: true,
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldUnicode: false,
                oldMaxLength: 15);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "IP_ADDRESSES",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(15)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 15);
        }
    }
}
