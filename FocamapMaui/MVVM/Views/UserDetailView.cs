using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using FocamapMaui.Services.Authentication;
using FocamapMaui.Services.Navigation;

namespace FocamapMaui.MVVM.Views
{
    public class UserDetailView : ContentPageBase
	{
        #region Properties

        private readonly IAuthenticationService _authenticationService;

        private readonly INavigationService _navigationService;

        public UserDetailViewModel ViewModel;

        public ComboboxEditCustom DropdownRegions = new();

        public TextEditCustom UserTextEdit;

        public PasswordEditCustom PasswordTextEdit;

        #endregion

        public UserDetailView(INavigationService navigationService, IAuthenticationService authenticationService)
		{
            _navigationService = navigationService;
            _authenticationService = authenticationService;

            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = BuildUserDetailView;

            ViewModel = new(_authenticationService);
            BindingContext = ViewModel;
        }

		public Grid BuildUserDetailView
        {
            get
            {
                var grid = CreateMainGrid();

                CreateHeader(grid);

                CreateInputs(grid);

                CreateButton(grid);

                return grid;
            }           
		}
       
        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = 110},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                },
                Margin = 10,
                RowSpacing = 30
            };
        }

        private void CreateHeader(Grid grid)
        {           
            var header = new HeaderWithIconAndImage(iconName: "arrow_back_24", iconGoBackEventHandler: GoBackButton_Clicked,
                            imageNameEventHandler: ImageNameTapGestureRecognizer_Tapped);
            header.LabelNameUser.SetBinding(Label.TextProperty, nameof(ViewModel.LetterUserName));

            grid.AddWithSpan(header);
        }

        private void CreateInputs(Grid grid)
        {
            var stackInputs = CommomBasic.GetStackLayoutBasic(spacing: 20);

            var emailInput = new TextEditCustom(startIcon: "email_24", endIcon: null, placeholder: "Email");
            emailInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Email), BindingMode.TwoWay);            
            emailInput.SetBinding(IsEnabledProperty, nameof(ViewModel.IsEnabledEmail), BindingMode.TwoWay);            
            stackInputs.Children.Add(emailInput);

            UserTextEdit = new TextEditCustom(startIcon: "user_24", endIcon: null, placeholder: "Usuário");
            UserTextEdit.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.DisplayName), BindingMode.TwoWay);
            UserTextEdit.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorDisplayName), BindingMode.TwoWay);
            UserTextEdit.SetBinding(IsEnabledProperty, nameof(ViewModel.IsEnabledDisplayName), BindingMode.TwoWay);
            UserTextEdit.TextChanged += UserInput_TextChanged;
            stackInputs.Children.Add(UserTextEdit);

            PasswordTextEdit = new PasswordEditCustom(icon: "password_24", placeholder: "Senha");
            PasswordTextEdit.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Password), BindingMode.TwoWay);
            PasswordTextEdit.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorPassword), BindingMode.TwoWay);
            PasswordTextEdit.SetBinding(IsEnabledProperty, nameof(ViewModel.IsEnabledPassword), BindingMode.TwoWay);
            PasswordTextEdit.TextChanged += PasswordInput_TextChanged;
            stackInputs.Children.Add(PasswordTextEdit);

            DropdownRegions = new ComboboxEditCustom(icon: "menu_24");
            DropdownRegions.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(ViewModel.Cities));         
            DropdownRegions.SetBinding(IsEnabledProperty, nameof(ViewModel.IsEnabledRegion), BindingMode.TwoWay);
            DropdownRegions.SelectionChanged += RegionDropdownInput_SelectionChanged;
            stackInputs.Children.Add(DropdownRegions);

            grid.AddWithSpan(stackInputs, 1);
        }
      
        private void CreateButton(Grid grid)
        {
            var editButton = new PrimaryButtonCustom(text: "Editar", textColor: "CLPrimary", backgroundColor: "CLPrimaryWhite");
            editButton.Clicked += EditButton_Clicked;

            grid.AddWithSpan(editButton, 2);
        }

        #region Events

        private void PasswordInput_TextChanged(object sender, EventArgs e) => ViewModel.CheckIfInputsAreOk();

        private void UserInput_TextChanged(object sender, EventArgs e) => ViewModel.CheckIfInputsAreOk();
       
        private void RegionDropdownInput_SelectionChanged(object sender, EventArgs e) => DropdownRegions.Unfocus();

        private async void GoBackButton_Clicked(object sender, EventArgs e) => await _navigationService.GoBack();

        private async void ImageNameTapGestureRecognizer_Tapped(object sender, TappedEventArgs e) => await SetBodyImageElementForEvent(sender, SetValuesIsEnabled);
      
        private void SetValuesIsEnabled() => ViewModel.SetsValueForIsEnabledInputs(true);

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            UserTextEdit.Unfocus();
            PasswordTextEdit.Unfocus();

            await ViewModel.UpdateProfileUser();
        }

        private static async Task SetBodyImageElementForEvent(object sender, Action action)
        {
            if (sender is Image element)
            {
                await element.FadeAnimation();

                action();
            }
        }

        #endregion

        #region Actions

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.LoadCities();
        }

        #endregion
    }
}

