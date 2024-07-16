using FocamapMaui.MVVM.Views;

namespace FocamapMaui
{
    public partial class AppShell : Shell
	{
        public AppShell()
        {
            InitializeComponentPerCodeBehind();

            RouteRegistration();
        }

        private void InitializeComponentPerCodeBehind()
        {
            FlyoutBehavior = FlyoutBehavior.Disabled;

            Items.Add(new ShellContent
            {
                ContentTemplate = new DataTemplate(typeof(LoginView)),
                Route = "LoginView"
            });
            Items.Add(new ShellContent
            {
                ContentTemplate = new DataTemplate(typeof(HomeMapView)),
                Route = "HomeMapView"
            });
        }

        private void RouteRegistration()
        {
            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
            Routing.RegisterRoute(nameof(ForgotPasswordView), typeof(ForgotPasswordView));
            Routing.RegisterRoute(nameof(HomeMapView), typeof(HomeMapView));
            Routing.RegisterRoute(nameof(UserDetailView), typeof(UserDetailView));
            Routing.RegisterRoute(nameof(OccurrencesHistoryView), typeof(OccurrencesHistoryView));
            Routing.RegisterRoute(nameof(UserDetailView), typeof(UserDetailView));
        }
    }
}