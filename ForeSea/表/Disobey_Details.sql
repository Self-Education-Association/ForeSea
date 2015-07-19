CREATE TABLE [dbo].[Disobey_Details]
(
	[GUID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Term] SMALLINT NOT NULL DEFAULT F_Term(), 
    [ID] INT NOT NULL, 
    [Staff] SMALLINT NOT NULL, 
    [Date] DATE NOT NULL DEFAULT GETDATE(), 
    [Type] TINYINT NOT NULL, 
    [Detail] NTEXT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [Note] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_Disobey_Details_Student] FOREIGN KEY ([ID]) REFERENCES [Student]([ID]), 
    CONSTRAINT [FK_Disobey_Details_Type] FOREIGN KEY ([Type]) REFERENCES [Disobey_Type]([Type])
)
