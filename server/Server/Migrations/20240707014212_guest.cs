using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class guest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt" },
                values: new object[] { new Guid("a4f83cb5-2ea6-48c7-9241-77587ddcfddd"), new DateTime(2024, 7, 6, 22, 42, 11, 551, DateTimeKind.Local).AddTicks(943), "guest@guest.com", new DateTime(2024, 7, 6, 22, 42, 11, 551, DateTimeKind.Local).AddTicks(980), "Guest", "GUEST", "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", "", 4, new DateTime(2024, 7, 6, 22, 42, 11, 551, DateTimeKind.Local).AddTicks(981) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a4f83cb5-2ea6-48c7-9241-77587ddcfddd"));
        }
    }
}
