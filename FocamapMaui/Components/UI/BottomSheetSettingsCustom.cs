using DevExpress.Maui.Controls;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Resources;
using Microsoft.Maui.Controls.Shapes;

namespace FocamapMaui.Components.UI
{
    public class BottomSheetSettingsCustom : BottomSheet
    {
        public Label LabelNameUser;
        public Image IconEdit;
        public TextEditCustom UserTextEdit;
        public PasswordEditCustom PasswordTextEdit;
        public ComboboxEditCustom DropdownRegions;
        public Button SaveUserEditedButton;
        public Border LogOffBorderButton;

        public BottomSheetSettingsCustom()
		{
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");
            GrabberColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
            Content = BuildBottomSheet();
        }

        private Grid BuildBottomSheet()
        {
            var grid = CreateMainGrid();

            CreateProfileContainer(grid);

            CreateButtonsContainer(grid);

            return grid;
        }
        
        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() { Height = GridLength.Auto },
                    new() { Height = GridLength.Auto },                  
                },
                RowSpacing = 20,
                Margin = 10
            };
        }

        private void CreateProfileContainer(Grid grid)
        {
            var vertProfile = new VerticalStackLayout
            {
                Spacing = 40
            };

            var photoAndNameUser = CreateImageLetterWithEditAction();
            vertProfile.Children.Add(photoAndNameUser);

            var inputsOfProfile = CreateInputs();
            vertProfile.Children.Add(inputsOfProfile);

            grid.AddWithSpan(vertProfile, 0);
        }
      
        private StackLayout CreateImageLetterWithEditAction()
        {
            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = -4
            };

            var border = new Border
            {
                BackgroundColor = ControlResources.GetResource<Color>("CLRoundImageLetterName"),
                StrokeThickness = 0,
                WidthRequest = 90,
                HeightRequest = 90,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 50,
                }             
            };

            var stackLetter = new StackLayout
            {              
                VerticalOptions = LayoutOptions.Center,
            };

            LabelNameUser = new Label
            {                
                FontFamily = "MontserratBold",                               
                FontSize = 50,               
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            stackLetter.Children.Add(LabelNameUser);

            border.Content = stackLetter;

            IconEdit = new Image
            {
                Source = ControlResources.GetImage("edit_24"),
                HeightRequest = 24,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(65, -20, 0, 0),
            };
            
            stack.Children.Add(border);
            stack.Children.Add(IconEdit);

           return stack;
        }

        private StackLayout CreateInputs()
        {
            var stackInputs = CommomBasic.GetStackLayoutBasic(spacing: 10);
          
            UserTextEdit = new TextEditCustom(startIcon: "user_24", endIcon: null, placeholder: "Usuário")
            {
                AffixFontSize = 2,             
                BoxPadding = 5,
                HeightRequest = 45,              
            };          
            stackInputs.Children.Add(UserTextEdit);
          
            DropdownRegions = new ComboboxEditCustom(icon: "menu_24")
            {                
                BoxPadding = 5,
                HeightRequest = 45,
            };
            stackInputs.Children.Add(DropdownRegions);

            PasswordTextEdit = new PasswordEditCustom(icon: "password_24", placeholder: "Senha")
            {            
                BoxPadding = 5,
                HeightRequest = 45,
            };
            stackInputs.Children.Add(PasswordTextEdit);

            SaveUserEditedButton = new Button
            {
                Text = "Salvar Alterações",
                FontFamily = "MontserratSemibold",
                FontSize = 15,
                HorizontalOptions = LayoutOptions.End,
                TextColor = ControlResources.GetResource<Color>("CLLinkColor"),
                BackgroundColor = Colors.Transparent
            };
            stackInputs.Children.Add(SaveUserEditedButton);

            return stackInputs;
        }

        private void CreateButtonsContainer(Grid grid)
        {
            grid.AddWithSpan(CreateBorderButtonLogOff(), 1);
        }

        private Border CreateBorderButtonLogOff()
        {
            LogOffBorderButton = new Border
            {
                HeightRequest = 75,
                BackgroundColor = ControlResources.GetResource<Color>("CLSecondary"),
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 10
                },
                StrokeThickness = 0,               
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {

                        CreateHorizontalStackLogoff()
                    }
                }
            };

            return LogOffBorderButton;
        }

        private static HorizontalStackLayout CreateHorizontalStackLogoff()
        {
            return new HorizontalStackLayout
            {
                Margin = new Thickness(10, 0, 0, 0),
                Spacing = 10,
                Children = {
                    CreateIconToBorderButton("exit_24"),
                    CreateTextsToBorderButton("Sair", "Deslogar da conta")
                }
            };
        }

        private static Image CreateIconToBorderButton(string nameIcon)
        {
            return new Image
            {
                Source = ControlResources.GetImage(nameIcon),
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 30,
                WidthRequest = 30
            };
        }

        private static VerticalStackLayout CreateTextsToBorderButton(string textTitle, string textSubtitle)
        {
            var verticalStack = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
            };

            var title = CommomBasic.GetLabelTitleBasic(title: textTitle);
            title.HorizontalOptions = LayoutOptions.Start;

            verticalStack.Children.Add(title);

            var subtitle = CommomBasic.GetLabelTitleBasic(title: textSubtitle, fontSize: 12, fontFamily: "MontserratRegular");
            subtitle.HorizontalOptions = LayoutOptions.Start;

            verticalStack.Children.Add(subtitle);

            return verticalStack;
        }
    }
}