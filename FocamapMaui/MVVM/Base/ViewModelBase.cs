using System.ComponentModel;
using System.Runtime.CompilerServices;
using FocamapMaui.Helpers.Models;
using FocamapMaui.Models;

namespace FocamapMaui.MVVM.Base
{
    public class ViewModelBase : INotifyPropertyChanged
	{
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ViewModelBase()
        {
        }
      
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

