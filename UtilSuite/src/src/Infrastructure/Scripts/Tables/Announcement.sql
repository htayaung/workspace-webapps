﻿IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE type = 'U' AND name = 'Announcement')
BEGIN
	CREATE TABLE dbo.Announcement
	(
		[Id] UNIQUEIDENTIFIER PRIMARY KEY,
		[Name] NVARCHAR(1024) NOT NULL,
		[Description] NVARCHAR(4000) NOT NULL,
		[StartDate] DATETIME NOT NULL,
		[EndDate] DATETIME NOT NULL,
		[IsPublic] BIT NOT NULL,
		[IsActive] BIT NOT NULL,
		[Created] DATETIME NOT NULL,
		[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
		[LastModified] DATETIME NULL,
		[LastModifiedBy] UNIQUEIDENTIFIER NULL
	)
END
GO

