CREATE TABLE [dbo].[Student] (
    [ID]      INT           NOT NULL,
    [Name]    NVARCHAR (20) NOT NULL,
    [Sex]     BIT           CONSTRAINT [DF__Student__Sex__3E52440B] DEFAULT ((0)) NULL,
    [Phone]   VARCHAR (20)  NULL,
    [Room]    NCHAR (2)     NULL,
    [RoomNum] SMALLINT      NULL,
    [Email]   NVARCHAR (50) NULL,
    [State]   TINYINT       CONSTRAINT [DF__Student__State__3F466844] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK__Student__3214EC2710661973] PRIMARY KEY CLUSTERED ([ID] ASC)
);


