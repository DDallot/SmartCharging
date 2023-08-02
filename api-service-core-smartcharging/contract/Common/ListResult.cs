namespace Api.Contracts.Core.SmartCharging.Common
{
    public class ListResult<T> : NoResult
    {
        public List<T> Items { get; set; }
    }
}