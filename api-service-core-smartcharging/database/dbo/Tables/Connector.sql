CREATE TABLE [dbo].[Connector] (
    [Identifier]      INT NOT NULL,
    [ChargeStationId] INT NOT NULL,
    [MaxCurrent]      INT NULL,
    PRIMARY KEY CLUSTERED ([ChargeStationId] ASC, [Identifier] ASC),
    CHECK ([Identifier]>=(1) AND [Identifier]<=(5)),
    CHECK ([MaxCurrent]>=(0)),
    FOREIGN KEY ([ChargeStationId]) REFERENCES [dbo].[ChargeStation] ([Identifier]) ON DELETE CASCADE
);




GO
