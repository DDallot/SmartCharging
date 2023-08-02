-- DROP PROCEDURE dbo.sp_ChargeStation_delete;
CREATE PROCEDURE [dbo].[sp_ChargeStation_delete]
	@Identifier INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.ChargeStation 
	OUTPUT deleted.Identifier
	WHERE Identifier = @Identifier	
END