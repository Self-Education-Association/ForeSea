CREATE TABLE [dbo].[Test_Relations] (
    [Id]        UNIQUEIDENTIFIER CONSTRAINT [DF_Test_Relations_Id] DEFAULT (newid()) NOT NULL,
    [TestId]    UNIQUEIDENTIFIER NOT NULL,
    [StudentId] INT              NOT NULL,
    [Time]      DATETIME2 (0)    CONSTRAINT [DF_Test_Relations_Time] DEFAULT (getdate()) NOT NULL
);
