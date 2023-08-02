namespace Api.Contracts.Core.SmartCharging.Connectors.v1
{
    public class CreateConnectorRequest
    {
        public int Identifier { get; set; }
        public int ChargeStationId { get; set; }
        public int MaxCurrent { get; set; }
    }
}
