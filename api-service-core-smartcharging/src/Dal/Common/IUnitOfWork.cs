using System.Data;

namespace Api.Services.Core.SmartCharging.Dal.Common
{
    public interface IUnitOfWork
    {
        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}
