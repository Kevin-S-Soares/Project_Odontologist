using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class EditedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e80c2912-ae43-48f3-b126-7617fccc2e2c"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt" },
                values: new object[] { new Guid("bcb7916e-b3ba-4da5-b757-3f57775093c0"), null, new DateTime(2025, 1, 25, 19, 20, 42, 225, DateTimeKind.Local).AddTicks(4483), "guest@guest.com", new DateTime(2025, 1, 25, 19, 20, 42, 225, DateTimeKind.Local).AddTicks(4514), "Guest", "GUEST", "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", "", 4, new DateTime(2025, 1, 25, 19, 20, 42, 225, DateTimeKind.Local).AddTicks(4516) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bcb7916e-b3ba-4da5-b757-3f57775093c0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt" },
                values: new object[] { new Guid("e80c2912-ae43-48f3-b126-7617fccc2e2c"), null, new DateTime(2024, 12, 4, 17, 49, 5, 757, DateTimeKind.Local).AddTicks(9434), "guest@guest.com", new DateTime(2024, 12, 4, 17, 49, 5, 757, DateTimeKind.Local).AddTicks(9469), "Guest", "GUEST", "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", "", 4, new DateTime(2024, 12, 4, 17, 49, 5, 757, DateTimeKind.Local).AddTicks(9471) });
        }
    }
}
