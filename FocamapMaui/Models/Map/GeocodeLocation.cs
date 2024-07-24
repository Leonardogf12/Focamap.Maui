using Newtonsoft.Json;

namespace FocamapMaui.Models.Map
{
    public class GeocodeLocation
	{        
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }        
    }
}