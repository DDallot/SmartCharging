using Api.Contracts.Core.SmartCharging.ChargeStations.v1;
using Api.Services.Core.SmartCharging.Dal.ChargeStationDal;

namespace Api.Services.Core.SmartCharging.Services.ChargeStations
{
    public static class ChargeStationConverter
    {
        public static ChargeStation Convert(this CreateChargeStationRequest value)
        {
            return new ChargeStation
            {
                Name = value?.Name ?? string.Empty,
                GroupId = value?.GroupId ?? 0
            };
        }

        public static ChargeStation Convert(this UpdateChargeStationRequest value, int identifier)
        {
            return new ChargeStation
            {
                Identifier = identifier,
                Name = value?.Name ?? string.Empty,
                GroupId = value?.GroupId ?? 0
            };
        }
    }
}
