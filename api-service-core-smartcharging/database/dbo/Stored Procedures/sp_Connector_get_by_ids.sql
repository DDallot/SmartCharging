-- DROP PROCEDURE dbo.sp_Connector_get_by_ids
CREATE PROCEDURE [dbo].[sp_Connector_get_by_ids]
	@Identifier INT,
	@ChargeStationId INT
AS
BEGIN
	SELECT Identifier, ChargeStationId, MaxCurrent 
	FROM dbo.Connector
	WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId
END