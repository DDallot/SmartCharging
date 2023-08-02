using Api.Contracts.Core.SmartCharging.Groups.v1;
using Api.Services.Core.SmartCharging.Dal.GroupDal;

namespace Api.Services.Core.SmartCharging.Services.Groups
{
    public static class GroupConverter
    {
        public static Group Convert(this CreateGroupRequest value)
        {
            return new Group
            {
                Name = value.Name,
                Capacity = value.Capacity,
            };
        }
        public static Group Convert(this UpdateGroupRequest value, int identifier)
        {
            return new Group
            {
                Identifier = identifier,
                Name = value.Name,
                Capacity = value.Capacity,
            };
        }
    }
}
