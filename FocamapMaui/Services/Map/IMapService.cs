using FocamapMaui.Models;

namespace FocamapMaui.Services.Map
{
    public interface IMapService
	{
        List<PinDto> GetPinsMock();

        List<PinDto> LoadContentPinsAsync();
    }
}

