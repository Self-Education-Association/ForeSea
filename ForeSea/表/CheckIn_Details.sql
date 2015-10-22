CREATE TABLE [dbo].[CheckIn_Details] (
    [GUID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Term]          SMALLINT         DEFAULT ([dbo].[F_Term]()) NOT NULL,
    [ID]            INT              NOT NULL,
    [IP]            VARCHAR (15)     DEFAULT ('0.0.0.0') NOT NULL,
    [Lesson]        TINYINT          NOT NULL,
    [Date]          DATE             DEFAULT (CONVERT([date],getdate())) NOT NULL,
    [CheckIn]       TIME (0)         DEFAULT (CONVERT([time](0),getdate())) NULL,
    [CheckOut]      TIME (0)         NULL,
    [Change]        TIME (0)         NULL,
    [Keep]          TIME (0)         DEFAULT (CONVERT([time](0),getdate())) NULL,
    [LateState]     BIT              DEFAULT ((0)) NOT NULL,
    [CheckOutState] BIT              DEFAULT ((0)) NOT NULL,
    [State]         TINYINT          NULL,
    [Note]          NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([GUID] ASC),
    CONSTRAINT [FK_CheckIn_Details_Student] FOREIGN KEY ([ID]) REFERENCES [dbo].[Student] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


