using Api.Contracts.Core.SmartCharging.ChargeStations.v1;
using Api.Contracts.Core.SmartCharging.Connectors.v1;
using Api.Services.Core.SmartCharging.Dal.ChargeStationDal;
using Api.Services.Core.SmartCharging.Dal.ConnectorDal;
using Api.Services.Core.SmartCharging.Services.ChargeStations;
using FluentAssertions;
using System.Text;
using Xunit;

namespace Api.Services.Core.SmartCharging.UnitTests.ChargeStations
{
    public class ChargeStationConverterUnitTest
    {
        [Fact]
        public void Convert_CreateConnectorRequest_To_Connector()
        {
            var value = new CreateChargeStationRequest { GroupId = 999, Name = "Station X"};
            var data = new ChargeStation { GroupId = 999, Name = "Station X" };

            var result = value.Convert();

            result.Should().BeEquivalentTo(data);
        }

        [Fact]
        public void Convert_UpdateConnectorRequest_To_Connector()
        {
            int identifier = 123;
            var value = new UpdateChargeStationRequest { GroupId = 999, Name = "Station X" };
            var data = new ChargeStation { Identifier = identifier, GroupId = 999, Name = "Station X" };

            var result = value.Convert(identifier);

            result.Should().BeEquivalentTo(data);
        }
    }
}
