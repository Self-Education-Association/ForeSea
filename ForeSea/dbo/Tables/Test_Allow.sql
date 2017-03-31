CREATE TABLE [dbo].[Test_Allow] (
    [StudentId] INT NOT NULL,
    [Done]      BIT CONSTRAINT [DF_Test_Allow_Done] DEFAULT ((0)) NOT NULL
);

