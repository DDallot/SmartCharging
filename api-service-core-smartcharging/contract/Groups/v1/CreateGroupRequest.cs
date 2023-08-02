namespace Api.Contracts.Core.SmartCharging.Groups.v1
{
    public class CreateGroupRequest
    {
        public string Name { get; set; }
        public int Capacity { get; set;  }
    }
}
