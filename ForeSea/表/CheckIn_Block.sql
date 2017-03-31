CREATE TABLE [dbo].[CheckIn_Block] (
    [Name]      NVARCHAR (50) NOT NULL,
    [StartTime] TIME (0)      NOT NULL,
    [EndTime]   TIME (0)      NOT NULL,
    CONSTRAINT [PK_CheckIn_Block] PRIMARY KEY CLUSTERED ([Name] ASC)
);




