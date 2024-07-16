using System.Windows.Input;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public static class RoundButton
    {
		public static Button GetRoundButton(string iconName = "", EventHandler eventHandler = null, ICommand command = null)
		{
            var roundButton = new Button
            {
                ImageSource = ControlResources.GetImage(iconName),              
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),                
                HeightRequest = 60,
                WidthRequest = 60,
                CornerRadius = 60,                
            };

            if (eventHandler is not null)
            {
                roundButton.Clicked += eventHandler;
            }

            if (command is not null)
            {
                roundButton.Command = command;
            }
            
            return roundButton;
        }        
    }
}

