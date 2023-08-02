using Api.Services.Core.SmartCharging.Dal.Common;
using Dapper;
using System.Data;

namespace Api.Services.Core.SmartCharging.Dal.ConnectorDal
{
    public class ConnectorRepository : IConnectorRepository
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public async Task<int> InsertAsync(Connector value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>("sp_Connector_insert",
                new { value.Identifier, value.ChargeStationId, value.MaxCurrent },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<bool> DeleteAsync(Connector value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<bool>("sp_Connector_soft_delete",
               new { value.Identifier },
               commandType: CommandType.StoredProcedure,
               transaction: UnitOfWork.Transaction);
        }

        public async Task<bool> UpdateAsync(Connector value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<bool>("sp_Connector_update",
                new { value.Identifier, value.ChargeStationId, value.MaxCurrent },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<Connector> GetAsync(int identifier, int chargeStationId)
        {
            return await UnitOfWork.Connection.QueryFirstOrDefaultAsync<Connector>("sp_Connector_get_by_ids",
                new { Identifier = identifier, ChargeStationId = chargeStationId },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<ConnectorUpperGroup> GetFullHierarchyMaxCurrentSumAsync(int identifier, int chargeStationId)
        {
            return await UnitOfWork.Connection.QueryFirstOrDefaultAsync<ConnectorUpperGroup>("sp_Connector_get_full_hierarchy_max_current_sum",
                new { Identifier = identifier, ChargeStationId = chargeStationId },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }
    }
}
