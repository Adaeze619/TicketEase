using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketEase.Persistence.Migrations
{
    public partial class main : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4fcae2fc-dab1-4f54-bac5-90f459156ac6", "24d4a2f5-6bc0-4360-9bf7-cc2e63040627", "Manager", "Manager" },
                    { "72b7b1f4-f761-4d03-9b05-5bcaffaa04a0", "bf5461ca-1066-4271-80f3-cc62452687b4", "User", "User" },
                    { "cce2a963-34bb-4956-a195-19d22c47c47b", "b2f62dea-d9b1-4a50-87e3-ef66d28125f9", "SuperAdmin", "SuperAdmin" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6680fd96-61f2-456f-9251-b4978e3da095", 0, "e7819500-514f-4fd3-a80b-35a628c3eb03", "superadmin@library.com", false, false, null, "SUPERADMIN@LIBRARY.COM", "SUPERADMIN@LIBRARY.COM", "AQAAAAEAACcQAAAAECg4SRSDbMHgd5CqGATtP+aXBn1w7JCd4ALmQbMtMtQIvCTezgYOxX1LROQG1xq6sg==", null, false, "57469b8f-6df0-4a91-a64f-dbca004c8cea", false, "superadmin@library.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4fcae2fc-dab1-4f54-bac5-90f459156ac6", "6680fd96-61f2-456f-9251-b4978e3da095" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "72b7b1f4-f761-4d03-9b05-5bcaffaa04a0", "6680fd96-61f2-456f-9251-b4978e3da095" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cce2a963-34bb-4956-a195-19d22c47c47b", "6680fd96-61f2-456f-9251-b4978e3da095" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4fcae2fc-dab1-4f54-bac5-90f459156ac6", "6680fd96-61f2-456f-9251-b4978e3da095" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "72b7b1f4-f761-4d03-9b05-5bcaffaa04a0", "6680fd96-61f2-456f-9251-b4978e3da095" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cce2a963-34bb-4956-a195-19d22c47c47b", "6680fd96-61f2-456f-9251-b4978e3da095" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fcae2fc-dab1-4f54-bac5-90f459156ac6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72b7b1f4-f761-4d03-9b05-5bcaffaa04a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cce2a963-34bb-4956-a195-19d22c47c47b");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "AspNetUsers");
        }
    }
}
