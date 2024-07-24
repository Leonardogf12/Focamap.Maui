using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Models;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using FocamapMaui.Services.Authentication;
using FocamapMaui.Services.Navigation;

namespace FocamapMaui.MVVM.Views
{
    public class RegisterView : ContentPageBase
	{
        #region Properties

        private readonly INavigationService _navigationService;

        private readonly IAuthenticationService _authenticationService;


        public RegisterViewModel ViewModel;

        public TextEditCustom NameTextEdit;
        public TextEditCustom EmailTextEdit;
        public PasswordEditCustom PasswordTextEdit;
        public PasswordEditCustom RePasswordTextEdit;        
        public ComboboxEditCustom DropdownRegions;

        #endregion

        public RegisterView(INavigationService navigationService,
                            IAuthenticationService authenticationService)
		{
            _navigationService = navigationService;
            _authenticationService = authenticationService;

            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = BuildRegisterView;

            ViewModel = new(_authenticationService);

            CreateLoadingPopupView(this, ViewModel);

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
            var header = new HeaderWithIconAndTitle(iconName: "arrow_back_24",
                            textTitle: "Registro", iconEventHandler: GoBackButton_Clicked);

            grid.AddWithSpan(header);
        }

        private void CreateInputs(Grid grid)
        {
            var stackInputs = CommomBasic.GetStackLayoutBasic(spacing: 20);

            NameTextEdit = new TextEditCustom(startIcon: "user_24", endIcon: null, placeholder: "Nome");
            NameTextEdit.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Name));
            NameTextEdit.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorNameInput));
            NameTextEdit.TextChanged += NameTextEdit_TextChanged;
            stackInputs.Children.Add(NameTextEdit);

            EmailTextEdit = new TextEditCustom(startIcon: "user_24", endIcon: null, placeholder: "Email", keyboard: Keyboard.Email);
            EmailTextEdit.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Email));
            EmailTextEdit.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorEmailInput));
            EmailTextEdit.TextChanged += EmailTextEdit_TextChanged;
            stackInputs.Children.Add(EmailTextEdit);

            PasswordTextEdit = new PasswordEditCustom(icon: "password_24", placeholder: "Senha");
            PasswordTextEdit.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Password));
            PasswordTextEdit.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorPasswordInput));
            PasswordTextEdit.TextChanged += PasswordTextEdit_TextChanged;
            stackInputs.Children.Add(PasswordTextEdit);

            RePasswordTextEdit = new PasswordEditCustom(icon: "password_24", placeholder: "Repita a senha");
            RePasswordTextEdit.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.RepeatPassword));
            RePasswordTextEdit.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorRePasswordInput));
            RePasswordTextEdit.TextChanged += RePasswordTextEdit_TextChanged;
            stackInputs.Children.Add(RePasswordTextEdit);

            DropdownRegions = new ComboboxEditCustom(icon: "menu_24");
            DropdownRegions.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(ViewModel.Cities));          
            DropdownRegions.SetBinding(ComboBoxEdit.SelectedItemProperty, nameof(ViewModel.SelectedCity));
            DropdownRegions.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorRegionInput));
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

        private void NameTextEdit_TextChanged(object sender, EventArgs e)
        {
            ViewModel.CheckIfInputsAreOk();
        }

        private void EmailTextEdit_TextChanged(object sender, EventArgs e)
        {
            ViewModel.CheckIfInputsAreOk();
        }

        private void PasswordTextEdit_TextChanged(object sender, EventArgs e)
        {
            ViewModel.CheckIfInputsAreOk();
        }

        private void RePasswordTextEdit_TextChanged(object sender, EventArgs e)
        {
            ViewModel.CheckIfInputsAreOk();
        }               

        private async void EnterButton_Clicked(object sender, EventArgs e)
        {
            SetUnfocusFromAllInputs();
            await ViewModel.RegisterNewUser();           
        }
      
        private async void GoBackButton_Clicked(object sender, EventArgs e) => await _navigationService.GoBack();          

        private void RegionDropdownInput_SelectionChanged(object sender, EventArgs e)
        {
            DropdownRegions.Unfocus();
        }

        #endregion

        #region Actions

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.LoadCities();
        }

        private void SetUnfocusFromAllInputs()
        {
            NameTextEdit.Unfocus();
            EmailTextEdit.Unfocus();
            PasswordTextEdit.Unfocus();
            RePasswordTextEdit.Unfocus();
            DropdownRegions.Unfocus();
        }

        #endregion
    }
}

