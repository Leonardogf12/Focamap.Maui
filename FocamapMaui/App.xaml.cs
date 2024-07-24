using FocamapMaui.Components.UI;
using FocamapMaui.Controls;
using FocamapMaui.MVVM.Models;

namespace FocamapMaui;

public partial class App : Application
{
    public static string FirebaseUserLocalIdKey;

    public static PopupLoadingView popupLoading = new();

    public static UserModel UserLogged;

    public App()
	{        
		InitializeComponent();

        MainPage = new AppShell();

        ControlUsers.SetLocalIdByUserLogged();

        ControlUsers.CheckUserHasLogged();
    }
}