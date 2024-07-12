using System.ComponentModel;
using FocamapMaui.Models.Map;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace FocamapMaui.Components.UI
{
    public class MapCustom
    {                
        public static Map GetMap(MapType mapType, Location location, EventHandler<MapClickedEventArgs> eventMapClicked, PropertyChangedEventHandler propertyChangedMap)
        {
            var map = new Map
            {
                MapType = mapType,
                IsScrollEnabled = true,                
                IsZoomEnabled = true,
                IsShowingUser = true,
                IsTrafficEnabled = false,
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(2700)));

            map.MapClicked += eventMapClicked;
            map.PropertyChanged += propertyChangedMap;

            return map;
        }
       
        public static List<PinDto> CreatePins(List<PinDto> pinDtos)
        {
            List<PinDto> pins = new();

            pinDtos.ForEach(x =>
            {
                PinDto pinDto = new()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Address = x.Address,
                    FullDate = x.FullDate,
                    Status = x.Status,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude
                };
            });

            return pins;
        }
        
        public static List<MapElement> CreateCircles(List<CircleDto> circleDtos)
        {           
            List<MapElement> circles = new();

            circleDtos.ForEach(x =>
            {
                Circle circle = new()
                {
                    Center = x.Center,
                    Radius = x.Radius,
                    StrokeWidth = x.StrokeWidth,
                    StrokeColor = x.StrokeColor,
                    FillColor = x.FillColor
                };

                circles.Add(circle);
            });

            return circles;
        }
    }
}