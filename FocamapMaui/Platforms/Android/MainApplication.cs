using Android.App;
using Android.Runtime;

namespace FocamapMaui;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
        //Remove underline of SerachBar
        Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
            Android.Widget.LinearLayout linearLayout = h.PlatformView.GetChildAt(0) as Android.Widget.LinearLayout;
            linearLayout = linearLayout.GetChildAt(2) as Android.Widget.LinearLayout;
            linearLayout = linearLayout.GetChildAt(1) as Android.Widget.LinearLayout;
            linearLayout.Background = null;            
        });

    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}