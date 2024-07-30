using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeManagement.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_user_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("b6c2cd49-5e4a-480f-8a6d-95de0104bde9"));

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("46ae7872-16e1-4124-80b3-9b22cee66c2c"), "96 Dinh cong", "CafeM1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("46ae7872-16e1-4124-80b3-9b22cee66c2c"));

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("b6c2cd49-5e4a-480f-8a6d-95de0104bde9"), "96 Dinh cong", "CafeM1" });
        }
    }
}
