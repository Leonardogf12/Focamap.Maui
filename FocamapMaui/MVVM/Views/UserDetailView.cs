using AndroidX.Lifecycle;
using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
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

            var userInput = new TextEditCustom(icon: "user_24", placeholder: "Usuário");
            userInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.DisplayName), BindingMode.TwoWay);
            stackInputs.Children.Add(userInput);

            DropdownRegions = new ComboboxEditCustom(icon: "menu_24");
            DropdownRegions.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(ViewModel.ListRegions));
            DropdownRegions.SelectionChanged += RegionDropdownInput_SelectionChanged; ;
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

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            await ViewModel.UpdateProfileUser();
        }

        private void RegionDropdownInput_SelectionChanged(object sender, EventArgs e)
        {
            DropdownRegions.Unfocus();
        }

        private async void BackButtonTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            await _navigationService.GoBack();
        }

        #endregion
    }
}

