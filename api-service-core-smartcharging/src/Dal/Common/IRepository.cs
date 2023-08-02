namespace Api.Services.Core.SmartCharging.Dal.Common
{
    public interface IRepository<T> where T : EntityBase
    {
        IUnitOfWork UnitOfWork { get; set; }

        Task<int> InsertAsync(T value);
        Task<bool> UpdateAsync(T value);
        Task<bool> DeleteAsync(T value);
    }
}
