using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FocamapMaui.MVVM.Base
{
    public class ViewModelBase : INotifyPropertyChanged
	{
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

