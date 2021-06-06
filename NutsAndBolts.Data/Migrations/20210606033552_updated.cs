using Microsoft.EntityFrameworkCore.Migrations;

namespace NutsAndBolts.Data.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SnapShotTime",
                table: "ProductInventorySnapshots",
                newName: "SnapshotTime");

            migrationBuilder.RenameColumn(
                name: "UpdateOn",
                table: "CustomersAddresses",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "CustomersAddresses",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdateOn",
                table: "Customers",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Customers",
                newName: "CreatedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SnapshotTime",
                table: "ProductInventorySnapshots",
                newName: "SnapShotTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "CustomersAddresses",
                newName: "UpdateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "CustomersAddresses",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Customers",
                newName: "UpdateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Customers",
                newName: "CreateOn");
        }
    }
}
