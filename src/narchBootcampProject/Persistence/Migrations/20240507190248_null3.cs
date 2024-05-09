using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class null3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "NationalIdentity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { 119, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Quizs.Finish", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("39fd3134-d80b-4bed-99b5-2ab286f8f2df"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "gulsum.oran@hotmail.com", "Gulsum", "Oran", "123456", new byte[] { 67, 15, 12, 191, 214, 46, 2, 224, 206, 182, 117, 148, 180, 38, 188, 17, 70, 253, 64, 11, 8, 58, 129, 25, 197, 142, 13, 100, 229, 116, 164, 93, 173, 77, 239, 97, 70, 105, 179, 125, 238, 30, 64, 182, 215, 236, 124, 58, 58, 130, 97, 244, 235, 78, 187, 219, 217, 158, 83, 143, 41, 13, 117, 140 }, new byte[] { 117, 41, 131, 185, 83, 7, 195, 228, 182, 16, 23, 202, 225, 226, 157, 7, 250, 251, 127, 55, 42, 182, 230, 28, 247, 162, 250, 62, 187, 249, 53, 243, 176, 7, 190, 28, 158, 198, 21, 234, 216, 247, 193, 207, 13, 15, 40, 188, 174, 239, 211, 64, 77, 181, 135, 246, 37, 111, 201, 75, 34, 237, 16, 155, 197, 167, 135, 62, 103, 130, 89, 22, 166, 21, 3, 9, 62, 154, 27, 194, 43, 159, 45, 88, 68, 152, 218, 115, 208, 174, 96, 254, 151, 30, 103, 121, 207, 168, 140, 157, 30, 199, 145, 31, 126, 121, 114, 169, 122, 76, 252, 104, 3, 6, 49, 40, 148, 166, 137, 211, 200, 247, 74, 53, 70, 79, 18, 27 }, null, "admin" },
                    { new Guid("3f7dfb50-7790-4c41-ada0-7ec11bdaa738"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 7, 22, 2, 48, 251, DateTimeKind.Local).AddTicks(4028), null, "admin@mail", "Admin", "Oran", "123456", new byte[] { 120, 99, 21, 86, 153, 75, 143, 77, 194, 240, 159, 169, 69, 77, 40, 239, 241, 180, 196, 122, 28, 129, 65, 119, 81, 109, 56, 139, 228, 62, 201, 141, 206, 15, 117, 196, 182, 230, 136, 131, 181, 142, 40, 77, 200, 211, 141, 163, 93, 88, 141, 200, 196, 112, 131, 99, 121, 56, 128, 41, 6, 207, 49, 163 }, new byte[] { 253, 233, 170, 116, 153, 0, 45, 182, 73, 89, 115, 145, 206, 227, 149, 122, 14, 158, 5, 250, 180, 21, 96, 229, 34, 96, 29, 194, 9, 134, 29, 133, 0, 101, 51, 152, 0, 134, 195, 134, 4, 186, 145, 228, 128, 177, 211, 208, 172, 144, 61, 160, 225, 124, 125, 168, 41, 237, 215, 32, 76, 20, 183, 71, 117, 174, 51, 120, 106, 207, 125, 97, 177, 73, 175, 132, 228, 178, 43, 13, 118, 30, 45, 70, 255, 158, 151, 56, 79, 197, 116, 8, 209, 188, 173, 211, 163, 54, 95, 226, 128, 195, 150, 152, 134, 186, 93, 180, 168, 229, 108, 168, 170, 85, 221, 139, 132, 115, 145, 22, 145, 72, 57, 169, 108, 151, 220, 31 }, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("2275b8d8-e0e5-428f-b3af-0540c33e48e4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("3f7dfb50-7790-4c41-ada0-7ec11bdaa738") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("2275b8d8-e0e5-428f-b3af-0540c33e48e4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("39fd3134-d80b-4bed-99b5-2ab286f8f2df"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3f7dfb50-7790-4c41-ada0-7ec11bdaa738"));

            migrationBuilder.AlterColumn<string>(
                name: "NationalIdentity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("ed46dddb-d543-4311-963b-a8bab6370cb6"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 23, 18, 18, 17, 85, DateTimeKind.Local).AddTicks(7862), null, "gulsum.oran@hotmail.com", "Gulsum", "Oran", "123456", new byte[] { 162, 96, 146, 80, 118, 56, 196, 117, 53, 111, 251, 124, 187, 236, 206, 76, 130, 11, 159, 156, 243, 35, 223, 128, 156, 221, 33, 115, 135, 177, 168, 13, 245, 159, 166, 242, 152, 15, 202, 78, 31, 224, 114, 161, 250, 208, 229, 168, 6, 86, 73, 153, 129, 33, 231, 247, 232, 128, 121, 122, 185, 111, 151, 213 }, new byte[] { 54, 144, 77, 55, 48, 144, 237, 38, 206, 174, 68, 169, 10, 51, 182, 243, 77, 147, 112, 30, 171, 32, 120, 129, 231, 222, 113, 135, 171, 33, 176, 95, 104, 242, 149, 48, 217, 239, 112, 184, 39, 165, 169, 65, 64, 167, 197, 149, 71, 115, 17, 239, 211, 25, 182, 88, 24, 85, 11, 117, 140, 176, 67, 125, 14, 178, 126, 95, 3, 127, 78, 51, 219, 116, 0, 70, 157, 127, 194, 234, 105, 115, 249, 123, 247, 248, 42, 151, 136, 221, 191, 116, 47, 39, 208, 200, 10, 133, 182, 84, 245, 172, 201, 10, 206, 3, 57, 178, 87, 58, 40, 74, 206, 39, 220, 39, 201, 29, 248, 247, 186, 104, 127, 54, 224, 179, 50, 67 }, null, "admin" });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("9ec3c82e-72ea-4713-a4a2-e1db2a742f3a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("ed46dddb-d543-4311-963b-a8bab6370cb6") });
        }
    }
}
