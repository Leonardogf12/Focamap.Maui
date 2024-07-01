using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public static class RoundButton
    {
		public static Button GetRoundButton(string iconName = "", EventHandler eventHandler = null)
		{
            var roundButton = new Button
            {
                ImageSource = ImageSource.FromFile(iconName),              
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),                
                HeightRequest = 60,
                WidthRequest = 60,
                CornerRadius = 60,                
            };
           
            if (eventHandler is not null)
            {
                roundButton.Clicked += eventHandler;
            }
            
            return roundButton;
        }        
    }
}

