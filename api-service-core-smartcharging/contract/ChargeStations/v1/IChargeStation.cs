using Api.Contracts.Core.SmartCharging.Common;

namespace Api.Contracts.Core.SmartCharging.ChargeStations.v1
{
    public interface IChargeStation
    {
        Task<ItemResult<int>> CreateAsync(CreateChargeStationRequest value);
        Task<ItemResult<bool>> UpdateAsync(int identifier, UpdateChargeStationRequest value);
        Task<ItemResult<bool>> DeleteAsync(int identifier);
    }
}
