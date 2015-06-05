CREATE TABLE [dbo].[Table_Term]
(
	[Term] CHAR(2) NOT NULL PRIMARY KEY, 
	[Term_Name] NVARCHAR(MAX) NOT NULL,
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL
)
