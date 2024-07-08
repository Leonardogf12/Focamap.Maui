using FocamapMaui.Models;

namespace FocamapMaui.Services.Map
{
    public interface IMapService
	{
        Task<List<PinDto>> GetPinsMock();

        List<PinDto> LoadContentPinsAsync();
    }
}

