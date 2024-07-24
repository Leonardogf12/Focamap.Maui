using FocamapMaui.Controls;
using FocamapMaui.Models.Map;
using Newtonsoft.Json;

namespace FocamapMaui.Services.Map
{
    public class MapService : IMapService
    {
        public async Task<string> GetReverseGeocodingAsync(Location location)
        {
            try
            {
                string apiKey = ControlFiles.GetValueFromFilePropertyJson("googlecloud-config.json", StringConstants.GOOGLE_CLOUD, StringConstants.REVERSE_GEOCODING_API_KEY);

                string latitude = location.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string longitude = location.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string requestUri = $"{StringConstants.GLOBAL_URL_GOOGLE_CLOUD}latlng={latitude},{longitude}&key={apiKey}";

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

        public async Task<GeocodeResult> GetGeocodingAsync(string entry)
        {                        
            try
            {
                string apiKey = ControlFiles.GetValueFromFilePropertyJson("googlecloud-config.json", StringConstants.GOOGLE_CLOUD, StringConstants.GEOCODING_API_KEY);

                var urlRequest = $"{StringConstants.GLOBAL_URL_GOOGLE_CLOUD}address={entry}&key={apiKey}";

                using HttpClient client = new();

                var response = await client.GetAsync(urlRequest);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<GeocodeResponse>(json);

                    if (result.Status == "OK" && result.Results.Count != 0)
                    {
                        return result.Results.First();
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