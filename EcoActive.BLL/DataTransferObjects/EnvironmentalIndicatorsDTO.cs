namespace EcoActive.BLL.DataTransferObjects
{
    public class EnvironmentalIndicatorsDTO
    {
        public string Id { get; set; } = null!;
        public string FactoryId { get; set; } = null!;
        public string AirTemperature { get; set; } = null!;
        public string RelativeAirHumidity { get; set; } = null!;
        public string AirSpeed { get; set; } = null!;
        public string IntensityOfThermalRadiation { get; set; } = null!;
        public DateTime Time { get; set; }
    }
}
