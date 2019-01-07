IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'CreateIdentitySchema')
BEGIN
    CREATE TABLE [Users] (
        [UserID] int NOT NULL IDENTITY,
        [Email] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [ConfirmedEmail] bit NOT NULL,
        [RoleId] int NOT NULL,
        CONSTRAINT [PK_UserID] PRIMARY KEY ([UserID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'CreateIdentitySchema')
BEGIN
    CREATE TABLE [UsersInfo] (
        [UserInfoID] int NOT NULL IDENTITY,
        [Street] nvarchar(max) NULL,
        [PostalCode] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [Phone] int NULL,
        [AlternativePhone] int NULL,
        [AlternativeEmail] nvarchar(max) NULL,
        [Facebook] nvarchar(max) NULL,
        [Twitter] nvarchar(max) NULL,
        [Instagram] nvarchar(max) NULL,
        [Tumblr] nvarchar(max) NULL,
        [Website] nvarchar(max) NULL,
        [UserID] int NOT NULL,
        CONSTRAINT [PK_UserInfoID] PRIMARY KEY ([UserInfoID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'CreateIdentitySchema')
BEGIN
    CREATE TABLE [Roles] (
        [RoleID] int NOT NULL IDENTITY,
        [RoleName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'CreateIdentitySchema')
BEGIN
    CREATE INDEX [UsersByID] ON [Users] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'CreateIdentitySchema')
BEGIN
    CREATE INDEX [UsersInfoByID] ON [UsersInfo] ([UserInfoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'CreateIdentitySchema')
BEGIN
    CREATE INDEX [RolesByID] ON [Roles] ([RoleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'CreateIdentitySchema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'CreateIdentitySchema', N'2.2.0-rtm-35687');
END;

GO

