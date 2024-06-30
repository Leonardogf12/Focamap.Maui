using System;
using System.Windows.Input;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components
{
    public static class RoundButton
    {
		public static Button GetRoundButton(string iconName = "", EventHandler eventHandler = null)
		{
            var roundButton = new Button
            {
                ImageSource = ImageSource.FromFile(iconName),              
                BackgroundColor = ControlResources.GetResource<Color>("CLBlack"),                
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

