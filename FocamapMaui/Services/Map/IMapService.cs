using Microsoft.Maui.Controls.Maps;

namespace FocamapMaui.Services.Map
{
    public interface IMapService
	{
        List<Pin> GetPinsMock();

        List<Pin> LoadContentPinsAsync();
    }
}

