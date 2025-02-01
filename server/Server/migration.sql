CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Odontologists" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Odontologists" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Phone" TEXT NOT NULL,
    "Email" TEXT NOT NULL
);

CREATE TABLE "Users" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "NormalizedName" TEXT NOT NULL,
    "ProfilePictureUrl" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    "LastLogin" TEXT NOT NULL,
    "VerifiedAt" TEXT NULL,
    "Role" INTEGER NOT NULL,
    "ContextId" INTEGER NULL
);

CREATE TABLE "Schedules" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Schedules" PRIMARY KEY AUTOINCREMENT,
    "OdontologistId" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "StartDay" INTEGER NOT NULL,
    "StartTime" TEXT NOT NULL,
    "EndDay" INTEGER NOT NULL,
    "EndTime" TEXT NOT NULL,
    CONSTRAINT "FK_Schedules_Odontologists_OdontologistId" FOREIGN KEY ("OdontologistId") REFERENCES "Odontologists" ("Id") ON DELETE CASCADE
);

CREATE TABLE "HashStorage" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_HashStorage" PRIMARY KEY AUTOINCREMENT,
    "Hash" TEXT NOT NULL,
    "UserId" INTEGER NOT NULL,
    "Operation" INTEGER NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    "Details" TEXT NULL,
    CONSTRAINT "FK_HashStorage_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Appointments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Appointments" PRIMARY KEY AUTOINCREMENT,
    "ScheduleId" INTEGER NOT NULL,
    "Start" TEXT NOT NULL,
    "End" TEXT NOT NULL,
    "PatientName" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    CONSTRAINT "FK_Appointments_Schedules_ScheduleId" FOREIGN KEY ("ScheduleId") REFERENCES "Schedules" ("Id") ON DELETE CASCADE
);

CREATE TABLE "BreakTimes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_BreakTimes" PRIMARY KEY AUTOINCREMENT,
    "ScheduleId" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "StartDay" INTEGER NOT NULL,
    "StartTime" TEXT NOT NULL,
    "EndDay" INTEGER NOT NULL,
    "EndTime" TEXT NOT NULL,
    CONSTRAINT "FK_BreakTimes_Schedules_ScheduleId" FOREIGN KEY ("ScheduleId") REFERENCES "Schedules" ("Id") ON DELETE CASCADE
);

INSERT INTO "Users" ("Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt")
VALUES (1, NULL, '2025-02-01 00:33:47.6015971', 'guest@guest.com', '2025-02-01 00:33:47.6016011', 'Guest', 'GUEST', '$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq', '', 4, '2025-02-01 00:33:47.6016013');
SELECT changes();


CREATE INDEX "IX_Appointments_ScheduleId" ON "Appointments" ("ScheduleId");

CREATE INDEX "IX_BreakTimes_ScheduleId" ON "BreakTimes" ("ScheduleId");

CREATE INDEX "IX_HashStorage_Hash_UserId_Operation" ON "HashStorage" ("Hash", "UserId", "Operation");

CREATE INDEX "IX_HashStorage_UserId" ON "HashStorage" ("UserId");

CREATE INDEX "IX_Schedules_OdontologistId" ON "Schedules" ("OdontologistId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250201033348_Setup', '8.0.2');

COMMIT;

