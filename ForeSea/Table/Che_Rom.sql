CREATE TABLE [dbo].[Che_Rom]
(
	[IP] CHAR(7) NOT NULL PRIMARY KEY, 
    [Name] NCHAR(5) NOT NULL, 
    [StaTim] TIME(0) NOT NULL, 
    [EndTim] TIME(0) NOT NULL, 
    [Block] NCHAR(6) NOT NULL, 
    [Valid] BIT NOT NULL DEFAULT 0
)
