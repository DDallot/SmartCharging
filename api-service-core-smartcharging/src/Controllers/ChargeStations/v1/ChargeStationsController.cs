using Api.Contracts.Core.SmartCharging.ChargeStations.v1;
using Api.Contracts.Core.SmartCharging.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services.Core.SmartCharging.Controllers.ChargeStations.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ChargeStationsController : ControllerBase
    {
        private readonly IChargeStation _chargeStationService;

        public ChargeStationsController(IChargeStation chargeStationService)
        {
            _chargeStationService = chargeStationService ?? throw new ArgumentNullException(nameof(chargeStationService));
        }

        [HttpPost()]
        public async Task<ItemResult<int>> CreateAsync(CreateChargeStationRequest value)
        {
            return await _chargeStationService.CreateAsync(value);
        }

        [HttpPut("{identifier}")]
        public async Task<ItemResult<bool>> UpdateAsync(int identifier, UpdateChargeStationRequest value)
        {
            return await _chargeStationService.UpdateAsync(identifier, value);
        }

        [HttpDelete()]
        public async Task<ItemResult<bool>> DeleteAsync(int id)
        {
            return await _chargeStationService.DeleteAsync(id);
        }
    }
}
