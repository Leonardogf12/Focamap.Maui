using System.Reflection;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace FocamapMaui.Controls.Maps
{
    public class OnMapReadyCallback : Java.Lang.Object, IOnMapReadyCallback
    {
        //Método criado para Localizar e Definir o arquivo de estilo de Mapa ao...
        //objeto Map criado na HomeMapView cujo a renderizacao ocorre fora de sincronia.
        public void OnMapReady(GoogleMap googleMap)
        {
            var assembly = Assembly.GetExecutingAssembly();
       
            using var stream = assembly.GetManifestResourceStream("FocamapMaui.Resources.Maps.night_map_style.json");

            string json = string.Empty;

            using (var reader = new StreamReader(stream!))
            {
                json = reader.ReadToEnd();
            }
          
            googleMap.SetMapStyle(new MapStyleOptions(json));
        }
    }
}

