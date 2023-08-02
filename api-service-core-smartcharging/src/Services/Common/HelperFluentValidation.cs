using FluentValidation.Results;

namespace Api.Services.Core.SmartCharging.Services.Common
{
    public static class HelperFluentValidation
    {
        public static List<string> GetErrors(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                             .Select(failure => $"Property {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}")
                             .ToList();
            }
            return new List<string>();
        }
    }
}
