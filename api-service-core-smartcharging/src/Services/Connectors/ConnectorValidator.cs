using Api.Services.Core.SmartCharging.Dal.ConnectorDal;
using FluentValidation;

namespace Api.Services.Core.SmartCharging.Services.Connectors
{
    public class ConnectorValidator : AbstractValidator<Connector>
    {
        public ConnectorValidator()
        {
            RuleFor(g => g.Identifier).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).WithMessage("MaxCurrent should be greater than zero and less then six.");
            RuleFor(g => g.ChargeStationId).NotEmpty().WithMessage("ChargeStationId is required.");
            RuleFor(g => g.MaxCurrent).GreaterThanOrEqualTo(1).WithMessage("MaxCurrent should be greater than zero.");
        }
    }
}
