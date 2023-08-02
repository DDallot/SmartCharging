using Api.Contracts.Core.SmartCharging.ChargeStations.v1;
using Api.Contracts.Core.SmartCharging.Common;
using Api.Services.Core.SmartCharging.Dal.ChargeStationDal;
using Api.Services.Core.SmartCharging.Dal.Common;
using Api.Services.Core.SmartCharging.Dal.ConnectorDal;
using Api.Services.Core.SmartCharging.Dal.GroupDal;
using Api.Services.Core.SmartCharging.Services.Common;
using FluentValidation;

namespace Api.Services.Core.SmartCharging.Services.ChargeStations
{
    public class ChargeStationService : IChargeStation
    {
        private readonly IChargeStationRepository _chargeStatioDal;
        private readonly IConnectorRepository _connectorDal;
        private readonly IGroupRepository _groupDal;
        private readonly IDalSession _dalSession;
        private readonly ILogger<ChargeStationService> _logger;
        private readonly IValidator<ChargeStation> _chargeStationValidator;

        public ChargeStationService(
            IChargeStationRepository chargeStatioDal,
            IConnectorRepository connectorDal,
            IGroupRepository groupDal, 
            IDalSession dalSession,
            IValidator<ChargeStation> chargeStationValidator,
            ILogger<ChargeStationService> logger)
        {
            _dalSession = dalSession ?? throw new ArgumentNullException(nameof(dalSession));
            _chargeStatioDal = chargeStatioDal ?? throw new ArgumentNullException(nameof(chargeStatioDal));
            _chargeStatioDal.UnitOfWork = _dalSession.UnitOfWork;
            _connectorDal = connectorDal ?? throw new ArgumentNullException(nameof(connectorDal));
            _connectorDal.UnitOfWork = _dalSession.UnitOfWork;
            _groupDal = groupDal ?? throw new ArgumentNullException(nameof(groupDal));
            _groupDal.UnitOfWork = _dalSession.UnitOfWork;
            _chargeStationValidator = chargeStationValidator ?? throw new ArgumentNullException(nameof(chargeStationValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ItemResult<int>> CreateAsync(CreateChargeStationRequest value)
        {
            try
            {
                var chargeStation = value.Convert();

                var errors = _chargeStationValidator.Validate(chargeStation).GetErrors();
                if (errors.Any())
                {
                    return new ItemResult<int>
                    {
                        HasError = true,
                        Errors = errors
                    };
                }

                _dalSession.UnitOfWork.Begin();

                var group = await _groupDal.GetAsync(chargeStation.GroupId);
                if (group == null)
                {
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<int>
                    {
                        HasError = true,
                        Errors = new List<string> { $"The group {chargeStation.GroupId} doesn't exist." }
                    };
                }

                var inserted = await _chargeStatioDal.InsertAsync(chargeStation);
                await _connectorDal.InsertAsync(new Connector { ChargeStationId = inserted, Identifier = 1, MaxCurrent = 0});
                await _connectorDal.InsertAsync(new Connector { ChargeStationId = inserted, Identifier = 2, MaxCurrent = 0 });
                await _connectorDal.InsertAsync(new Connector { ChargeStationId = inserted, Identifier = 3, MaxCurrent = 0 });
                await _connectorDal.InsertAsync(new Connector { ChargeStationId = inserted, Identifier = 4, MaxCurrent = 0 });
                await _connectorDal.InsertAsync(new Connector { ChargeStationId = inserted, Identifier = 5, MaxCurrent = 0 });
                
                _dalSession.UnitOfWork.Commit();

                return new ItemResult<int> { Item = inserted };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while creating a ChargeStation");
                _dalSession.UnitOfWork.Rollback();
                return new ItemResult<int>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while creating a ChargeStation" }
                };
            }
        }

        public async Task<ItemResult<bool>> DeleteAsync(int identifier)
        {
            try
            {
                var result = await _chargeStatioDal.DeleteAsync(new ChargeStation { Identifier = identifier });
                return new ItemResult<bool> { Item = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while deleting a ChargeStation");
                return new ItemResult<bool>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while deleting a ChargeStation" }
                };
            }
        }

        public async Task<ItemResult<bool>> UpdateAsync(int identifier, UpdateChargeStationRequest value)
        {
            try
            {
                var chargeStation = value.Convert(identifier);

                var errors = _chargeStationValidator.Validate(chargeStation).GetErrors();
                if (errors.Any())
                {
                    return new ItemResult<bool>
                    {
                        HasError = true,
                        Errors = errors
                    };
                }

                _dalSession.UnitOfWork.Begin();

                var oldChargeStation = await _chargeStatioDal.GetAsync(identifier);
                if(oldChargeStation == null)
                {
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<bool>
                    {
                        HasError = true,
                        Errors = new List<string> { $"The identifier {identifier} doesn't exist." }
                    };
                }
                
                if (oldChargeStation.GroupId == chargeStation.GroupId)
                {
                    var result = await _chargeStatioDal.UpdateAsync(chargeStation);
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<bool> { Item = result };
                }
                else
                {
                    var group = await _groupDal.GetAsync(chargeStation.GroupId);
                    if (group == null)
                    {
                        _dalSession.UnitOfWork.Commit();
                        return new ItemResult<bool>
                        {
                            HasError = true,
                            Errors = new List<string> { $"The group {chargeStation.GroupId} doesn't exist." }
                        };
                    }

                    var groupMaxCurrentSum = await _groupDal.GetMaxCurrentSumAsync(chargeStation.GroupId);
                    var chargeStationMaxCurrentSum = await _chargeStatioDal.GetMaxCurrentSumAsync(chargeStation.Identifier);
                    if(groupMaxCurrentSum + chargeStationMaxCurrentSum <= group.Capacity)
                    {
                        var result = await _chargeStatioDal.UpdateAsync(chargeStation);
                        _dalSession.UnitOfWork.Commit();
                        return new ItemResult<bool> { Item = result };
                    }
                    else
                    {
                        _dalSession.UnitOfWork.Commit();
                        return new ItemResult<bool>
                        {
                            HasError = true,
                            Errors = new List<string> { $"The Capacity is {group.Capacity} and the new max current would be {groupMaxCurrentSum + chargeStationMaxCurrentSum}." }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while updating a ChargeStation");
                _dalSession.UnitOfWork.Rollback();
                return new ItemResult<bool>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while updating a ChargeStation" }
                };
            }
        }
    }
}