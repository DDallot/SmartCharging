CREATE TABLE [dbo].[ChargeStation] (
    [Identifier] INT  IDENTITY (1, 1) NOT NULL,
    [Name]       TEXT NOT NULL,
    [GroupId]    INT  NOT NULL,
    PRIMARY KEY CLUSTERED ([Identifier] ASC),
    FOREIGN KEY ([GroupId]) REFERENCES [dbo].[MyGroup] ([Identifier]) ON DELETE CASCADE
);




GO


