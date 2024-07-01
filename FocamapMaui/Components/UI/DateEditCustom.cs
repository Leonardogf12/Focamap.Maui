using System.Windows.Input;
using DevExpress.Maui.Editors;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class DateEditCustom : DateEdit
	{       
        public DateEditCustom(string icon = "", string placeholder = "", bool useNativePicker = false)
        {
            StartIcon = ImageSource.FromFile(icon);
            PlaceholderText = placeholder;
            IsDateIconVisible = false;
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
            IconColor = ControlResources.GetResource<Color>("CLWhite");                
            PickerBackgroundColor = ControlResources.GetResource<Color>("CLSecondary");
            PickerHeaderAppearance = new CalendarHeaderAppearance
            {
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                HeaderSubtitleTextColor = ControlResources.GetResource<Color>("CLWhite"),
                HeaderTitleTextColor = ControlResources.GetResource<Color>("CLWhite"),
            };                              
            PickerDayCellAppearance = new CalendarDayCellAppearance
            {
                TodayEllipseBackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                TextColor = ControlResources.GetResource<Color>("CLWhite"),
                TodayTextColor = ControlResources.GetResource<Color>("CLWhite"),
                SelectedTextColor = ControlResources.GetResource<Color>("CLPrimary"),
                SelectedEllipseBackgroundColor = ControlResources.GetResource<Color>("CLPrimaryOrange"),
            };
            DisplayFormat = "dd/MM/yyyy";
            UseNativePicker = useNativePicker;
        }

        private static View CreateTemplateButton(ICommand command)
        {           
            var okButton = new Button
            {
                Text = "OK",
            };
            okButton.Command = command;

            return okButton;            
        }
    }
}

