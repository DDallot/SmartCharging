using Api.Services.Core.SmartCharging.Dal.Common;

namespace Api.Services.Core.SmartCharging.Dal.ConnectorDal
{
    public class Connector : EntityBase
    {
        public int ChargeStationId { get; set; }
        public int MaxCurrent { get; set; }
    }
}