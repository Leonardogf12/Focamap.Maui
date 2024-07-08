using System.ComponentModel;
using CommunityToolkit.Maui.Views;

namespace FocamapMaui.MVVM.Base
{
    public class ContentPageBase : ContentPage
	{              
        public ContentPageBase()
		{           
			Shell.SetNavBarIsVisible(this, false);

            Content = new Grid();
        }

        public static void CreateLoadingPopupView<TViewModel>(Page page, TViewModel viewModel) where TViewModel : INotifyPropertyChanged
        {
            viewModel.PropertyChanged += (s, a) =>
            {
                var vm = s as ViewModelBase;

                if (a.PropertyName == "IsBusy")
                {
                    MainThread.BeginInvokeOnMainThread(async () => {

                        if (vm.IsBusy)
                        {
                            if (App.popupLoading == null)
                            {
                                App.popupLoading = new();
                            }

                            await page.ShowPopupAsync(App.popupLoading);
                        }
                        else
                        {
                            if (App.popupLoading == null) return;

                            await App.popupLoading.CloseAsync();

                            App.popupLoading = null;
                        }
                    });
                }
            };
        }
    }
}

