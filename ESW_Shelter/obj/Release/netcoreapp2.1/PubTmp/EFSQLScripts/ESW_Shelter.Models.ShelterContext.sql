IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213003200_RemoveUsersInfoTable')
BEGIN
    DROP TABLE [UsersInfo];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213003200_RemoveUsersInfoTable')
BEGIN
    ALTER TABLE [Users] ADD [City] nvarchar(40) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213003200_RemoveUsersInfoTable')
BEGIN
    ALTER TABLE [Users] ADD [DateOfBirth] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213003200_RemoveUsersInfoTable')
BEGIN
    ALTER TABLE [Users] ADD [Phone] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213003200_RemoveUsersInfoTable')
BEGIN
    ALTER TABLE [Users] ADD [PostalCode] nvarchar(10) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213003200_RemoveUsersInfoTable')
BEGIN
    ALTER TABLE [Users] ADD [Street] nvarchar(100) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213003200_RemoveUsersInfoTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181213003200_RemoveUsersInfoTable', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213010905_seed_roles_table')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] ON;
    INSERT INTO [Roles] ([RoleName])
    VALUES (N'Client');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213010905_seed_roles_table')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] ON;
    INSERT INTO [Roles] ([RoleName])
    VALUES (N'Volunteer');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213010905_seed_roles_table')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] ON;
    INSERT INTO [Roles] ([RoleName])
    VALUES (N'Employee');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213010905_seed_roles_table')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] ON;
    INSERT INTO [Roles] ([RoleName])
    VALUES (N'Administrator');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181213010905_seed_roles_table')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181213010905_seed_roles_table', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181215154229_ProductsTable')
BEGIN
    CREATE TABLE [AnimalTypes] (
        [AnimalTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(256) NOT NULL,
        CONSTRAINT [PK_AnimalTypes] PRIMARY KEY ([AnimalTypeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181215154229_ProductsTable')
BEGIN
    CREATE TABLE [Products] (
        [ProductID] int NOT NULL IDENTITY,
        [Name] nvarchar(256) NOT NULL,
        [Quantity] int NOT NULL,
        [AnimalTypeFK] int NOT NULL,
        [ProductTypeFK] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([ProductID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181215154229_ProductsTable')
BEGIN
    CREATE TABLE [ProductTypes] (
        [ProductTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(256) NOT NULL,
        CONSTRAINT [PK_ProductTypes] PRIMARY KEY ([ProductTypeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181215154229_ProductsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181215154229_ProductsTable', N'2.1.4-rtm-31024');
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
    VALUES (N'CreateIdentitySchema', N'2.1.4-rtm-31024');
END;

GO

