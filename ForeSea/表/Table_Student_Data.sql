CREATE TABLE [dbo].[Table_Student_Data]
(
	[ID] CHAR(9) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
	[College] CHAR(2) NOT NULL,
	[DynEd_Class] NVARCHAR(10) NOT NULL,
	[Finished_Class] NVARCHAR(MAX) NOT NULL DEFAULT '0',
    [Completion] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Table_Student_Data_Table_College] FOREIGN KEY ([College]) REFERENCES [Table_College]([College]) 
)
