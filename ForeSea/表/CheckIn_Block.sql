CREATE TABLE [dbo].[CheckIn_Block]
(
	[Block] TINYINT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(10) NOT NULL, 
    [StartTime] TIME(0) NOT NULL, 
    [EndTime] TIME(0) NOT NULL
)
