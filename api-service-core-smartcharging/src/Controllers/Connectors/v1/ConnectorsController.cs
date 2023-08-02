using Api.Contracts.Core.SmartCharging.Common;
using Api.Contracts.Core.SmartCharging.Connectors.v1;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services.Core.SmartCharging.Controllers.Connectors.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConnectorsController : ControllerBase
    {
        private readonly IConnector _connectorService;

        public ConnectorsController(IConnector connectorService)
        {
            _connectorService = connectorService ?? throw new ArgumentNullException(nameof(connectorService));
        }

        [HttpPost()]
        public async Task<ItemResult<int>> CreateAsync(CreateConnectorRequest value)
        {
            return await _connectorService.CreateAsync(value);
        }

        [HttpPut("{identifier}")]
        public async Task<ItemResult<bool>> UpdateAsync(int identifier, int chargeStationId, UpdateConnectorRequest value)
        {
            return await _connectorService.UpdateAsync(identifier, chargeStationId, value);
        }

        [HttpDelete()]
        public async Task<ItemResult<bool>> DeleteAsync(int identifier, int chargeStationId)
        {
            return await _connectorService.DeleteAsync(identifier, chargeStationId);
        }
    }
}
