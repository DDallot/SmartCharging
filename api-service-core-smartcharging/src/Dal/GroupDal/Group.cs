using Api.Services.Core.SmartCharging.Dal.Common;

namespace Api.Services.Core.SmartCharging.Dal.GroupDal
{
    public class Group : EntityBase
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
