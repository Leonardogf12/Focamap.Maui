using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class PrimaryButtonCustom : Button
	{
		public PrimaryButtonCustom(string text, string textColor, string backgroundColor)
		{
            Text = text;                           
            BackgroundColor = ControlResources.GetResource<Color>(backgroundColor);
            TextColor = ControlResources.GetResource<Color>(textColor);
            HeightRequest = 45;
            CornerRadius = 30;
            FontSize = 16;           
            FontFamily = "MontserratSemibold";
        }
	}
}

