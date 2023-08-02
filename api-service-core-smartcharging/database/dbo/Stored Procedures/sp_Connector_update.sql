-- DROP PROCEDURE dbo.sp_Connector_update;
CREATE PROCEDURE [dbo].[sp_Connector_update]
	@Identifier INT,
	@ChargeStationId INT,
	@MaxCurrent INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Connector 
	SET MaxCurrent = @MaxCurrent
	OUTPUT inserted.Identifier
	WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId	
END