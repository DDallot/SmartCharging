namespace Api.Services.Core.SmartCharging.Dal.Common
{
    public interface IDalSession : IDisposable
    {
        UnitOfWork UnitOfWork { get; }
    }
}
