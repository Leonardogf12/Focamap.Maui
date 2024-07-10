using FocamapMaui.Components;
using FocamapMaui.Controls;

namespace FocamapMaui;

public partial class App : Application
{
    public static string FirebaseUserLocalIdKey;

    public static PopupLoadingView popupLoading = new();

    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

        ControlUsers.SetLocalIdByUserLogged();

        ControlUsers.CheckUserHasLogged();
    }    
}

