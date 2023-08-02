CREATE PROCEDURE [dbo].[sp_Connector_soft_delete]
	@Identifier INT,
	@ChargeStationId INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Connector 
	SET MaxCurrent = 0
	OUTPUT inserted.Identifier
	WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId	
END