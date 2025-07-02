using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedPersonNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "person",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "person",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "person",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "person",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "person",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "person",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "person",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "person",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "person",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "person",
                newName: "created_at");
        }
    }
}
