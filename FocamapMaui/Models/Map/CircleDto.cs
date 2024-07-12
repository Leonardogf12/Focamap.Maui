using Microsoft.Maui.Maps;

namespace FocamapMaui.Models.Map
{
    public class CircleDto
	{
        public Location Center { get; set; }
        public Distance Radius { get; set; }
        public float StrokeWidth { get; set; }
        public Color StrokeColor { get; set; }
        public Color FillColor { get; set; }
    }
}