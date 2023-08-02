# SmartCharging
**Version: 14** 

An API that exposes a simplified smart charging

# Dependencies
* Api.Contracts.Core.SmartCharging
* Api.Services.Core.SmartCharging
	* Dapper (2.0.143)
	* FluentValidation.AspNetCore (11.3.0)
    * Microsoft.AspNetCore.Mvc.Versioning (5.0.0)
    * Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer (5.0.0)
    * Swashbuckle.AspNetCore(6.2.3)
	* System.Data.SqlClient (4.8.5)
* Api.Services.Core.SmartCharging.UnitTests
    * FakeItEasy (7.4.0)
	* FluentAssertions (6.11.0)
    * Microsoft.NET.Test.Sdk (16.11.0)
	* coverlet.collector (3.1.0)
    * xunit (2.5.0)
    * xunit.runner.visualstudio (2.5.0)
* Api.Services.Core.SmartCharging.Database

# Domain model

**Group** – has a unique Identifier (cannot be changed), Name (can be changed), Capacity in Amps (integer, value greater than zero, can be changed). A Group can contain multiple charge stations.

**Charge station**  – has a unique Identifier (cannot be changed), Name (can be changed), and Connectors (at least one, but not more than 5).

**Connector** – has integer Identifier unique within the context of a charge station with (possible range of values from 1 to 5), Max current in Amps (integer, value greater than zero, can be changed).

# Functional requirements

1. Group/Charge Station/Connector can be created, updated, and removed.
2. If a Group is removed, all Charge Stations in the Group are removed as well.
3. Only one Charge Station can be added/removed to a Group in one call.
4. The Charge Station can be only in one Group at the same time.
The Charge Station cannot exist in the domain without Group.
5. A Connector cannot exist in the domain without a Charge Station.
6. The Max current in Amps of an existing Connector can be changed (updated).
7. The Capacity in Amps of a Group should always be greater than or equal to the sum of the Max current in Amps of all Connectors indirectly belonging to the Group.
8. All operations/requests not meeting the above requirement should be rejected.