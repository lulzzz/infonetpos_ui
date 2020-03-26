using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.UI.Model.Settings.Maintenance;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.SettingsMenu.Maintenance
{
    public class SwitchUserVM : VMBase
    {
        public RelayCommand ChangePasswordCommand { get; set; }
        public RelayCommand<object> ConfirmPasswordCompletedCommand { get; set; }
        public RelayCommand SwitchUserCommand { get; set; }

        public ChangePassword ChangePassword { get; set; }
        = new ChangePassword();

        private bool _userCanChangePassword;

        private readonly IMaintenanceBussinessLogic _maintenanceBussinessLogic;

        private readonly ICacheBusinessLogic _cacheManager;

        public bool UserCanChangePassword
        {
            get { return _userCanChangePassword; }
            set
            {
                _userCanChangePassword = value;
                RaisePropertyChanged(nameof(UserCanChangePassword));
            }
        }

        public SwitchUserVM(IMaintenanceBussinessLogic maintenanceBussinessLogic,
            ICacheBusinessLogic cacheManager)
        {
            _cacheManager = cacheManager;
            _maintenanceBussinessLogic = maintenanceBussinessLogic;
           
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ConfirmPasswordCompletedCommand = new RelayCommand<object>((args) => ConfirmPasswordCompleted(args));
            SwitchUserCommand = new RelayCommand(PerformRequiredActionForSwitchUser);
            ChangePasswordCommand = new RelayCommand(() => PerformAction(ChangePasswordAsync));
        }

        /// <summary>
        /// Change password if entered is pressed
        /// </summary>
        /// <param name="args"></param>
        private void ConfirmPasswordCompleted(object args)
        {
            if (Helper.IsEnterKey(args))
            {
                PerformAction(ChangePasswordAsync);
            }
        }

        private void PerformRequiredActionForSwitchUser()
        {
            CacheBusinessLogic.FramePriorSwitchUserNavigation = "SwitchUser";
            NavigateService.Instance.NavigateToLogout();
        }


        /// <summary>
        /// Method to change password
        /// </summary>
        /// <returns></returns>
        private async Task ChangePasswordAsync()
        {
            Success result;
            try
            {
                result = await _maintenanceBussinessLogic.ChangePassword(
               ChangePassword.ConfirmNewPassword,
               ChangePassword.Password);

                ShowNotification(result.Message, null, null, ApplicationConstants.ButtonConfirmationColor);
                if (result.IsSuccess)
                {
                    CacheBusinessLogic.Password = ChangePassword.Password;
                    NavigateService.Instance.NavigateToHome();
                }
            }
            finally
            {
                ChangePassword.ConfirmNewPassword = ChangePassword.Password = string.Empty;
            }
        }


        internal void ResetVM()
        {
            UserCanChangePassword = _cacheManager.UserCanChangePassword;
        }

    }
}
