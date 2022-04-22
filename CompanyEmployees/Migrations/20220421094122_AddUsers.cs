using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyEmployees.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20564f98-1b32-45c3-94d5-368324a950d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6338dd7a-c1c2-45fe-9611-b04044ce8c6f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c9193e0-4f0b-4462-96cb-6c5b15b0d9ee", "ad16393d-a3d1-4332-b051-4e3eb8804e50", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7bfb3e1c-53ed-4e05-9f8e-1080d64625cc", "50e7c66f-19be-4abf-906e-46470df35867", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bfb3e1c-53ed-4e05-9f8e-1080d64625cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c9193e0-4f0b-4462-96cb-6c5b15b0d9ee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6338dd7a-c1c2-45fe-9611-b04044ce8c6f", "c67a6cce-c3a3-4e43-9a4f-2be471aaa2b4", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20564f98-1b32-45c3-94d5-368324a950d0", "b19d1e17-6e55-4c08-ba71-862e555ce48d", "Administrator", "ADMINISTRATOR" });
        }
    }
}
