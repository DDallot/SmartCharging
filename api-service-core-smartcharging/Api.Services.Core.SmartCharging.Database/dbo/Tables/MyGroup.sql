CREATE TABLE [dbo].[MyGroup] (
    [Identifier] INT  IDENTITY (1, 1) NOT NULL,
    [Name]       TEXT NOT NULL,
    [Capacity]   INT  NULL,
    PRIMARY KEY CLUSTERED ([Identifier] ASC),
    CHECK ([Capacity]>(0))
);



