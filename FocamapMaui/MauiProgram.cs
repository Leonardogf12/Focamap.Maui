using CommunityToolkit.Maui;
using DevExpress.Maui;
using FocamapMaui.MVVM.Views;
using FocamapMaui.Services.Authentication;
using FocamapMaui.Services.Map;
using FocamapMaui.Services.Navigation;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FocamapMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseDevExpress()
			.UseMauiMaps()
            .UseSkiaSharp()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
                fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
                fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemibold");               
			});      

        RegisterDependencyForViews(builder);

        RegisterDependencyForInterfaces(builder);

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    private static void RegisterDependencyForViews(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<RegisterView>();
        builder.Services.AddTransient<ForgotPasswordView>();
        builder.Services.AddTransient<HomeMapView>();
        builder.Services.AddTransient<OccurrencesHistoryView>();
        builder.Services.AddTransient<UserDetailView>();
    }
    
    private static void RegisterDependencyForInterfaces(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<IMapService, MapService>();        
    }
}

