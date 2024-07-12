using Newtonsoft.Json;

namespace FocamapMaui.Models.Map
{
    public class GeocodeResult
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
    }
}