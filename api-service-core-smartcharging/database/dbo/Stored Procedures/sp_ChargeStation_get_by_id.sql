-- DROP PROCEDURE dbo.sp_ChargeStation_get_by_id
CREATE PROCEDURE [dbo].[sp_ChargeStation_get_by_id]
	@Identifier INT
AS
BEGIN
	SELECT Identifier, Name, GroupId 
	FROM ChargeStation
	WHERE Identifier = @Identifier
END