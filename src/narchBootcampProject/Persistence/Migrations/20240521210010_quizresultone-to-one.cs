using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class quizresultonetoone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Results_QuizId",
                table: "Results");


            migrationBuilder.CreateIndex(
                name: "IX_Results_QuizId",
                table: "Results",
                column: "QuizId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Results_QuizId",
                table: "Results");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("508a68c3-00b1-483e-b092-10ce528effa2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b8f1aa9f-2e9e-47e1-b829-85f4f19cf362"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6bb7f3e0-0aa3-4653-9886-858a1bf7cf33"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("af01b85a-c3e6-4321-b2d4-25cc514f14ba"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 18, 12, 15, 0, 79, DateTimeKind.Local).AddTicks(3947), null, "admin@mail", "Admin", "Oran", "123456", new byte[] { 31, 59, 173, 96, 189, 28, 60, 108, 171, 119, 157, 162, 52, 231, 76, 46, 108, 22, 192, 186, 105, 211, 16, 185, 181, 33, 99, 182, 158, 221, 1, 162, 196, 91, 136, 81, 10, 113, 197, 247, 48, 195, 5, 121, 21, 114, 23, 232, 161, 2, 13, 156, 129, 155, 72, 137, 210, 123, 169, 57, 12, 185, 41, 252 }, new byte[] { 124, 233, 121, 225, 104, 139, 45, 181, 245, 94, 251, 18, 127, 226, 158, 198, 104, 178, 29, 125, 167, 41, 18, 96, 174, 18, 181, 211, 155, 174, 8, 125, 135, 179, 44, 83, 252, 94, 95, 9, 201, 114, 123, 249, 224, 178, 13, 194, 1, 50, 38, 139, 219, 134, 116, 83, 161, 113, 138, 73, 181, 47, 222, 30, 28, 121, 44, 143, 15, 202, 69, 242, 146, 83, 100, 201, 198, 71, 187, 27, 205, 247, 115, 32, 48, 80, 164, 153, 107, 239, 143, 162, 216, 14, 186, 167, 206, 149, 115, 8, 154, 78, 32, 117, 109, 31, 89, 215, 127, 233, 83, 230, 37, 177, 132, 52, 101, 68, 170, 182, 248, 205, 106, 89, 49, 49, 67, 26 }, null, "admin" },
                    { new Guid("c3cbe7d1-d9f0-4d43-86a2-d1fcac93518a"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "gulsum.oran@hotmail.com", "Gulsum", "Oran", "123456", new byte[] { 214, 227, 239, 112, 62, 187, 206, 211, 50, 165, 97, 226, 28, 3, 82, 154, 58, 179, 25, 126, 72, 82, 55, 198, 93, 109, 88, 79, 253, 128, 22, 203, 198, 214, 85, 89, 154, 241, 168, 71, 137, 164, 90, 65, 117, 89, 49, 107, 28, 232, 215, 248, 101, 253, 59, 134, 194, 82, 172, 221, 209, 195, 105, 169 }, new byte[] { 13, 218, 106, 55, 239, 182, 13, 114, 52, 215, 15, 2, 211, 66, 198, 214, 21, 41, 106, 108, 118, 113, 173, 220, 33, 73, 86, 193, 229, 22, 175, 122, 22, 82, 255, 44, 6, 63, 224, 142, 121, 36, 18, 216, 64, 179, 33, 175, 81, 94, 91, 110, 148, 103, 218, 112, 195, 116, 195, 200, 12, 127, 162, 241, 252, 228, 40, 66, 214, 245, 246, 110, 205, 10, 98, 153, 123, 78, 20, 226, 185, 253, 40, 224, 173, 252, 221, 83, 57, 46, 50, 35, 95, 174, 66, 73, 56, 62, 8, 42, 67, 232, 40, 10, 240, 149, 114, 118, 192, 249, 159, 129, 63, 91, 227, 181, 80, 231, 252, 114, 201, 233, 187, 213, 242, 185, 49, 25 }, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("01b4dd6f-93ec-4dbe-aec7-67e6b12fd2ee"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("af01b85a-c3e6-4321-b2d4-25cc514f14ba") });

            migrationBuilder.CreateIndex(
                name: "IX_Results_QuizId",
                table: "Results",
                column: "QuizId");
        }
    }
}
