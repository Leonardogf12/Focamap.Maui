using FocamapMaui.Models.Map;

namespace FocamapMaui.Services.Map
{
    public interface IMapService
	{
        Task<string> GetReverseGeocodingAsync(Location location);

        Task<GeocodeResult> GetGeocodingAsync(string entry);
    }
}