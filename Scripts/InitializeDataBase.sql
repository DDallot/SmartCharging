USE SmartCharging

-- Create the Group table
CREATE TABLE MyGroup (
    Identifier INTEGER PRIMARY KEY IDENTITY(1,1),
    Name TEXT NOT NULL,
    Capacity INTEGER CHECK (Capacity > 0) 
);

-- Create the ChargeStation table
CREATE TABLE ChargeStation (
    Identifier INTEGER PRIMARY KEY IDENTITY(1,1),
    Name TEXT NOT NULL,
    GroupId INTEGER NOT NULL,
    FOREIGN KEY (GroupId) REFERENCES MyGroup (Identifier) ON DELETE CASCADE
);

-- Create the Connector table
CREATE TABLE Connector (
    Identifier INTEGER CHECK (Identifier >= 1 AND Identifier <= 5),
	ChargeStationId INTEGER,
    MaxCurrent INTEGER CHECK (MaxCurrent >= 0), -- Zero is the default value for the connectors
    PRIMARY KEY (ChargeStationId, Identifier),
    FOREIGN KEY (ChargeStationId) REFERENCES ChargeStation(Identifier) ON DELETE CASCADE
);

SET IDENTITY_INSERT MyGroup ON;
-- Sample data for the Group table
INSERT INTO MyGroup (Identifier, Name, Capacity) VALUES
    (1, 'Group A', 100),
    (2, 'Group B', 150),
    (3, 'Group C', 200);
SET IDENTITY_INSERT MyGroup OFF;

-- Sample data for the ChargeStation table
SET IDENTITY_INSERT ChargeStation ON;
INSERT INTO ChargeStation (Identifier, Name, GroupId) VALUES
    (11, 'Station 1', 1),
    (12, 'Station 2', 2),
    (13, 'Station 3', 2),
    (14, 'Station 4', 3);
SET IDENTITY_INSERT ChargeStation OFF;

-- Sample data for the Connector table
INSERT INTO Connector (ChargeStationId, Identifier, MaxCurrent) VALUES
    (11, 1, 50),
    (11, 2, 30),
	(11, 3, 0),
	(11, 4, 0),
	(11, 5, 0),
    (12, 1, 20),
	(12, 2, 20),
	(12, 3, 0),
	(12, 4, 0),
	(12, 5, 0),
    (13, 1, 25),
    (13, 2, 15),
	(13, 3, 0),
	(13, 4, 0),
	(13, 5, 0),
    (14, 1, 50),
    (14, 2, 50),
    (14, 3, 50),
	(14, 4, 0),
	(14, 5, 0);


select * from MyGroup
select * from ChargeStation
select * from Connector

/*
Delete from MyGroup
Delete from ChargeStation
Delete from Connector

DROP TABLE Connector;
DROP TABLE ChargeStation;
DROP TABLE MyGroup;
*/