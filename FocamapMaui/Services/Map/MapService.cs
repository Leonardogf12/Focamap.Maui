using FocamapMaui.Controls;
using FocamapMaui.Models.Map;
using Newtonsoft.Json;

namespace FocamapMaui.Services.Map
{
    public class MapService : IMapService
    {
        public async Task<string> GetAddressFromLocationAsync(Location location)
        {
            try
            {
                string apiKey = ControlFiles.GetValueFromFilePropertyJson("googlecloud-config.json", StringConstants.GOOGLE_CLOUD, StringConstants.GEOCODING_API_KEY);

                string latitude = location.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string longitude = location.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string requestUri = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={apiKey}";

                using HttpClient client = new();

                var response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<GeocodeResponse>(json);

                    if (result.Status == "OK" && result.Results.Count != 0)
                    {
                        return result.Results.First().FormattedAddress;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }           
        }
    }
}