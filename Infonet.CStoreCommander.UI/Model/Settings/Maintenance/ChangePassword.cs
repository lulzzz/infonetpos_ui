using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Settings.Maintenance
{
    public class ChangePassword : ViewModelBase
    {
        private string _password;
        private string _confirmNewPassword;

        public string ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set
            {
                _confirmNewPassword = value;
                RaisePropertyChanged(nameof(ConfirmNewPassword));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
    }
}
