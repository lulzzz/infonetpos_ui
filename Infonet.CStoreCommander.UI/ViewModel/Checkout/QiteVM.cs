using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Diagnostics;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class QiteVM : VMBase
    {
        #region Private variables
        private string _bandMember;
        private bool _isSubmitQiteEnabled;
        private CheckoutSummary _checkoutSummary = new CheckoutSummary();
        private bool _isBandMemberNameVisible;
        private string _bandMemberName;
        private QiteValidate _qiteValidate;

        public string BandMemberName
        {
            get { return _bandMemberName; }
            set
            {
                _bandMemberName = value;
                IsBandMemberNameVisible = !string.IsNullOrEmpty(_bandMemberName);
                RaisePropertyChanged(nameof(BandMemberName));
            }
        }


        public bool IsBandMemberNameVisible
        {
            get { return _isBandMemberNameVisible; }
            set
            {
                _isBandMemberNameVisible = value;
                RaisePropertyChanged(nameof(IsBandMemberNameVisible));
            }
        }

        #endregion

        #region Properties
        public string BandMember
        {
            get { return _bandMember; }
            set
            {
                _bandMember = value;
                IsSubmitQiteEnabled = !string.IsNullOrEmpty(_bandMember);
                RaisePropertyChanged(nameof(BandMember));
            }
        }

        public bool IsSubmitQiteEnabled
        {
            get { return _isSubmitQiteEnabled; }
            set
            {
                _isSubmitQiteEnabled = value;
                RaisePropertyChanged(nameof(IsSubmitQiteEnabled));
            }
        }
        #endregion

        public RelayCommand ValidateQiteCommand { get; private set; }
        public RelayCommand GetBandMemberNameCommand { get; private set; }
        public RelayCommand ClosePopupCommand { get; private set; }

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        public QiteVM(ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            InitializeCommands();
        }

        public void ReInitialize(bool flag)
        {
            BandMember = string.Empty;
            IsBandMemberNameVisible = false;
            BandMemberName = string.Empty;
            _qiteValidate = null;
        }

        private void InitializeCommands()
        {
            ValidateQiteCommand = new RelayCommand(ValidateQite);
            GetBandMemberNameCommand = new RelayCommand(GetBandMemberName);
            ClosePopupCommand = new RelayCommand(ClosePopup);
        }

        private void ClosePopup()
        {
            PerformAction(async () =>
            {
                PopupService.PopupInstance.IsQitePopupOpen = false;
                PopupService.IsPopupOpen = false;
                MessengerInstance.Send(true,
                   "ReinitalizeQitePopup");
                var checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send(checkoutSummary);
                ReInitialize(true);
            });
        }

        private void GetBandMemberName()
        {
            PerformAction(async () =>
            {
                try
                {
                    PopupService.PopupInstance.IsPopupOpen = false;
                    PopupService.PopupInstance.IsQitePopupOpen = false;
                    _qiteValidate = await _checkoutBusinessLogic.ValidateQite(BandMember);
                    BandMemberName = _qiteValidate.BandMemberName;
                    PopupService.PopupInstance.IsPopupOpen = true;
                    PopupService.PopupInstance.IsQitePopupOpen = true;
                }
                catch (ApiDataException ex)
                {
                    ShowNotification(ex.Error.Message, OpenQitePopup, OpenQitePopup,
                      ApplicationConstants.ButtonWarningColor);
                }
            });
        }

        private void ValidateQite()
        {
            PerformAction(async () =>
           {
               PopupService.IsPopupOpen = false;
               PopupService.IsQitePopupOpen = false;
               var timer = new Stopwatch();
               timer.Restart();
               try
               {

                   if (_qiteValidate == null)
                   {
                       _qiteValidate = await _checkoutBusinessLogic.ValidateQite(BandMember);
                   }

                   _checkoutSummary.SaleSummary = _qiteValidate.SaleSummary;
                   _checkoutSummary.TenderSummary = _qiteValidate.TenderSummary;
                   NavigateService.Instance.NavigateToSaleSummary();
                   MessengerInstance.Send(_checkoutSummary);
               }
               catch (ApiDataException ex)
               {
                   ShowNotification(ex.Error.Message, OpenQitePopup, OpenQitePopup,
                       ApplicationConstants.ButtonWarningColor);
               }
               finally
               {
                   ReInitialize(true);
                   timer.Stop();
                   Log.Info(string.Format("Time taken in Validate Qite is {0}ms ", timer.ElapsedMilliseconds));
               }
           });
        }

        private void OpenQitePopup()
        {
            ReInitialize(true);

            PopupService.PopupInstance.IsPopupOpen = true;
            PopupService.PopupInstance.IsQitePopupOpen = true;
            PopupService.PopupInstance.CloseCommand = new RelayCommand(() =>
            {
                PopupService.PopupInstance.IsPopupOpen = false;
                PopupService.PopupInstance.IsQitePopupOpen = false;
                ReInitialize(true);
            });
        }
    }
}
