-- DROP PROCEDURE dbo.sp_Group_delete;
CREATE PROCEDURE [dbo].[sp_Group_delete]
	@Identifier INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.MyGroup 
	OUTPUT deleted.Identifier
	WHERE Identifier = @Identifier	
END