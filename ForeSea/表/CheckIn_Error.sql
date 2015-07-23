﻿CREATE TABLE [dbo].[CheckIn_Error]
(
	[GUID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Content] NTEXT NULL, 
    [Time] DATETIME2(0) NULL DEFAULT GETDATE()
)
