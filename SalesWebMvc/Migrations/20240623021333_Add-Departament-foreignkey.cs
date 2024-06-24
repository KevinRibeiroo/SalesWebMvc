using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesWebMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartamentforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Departament_DepartamentId",
                table: "Seller");

            migrationBuilder.RenameColumn(
                name: "DepartamentId",
                table: "Seller",
                newName: "DepartamentID");

            migrationBuilder.RenameIndex(
                name: "IX_Seller_DepartamentId",
                table: "Seller",
                newName: "IX_Seller_DepartamentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Departament_DepartamentID",
                table: "Seller",
                column: "DepartamentID",
                principalTable: "Departament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Departament_DepartamentID",
                table: "Seller");

            migrationBuilder.RenameColumn(
                name: "DepartamentID",
                table: "Seller",
                newName: "DepartamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Seller_DepartamentID",
                table: "Seller",
                newName: "IX_Seller_DepartamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Departament_DepartamentId",
                table: "Seller",
                column: "DepartamentId",
                principalTable: "Departament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
