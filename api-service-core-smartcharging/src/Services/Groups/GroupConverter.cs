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
                Name = value?.Name ?? string.Empty,
                Capacity = value?.Capacity ?? 0
            };
        }
        public static Group Convert(this UpdateGroupRequest value, int identifier)
        {
            return new Group
            {
                Identifier = identifier,
                Name = value?.Name ?? string.Empty,
                Capacity = value?.Capacity ?? 0
            };
        }

        public static GroupResponse Convert(this Group value)
        {
            return new GroupResponse
            {
                Identifier = value?.Identifier ?? 0,
                Name = value?.Name ?? string.Empty,
                Capacity = value?.Capacity ?? 0
            };
        }
    }
}
