CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Users" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "ProfilePictureUrl" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    "VerifiedAt" TEXT NULL,
    "Role" INTEGER NOT NULL
);

CREATE TABLE "HashStorage" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_HashStorage" PRIMARY KEY,
    "Hash" TEXT NOT NULL,
    "UserId" TEXT NOT NULL,
    "Operation" INTEGER NOT NULL,
    CONSTRAINT "FK_HashStorage_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_HashStorage_Hash_UserId_Operation" ON "HashStorage" ("Hash", "UserId", "Operation");

CREATE INDEX "IX_HashStorage_UserId" ON "HashStorage" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240216212541_Setup', '8.0.2');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "HashStorage" ADD "CreatedAt" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240313235053_ChangedHashStorage', '8.0.2');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "Users" ADD "LastLogin" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE "HashStorage" ADD "Details" TEXT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240324031352_UpdatedUsersAndHashOperation', '8.0.2');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "Users" ADD "NormalizedName" TEXT NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240324033500_UpdatedUsersNormalizedName', '8.0.2');

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "Users" ("Id", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt")
VALUES ('F1D73F4A-A246-4EE7-BBBB-31208EB9CC2E', '2024-07-07 17:33:14.1320783', 'guest@guest.com', '2024-07-07 17:33:14.1320814', 'Guest', 'GUEST', '$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq', '', 4, '2024-07-07 17:33:14.1320816');
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240707203314_GuestAccount', '8.0.2');

COMMIT;

BEGIN TRANSACTION;

DELETE FROM "Users"
WHERE "Id" = 'F1D73F4A-A246-4EE7-BBBB-31208EB9CC2E';
SELECT changes();


ALTER TABLE "Users" ADD "ContextId" INTEGER NULL;

CREATE TABLE "Odontologists" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Odontologists" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Phone" TEXT NOT NULL,
    "Email" TEXT NOT NULL
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

CREATE TABLE "Appointments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Appointments" PRIMARY KEY AUTOINCREMENT,
    "ScheduleId" INTEGER NOT NULL,
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

CREATE TABLE "DetailedTimes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_DetailedTimes" PRIMARY KEY AUTOINCREMENT,
    "AppointmentId" INTEGER NOT NULL,
    "StartDay" INTEGER NOT NULL,
    "StartDayOfWeek" INTEGER NOT NULL,
    "StartMonth" INTEGER NOT NULL,
    "StartYear" INTEGER NOT NULL,
    "StartTime" TEXT NOT NULL,
    "EndDay" INTEGER NOT NULL,
    "EndDayOfWeek" INTEGER NOT NULL,
    "EndMonth" INTEGER NOT NULL,
    "EndYear" INTEGER NOT NULL,
    "EndTime" TEXT NOT NULL,
    CONSTRAINT "FK_DetailedTimes_Appointments_AppointmentId" FOREIGN KEY ("AppointmentId") REFERENCES "Appointments" ("Id") ON DELETE CASCADE
);

INSERT INTO "Users" ("Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt")
VALUES ('7E92CA74-FDC1-4913-854B-204E5E7CF3B5', NULL, '2024-11-28 08:55:19.2775593', 'guest@guest.com', '2024-11-28 08:55:19.2775625', 'Guest', 'GUEST', '$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq', '', 4, '2024-11-28 08:55:19.2775627');
SELECT changes();


CREATE INDEX "IX_Appointments_ScheduleId" ON "Appointments" ("ScheduleId");

CREATE INDEX "IX_BreakTimes_ScheduleId" ON "BreakTimes" ("ScheduleId");

CREATE UNIQUE INDEX "IX_DetailedTimes_AppointmentId" ON "DetailedTimes" ("AppointmentId");

CREATE INDEX "IX_Schedules_OdontologistId" ON "Schedules" ("OdontologistId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241128115519_NewModels', '8.0.2');

COMMIT;

BEGIN TRANSACTION;

DROP TABLE "DetailedTimes";

DELETE FROM "Users"
WHERE "Id" = '7E92CA74-FDC1-4913-854B-204E5E7CF3B5';
SELECT changes();


ALTER TABLE "Appointments" ADD "End" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE "Appointments" ADD "Start" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

INSERT INTO "Users" ("Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt")
VALUES ('E80C2912-AE43-48F3-B126-7617FCCC2E2C', NULL, '2024-12-04 17:49:05.7579434', 'guest@guest.com', '2024-12-04 17:49:05.7579469', 'Guest', 'GUEST', '$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq', '', 4, '2024-12-04 17:49:05.7579471');
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241204204906_RemovedDetailedTime', '8.0.2');

COMMIT;

BEGIN TRANSACTION;

DELETE FROM "Users"
WHERE "Id" = 'E80C2912-AE43-48F3-B126-7617FCCC2E2C';
SELECT changes();


INSERT INTO "Users" ("Id", "ContextId", "CreatedAt", "Email", "LastLogin", "Name", "NormalizedName", "Password", "ProfilePictureUrl", "Role", "VerifiedAt")
VALUES ('BCB7916E-B3BA-4DA5-B757-3F57775093C0', NULL, '2025-01-25 19:20:42.2254483', 'guest@guest.com', '2025-01-25 19:20:42.2254514', 'Guest', 'GUEST', '$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq', '', 4, '2025-01-25 19:20:42.2254516');
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250125222042_EditedModels', '8.0.2');

COMMIT;

