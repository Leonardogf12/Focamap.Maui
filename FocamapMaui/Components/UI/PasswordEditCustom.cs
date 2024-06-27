using DevExpress.Maui.Editors;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class PasswordEditCustom : PasswordEdit
	{
        public PasswordEditCustom(string icon = "", string placeholder = "")
        {
            StartIcon = ImageSource.FromFile(icon);
            PlaceholderText = placeholder;          
            IsLabelFloating = false;
            LabelText = null;
            IconIndent = 10;           
            HeightRequest = 55;
            CornerRadius = 10;
            FocusedBorderColor = Colors.Gray;
            BorderColor = Colors.Transparent;
            BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");
            PlaceholderColor = ControlResources.GetResource<Color>("CLTertiary");
            TextColor = ControlResources.GetResource<Color>("CLWhite");
            CursorColor = ControlResources.GetResource<Color>("CLWhite");
            ClearIconColor = ControlResources.GetResource<Color>("CLGray");
            IconColor = ControlResources.GetResource<Color>("CLWhite");
            IconVerticalAlignment = LayoutAlignment.Center;
            TextVerticalAlignment = TextAlignment.Center;
        }
    }
}

