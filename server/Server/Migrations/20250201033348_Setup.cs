using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class Setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    ContextId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                name: "HashStorage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hash = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    Operation = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HashStorage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt" },
                values: new object[] { 1L, null, new DateTime(2025, 2, 1, 0, 33, 47, 601, DateTimeKind.Local).AddTicks(5971), "guest@guest.com", new DateTime(2025, 2, 1, 0, 33, 47, 601, DateTimeKind.Local).AddTicks(6011), "Guest", "GUEST", "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", "", 4, new DateTime(2025, 2, 1, 0, 33, 47, 601, DateTimeKind.Local).AddTicks(6013) });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduleId",
                table: "Appointments",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BreakTimes_ScheduleId",
                table: "BreakTimes",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_HashStorage_Hash_UserId_Operation",
                table: "HashStorage",
                columns: new[] { "Hash", "UserId", "Operation" });

            migrationBuilder.CreateIndex(
                name: "IX_HashStorage_UserId",
                table: "HashStorage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_OdontologistId",
                table: "Schedules",
                column: "OdontologistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "BreakTimes");

            migrationBuilder.DropTable(
                name: "HashStorage");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Odontologists");
        }
    }
}
