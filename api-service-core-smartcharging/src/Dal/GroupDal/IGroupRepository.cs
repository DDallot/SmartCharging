using Api.Services.Core.SmartCharging.Dal.Common;

namespace Api.Services.Core.SmartCharging.Dal.GroupDal
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group> GetAsync(int identifier);
        Task<int> GetMaxCurrentSumAsync(int identifier);
    }
}
