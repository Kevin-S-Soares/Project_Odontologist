using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class GuestAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt" },
                values: new object[] { new Guid("f1d73f4a-a246-4ee7-bbbb-31208eb9cc2e"), new DateTime(2024, 7, 7, 17, 33, 14, 132, DateTimeKind.Local).AddTicks(783), "guest@guest.com", new DateTime(2024, 7, 7, 17, 33, 14, 132, DateTimeKind.Local).AddTicks(814), "Guest", "GUEST", "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", "", 4, new DateTime(2024, 7, 7, 17, 33, 14, 132, DateTimeKind.Local).AddTicks(816) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1d73f4a-a246-4ee7-bbbb-31208eb9cc2e"));
        }
    }
}
