using Microsoft.AspNetCore.Routing.Constraints;

namespace DynamicSun_weather.Models
{
    public class EntryModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public float? Temp { get; set; }
        public float? Humidity { get; set; }
        public float? Dew { get; set; }
        public int? Pressure { get; set; }
        public string? WindDirection { get; set; }
        public int? WindSpeed { get; set; }
        public int? Cloudiness { get; set; }
        public int? CloudinessLowerBound { get; set; }
        public int? Visibility { get; set; }
        public string? Conditions { get; set; }
    }
}
