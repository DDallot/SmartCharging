CREATE PROCEDURE [dbo].[sp_Connector_get_full_hierarchy_max_current_sum]
	@Identifier INT,
	@ChargeStationId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @upper_group_Id INT;
	DECLARE @upper_group_capacity INT;

	SELECT @upper_group_Id = g.Identifier, @upper_group_capacity = g.Capacity 
	FROM Connector c
	JOIN ChargeStation cs ON c.ChargeStationId = cs.Identifier
	JOIN MyGroup g on cs.GroupId = g.Identifier
	WHERE c.Identifier = @Identifier AND c.ChargeStationId = @ChargeStationId

	SELECT 
		SUM(MaxCurrent) as MaxCurrent, 
		@upper_group_capacity as Capacity, 
		@upper_group_Id as GroupId,  
		@Identifier as Identifier, 
		@ChargeStationId as ChargeStationId 
	FROM Connector c
	JOIN ChargeStation cs ON c.ChargeStationId = cs.Identifier
	JOIN MyGroup g on cs.GroupId = g.Identifier
	WHERE cs.GroupId = @upper_group_Id;
END