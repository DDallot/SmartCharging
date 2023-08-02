-- DROP PROCEDURE dbo.sp_Group_get_max_current_sum;
CREATE PROCEDURE [dbo].[sp_Group_get_max_current_sum]
	@Identifier INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT SUM(MaxCurrent)
	FROM Connector c
	JOIN ChargeStation cs ON c.ChargeStationId = cs.Identifier
	WHERE cs.GroupId = @Identifier;	
END