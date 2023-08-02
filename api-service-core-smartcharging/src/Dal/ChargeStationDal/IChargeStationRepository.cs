using Api.Services.Core.SmartCharging.Dal.Common;

namespace Api.Services.Core.SmartCharging.Dal.ChargeStationDal
{
    public interface IChargeStationRepository : IRepository<ChargeStation>
    {
        Task<ChargeStation> GetAsync(int identifier);
        Task<int> GetMaxCurrentSumAsync(int identifier);
    }
}
