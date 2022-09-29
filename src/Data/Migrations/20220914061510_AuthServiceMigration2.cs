using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AuthServiceMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Userss",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "Userss",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Userss",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Userss",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Userss",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Userss",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Attachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "Attachments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Attachments",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Userss");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Userss");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Userss");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Userss");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Userss");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Userss");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Attachments");
        }
    }
}
