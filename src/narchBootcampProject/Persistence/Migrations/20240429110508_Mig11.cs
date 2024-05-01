using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9ec3c82e-72ea-4713-a4a2-e1db2a742f3a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed46dddb-d543-4311-963b-a8bab6370cb6"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { 114, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Quizs.Finish", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("46b2361f-e029-4989-bbed-66afe042c778"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 29, 14, 5, 7, 731, DateTimeKind.Local).AddTicks(5857), null, "admin@mail", "Admin", "Oran", "123456", new byte[] { 248, 90, 255, 37, 129, 57, 88, 255, 153, 84, 34, 113, 131, 14, 121, 90, 96, 224, 207, 134, 97, 163, 198, 76, 76, 198, 90, 11, 48, 129, 58, 162, 84, 55, 90, 64, 22, 71, 94, 183, 175, 36, 243, 155, 83, 221, 34, 108, 19, 29, 227, 139, 112, 162, 139, 88, 98, 27, 133, 121, 225, 105, 78, 108 }, new byte[] { 114, 101, 125, 18, 209, 230, 221, 217, 82, 85, 40, 241, 50, 56, 30, 138, 152, 176, 233, 60, 117, 68, 35, 208, 29, 181, 143, 43, 140, 143, 235, 169, 84, 247, 1, 176, 175, 229, 139, 37, 103, 217, 5, 133, 96, 102, 17, 112, 153, 33, 147, 170, 226, 98, 22, 28, 73, 39, 205, 244, 138, 40, 90, 126, 157, 193, 224, 234, 80, 5, 178, 95, 140, 35, 153, 66, 232, 253, 43, 157, 178, 134, 164, 235, 45, 107, 200, 179, 4, 206, 58, 24, 81, 144, 62, 243, 132, 186, 85, 217, 127, 26, 189, 68, 160, 19, 108, 14, 201, 220, 163, 94, 20, 49, 28, 200, 185, 120, 68, 23, 140, 157, 39, 192, 86, 221, 125, 29 }, null, "admin" });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("9ce224b6-fa66-4971-a1d6-84d71428a914"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("46b2361f-e029-4989-bbed-66afe042c778") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9ce224b6-fa66-4971-a1d6-84d71428a914"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46b2361f-e029-4989-bbed-66afe042c778"));

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
