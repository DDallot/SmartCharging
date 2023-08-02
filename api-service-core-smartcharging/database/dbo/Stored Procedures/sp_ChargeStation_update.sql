-- DROP PROCEDURE dbo.sp_ChargeStation_update;
CREATE PROCEDURE [dbo].[sp_ChargeStation_update]
	@Identifier INT,
	@Name TEXT,
	@GroupId INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.ChargeStation 
	SET Name = @Name,  GroupId= @GroupId
	OUTPUT inserted.Identifier
	WHERE Identifier = @Identifier	
END