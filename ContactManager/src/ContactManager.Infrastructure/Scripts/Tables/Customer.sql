﻿IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE type = 'U' AND name = 'Customer')
BEGIN
	CREATE TABLE dbo.Customer
	(
		[Id] UNIQUEIDENTIFIER PRIMARY KEY,
		[CustomerId] VARCHAR(16) NOT NULL,
		[DisplayName] NVARCHAR(256) NOT NULL,
		[FullName] NVARCHAR(256) NOT NULL,
		[Email] VARCHAR(256) NOT NULL,
		[JobTitle] VARCHAR(128) NOT NULL,
		[Department] VARCHAR(128) NOT NULL,
		[WorkPhone] VARCHAR(128),
		[MobilePhone] VARCHAR(128),
		[IsActive] BIT NOT NULL,
		[Created] DATETIME NOT NULL,
		[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
		[LastModified] DATETIME NULL,
		[LastModifiedBy] UNIQUEIDENTIFIER NULL
	)
END
GO

