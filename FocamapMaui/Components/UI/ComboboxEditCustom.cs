using DevExpress.Maui.Editors;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Models;

namespace FocamapMaui.Components.UI
{
    public class ComboboxEditCustom : ComboBoxEdit
    {
        public ComboboxEditCustom(string icon = "")
        {
            StartIcon = ImageSource.FromFile(icon);
            DisplayMember = nameof(City.Name);
            IsFilterEnabled = true;
            IsLabelFloating = false;
            HeightRequest = 55;            
            CornerRadius = 10;
            IconIndent = 10;
            FocusedBorderThickness = 1;
            IconVerticalAlignment = LayoutAlignment.Center;
            TextVerticalAlignment = TextAlignment.Center;
            BorderColor = Colors.Transparent;
            LabelColor = Colors.LightGray;
            FocusedLabelColor = Colors.Gray;
            TextColor = ControlResources.GetResource<Color>("CLWhite");
            CursorColor = ControlResources.GetResource<Color>("CLWhite");
            ClearIconColor = ControlResources.GetResource<Color>("CLWhite");
            IconColor = ControlResources.GetResource<Color>("CLWhite");                                
            FocusedBorderColor = ControlResources.GetResource<Color>("CLPrimaryOrange");                  
            DropDownBackgroundColor = ControlResources.GetResource<Color>("CLPrimary");           
            DropDownItemTextColor = ControlResources.GetResource<Color>("CLWhite");
            DropDownSelectedItemBackgroundColor = ControlResources.GetResource<Color>("CLPrimaryOrange");           
            BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");

            PlaceholderText = "Selecione";
            PlaceholderColor = Colors.Gray;
        }
    }
}

