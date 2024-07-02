using FocamapMaui.Controls;

namespace FocamapMaui;

public partial class App : Application
{
    public static string UserLocalIdLogged;

    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

        ControlUsers.CheckUserHasLogged();
    }
}

