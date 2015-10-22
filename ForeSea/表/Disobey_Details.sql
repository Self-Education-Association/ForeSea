CREATE TABLE [dbo].[Disobey_Details] (
    [GUID]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Term]   SMALLINT         DEFAULT ([dbo].[F_Term]()) NOT NULL,
    [ID]     INT              NOT NULL,
    [Staff]  SMALLINT         NOT NULL,
    [Date]   DATE             DEFAULT (getdate()) NOT NULL,
    [Type]   TINYINT          NOT NULL,
    [Detail] NTEXT            NULL,
    [State]  NVARCHAR (50)    NOT NULL,
    [Note]   NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([GUID] ASC),
    CONSTRAINT [FK_Disobey_Details_Student] FOREIGN KEY ([ID]) REFERENCES [dbo].[Student] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Disobey_Details_Type] FOREIGN KEY ([Type]) REFERENCES [dbo].[Disobey_Type] ([Type]) ON DELETE CASCADE ON UPDATE CASCADE
);


