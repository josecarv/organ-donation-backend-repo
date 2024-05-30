IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Donors] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(100) NOT NULL,
    [BloodType] nvarchar(10) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [ContactNumber] nvarchar(15) NOT NULL,
    [IdentityNumber] nvarchar(50) NOT NULL,
    [ResidentialAddress] nvarchar(250) NOT NULL,
    [MailingAddress] nvarchar(250) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [TelephoneNumber] nvarchar(15) NOT NULL,
    [MobileNumber] nvarchar(15) NOT NULL,
    [Nationality] nvarchar(max) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [PreferredContact] nvarchar(10) NOT NULL,
    CONSTRAINT [PK_Donors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Organ] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Organ] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DonationPreferences] (
    [Id] int NOT NULL IDENTITY,
    [DonorId] int NOT NULL,
    [OrganId] int NOT NULL,
    CONSTRAINT [PK_DonationPreferences] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DonationPreferences_Donors_DonorId] FOREIGN KEY ([DonorId]) REFERENCES [Donors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DonationPreferences_Organ_OrganId] FOREIGN KEY ([OrganId]) REFERENCES [Organ] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Organ]'))
    SET IDENTITY_INSERT [Organ] ON;
INSERT INTO [Organ] ([Id], [Name])
VALUES (1, N'Kidney'),
(2, N'Liver'),
(3, N'Cartilage'),
(4, N'Bone Tissue'),
(5, N'Small Bowel'),
(6, N'Lungs'),
(7, N'Heart Valves'),
(8, N'Ligaments'),
(9, N'Pancreas'),
(10, N'Heart'),
(11, N'Cornea'),
(12, N'Tendons');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Organ]'))
    SET IDENTITY_INSERT [Organ] OFF;
GO

CREATE INDEX [IX_DonationPreferences_DonorId] ON [DonationPreferences] ([DonorId]);
GO

CREATE INDEX [IX_DonationPreferences_OrganId] ON [DonationPreferences] ([OrganId]);
GO

CREATE UNIQUE INDEX [IX_Donors_Email] ON [Donors] ([Email]);
GO

CREATE UNIQUE INDEX [IX_Donors_IdentityNumber] ON [Donors] ([IdentityNumber]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240528100537_InitialCreate', N'6.0.0');
GO

COMMIT;
GO

