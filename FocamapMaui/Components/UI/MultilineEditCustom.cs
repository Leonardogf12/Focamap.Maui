using DevExpress.Maui.Editors;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class MultilineEditCustom : MultilineEdit
	{
        public MultilineEditCustom(string icon = "", string placeholder = "", int minimumHeightRequest = 50, int maximumHeigthRequest = 200, int maxCharacterCount = 200, int maxLineCount = 7)
        {
            StartIcon = ControlResources.GetImage(icon);
            MaxCharacterCountOverflowMode = OverflowMode.LimitInput;
            MaxCharacterCount = maxCharacterCount;
            MaxLineCount = maxLineCount;
            PlaceholderText = placeholder;
            MinimumHeightRequest = minimumHeightRequest;
            MaximumHeightRequest = maximumHeigthRequest;
            Keyboard = Keyboard.Default;
            IconIndent = 5;           
            CornerRadius = 10;
            IsLabelFloating = false;
            LabelText = null;
            IconVerticalAlignment = LayoutAlignment.Start;
            TextVerticalAlignment = TextAlignment.Start;
            FlowDirection = FlowDirection.LeftToRight;                                                                                         
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

