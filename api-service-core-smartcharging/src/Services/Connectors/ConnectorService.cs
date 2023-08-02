using Api.Contracts.Core.SmartCharging.Common;
using Api.Contracts.Core.SmartCharging.Connectors.v1;
using Api.Services.Core.SmartCharging.Dal.ChargeStationDal;
using Api.Services.Core.SmartCharging.Dal.Common;
using Api.Services.Core.SmartCharging.Dal.ConnectorDal;
using Api.Services.Core.SmartCharging.Dal.GroupDal;
using Api.Services.Core.SmartCharging.Services.Common;
using FluentValidation;

namespace Api.Services.Core.SmartCharging.Services.Connectors
{
    public class ConnectorService : IConnector
    {
        private readonly IGroupRepository _groupDal;
        private readonly IChargeStationRepository _chargeStatioDal;
        private readonly IConnectorRepository _connectorDal;
        private readonly IDalSession _dalSession;
        private readonly ILogger<ConnectorService> _logger;
        private readonly IValidator<Connector> _connectorValidator;

        public ConnectorService(
            IGroupRepository groupDal,
            IChargeStationRepository chargeStatioDal,
            IConnectorRepository connectorDal, 
            IDalSession dalSession, 
            IValidator<Connector> connectorValidator, 
            ILogger<ConnectorService> logger) 
        {
            _dalSession = dalSession ?? throw new ArgumentNullException(nameof(dalSession));
            _groupDal = groupDal ?? throw new ArgumentNullException(nameof(groupDal));
            _groupDal.UnitOfWork = _dalSession.UnitOfWork;
            _chargeStatioDal = chargeStatioDal ?? throw new ArgumentNullException(nameof(chargeStatioDal));
            _chargeStatioDal.UnitOfWork = _dalSession.UnitOfWork;
            _connectorDal = connectorDal ?? throw new ArgumentNullException(nameof(connectorDal));
            _connectorDal.UnitOfWork = _dalSession.UnitOfWork;
            _connectorValidator = connectorValidator ?? throw new ArgumentNullException(nameof(connectorValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ItemResult<int>> CreateAsync(CreateConnectorRequest value)
        {
            try
            {
                var connector = value.Convert();

                var errors = _connectorValidator.Validate(connector).GetErrors();
                if (errors.Any())
                {
                    return new ItemResult<int>
                    {
                        HasError = true,
                        Errors = errors
                    };
                }
                _dalSession.UnitOfWork.Begin();

                var chargeStation = await _chargeStatioDal.GetAsync(connector.ChargeStationId);
                if (chargeStation == null)
                {
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<int>
                    {
                        HasError = true,
                        Errors = new List<string> { $"The chargeStationId {connector.ChargeStationId} doesn't exist." }
                    };
                }

                var oldConnector = await _connectorDal.GetAsync(connector.Identifier, connector.ChargeStationId);
                if(oldConnector != null && oldConnector.MaxCurrent != 0)
                {
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<int>
                    {
                        HasError = true,
                        Errors = new List<string> { $"The connector ({connector.Identifier},{connector.ChargeStationId}) already exists." }
                    };
                }

                var result = await _connectorDal.InsertAsync(connector);

                _dalSession.UnitOfWork.Commit();

                return new ItemResult<int> { Item = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while inserting a Connector");
                _dalSession.UnitOfWork.Rollback();
                return new ItemResult<int>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while inserting a Connector" }
                };
            }
        }

        public async Task<ItemResult<bool>> DeleteAsync(int identifier, int ChargeStationId)
        {
            try
            {
                var result = await _connectorDal.DeleteAsync(new Connector { Identifier = identifier });
                return new ItemResult<bool> { Item = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while deleting a Connector");
                return new ItemResult<bool>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while deleting a Connector" }
                };
            }
        }

        public async Task<ItemResult<bool>> UpdateAsync(int identifier, int ChargeStationId, UpdateConnectorRequest value)
        {
            try
            {
                var connector = value.Convert(identifier, ChargeStationId);

                var errors = _connectorValidator.Validate(connector).GetErrors();
                if (errors.Any())
                {
                    return new ItemResult<bool>
                    {
                        HasError = true,
                        Errors = errors
                    };
                }
                _dalSession.UnitOfWork.Begin();

                var oldConnector = await _connectorDal.GetAsync(connector.Identifier, connector.ChargeStationId);
                if (oldConnector == null || oldConnector.MaxCurrent == 0)
                {
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<bool>
                    {
                        HasError = true,
                        Errors = new List<string> { $"The connector ({connector.Identifier},{connector.ChargeStationId}) doesn't exist." }
                    };
                }

                if (oldConnector.MaxCurrent >= connector.MaxCurrent)
                {
                    var result = await _connectorDal.UpdateAsync(connector);
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<bool> { Item = result };
                }
                else
                {
                    var connectorupper = await _connectorDal.GetFullHierarchyMaxCurrentSumAsync(connector.Identifier, connector.ChargeStationId);
                    if (connectorupper.MaxCurrent + (connector.MaxCurrent - oldConnector.MaxCurrent) > connectorupper.Capacity)
                    {
                        _dalSession.UnitOfWork.Commit();
                        return new ItemResult<bool>
                        {
                            HasError = true,
                            Errors = new List<string> { $"This connector exceeds the capacity of the group." }
                        };
                    }
                    else
                    {
                        var result = await _connectorDal.UpdateAsync(connector);

                        _dalSession.UnitOfWork.Commit();

                        return new ItemResult<bool> { Item = result };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while updating a Connector");
                _dalSession.UnitOfWork.Rollback();
                return new ItemResult<bool>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while updating a Connector" }
                };
            }
        }
    }
}
