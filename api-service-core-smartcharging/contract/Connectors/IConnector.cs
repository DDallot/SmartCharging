using Api.Contracts.Core.SmartCharging.Common;

namespace Api.Contracts.Core.SmartCharging.Connectors.v1
{
    public interface IConnector
    {
        Task<ItemResult<int>> CreateAsync(CreateConnectorRequest value);
        Task<ItemResult<bool>> UpdateAsync(int identifier, int ChargeStationId, UpdateConnectorRequest value);
        Task<ItemResult<bool>> DeleteAsync(int identifier, int ChargeStationId);
    }
}
