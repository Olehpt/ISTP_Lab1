using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationSystemInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmailFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Users",
            type: "nvarchar(50)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Users",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(50)");
        }
    }
}
