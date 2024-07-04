using Microsoft.Maui.Controls.Maps;

namespace FocamapMaui.Services.Map
{
    public class MapService : IMapService
	{       
        public List<Pin> GetPinsMock()
        {
            try
            {
                // Simulação de carregamento de Pins do Firebase
                return LoadContentPinsAsync();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }            
        }
      
        public List<Pin> LoadContentPinsAsync()
        {
            return new List<Pin>
            {
                new Pin
                {
                    Label = "Assalto",
                    Address = "Nesta esquina tem muitos assaltos pois ali fica muitos trabalhadores no ponto de onibus.",
                    Type = PinType.Generic,
                    Location = new Location(-19.394837, -40.049279),
                },
                new Pin
                {
                    Label = "Furto",
                    Address = "Nesta are tem muito relato de furto e roubo.",
                    Type = PinType.Place,
                    Location = new Location(-19.391254, -40.050202)
                },
                new Pin
                {
                    Label = "Morador de rua",
                    Address = "Nesta rua há muitos moradores de rua.",
                    Type = PinType.SearchResult,
                    Location = new Location(-19.395747, -40.037993)
                },
                new Pin
                {
                    Label = "Usuarios de Drogras",
                    Address = "Nesta area ocorre muito a presenca de usuarios de drogas.",
                    Type = PinType.SavedPin,
                    Location = new Location(-19.400564, -40.045224)
                }
            };                                
        }
    }
}

