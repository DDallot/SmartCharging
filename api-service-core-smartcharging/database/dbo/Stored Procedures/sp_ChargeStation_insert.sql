-- DROP PROCEDURE dbo.sp_ChargeStation_insert; 
CREATE PROCEDURE [dbo].[sp_ChargeStation_insert]
	@Name TEXT,
	@GroupId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.ChargeStation(Name, GroupId) VALUES(@Name, @GroupId);

	SELECT SCOPE_IDENTITY();
END