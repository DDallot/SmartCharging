using Api.Services.Core.SmartCharging.Dal.Common;

namespace Api.Services.Core.SmartCharging.Dal.ChargeStationDal
{
    public class ChargeStation : EntityBase
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
}
