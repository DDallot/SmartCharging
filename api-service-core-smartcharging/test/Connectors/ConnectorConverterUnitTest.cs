using Api.Contracts.Core.SmartCharging.Connectors.v1;
using Api.Services.Core.SmartCharging.Dal.ConnectorDal;
using Api.Services.Core.SmartCharging.Services.Connectors;
using FluentAssertions;
using System.Text;
using Xunit;

namespace Api.Services.Core.SmartCharging.UnitTests.Connectors
{
    public class ConnectorConverterUnitTest
    {
        [Fact]
        public void Convert_CreateConnectorRequest_To_Connector()
        {
            var value = new CreateConnectorRequest { Identifier = 1, ChargeStationId = 11, MaxCurrent = 200 };
            var data = new Connector { Identifier = 1, ChargeStationId = 11, MaxCurrent = 200 };

            var result = value.Convert();

            result.Should().BeEquivalentTo(data);
        }

        [Fact]
        public void Convert_UpdateConnectorRequest_To_Connector()
        {
            int identifier = 123;
            int chargeStationId = 123;
            var value = new UpdateConnectorRequest { MaxCurrent = 200 };
            var data = new Connector { Identifier = identifier, ChargeStationId = chargeStationId, MaxCurrent = 200 };

            var result = value.Convert(identifier, chargeStationId);

            result.Should().BeEquivalentTo(data);
        }
    }
}