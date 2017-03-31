CREATE TABLE [dbo].[Test_Time] (
    [TestId]      UNIQUEIDENTIFIER CONSTRAINT [DF_Test_Time_TestId] DEFAULT (newid()) NOT NULL,
    [Description] NVARCHAR (50)    NOT NULL,
    [Limit]       SMALLINT         NOT NULL
);


