using Android.App;
using Android.Runtime;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace FocamapMaui;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
        /*
        //Remove Entry control underline
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
            h.PlatformView.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
        });
        */
        /*
        Microsoft.Maui.Controls.Maps.Pin.ControlsElementMapper.AppendToMapping("ChangeImageSource", (h, v) =>
        {
            var a = h.PlatformView.GetType();
            var aa = a.MakePointerType();

            var aaa = aa.GenericParameterPosition.

            
        });
        */
        /*
        Microsoft.Maui.Controls.Maps.Pin.ControlsElementMapper.AppendToMapping("ChangeImageSource", (h, v) =>
        {
           
        });
        */
        /*
        Microsoft.Maui.Controls.Maps.Pin.ControlsElementMapper.ReplaceMapping(key: "ChangeImageSource",(arg1, arg2) => 
        {
            
        });*/

        /*

        Microsoft.Maui.Controls.Maps.Pin.ControlsElementMapper.ModifyMapping(key: "ChangeImageSource", (w,e) =>
        {
         
        });*/

        /*
        Microsoft.Maui.Controls.Maps.Pin.ControlsElementMapper.PrependToMapping(key: "ChangeImageSource", (w, e) =>
        {
        
        }); */
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

