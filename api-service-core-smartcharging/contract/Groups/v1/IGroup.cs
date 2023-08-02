using Api.Contracts.Core.SmartCharging.Common;

namespace Api.Contracts.Core.SmartCharging.Groups.v1
{
    public interface IGroup
    {
        Task<ItemResult<int>> CreateAsync(CreateGroupRequest value);
        Task<ItemResult<bool>> UpdateAsync(int identifier, UpdateGroupRequest value);
        Task<ItemResult<bool>> DeleteAsync(int identifier);
    }
}
