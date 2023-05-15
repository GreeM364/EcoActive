namespace EcoActive.DAL.Entities
{
    public class EnvironmentalIndicators : BaseModel
    {
        public string AirTemperature { get; set; } = null!;
        public string RelativeAirHumidity { get; set; } = null!;
        public string AirSpeed { get; set; } = null!;
        public string IntensityOfThermalRadiation { get; set; } = null!;

        public string FactoryId { get; set; } = null!;
        public Factory Factory { get; set; } = null!;
    }
}
