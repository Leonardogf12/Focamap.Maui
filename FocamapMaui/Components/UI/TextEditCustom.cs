using DevExpress.Maui.Editors;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class TextEditCustom : TextEdit
	{
        public TextEditCustom(string startIcon = "", string endIcon = null, string placeholder = "", Keyboard keyboard = null, char maskPlaceholder = new(), string mask = null)
        {       
            StartIcon = ControlResources.GetImage(startIcon);
            EndIcon = !string.IsNullOrEmpty(endIcon) ? ControlResources.GetImage(endIcon) : "";
            IsEndIconVisible = !string.IsNullOrEmpty(endIcon);
            MaskPlaceholderChar = maskPlaceholder;
            Mask = mask;
            PlaceholderText = placeholder;
            Keyboard = keyboard ?? Keyboard.Default;
            IsLabelFloating = false;                    
            LabelText = null;
            IconIndent = 10;           
            HeightRequest = 55;
            CornerRadius = 10;
            BorderThickness = 1;
            FocusedBorderThickness = 1;
            IconVerticalAlignment = LayoutAlignment.Center;
            TextVerticalAlignment = TextAlignment.Center;
            BorderColor = Colors.Transparent;
            FocusedBorderColor = ControlResources.GetResource<Color>("CLPrimaryOrange");           
            BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");
            PlaceholderColor = ControlResources.GetResource<Color>("CLTertiary");                              
            TextColor = ControlResources.GetResource<Color>("CLWhite");
            CursorColor = ControlResources.GetResource<Color>("CLWhite");
            ClearIconColor = ControlResources.GetResource<Color>("CLGray");
            AffixColor = ControlResources.GetResource<Color>("BorderGray400");
            IconColor = ControlResources.GetResource<Color>("CLWhite");                 
        }
    }
}