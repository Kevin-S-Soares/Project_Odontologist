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
VALUES ('A4F83CB5-2EA6-48C7-9241-77587DDCFDDD', '2024-07-06 22:42:11.5510943', 'guest@guest.com', '2024-07-06 22:42:11.551098', 'Guest', 'GUEST', '$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq', '', 4, '2024-07-06 22:42:11.5510981');
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240707014212_guest', '8.0.2');

COMMIT;

