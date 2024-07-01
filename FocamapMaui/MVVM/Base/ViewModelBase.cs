using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FocamapMaui.MVVM.Base
{
    public class ViewModelBase : INotifyPropertyChanged
	{
        private List<string> _listRegions;
        public List<string> ListRegions
        {
            get => _listRegions;
            set
            {
                _listRegions = value;
                OnPropertyChanged();
            }
        }

        public ViewModelBase()
        {
        }

        public void LoadRegionListMock()
        {
            ListRegions = new List<string>
            {
                "Espirito Santo",
                "Minas Gerais",
                "São Paulo",
                "Rio de Janeiro"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

