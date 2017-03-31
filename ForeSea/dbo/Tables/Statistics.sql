CREATE TABLE [dbo].[Statistics] (
    [IP]        CHAR (15)     NOT NULL,
    [StartTime] DATETIME2 (0) CONSTRAINT [DF_Statistics_StartTime] DEFAULT (getdate()) NULL,
    [EndTime]   DATETIME2 (0) NULL
);

