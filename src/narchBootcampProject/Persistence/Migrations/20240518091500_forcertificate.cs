using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class forcertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BootcampId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificates_Bootcamps_BootcampId",
                        column: x => x.BootcampId,
                        principalTable: "Bootcamps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 150, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Certificates.Admin", null },
                    { 151, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Certificates.Read", null },
                    { 152, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Certificates.Write", null },
                    { 153, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Certificates.Create", null },
                    { 154, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Certificates.Update", null },
                    { 155, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Certificates.Delete", null }
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ApplicantId",
                table: "Certificates",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_BootcampId",
                table: "Certificates",
                column: "BootcampId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("01b4dd6f-93ec-4dbe-aec7-67e6b12fd2ee"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3cbe7d1-d9f0-4d43-86a2-d1fcac93518a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("af01b85a-c3e6-4321-b2d4-25cc514f14ba"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("6f705d37-a89e-40c3-a2c2-e7a949de7d75"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 13, 12, 0, 55, 378, DateTimeKind.Local).AddTicks(2965), null, "admin@mail", "Admin", "Oran", "123456", new byte[] { 9, 117, 54, 99, 138, 17, 215, 80, 199, 105, 246, 6, 74, 133, 196, 58, 140, 45, 68, 88, 17, 21, 6, 67, 85, 1, 159, 1, 7, 6, 110, 65, 246, 29, 128, 250, 38, 37, 115, 119, 227, 72, 203, 255, 122, 53, 101, 117, 221, 20, 239, 38, 255, 36, 176, 195, 18, 63, 209, 130, 241, 195, 89, 184 }, new byte[] { 22, 118, 13, 212, 126, 72, 82, 227, 192, 131, 217, 109, 135, 232, 18, 62, 133, 157, 164, 162, 24, 139, 110, 119, 50, 81, 46, 200, 200, 10, 215, 137, 153, 188, 8, 90, 26, 140, 121, 209, 180, 52, 240, 141, 174, 61, 37, 34, 115, 120, 214, 224, 232, 178, 129, 192, 113, 238, 236, 73, 99, 241, 36, 181, 99, 84, 76, 92, 78, 255, 133, 138, 61, 157, 45, 53, 47, 118, 52, 103, 238, 233, 51, 165, 151, 1, 162, 234, 244, 66, 64, 54, 251, 241, 183, 44, 80, 225, 90, 114, 253, 88, 95, 80, 65, 196, 59, 13, 68, 155, 99, 29, 164, 62, 25, 167, 137, 196, 198, 63, 143, 172, 168, 89, 213, 165, 210, 40 }, null, "admin" },
                    { new Guid("d248f0eb-a1da-45bb-9a5c-ab46f1d5dc83"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "gulsum.oran@hotmail.com", "Gulsum", "Oran", "123456", new byte[] { 124, 101, 104, 206, 49, 184, 76, 16, 82, 198, 90, 87, 163, 57, 68, 70, 155, 248, 95, 240, 107, 91, 243, 166, 77, 254, 244, 1, 161, 118, 213, 208, 45, 112, 169, 121, 45, 246, 30, 97, 247, 245, 169, 120, 236, 196, 249, 111, 86, 107, 95, 105, 221, 38, 30, 186, 27, 179, 35, 63, 112, 205, 164, 12 }, new byte[] { 30, 90, 56, 78, 190, 14, 218, 88, 59, 244, 214, 66, 214, 189, 53, 10, 202, 111, 33, 130, 58, 213, 197, 80, 227, 53, 67, 58, 27, 149, 72, 123, 202, 93, 125, 254, 107, 133, 244, 48, 175, 13, 196, 86, 200, 110, 73, 50, 35, 107, 9, 218, 132, 238, 235, 28, 168, 20, 7, 159, 88, 247, 52, 66, 108, 117, 94, 197, 89, 241, 222, 148, 33, 27, 88, 28, 218, 99, 230, 7, 67, 56, 173, 164, 44, 63, 30, 43, 192, 225, 50, 26, 71, 132, 233, 94, 180, 114, 187, 197, 237, 237, 55, 237, 231, 48, 71, 108, 140, 14, 161, 20, 72, 190, 90, 213, 133, 22, 97, 146, 74, 86, 111, 107, 83, 155, 79, 208 }, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("aab8b06f-079c-4afc-8f31-58d3f1523563"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("6f705d37-a89e-40c3-a2c2-e7a949de7d75") });
        }
    }
}
