namespace Api.Contracts.Core.SmartCharging.Common
{
    public class NoResult
    {
        public bool HasError { get; set; }
        public List<string> Errors { get; set; }
    }
}
