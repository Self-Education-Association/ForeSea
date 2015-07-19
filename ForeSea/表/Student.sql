CREATE TABLE [dbo].[Student]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(20) NOT NULL, 
    [Sex] BIT NOT NULL DEFAULT 0, 
    [Phone] CHAR(11) NULL, 
    [Room] NCHAR(2) NULL, 
    [RoomNum] CHAR(3) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [State] TINYINT NOT NULL DEFAULT 0
)
