using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using FocamapMaui.Services.Navigation;

namespace FocamapMaui.MVVM.Views
{
    public class RegisterView : ContentPageBase
	{
        #region Properties

        private readonly INavigationService _navigationService;

        public RegisterViewModel ViewModel = new();

        public ComboboxEditCustom DropdownRegions;

        #endregion

        public RegisterView(INavigationService navigationService)
		{
            _navigationService = navigationService;

            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = BuildRegisterView;

            BindingContext = ViewModel;
        }

        #region UI

        public View BuildRegisterView
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

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = 80},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                },
                Margin = 10,
                RowSpacing = 30
            };
        }

        private void CreateHeader(Grid grid)
        {
            var header = new HeaderWithIconAndTitle(iconName: "back_24",
                            textTitle: "Registro", iconEventHandler: BackButtonTapGestureRecognizer_Tapped);

            grid.AddWithSpan(header);
        }

        private void CreateInputs(Grid grid)
        {
            var stackInputs = CommomBasic.GetStackLayoutBasic(spacing: 20);

            var emailInput = new TextEditCustom(icon: "email_24", placeholder: "Email", keyboard: Keyboard.Email);
            emailInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Email));
            stackInputs.Children.Add(emailInput);

            var passwordInput = new PasswordEditCustom(icon: "password_24", placeholder: "Senha");
            passwordInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Password));
            stackInputs.Children.Add(passwordInput);

            var repasswordInput = new PasswordEditCustom(icon: "password_24", placeholder: "Repita a senha");
            repasswordInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.RepeatPassword));
            stackInputs.Children.Add(repasswordInput);

            DropdownRegions = new ComboboxEditCustom(icon: "menu_24");
            DropdownRegions.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(ViewModel.ListRegions));
            DropdownRegions.SelectionChanged += RegionDropdownInput_SelectionChanged;

            stackInputs.Children.Add(DropdownRegions);

            grid.AddWithSpan(stackInputs, 1);
        }
       
        private void CreateButtons(Grid grid)
        {
            var enterButton = new PrimaryButtonCustom(text: "Registrar", textColor: "CLPrimary", backgroundColor: "CLPrimaryOrange");
            enterButton.Clicked += EnterButton_Clicked;

            grid.AddWithSpan(enterButton, 2);
        }

        #endregion

        #region Events

        private async void EnterButton_Clicked(object sender, EventArgs e) => await DisplayAlert("Clicou", "Clicou em Registrar", "OK");
       
        private async void BackButtonTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is Image element)
            {
                await element.FadeAnimation();

                await _navigationService.GoBack();                
            }
        }

        private void RegionDropdownInput_SelectionChanged(object sender, EventArgs e)
        {
            DropdownRegions.Unfocus();
        }

        #endregion
    }
}

