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
            
            Items.Add(GetShellItem<LoginView>());
            Items.Add(GetShellItem<HomeMapView>());           
        }
        
        private static ShellContent GetShellItem<T>() where T : ContentPage
        {                      
            var name = typeof(T).Name;

            var rote = new ShellContent
            {
                ContentTemplate = new DataTemplate(typeof(T)),
                Route = name
            };

            return rote;
        }
        
        private void RouteRegistration()
        {
            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
            Routing.RegisterRoute(nameof(ForgotPasswordView), typeof(ForgotPasswordView));
            Routing.RegisterRoute(nameof(HomeMapView), typeof(HomeMapView));               
            Routing.RegisterRoute(nameof(OccurrencesHistoryView), typeof(OccurrencesHistoryView));                       
        }
    }
}