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

CREATE TABLE [Product] (
    [ProductId] nvarchar(450) NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    [ProductPrice] float NOT NULL,
    [ProductImage] nvarchar(max) NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240523132457_developerdbmigration', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Product] ADD [Quantity] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240523134450_AddQuantityToProduct', N'8.0.5');
GO

COMMIT;
GO

