BEGIN TRANSACTION;
GO

ALTER TABLE [AppProducts] ADD [CategoryName] nvarchar(50) NULL;
GO

ALTER TABLE [AppProducts] ADD [CategorySlug] nvarchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231226081934_AddCategoryFieldInProduct', N'6.0.5');
GO

COMMIT;
GO

