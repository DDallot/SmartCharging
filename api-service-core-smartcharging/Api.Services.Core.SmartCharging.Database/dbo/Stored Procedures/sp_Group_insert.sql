-- DROP PROCEDURE dbo.sp_Group_insert; 
CREATE PROCEDURE [dbo].[sp_Group_insert]
	@Name TEXT,
	@Capacity INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.MyGroup(Name, Capacity) VALUES(@Name, @Capacity);

	SELECT SCOPE_IDENTITY();
END