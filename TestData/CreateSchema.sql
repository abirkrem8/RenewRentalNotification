CREATE SCHEMA RentalManagement;

USE RentalManagement;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `RentalProperties` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `DateListed` datetime(6) NOT NULL,
    `Address1` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Address2` longtext CHARACTER SET utf8mb4 NOT NULL,
    `City` longtext CHARACTER SET utf8mb4 NOT NULL,
    `State` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ZipCode` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreationTimestamp` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_RentalProperties` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `RentalTenants` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NOT NULL,
    `EmailAddress` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreationTimestamp` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_RentalTenants` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TenantAssignments` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `RentalPropertyId` char(36) COLLATE ascii_general_ci NOT NULL,
    `DateOfMoveIn` datetime(6) NOT NULL,
    `ExpectedMoveOutDate` datetime(6) NOT NULL,
    `RentalTenantId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreationTimestamp` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TenantAssignments` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TenantAssignments_RentalProperties_RentalPropertyId` FOREIGN KEY (`RentalPropertyId`) REFERENCES `RentalProperties` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_TenantAssignments_RentalTenants_RentalTenantId` FOREIGN KEY (`RentalTenantId`) REFERENCES `RentalTenants` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_TenantAssignments_RentalPropertyId` ON `TenantAssignments` (`RentalPropertyId`);

CREATE INDEX `IX_TenantAssignments_RentalTenantId` ON `TenantAssignments` (`RentalTenantId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240401173705_Initial', '7.0.17');

COMMIT;