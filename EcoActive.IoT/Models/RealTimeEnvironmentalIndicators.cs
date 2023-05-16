namespace EcoActive.IoT.Models
{
    public class RealTimeEnvironmentalIndicators
    {
        public string FactoryId { get; set; } = null!;
        public string AirTemperature { get; set; } = null!;
        public string RelativeAirHumidity { get; set; } = null!;
        public string AirSpeed { get; set; } = null!;
        public string IntensityOfThermalRadiation { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}
