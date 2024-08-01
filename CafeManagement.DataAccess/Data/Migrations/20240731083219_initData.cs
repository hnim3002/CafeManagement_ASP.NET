using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("e3c77ca4-3c53-492d-9702-7c0ab41d6860"));

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("7ac1f73b-64bd-41fd-9532-0ad57d98ff11"), "96 Dinh cong", "CafeM1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("7ac1f73b-64bd-41fd-9532-0ad57d98ff11"));

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("e3c77ca4-3c53-492d-9702-7c0ab41d6860"), "96 Dinh cong", "CafeM1" });
        }
    }
}
