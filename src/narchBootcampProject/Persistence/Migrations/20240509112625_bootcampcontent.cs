using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class bootcampcontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Bootcamps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Bootcamps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "BootcampContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BootcampId = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BootcampContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BootcampContents_Bootcamps_BootcampId",
                        column: x => x.BootcampId,
                        principalTable: "Bootcamps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantBootcampContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BootcampContentId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantBootcampContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantBootcampContents_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicantBootcampContents_BootcampContents_BootcampContentId",
                        column: x => x.BootcampContentId,
                        principalTable: "BootcampContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 130, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BootcampContents.Admin", null },
                    { 131, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BootcampContents.Read", null },
                    { 132, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BootcampContents.Write", null },
                    { 133, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BootcampContents.Create", null },
                    { 134, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BootcampContents.Update", null },
                    { 135, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BootcampContents.Delete", null },
                    { 136, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ApplicantBootcampContents.Admin", null },
                    { 137, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ApplicantBootcampContents.Read", null },
                    { 138, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ApplicantBootcampContents.Write", null },
                    { 139, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ApplicantBootcampContents.Create", null },
                    { 140, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ApplicantBootcampContents.Update", null },
                    { 141, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ApplicantBootcampContents.Delete", null }
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantBootcampContents_ApplicantId",
                table: "ApplicantBootcampContents",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantBootcampContents_BootcampContentId",
                table: "ApplicantBootcampContents",
                column: "BootcampContentId");

            migrationBuilder.CreateIndex(
                name: "IX_BootcampContents_BootcampId",
                table: "BootcampContents",
                column: "BootcampId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantBootcampContents");

            migrationBuilder.DropTable(
                name: "BootcampContents");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("998c508a-aa62-4a82-99b7-7683099ed625"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("81308f37-349e-4820-bd5a-af797694207a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2aa46fc3-9da0-483b-a8e1-ce24bd356aac"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Bootcamps");

            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Bootcamps");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
