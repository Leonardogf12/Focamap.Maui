using FocamapMaui.MVVM.Views;

namespace FocamapMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
        Routing.RegisterRoute(nameof(ForgotPasswordView), typeof(ForgotPasswordView));
        Routing.RegisterRoute(nameof(HomeMapView), typeof(HomeMapView));
        Routing.RegisterRoute(nameof(UserDetailView), typeof(UserDetailView));
        Routing.RegisterRoute(nameof(OccurrencesHistoryView), typeof(OccurrencesHistoryView));
    }
}

