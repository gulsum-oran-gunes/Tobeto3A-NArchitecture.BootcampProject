using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class instructorimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

           

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Instructors");

            migrationBuilder.CreateTable(
                name: "InstructorImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructorImages_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 143, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "InstructorImages.Admin", null },
                    { 144, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "InstructorImages.Read", null },
                    { 145, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "InstructorImages.Write", null },
                    { 146, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "InstructorImages.Create", null },
                    { 147, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "InstructorImages.Update", null },
                    { 148, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "InstructorImages.Delete", null }
                });

          
           

            migrationBuilder.CreateIndex(
                name: "IX_InstructorImages_InstructorId",
                table: "InstructorImages",
                column: "InstructorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorImages");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("aab8b06f-079c-4afc-8f31-58d3f1523563"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d248f0eb-a1da-45bb-9a5c-ab46f1d5dc83"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6f705d37-a89e-40c3-a2c2-e7a949de7d75"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("2aa46fc3-9da0-483b-a8e1-ce24bd356aac"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 9, 14, 26, 24, 596, DateTimeKind.Local).AddTicks(4544), null, "admin@mail", "Admin", "Oran", "123456", new byte[] { 45, 195, 236, 174, 183, 164, 84, 242, 61, 11, 52, 157, 4, 180, 171, 155, 34, 190, 2, 219, 8, 229, 51, 66, 70, 200, 70, 97, 238, 7, 4, 189, 115, 61, 111, 99, 24, 239, 245, 67, 194, 245, 30, 83, 67, 200, 225, 14, 22, 5, 224, 238, 81, 38, 158, 173, 29, 106, 157, 148, 53, 73, 141, 43 }, new byte[] { 75, 110, 215, 153, 211, 177, 109, 79, 133, 168, 70, 223, 37, 34, 221, 120, 47, 113, 2, 251, 73, 128, 31, 216, 1, 184, 130, 153, 167, 167, 94, 193, 229, 66, 77, 120, 193, 60, 162, 201, 19, 248, 61, 193, 163, 88, 137, 15, 29, 82, 27, 140, 194, 159, 20, 107, 3, 241, 50, 15, 177, 226, 59, 124, 219, 97, 77, 18, 211, 163, 98, 245, 210, 95, 16, 92, 77, 129, 40, 210, 43, 111, 113, 60, 9, 1, 176, 91, 31, 254, 156, 230, 111, 54, 170, 227, 128, 243, 192, 177, 197, 103, 119, 87, 205, 83, 242, 156, 60, 252, 187, 128, 108, 189, 136, 32, 23, 104, 70, 121, 82, 75, 212, 181, 246, 190, 163, 24 }, null, "admin" },
                    { new Guid("81308f37-349e-4820-bd5a-af797694207a"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "gulsum.oran@hotmail.com", "Gulsum", "Oran", "123456", new byte[] { 81, 156, 180, 15, 24, 110, 242, 196, 0, 195, 94, 32, 176, 239, 85, 231, 229, 27, 243, 78, 35, 225, 162, 140, 120, 193, 25, 189, 38, 33, 51, 96, 229, 98, 57, 131, 47, 239, 84, 138, 119, 159, 74, 144, 132, 94, 108, 170, 208, 224, 214, 62, 52, 20, 201, 39, 16, 83, 147, 44, 169, 158, 55, 253 }, new byte[] { 134, 41, 197, 183, 131, 239, 150, 2, 16, 226, 131, 236, 244, 128, 255, 41, 85, 184, 112, 206, 172, 215, 164, 46, 11, 89, 159, 151, 41, 118, 237, 136, 107, 229, 237, 106, 126, 190, 85, 143, 234, 173, 176, 162, 113, 127, 250, 90, 41, 55, 143, 138, 193, 67, 141, 6, 233, 112, 149, 230, 166, 21, 255, 239, 163, 68, 175, 189, 233, 83, 90, 51, 226, 54, 213, 220, 150, 91, 133, 181, 220, 27, 97, 222, 52, 212, 5, 117, 149, 216, 212, 24, 63, 129, 183, 126, 109, 52, 40, 181, 168, 61, 240, 167, 39, 4, 102, 47, 87, 181, 208, 186, 54, 45, 137, 63, 99, 102, 246, 21, 112, 232, 208, 139, 231, 203, 198, 23 }, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("998c508a-aa62-4a82-99b7-7683099ed625"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("2aa46fc3-9da0-483b-a8e1-ce24bd356aac") });
        }
    }
}
