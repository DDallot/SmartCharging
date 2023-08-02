using Api.Contracts.Core.SmartCharging.ChargeStations.v1;
using Api.Contracts.Core.SmartCharging.Connectors.v1;
using Api.Contracts.Core.SmartCharging.Groups.v1;
using Api.Services.Core.SmartCharging.Dal.ChargeStationDal;
using Api.Services.Core.SmartCharging.Dal.Common;
using Api.Services.Core.SmartCharging.Dal.ConnectorDal;
using Api.Services.Core.SmartCharging.Dal.GroupDal;
using Api.Services.Core.SmartCharging.Services.ChargeStations;
using Api.Services.Core.SmartCharging.Services.Connectors;
using Api.Services.Core.SmartCharging.Services.Groups;
using FluentValidation;

namespace Api.Services.Core.SmartCharging.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceProvider Initialize(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Services
            serviceCollection.AddScoped<IGroup, GroupService>();
            serviceCollection.AddScoped<IChargeStation, ChargeStationService>();
            serviceCollection.AddScoped<IConnector, ConnectorService>();

            // Validatores
            serviceCollection.AddScoped<IValidator<Group>, GroupValidator>();
            serviceCollection.AddScoped<IValidator<ChargeStation>, ChargeStationValidator>();
            serviceCollection.AddScoped<IValidator<Connector>, ConnectorValidator>();

            // Dal
            serviceCollection.AddScoped<IDalSession, DalSession>();
            serviceCollection.AddScoped<IGroupRepository, GroupRepository>();
            serviceCollection.AddScoped<IChargeStationRepository, ChargeStationRepository>();
            serviceCollection.AddScoped<IConnectorRepository, ConnectorRepository>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
