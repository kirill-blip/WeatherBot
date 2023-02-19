using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTest
{
    public class WeatherInfo
    {
        public float temp { get; set; }
        public float feels_Like { get; set; }
        public float temp_water { get; set; }
        public string? icon { get; set; }
        public string? condition { get; set; }
        public float wind_speed { get; set; }
    }
}
