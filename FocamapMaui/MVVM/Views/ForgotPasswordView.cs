using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.Services.Navigation;

namespace FocamapMaui.MVVM.Views
{
    public class ForgotPasswordView : ContentPageBase
	{

        #region Properties

        private readonly INavigationService _navigationService;

        #endregion

        public ForgotPasswordView(INavigationService navigationService)
		{
            _navigationService = navigationService;

            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = BuildForgotPasswordView;
        }

        #region UI

        public View BuildForgotPasswordView
        {
            get
            {
                var grid = CreateMainGrid();

                CreateHeader(grid);

                CreateInputs(grid);

                CreateButtons(grid);

                return grid;
            }
        }

        public object CommomBasics { get; private set; }

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = 100},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                },
                Margin = 10,
                RowSpacing = 30
            };
        }

        private static void CreateHeader(Grid grid)
        {
            var stackHeader = new StackLayout
            {               
                Spacing = 10,
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End
            };

            var title = CommomBasic.GetLabelTitleBasic(title: "Esqueceu a senha?");
            stackHeader.Children.Add(title);

            var subtitle = new Label
            {
                Text = "Sem problemas, nós enviaremos um email com as instrucões para redefinição.",
                FontSize = 14,
                FontFamily = "MontserratRegular",
                TextColor = ControlResources.GetResource<Color>("CLGray"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center
            };
            stackHeader.Children.Add(subtitle);

            grid.AddWithSpan(stackHeader);
        }

        private static void CreateInputs(Grid grid)
        {
            var emailInput = new TextEditCustom(icon: "email_24", placeholder: "Email da conta", keyboard: Keyboard.Email);
            
            grid.AddWithSpan(emailInput, 1);
        }

        private void CreateButtons(Grid grid)
        {
            var stackButtons = CommomBasic.GetStackLayoutBasic();

            var resetPasswordButton = new PrimaryButtonCustom(text: "Redefinir Senha", textColor: "CLPrimary", backgroundColor: "CLPrimaryOrange");
            resetPasswordButton.Clicked += ResetPasswordButton_Clicked;
            stackButtons.Children.Add(resetPasswordButton);

            var backButton = new PrimaryEmptyButtonCustom(text: "Voltar", textColor: "CLPrimaryOrange", borderColor: "CLPrimaryOrange");
            backButton.Clicked += BackButton_Clicked;

            stackButtons.Children.Add(backButton);

            grid.AddWithSpan(stackButtons, 2);
        }

        
        #endregion

        #region Events

        private async void ResetPasswordButton_Clicked(object sender, EventArgs e) => await DisplayAlert("Clicou", "Clicou em Redefinir Senha", "OK");
      
        private async void BackButton_Clicked(object sender, EventArgs e) => await _navigationService.GoBack();

        #endregion
    }
}

