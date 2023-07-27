namespace Api.Contracts.Core.SmartCharging.Common
{
    public class ItemResult<T> : NoResult
    {
        public T Item { get; set; }
    }
}
