using FocamapMaui.Controls.Maps;
using FocamapMaui.Models;

namespace FocamapMaui.Services.Map
{
    public class MapService : IMapService
	{       
        public async Task<List<PinDto>> GetPinsMock()
        {
            try
            {
                await Task.Delay(5000);

                // Simulação de carregamento de Pins do Firebase
                return LoadContentPinsAsync();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }            
        }
      
        public List<PinDto> LoadContentPinsAsync()
        {
            return new List<PinDto>
            {
                new PinDto
                {
                     Id = "aaa",
                     Title = "Assalto",
                     Content = "Nesta esquina tem muitos assaltos pois ali fica muitos trabalhadores no ponto de onibus, cuidado ao passar naquela rua a noite, nunca de madrugada ok.",
                     Address = "Rua Banda de Rock Linkin Park, Interlagos, Linhares ES 29900-000",
                     FullDate = "01/07/2024 ás 14:15",
                     Status = (int)PinStatus.Medio,
                     Latitude = -19.394837,
                     Longitude = -40.049279,                    
                },
                 new PinDto
                 {
                     Id = "bbb",
                     Title = "Furto",
                     Content = "Nesta are tem muito relato de furto e roubo.",
                     Address = "Av. Guns N Roses, Interlagos, Linhares ES.",
                     FullDate = "08/06/2024 ás 10:45",
                     Status = (int)PinStatus.Medio,
                     Latitude = -19.391254,
                     Longitude = -40.050202,
                 },
                 new PinDto
                 {
                     Id = "ccc",
                     Title = "Morador de rua",
                     Content = "Nesta rua há muitos moradores de rua.",
                     Address = "Av. Pearl Jam, Interlagos, Linhares ES.",
                     FullDate = "25/04/2024 ás 23:03",
                     Status = (int)PinStatus.Baixo,
                     Latitude = -19.395747,
                     Longitude = -40.037993,
                 },
                 new PinDto
                 {
                     Id = "ddd",
                     Title = "Usuarios de Drogras",
                     Content = "Nesta area ocorre muito a presenca de usuarios de drogas.",
                     Address = "Rua Foo Fighters 29903045, Interlagos, Linhares ES.",
                     FullDate = "25/04/2024 ás 01:55",
                     Status = (int)PinStatus.Alto,
                     Latitude = -19.400564,
                     Longitude = -40.045224,
                 }
            };                                
        }
    }
}

