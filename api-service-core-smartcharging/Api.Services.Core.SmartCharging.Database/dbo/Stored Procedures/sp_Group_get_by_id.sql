-- DROP PROCEDURE dbo.sp_Group_get_by_id
CREATE PROCEDURE [dbo].[sp_Group_get_by_id]
	@Identifier INT
AS
BEGIN
	SELECT Identifier, Name, Capacity
	FROM MyGroup
	WHERE Identifier = @Identifier
END