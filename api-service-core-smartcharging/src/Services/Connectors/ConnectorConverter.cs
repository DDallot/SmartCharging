using Api.Contracts.Core.SmartCharging.Connectors.v1;
using Api.Services.Core.SmartCharging.Dal.ConnectorDal;

namespace Api.Services.Core.SmartCharging.Services.Connectors
{
    public static class ConnectorConverter
    {
        public static Connector Convert(this CreateConnectorRequest value)
        {
            return new Connector
            {
                Identifier = value?.Identifier ?? 0,
                ChargeStationId = value?.ChargeStationId ?? 0,
                MaxCurrent = value?.MaxCurrent ?? 0
            };
        }

        public static Connector Convert(this UpdateConnectorRequest value, int identifier, int chargeStationId)
        {
            return new Connector
            {
                Identifier = identifier,
                ChargeStationId = chargeStationId,
                MaxCurrent = value?.MaxCurrent ?? 0
            };
        }
    }
}
