namespace FocamapMaui.Services.Map
{
    public interface IMapService
	{
        Task<string> GetAddressFromLocationAsync(Location location);
    }
}