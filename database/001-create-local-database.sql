-- Run manually in SSMS against the local default SQL Server instance.
-- Creates only SecureCodingWebshopLocal; does not drop or reset databases.
USE [master];
GO

IF DB_ID(N'SecureCodingWebshopLocal') IS NULL
BEGIN
    CREATE DATABASE [SecureCodingWebshopLocal];
END;
GO

USE [SecureCodingWebshopLocal];
GO
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF OBJECT_ID(N'dbo.Product', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Product
    (
        ProductId int IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_Product PRIMARY KEY,
        Name nvarchar(200) NOT NULL,
        Description nvarchar(2000) NOT NULL,
        Price decimal(18,2) NOT NULL,
        Stock int NOT NULL,
        ImageUrl nvarchar(2048) NULL,
        CONSTRAINT CK_Product_Price_Nonnegative CHECK (Price >= 0),
        CONSTRAINT CK_Product_Stock_Nonnegative CHECK (Stock >= 0)
    );
END;

-- Seed only an empty table. Rerunning never resets prices or stock.
IF NOT EXISTS (SELECT 1 FROM dbo.Product WITH (UPDLOCK, HOLDLOCK))
BEGIN
    INSERT INTO dbo.Product (Name, Description, Price, Stock, ImageUrl)
    VALUES
        (N'Tastatur', N'Eksempelprodukt til den lokale webshop.', 299.00, 10, NULL),
        (N'Mus', N'Eksempelprodukt til den lokale webshop.', 149.00, 15, NULL),
        (N'Skaerm', N'Eksempelprodukt til den lokale webshop.', 1299.00, 5, NULL);
END;

COMMIT TRANSACTION;
GO

SELECT ProductId, Name, Price, Stock FROM dbo.Product ORDER BY ProductId;
