using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Models;

namespace FocamapMaui.MVVM.ViewModels
{
    public class UserDetailViewModel : ViewModelBase
	{
        #region Properties

        private UserModel _userDetail = new();
        public UserModel UserDetail
        {
            get => _userDetail;
            set
            {
                _userDetail = value;
                OnPropertyChanged();
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public UserDetailViewModel()
		{
            LoadRegionListMock();
        }
	}
}

