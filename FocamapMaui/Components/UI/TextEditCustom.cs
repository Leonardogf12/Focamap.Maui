using DevExpress.Maui.Editors;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class TextEditCustom : TextEdit
	{
        public TextEditCustom(string icon = "", string placeholder = "", char maskPlaceholder = new(), string mask = null)
        {       
            StartIcon = ImageSource.FromFile(icon);
            MaskPlaceholderChar = maskPlaceholder;
            Mask = mask;
            PlaceholderText = placeholder;           
            IsLabelFloating = false;
            LabelText = null;
            IconIndent = 10;           
            HeightRequest = 55;
            CornerRadius = 10;
            BorderThickness = 1;
            BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");
            PlaceholderColor = ControlResources.GetResource<Color>("CLTertiary");
            FocusedBorderColor = Colors.Gray;            
            BorderColor = Colors.Transparent;
            TextColor = ControlResources.GetResource<Color>("CLWhite");
            CursorColor = ControlResources.GetResource<Color>("CLWhite");
            ClearIconColor = ControlResources.GetResource<Color>("CLGray");
            AffixColor = ControlResources.GetResource<Color>("BorderGray400");
            IconColor = ControlResources.GetResource<Color>("CLWhite");
            IconVerticalAlignment = LayoutAlignment.Center;
            TextVerticalAlignment = TextAlignment.Center;           
        }
    }
}

