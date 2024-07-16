using DevExpress.Maui.Editors;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class HourEditCustom : TimeEdit
	{
        public HourEditCustom(string icon = "", string placeholder = "")
        {            
            StartIcon = ImageSource.FromFile(icon);
            PlaceholderText = placeholder;
            IsTimeIconVisible = false;
            IsLabelFloating = false;
            LabelText = null;
            IconIndent = 10;
            HeightRequest = 55;
            CornerRadius = 10;
            BorderThickness = 1;
            FocusedBorderThickness = 1;
            HorizontalOptions = LayoutOptions.Fill;
            IconVerticalAlignment = LayoutAlignment.Center;
            TextVerticalAlignment = TextAlignment.Center;
            BorderColor = Colors.Transparent;
            FocusedBorderColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
            BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");
            PlaceholderColor = ControlResources.GetResource<Color>("CLTertiary");
            TextColor = ControlResources.GetResource<Color>("CLWhite");
            CursorColor = ControlResources.GetResource<Color>("CLWhite");
            ClearIconColor = ControlResources.GetResource<Color>("CLGray");
            IconColor = ControlResources.GetResource<Color>("CLWhite");                        
        }
    }
}