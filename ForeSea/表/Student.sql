CREATE TABLE [dbo].[Student]
(
	[ID] CHAR(9) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(20) NOT NULL, 
    [Sex] BIT NOT NULL, 
    [Phone] CHAR(11) NULL, 
    [Room] NCHAR(2) NULL, 
    [RoomNum] CHAR(3) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [State] INT NOT NULL DEFAULT 0
)
