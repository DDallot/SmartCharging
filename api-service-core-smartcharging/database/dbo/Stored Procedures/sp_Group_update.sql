-- DROP PROCEDURE dbo.sp_Group_update; 
CREATE PROCEDURE [dbo].[sp_Group_update]
	@Identifier INT,
	@Name TEXT,
	@Capacity INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.MyGroup 
	SET Name = @Name, Capacity = @Capacity
	OUTPUT inserted.Identifier
	WHERE Identifier = @Identifier	
END