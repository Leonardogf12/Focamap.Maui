using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class PrimaryEmptyButtonCustom : Button
    {
        public PrimaryEmptyButtonCustom(string text, string textColor, string borderColor)
        {
            Text = text;
            BackgroundColor = Colors.Transparent;
            BorderColor = ControlResources.GetResource<Color>(borderColor);
            TextColor = ControlResources.GetResource<Color>(textColor);
            HeightRequest = 45;
            CornerRadius = 30;
            FontSize = 16;
            FontFamily = "MontserratSemibold";
        }
    }
}