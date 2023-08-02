using Api.Contracts.Core.SmartCharging.Common;
using Api.Contracts.Core.SmartCharging.Groups.v1;
using Api.Services.Core.SmartCharging.Dal.Common;
using Api.Services.Core.SmartCharging.Dal.GroupDal;
using Api.Services.Core.SmartCharging.Services.Common;
using FluentValidation;

namespace Api.Services.Core.SmartCharging.Services.Groups
{
    public class GroupService : IGroup
    {
        private readonly IGroupRepository _groupDal;
        private readonly IDalSession _dalSession;
        private readonly ILogger<GroupService> _logger;
        private readonly IValidator<Group> _groupValidator;

        public GroupService(IGroupRepository groupDal, IDalSession dalSession, IValidator<Group> groupValidator, ILogger<GroupService> logger) 
        {
            _dalSession = dalSession ?? throw new ArgumentNullException(nameof(_dalSession));
            _groupDal = groupDal ?? throw new ArgumentNullException(nameof(groupDal));
            _groupDal.UnitOfWork = _dalSession.UnitOfWork;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _groupValidator = groupValidator ?? throw new ArgumentNullException(nameof(groupValidator));
        }

        public async Task<ItemResult<GroupResponse>> GetAsync(int identifier)
        {
            try
            {
                var result = (await _groupDal.GetAsync(identifier)).Convert();
                return new ItemResult<GroupResponse> { Item = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while deleting a Group");
                return new ItemResult<GroupResponse>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while deleting a Group" }
                };
            }
        }
        public async Task<ItemResult<int>> CreateAsync(CreateGroupRequest value)
        {
            try
            {
                var group = value.Convert();

                var errors = _groupValidator.Validate(group).GetErrors();
                if (errors.Any())
                {
                    return new ItemResult<int>
                    {
                        HasError = true,
                        Errors = errors
                    };
                }

                var inserted = await _groupDal.InsertAsync(group);

                return new ItemResult<int> { Item = inserted };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while creating a Group");
                return new ItemResult<int>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while creating a Group" }
                };
            }
        }

        public async Task<ItemResult<bool>> UpdateAsync(int identifier, UpdateGroupRequest value)
        {
            try
            {
                var group = value.Convert(identifier);

                var errors = _groupValidator.Validate(group).GetErrors();
                if (errors.Any())
                {
                    return new ItemResult<bool>
                    {
                        HasError = true,
                        Errors = errors
                    };
                }

                _dalSession.UnitOfWork.Begin();

                var maxCurrentSum = await _groupDal.GetMaxCurrentSumAsync(identifier);

                if(maxCurrentSum <= group.Capacity)
                {
                    var result = await _groupDal.UpdateAsync(group);
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<bool> { Item = result };
                }
                else
                {
                    _dalSession.UnitOfWork.Commit();
                    return new ItemResult<bool>
                    {
                        HasError = true,
                        Errors = new List<string> { $"The Capacity is {group.Capacity} and max current would be {maxCurrentSum}." }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while updating a Group");
                _dalSession.UnitOfWork.Rollback();
                return new ItemResult<bool>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while updating a Group" }
                };
            }
        }

        public async Task<ItemResult<bool>> DeleteAsync(int identifier)
        {
            try
            {
                var result = await _groupDal.DeleteAsync(new Group { Identifier = identifier });
                return new ItemResult<bool> { Item = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while deleting a Group");
                return new ItemResult<bool>
                {
                    HasError = true,
                    Errors = new List<string> { "An error ocurred while deleting a Group" }
                };
            }
        }
    }
}