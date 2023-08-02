using Api.Services.Core.SmartCharging.Dal.ChargeStationDal;
using FluentValidation;

namespace Api.Services.Core.SmartCharging.Services.ChargeStations
{
    public class ChargeStationValidator : AbstractValidator<ChargeStation>
    {
        public ChargeStationValidator()
        {
            RuleFor(cs => cs.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(cs => cs.GroupId).NotEmpty().WithMessage("The Charge Station cannot exist without Group.");
        }
    }
}
