using Newtonsoft.Json;

namespace FocamapMaui.Models.Map
{
    public class GeocodeGeometry
	{        
        [JsonProperty("location")]
        public GeocodeLocation Location { get; set; }        
    }
}