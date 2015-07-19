CREATE TABLE [dbo].[Certificate_Details]
(
	[GUID] UNIQUEIDENTIFIER NOT NULL  DEFAULT NEWID(), 
    [Term] SMALLINT NOT NULL DEFAULT F_Term(), 
    [School] TINYINT NOT NULL, 
    [Staff] SMALLINT NOT NULL, 
    [Date] DATE NOT NULL DEFAULT GETDATE(), 
    [Type] TINYINT NOT NULL, 
    [Detail] NTEXT NULL, 
    [State] NVARCHAR(10) NOT NULL, 
    [Note] NTEXT NULL
)

GO

CREATE INDEX [IX_Certificate_Details_GUID] ON [dbo].[Certificate_Details] ([GUID])

GO

CREATE CLUSTERED INDEX [IX_Certificate_Details_Date] ON [dbo].[Certificate_Details] ([Date])
