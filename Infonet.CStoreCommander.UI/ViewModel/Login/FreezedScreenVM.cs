using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Login
{
    /// <summary>
    /// View model for Freeze Screen
    /// </summary>
    public class FreezedScreenVM : VMBase
    {
        private string _password;
        private bool _isUserFormVisible;

        public string UserName => CacheBusinessLogic.UserName;

        public string Password
        {
            get
            {
                if (_password == null)
                {
                    _password = string.Empty;
                }
                return _password;
            }
            set
            {
                _password = value;
                IsUnFreezeEnabled = _password.Length > 0;
                RaisePropertyChanged(nameof(Password));
            }
        }

        public bool IsUserFormVisible
        {
            get { return _isUserFormVisible; }
            set
            {
                _isUserFormVisible = value;
                RaisePropertyChanged(nameof(IsUserFormVisible));

                if (_isUserFormVisible)
                {
                    PerformAction(async () => { await Task.Delay(200); }, "Password");
                }
            }
        }

        private bool _isUnFreezeEnabled;

        public bool IsUnFreezeEnabled
        {
            get { return _isUnFreezeEnabled; }
            set
            {
                _isUnFreezeEnabled = value;
                RaisePropertyChanged(nameof(IsUnFreezeEnabled));
            }
        }

        public RelayCommand<object> PasswordCompletedCommand { get; private set; }
        public RelayCommand UnFreezeCommand { get; private set; }
        public RelayCommand HideUserFormCommand { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FreezedScreenVM()
        {
            InitiallizeCommands();
        }

        /// <summary>
        /// Initializes all the commands used
        /// </summary>
        private void InitiallizeCommands()
        {
            PasswordCompletedCommand = new RelayCommand<object>(PasswordCompleted);
            UnFreezeCommand = new RelayCommand(UnFreeze);
            HideUserFormCommand = new RelayCommand(() => { IsUserFormVisible = false; });
        }

        /// <summary>
        /// Checks whether enter key is pressed and then unfreezes the application
        /// </summary>
        /// <param name="obj"></param>
        private void PasswordCompleted(dynamic obj)
        {
            if (Helper.IsEnterKey(obj))
            {
                UnFreeze();
            }
        }

        /// <summary>
        /// Checks for the password and ten unfreezes the application
        /// </summary>
        private void UnFreeze()
        {
            if (Password.Equals(CacheBusinessLogic.Password))
            {
                NavigateService.Instance.OpenedScreenPriorFreezeScreen();
                MessengerInstance.Send(new CloseKeyboardMessage());
            }
            else
            {
                ShowNotification(ApplicationConstants.InvalidPassword,
                () =>
                {
                    Password = string.Empty;
                },
                () =>
                {
                    Password = string.Empty;
                },
                ApplicationConstants.ButtonWarningColor);
            }
        }

        public void ReInitialize()
        {
            Password = string.Empty;
            IsUserFormVisible = false;
        }
    }
}
