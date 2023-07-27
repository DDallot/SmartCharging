using Api.Contracts.Core.SmartCharging.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services.Core.SmartCharging.Controllers.SmartCharging.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SmartChargingController : ControllerBase
    {
        private readonly ILogger<SmartChargingController> _logger;

        public SmartChargingController(ILogger<SmartChargingController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public Task<ItemResult<List<int>>> GetAsync()
        {
            return Task.FromResult(new ItemResult<List<int>>
            {
                Item = Enumerable.Range(1, 5).ToList()
            });
        }
    }
}