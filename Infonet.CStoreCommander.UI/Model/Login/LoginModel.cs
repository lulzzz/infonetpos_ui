using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Utility;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.UI.Model.Login
{
    public class LoginModel : ViewModelBase
    {
        private string _tillFloat;
        private List<int> _tillNumbers;
        private List<string> _shifts;
        private string _userName;
        private string _password;
        public string KeypadFormat { get; set; }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(nameof(UserName));
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

        public List<int> TillNumbers
        {
            get { return _tillNumbers; }
            set
            {
                _tillNumbers = value;
                RaisePropertyChanged(nameof(TillNumbers));
            }
        }

        public List<string> Shifts
        {
            get { return _shifts; }
            set
            {
                _shifts = value;
                RaisePropertyChanged(nameof(Shifts));
            }
        }

        public string TillFloat
        {
            get { return _tillFloat; }
            set
            {
                if (value != _tillFloat)
                {
                    _tillFloat = value;
                    RaisePropertyChanged(nameof(TillFloat));
                }
            }
        }
    }
}
