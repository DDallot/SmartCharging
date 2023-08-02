using Api.Services.Core.SmartCharging.Dal.GroupDal;
using FluentValidation;

namespace Api.Services.Core.SmartCharging.Services.Groups
{
    public class GroupValidator : AbstractValidator<Group> 
    {
        public GroupValidator()
        {
            RuleFor(g => g.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(g => g.Capacity).GreaterThanOrEqualTo(1).WithMessage("Capacity should be greater than zero.");
        }
    }   
}
