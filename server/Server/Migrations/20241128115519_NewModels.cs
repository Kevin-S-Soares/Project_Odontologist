using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class NewModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1d73f4a-a246-4ee7-bbbb-31208eb9cc2e"));

            migrationBuilder.AddColumn<long>(
                name: "ContextId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Odontologists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odontologists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OdontologistId = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartDay = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndDay = table.Column<int>(type: "INTEGER", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Odontologists_OdontologistId",
                        column: x => x.OdontologistId,
                        principalTable: "Odontologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleId = table.Column<long>(type: "INTEGER", nullable: false),
                    PatientName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BreakTimes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleId = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartDay = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndDay = table.Column<int>(type: "INTEGER", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakTimes_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailedTimes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppointmentId = table.Column<long>(type: "INTEGER", nullable: false),
                    StartDay = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    StartMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    StartYear = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndDay = table.Column<int>(type: "INTEGER", nullable: false),
                    EndDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    EndMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    EndYear = table.Column<int>(type: "INTEGER", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
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
                name: "IX_Appointments_ScheduleId",
                table: "Appointments",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BreakTimes_ScheduleId",
                table: "BreakTimes",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailedTimes_AppointmentId",
                table: "DetailedTimes",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_OdontologistId",
                table: "Schedules",
                column: "OdontologistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakTimes");

            migrationBuilder.DropTable(
                name: "DetailedTimes");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Odontologists");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7e92ca74-fdc1-4913-854b-204e5e7cf3b5"));

            migrationBuilder.DropColumn(
                name: "ContextId",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt" },
                values: new object[] { new Guid("f1d73f4a-a246-4ee7-bbbb-31208eb9cc2e"), new DateTime(2024, 7, 7, 17, 33, 14, 132, DateTimeKind.Local).AddTicks(783), "guest@guest.com", new DateTime(2024, 7, 7, 17, 33, 14, 132, DateTimeKind.Local).AddTicks(814), "Guest", "GUEST", "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", "", 4, new DateTime(2024, 7, 7, 17, 33, 14, 132, DateTimeKind.Local).AddTicks(816) });
        }
    }
}
