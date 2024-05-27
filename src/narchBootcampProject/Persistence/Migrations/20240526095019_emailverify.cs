using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class emailverify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<bool>(
                name: "EmailVerified",
                table: "Applicants",
                type: "bit",
                nullable: true);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("6f2c6f9a-3ce2-4203-9a4d-2fe8464fa33a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11ab06ea-b481-4d34-a6fd-203473a034a0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0b1ac994-f7fa-497e-a63b-44656f9d39db"));

            migrationBuilder.DropColumn(
                name: "EmailVerified",
                table: "Applicants");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("6bb7f3e0-0aa3-4653-9886-858a1bf7cf33"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 22, 0, 0, 9, 476, DateTimeKind.Local).AddTicks(3882), null, "admin@mail", "Admin", "Oran", "123456", new byte[] { 85, 166, 199, 213, 158, 243, 104, 4, 240, 119, 212, 105, 98, 240, 90, 123, 242, 244, 229, 18, 41, 247, 66, 240, 229, 165, 236, 89, 34, 64, 141, 43, 0, 174, 149, 3, 125, 173, 142, 99, 253, 98, 78, 26, 130, 21, 189, 45, 29, 95, 223, 241, 87, 192, 176, 30, 83, 243, 50, 227, 95, 243, 175, 225 }, new byte[] { 117, 123, 67, 119, 233, 225, 245, 199, 240, 30, 218, 57, 92, 58, 79, 126, 16, 216, 54, 68, 28, 65, 78, 69, 203, 1, 12, 173, 158, 255, 175, 66, 4, 48, 180, 42, 96, 105, 242, 249, 54, 185, 221, 149, 221, 94, 126, 99, 132, 229, 222, 238, 11, 190, 179, 165, 72, 96, 163, 48, 14, 182, 123, 149, 236, 3, 158, 127, 149, 79, 36, 67, 9, 184, 185, 99, 98, 155, 201, 99, 75, 252, 239, 146, 103, 8, 123, 38, 212, 42, 48, 237, 250, 14, 202, 15, 17, 147, 44, 22, 117, 123, 34, 250, 66, 237, 212, 155, 137, 55, 205, 39, 210, 126, 242, 1, 191, 116, 143, 210, 115, 39, 134, 220, 4, 121, 149, 201 }, null, "admin" },
                    { new Guid("b8f1aa9f-2e9e-47e1-b829-85f4f19cf362"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "gulsum.oran@hotmail.com", "Gulsum", "Oran", "123456", new byte[] { 208, 181, 99, 85, 72, 46, 163, 20, 100, 207, 161, 181, 118, 145, 65, 57, 166, 62, 184, 156, 39, 56, 31, 158, 4, 225, 118, 161, 94, 91, 187, 44, 112, 93, 211, 85, 138, 49, 210, 183, 155, 55, 20, 127, 133, 40, 129, 112, 150, 244, 56, 47, 202, 9, 74, 110, 6, 241, 105, 13, 218, 230, 241, 140 }, new byte[] { 153, 146, 101, 130, 234, 150, 66, 124, 51, 3, 108, 29, 0, 170, 102, 5, 135, 222, 180, 64, 79, 72, 117, 156, 48, 250, 29, 82, 194, 74, 44, 149, 78, 252, 2, 190, 108, 14, 246, 223, 165, 80, 40, 132, 39, 247, 253, 171, 72, 158, 81, 40, 59, 35, 148, 50, 173, 25, 9, 232, 118, 176, 160, 137, 20, 30, 191, 15, 136, 99, 237, 104, 182, 91, 108, 61, 190, 121, 242, 73, 107, 67, 142, 3, 212, 87, 98, 86, 129, 169, 39, 154, 150, 162, 92, 66, 14, 126, 77, 133, 54, 253, 24, 10, 82, 236, 104, 105, 43, 149, 12, 60, 192, 47, 176, 160, 210, 11, 178, 127, 183, 15, 204, 158, 68, 254, 42, 109 }, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("508a68c3-00b1-483e-b092-10ce528effa2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("6bb7f3e0-0aa3-4653-9886-858a1bf7cf33") });
        }
    }
}
