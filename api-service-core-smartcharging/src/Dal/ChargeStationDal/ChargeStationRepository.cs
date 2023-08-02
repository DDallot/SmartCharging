using Api.Services.Core.SmartCharging.Dal.Common;
using Dapper;
using System.Data;

namespace Api.Services.Core.SmartCharging.Dal.ChargeStationDal
{
    public class ChargeStationRepository : IChargeStationRepository
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public async Task<int> InsertAsync(ChargeStation value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>("sp_ChargeStation_insert",
                new { value.GroupId, value.Name},
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<bool> DeleteAsync(ChargeStation value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<bool>("sp_ChargeStation_delete",
                new { value.Identifier },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<bool> UpdateAsync(ChargeStation value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<bool>("sp_ChargeStation_update",
                new { value.Identifier, value.GroupId, value.Name },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<ChargeStation> GetAsync(int identifier)
        {
            return await UnitOfWork.Connection.QueryFirstOrDefaultAsync<ChargeStation>("sp_ChargeStation_get_by_id",
                new { Identifier = identifier },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<int> GetMaxCurrentSumAsync(int identifier)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>("sp_ChargeStation_get_max_current_sum",
                new { Identifier = identifier },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }
    }
}
