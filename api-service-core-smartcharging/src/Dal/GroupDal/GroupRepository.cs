using Api.Services.Core.SmartCharging.Dal.Common;
using Dapper;
using System.Data;

namespace Api.Services.Core.SmartCharging.Dal.GroupDal
{
    public class GroupRepository : IGroupRepository
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public async Task<int> InsertAsync(Group value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>("sp_Group_insert",
                new { value.Name, value.Capacity },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<bool> DeleteAsync(Group value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<bool>("sp_Group_delete",
                new { value.Identifier },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<bool> UpdateAsync(Group value)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<bool>("sp_Group_update",
                new { value.Identifier, value.Name, value.Capacity },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }
        public async Task<Group> GetAsync(int identifier)
        {
            return await UnitOfWork.Connection.QueryFirstOrDefaultAsync<Group>("sp_Group_get_by_id",
                new { Identifier = identifier },
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }

        public async Task<int> GetMaxCurrentSumAsync(int identifier)
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>("sp_Group_get_max_current_sum",
                new { Identifier = identifier},
                commandType: CommandType.StoredProcedure,
                transaction: UnitOfWork.Transaction);
        }
    }
}
