CREATE TABLE [dbo].[Che_Stu_Dtl]
(
	[ID] CHAR(9) NOT NULL,
	[Term] CHAR(5) NOT NULL,
	[Date] DATE NOT NULL, 
    [IP] VARCHAR(15) NOT NULL, 
    [SigInTim] TIME(0) NULL, 
    [ChaTim] TIME(0) NULL, 
    [KepTim] TIME(0) NULL, 
    [SigOutTim] TIME(0) NULL, 
    [Action] BIT NOT NULL DEFAULT 0, 
    [SigInSta] CHAR(1) NOT NULL, 
    [State] CHAR(1) NOT NULL, 
    CONSTRAINT [FK_Che_Stu_Dtl_FS_Term] FOREIGN KEY ([Term]) REFERENCES [FS_Term]([Term]),
)

GO

CREATE CLUSTERED INDEX [IX_Che_Stu_Dtl_ID] ON [dbo].[Che_Stu_Dtl] ([ID])
