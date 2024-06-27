using DevExpress.Maui;
using Microsoft.Extensions.Logging;

namespace FocamapMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseDevExpress()		
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
                fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
                fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemibold");               
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

