using Newtonsoft.Json;

namespace FocamapMaui.Models.Map
{
    public class GeocodeResponse
    {
        [JsonProperty("results")]
        public List<GeocodeResult> Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }   
}