CREATE PROCEDURE [dbo].[sp_Connector_insert]
	@Identifier INT,
	@ChargeStationId INT,
	@MaxCurrent INT
AS
BEGIN
	
	IF EXISTS(SELECT * FROM dbo.Connector WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId)
	BEGIN
		UPDATE dbo.Connector 
		SET MaxCurrent = @MaxCurrent
		OUTPUT inserted.Identifier
		WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId	
	END
	ELSE
	BEGIN
		INSERT INTO dbo.Connector( Identifier, ChargeStationId, MaxCurrent) VALUES(@Identifier, @ChargeStationId, @MaxCurrent);	
	END
END