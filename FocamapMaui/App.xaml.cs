using FocamapMaui.MVVM.Views;

namespace FocamapMaui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}

