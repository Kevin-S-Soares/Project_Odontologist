using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDetailedTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailedTimes");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7e92ca74-fdc1-4913-854b-204e5e7cf3b5"));

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt" },
                values: new object[] { new Guid("e80c2912-ae43-48f3-b126-7617fccc2e2c"), null, new DateTime(2024, 12, 4, 17, 49, 5, 757, DateTimeKind.Local).AddTicks(9434), "guest@guest.com", new DateTime(2024, 12, 4, 17, 49, 5, 757, DateTimeKind.Local).AddTicks(9469), "Guest", "GUEST", "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", "", 4, new DateTime(2024, 12, 4, 17, 49, 5, 757, DateTimeKind.Local).AddTicks(9471) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e80c2912-ae43-48f3-b126-7617fccc2e2c"));

            migrationBuilder.DropColumn(
                name: "End",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Appointments");

            migrationBuilder.CreateTable(
                name: "DetailedTimes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppointmentId = table.Column<long>(type: "INTEGER", nullable: false),
                    EndDay = table.Column<int>(type: "INTEGER", nullable: false),
                    EndDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    EndMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndYear = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDay = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    StartMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    StartYear = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailedTimes_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt" },
                values: new object[] { new Guid("7e92ca74-fdc1-4913-854b-204e5e7cf3b5"), null, new DateTime(2024, 11, 28, 8, 55, 19, 277, DateTimeKind.Local).AddTicks(5593), "guest@guest.com", new DateTime(2024, 11, 28, 8, 55, 19, 277, DateTimeKind.Local).AddTicks(5625), "Guest", "GUEST", "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", "", 4, new DateTime(2024, 11, 28, 8, 55, 19, 277, DateTimeKind.Local).AddTicks(5627) });

            migrationBuilder.CreateIndex(
                name: "IX_DetailedTimes_AppointmentId",
                table: "DetailedTimes",
                column: "AppointmentId",
                unique: true);
        }
    }
}
