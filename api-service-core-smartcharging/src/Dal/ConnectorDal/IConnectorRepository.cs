using Api.Services.Core.SmartCharging.Dal.Common;

namespace Api.Services.Core.SmartCharging.Dal.ConnectorDal
{
    public interface IConnectorRepository : IRepository<Connector>
    {
        Task<Connector> GetAsync(int identifier, int chargeStationId);
        Task<ConnectorUpperGroup> GetFullHierarchyMaxCurrentSumAsync(int identifier, int chargeStationId);
    }
}
