Use SmartCharging

GO
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

GO
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

GO
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

GO
-- DROP PROCEDURE dbo.sp_Group_get_by_id
CREATE PROCEDURE [dbo].[sp_Group_get_by_id]
	@Identifier INT
AS
BEGIN
	SELECT Identifier, Name, Capacity
	FROM MyGroup
	WHERE Identifier = @Identifier
END

GO
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

GO
-- DROP PROCEDURE dbo.sp_ChargeStation_get_by_id
CREATE PROCEDURE [dbo].[sp_ChargeStation_get_by_id]
	@Identifier INT
AS
BEGIN
	SELECT Identifier, Name, GroupId 
	FROM ChargeStation
	WHERE Identifier = @Identifier
END

GO
-- DROP PROCEDURE dbo.sp_ChargeStation_get_max_current_sum;
CREATE PROCEDURE [dbo].[sp_ChargeStation_get_max_current_sum]
	@Identifier INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT SUM(MaxCurrent)
	FROM Connector c
	JOIN ChargeStation cs ON c.ChargeStationId = cs.Identifier
	WHERE cs.Identifier = @Identifier;	
END

GO
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

GO
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

GO
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

GO
-- DROP PROCEDURE dbo.sp_Connector_insert; 
CREATE PROCEDURE [dbo].[sp_Connector_insert]
	@Identifier INT,
	@ChargeStationId INT,
	@MaxCurrent INT
AS
BEGIN
	
	IF EXISTS(SELECT * FROM dbo.Connector WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId)
	BEGIN
		UPDATE dbo.Connector 
		SET MaxCurrent = @MaxCurrent
		OUTPUT inserted.Identifier
		WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId	
	END
	ELSE
	BEGIN
		INSERT INTO dbo.Connector( Identifier, ChargeStationId, MaxCurrent) VALUES(@Identifier, @ChargeStationId, @MaxCurrent);	
	END
END

GO
-- DROP PROCEDURE dbo.sp_Connector_soft_delete;
CREATE PROCEDURE [dbo].[sp_Connector_soft_delete]
	@Identifier INT,
	@ChargeStationId INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Connector 
	SET MaxCurrent = 0
	OUTPUT inserted.Identifier
	WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId	
END

-- DROP PROCEDURE dbo.sp_Connector_update;
CREATE PROCEDURE [dbo].[sp_Connector_update]
	@Identifier INT,
	@ChargeStationId INT,
	@MaxCurrent INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Connector 
	SET MaxCurrent = @MaxCurrent
	OUTPUT inserted.Identifier
	WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId	
END


GO
-- DROP PROCEDURE dbo.sp_Connector_get_by_ids
CREATE PROCEDURE [dbo].[sp_Connector_get_by_ids]
	@Identifier INT,
	@ChargeStationId INT
AS
BEGIN
	SELECT Identifier, ChargeStationId, MaxCurrent 
	FROM dbo.Connector
	WHERE Identifier = @Identifier AND ChargeStationId = @ChargeStationId
END

GO
-- DROP PROCEDURE dbo.sp_Connector_get_full_hierarchy_max_current_sum;
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


/*
DROP PROCEDURE dbo.sp_Group_insert; 
DROP PROCEDURE dbo.sp_Group_update; 
DROP PROCEDURE dbo.sp_Group_delete;
DROP PROCEDURE dbo.sp_Group_get_by_id
DROP PROCEDURE dbo.sp_Group_get_max_current_sum;
DROP PROCEDURE dbo.sp_ChargeStation_get_by_id
DROP PROCEDURE dbo.sp_ChargeStation_get_max_current_sum;
DROP PROCEDURE dbo.sp_ChargeStation_update;
DROP PROCEDURE dbo.sp_ChargeStation_delete;
DROP PROCEDURE dbo.sp_ChargeStation_insert; 
DROP PROCEDURE dbo.sp_Connector_insert; 
DROP PROCEDURE dbo.sp_Connector_soft_delete;
DROP PROCEDURE dbo.sp_Connector_update;
DROP PROCEDURE dbo.sp_Connector_get_by_ids
DROP PROCEDURE dbo.sp_Connector_get_full_hierarchy_max_current_sum;
*/