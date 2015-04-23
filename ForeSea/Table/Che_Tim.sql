CREATE TABLE [dbo].[Che_Tim]
(
	[Lesson] CHAR(2) NOT NULL PRIMARY KEY, 
    [StaCheIn] TIME(0) NOT NULL, 
    [LatCheIn] TIME(0) NOT NULL, 
    [EndCheIn] TIME(0) NOT NULL, 
    [StaCheOut] TIME(0) NOT NULL, 
    [EndCheOut] TIME(0) NOT NULL, 
    [Valid] BIT NOT NULL DEFAULT 1
)

GO


