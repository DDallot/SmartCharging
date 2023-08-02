using Api.Contracts.Core.SmartCharging.Common;
using Api.Contracts.Core.SmartCharging.Groups.v1;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services.Core.SmartCharging.Controllers.SmartChargings.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroup _groupService;

        public GroupsController(IGroup groupService)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        [HttpGet()]
        public async Task<ItemResult<GroupResponse>> GetAsync(int identifier)
        {
            return await _groupService.GetAsync(identifier);
        }

        [HttpPost()]
        public async Task<ItemResult<int>> CreateAsync(CreateGroupRequest value)
        {
            return await _groupService.CreateAsync(value);
        }

        [HttpPut("{identifier}")]
        public async Task<ItemResult<bool>> UpdateAsync(int identifier, UpdateGroupRequest value)
        {
            return await _groupService.UpdateAsync(identifier, value);
        }

        [HttpDelete()]
        public async Task<ItemResult<bool>> DeleteAsync(int identifier)
        {
            return await _groupService.DeleteAsync(identifier);
        }
    }
}