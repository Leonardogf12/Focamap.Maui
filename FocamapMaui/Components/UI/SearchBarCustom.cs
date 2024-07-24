using FocamapMaui.Controls.Resources;
using Microsoft.Maui.Controls.Shapes;

namespace FocamapMaui.Components.UI
{
    public class SearchBarCustom : Border
	{
        public SearchBarCustom(string placeholder = "Buscar", EventHandler eventSearchButtonPressed = null)
        {
            Margin = new Thickness(10, 0, 10, 0);
            HeightRequest = 50;
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 10,
            };
            StrokeThickness = 0;       
            
            var searchBar = new SearchBar
            {
                HeightRequest = 50,
                FontSize = 16,
                FontFamily = "MontserratRegular",
                VerticalOptions = LayoutOptions.Center,
                Placeholder = placeholder,
                TextColor = Colors.White,
                PlaceholderColor = Colors.Gray,
                BackgroundColor = Colors.Transparent,
                CancelButtonColor = Colors.Gray,
            };            

            if (eventSearchButtonPressed is not null)
            {
                searchBar.SearchButtonPressed += eventSearchButtonPressed;
            }
            
            Content = searchBar;
        }               
    }
}