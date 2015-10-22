CREATE TABLE [dbo].[Course_Details] (
    [GUID]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Term]   SMALLINT         DEFAULT ([dbo].[F_Term]()) NOT NULL,
    [ID]     INT              NOT NULL,
    [Lesson] TINYINT          CONSTRAINT [DF_Course_Details_Lesson] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([GUID] ASC),
    CONSTRAINT [FK_Course_Details_Student] FOREIGN KEY ([ID]) REFERENCES [dbo].[Student] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


