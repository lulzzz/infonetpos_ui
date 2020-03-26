using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class SiteVM : VMBase
    {
        private SiteValidate _siteValidateResponse;
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = Helper.SelectIntegers(value);
                    RaisePropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        #region Private variables
        private string _treatyNumber;
        private string _customerName;
        private string _permitNumber;
        private bool _isPermitNumberVisible;
        private CheckoutSummary _checkoutSummary = new CheckoutSummary();
        private bool _isTreatyNumberEnable;
        private bool _isCustomerNameEnable;

        public bool IsCustomerNameEnable
        {
            get { return _isCustomerNameEnable; }
            set
            {
                _isCustomerNameEnable = value;
                RaisePropertyChanged(nameof(IsCustomerNameEnable));
            }
        }


        public bool IsTreatyNumberEnable
        {
            get { return _isTreatyNumberEnable; }
            set
            {
                _isTreatyNumberEnable = value;
                RaisePropertyChanged(nameof(IsTreatyNumberEnable));
            }
        }

        #endregion

        #region Properties
        public bool IsPermitNumberVisible
        {
            get { return _isPermitNumberVisible; }
            set
            {
                _isPermitNumberVisible = value;
                RaisePropertyChanged(nameof(IsPermitNumberVisible));
            }
        }

        public string TreatyNumber
        {
            get { return _treatyNumber; }
            set
            {
                _treatyNumber = value;
                
                RaisePropertyChanged(nameof(TreatyNumber));
            }
        }

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                RaisePropertyChanged(nameof(CustomerName));
            }
        }

        public string PermitNumber
        {
            get { return _permitNumber; }
            set
            {
                _permitNumber = value;
                RaisePropertyChanged(nameof(PermitNumber));
            }
        }
        #endregion

        #region Commands
        public RelayCommand ValidateSiteCommand { get; private set; }
        public RelayCommand RemoveTaxCommand { get; private set; }
        public RelayCommand GetSaleSummaryCommand { get; set; }
        public RelayCommand GetTreatyNameCommand { get; set; }
        public RelayCommand<object> EnterPressedOnPhoneNumberCommand { get; set; }
        public RelayCommand PerformFngtrCommand { get; set; }
        #endregion

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;

        public SiteVM(ICheckoutBusinessLogic checkoutBusinessLogic , ICacheBusinessLogic cacheBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            _cacheBusinessLogic = cacheBusinessLogic;
            RegisterMessages();
            InitializeCommands();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<SITEMessage>(this,
                "PermitNumberVisible",
                (SITEMessage) =>
                {
                    TreatyNumber = SITEMessage.TreatyNumber;
                    CustomerName = SITEMessage.TreatyName;

                    if (!string.IsNullOrEmpty(TreatyNumber))
                    {
                        IsTreatyNumberEnable = false;
                        IsCustomerNameEnable = false;
                    }

                    IsPermitNumberVisible = SITEMessage.PermitNumberVisible;
                });
        }

        private void InitializeCommands()
        {
            RemoveTaxCommand = new RelayCommand(RemoveSiteTax);
            ValidateSiteCommand = new RelayCommand(ValidateSite);
            GetSaleSummaryCommand = new RelayCommand(() => PerformAction(GetSaleSummary));
            GetTreatyNameCommand = new RelayCommand(() => PerformAction(GetTreatyName));
            PerformFngtrCommand = new RelayCommand(FNGTR);
            EnterPressedOnPhoneNumberCommand = new RelayCommand<object>(EnterPressedOnPhoneNumber);
        }

        private async Task GetTreatyName()
        {
            try
            {
                if (CacheBusinessLogic.RequireToGetCustomerName)
                {
                    //TODO:Set capture Method parameter once confirmed from API side
                    CustomerName = await _checkoutBusinessLogic.GetTreatyName(TreatyNumber, "0");
                }
                else
                {
                    ValidateSite();
                }
            }
            catch (ApiDataException ex)
            {
                TreatyNumber = string.Empty;
                throw;
            }
        }

        private async Task GetSaleSummary()
        {
            PhoneNumber = string.Empty;
            OpenOrCloseFngtrPopup(false, string.Empty);
            var checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
            NavigateService.Instance.NavigateToTenderScreen();
            MessengerInstance.Send(checkoutSummary);
        }

        public void ReInitialize()
        {
            IsTreatyNumberEnable = true;
            IsCustomerNameEnable = true;
            CustomerName = string.Empty;
            PermitNumber = string.Empty;
            TreatyNumber = string.Empty;
            MessengerInstance.Send(true, "ResetSaleSummary");
        }

        private void ValidateSite()
        {
            PerformAction(async () =>
            {
                var timer = new Stopwatch();
                timer.Restart();

                try
                {
                    // TODO: Find a way to get the capture method
                    _siteValidateResponse = await _checkoutBusinessLogic.ValidateSite(TreatyNumber,
                       1, CustomerName, PermitNumber);
                    CacheBusinessLogic.RequireSignature = _siteValidateResponse.RequireSignature;
                    PerformSITEAction();
                }
                finally
                {
                    timer.Stop();
                    Log.Info(string.Format("Time taken in Validate Site is {0}ms ", timer.ElapsedMilliseconds));
                }
            });
        }

        private void PerformSITEAction()
        {

            _cacheBusinessLogic.TreatyNumber = TreatyNumber;
            _cacheBusinessLogic.TreatyName = CustomerName;

            _checkoutSummary.SaleSummary = _siteValidateResponse.SaleSummary;
            _checkoutSummary.TenderSummary = _siteValidateResponse.TenderSummary;
            if (_siteValidateResponse.IsFrmOverrideLimit)
            {
                NavigateService.Instance.NavigateToOverrideLimitScreen();
            }
            else if (_siteValidateResponse.IsFngtr)
            {
                PhoneNumber = string.Empty;
                OpenOrCloseFngtrPopup(true, _siteValidateResponse.FngtrMessage);
            }
            else
            {
                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send(_checkoutSummary);
            }
        }

        private void OpenOrCloseFngtrPopup(bool openPopup, string message)
        {
            PopupService.PopupInstance.IsPopupOpen = openPopup;
            PopupService.PopupInstance.IsFngtrPopupOpen = openPopup;
            PopupService.PopupInstance.Title = message;
            PopupService.PopupInstance.CloseCommand = new RelayCommand(() =>
            {
                OpenOrCloseFngtrPopup(false, string.Empty);
            });
        }

        private void RemoveSiteTax()
        {
            PerformAction(async () =>
            {
                // TODO: Find a way to get the capture method
                var siteTax = await _checkoutBusinessLogic.RemoveSiteTax(TreatyNumber,
                    1, CustomerName, PermitNumber);
                _checkoutSummary.SaleSummary = siteTax.SaleSummary;
                _checkoutSummary.TenderSummary = siteTax.TenderSummary;
                if (siteTax.IsFrmOverrideLimit)
                {
                    NavigateService.Instance.NavigateToOverrideLimitScreen();
                }
                else
                {
                    NavigateService.Instance.NavigateToTenderScreen();
                    MessengerInstance.Send(_checkoutSummary);
                }
            });
        }


        private void FNGTR()
        {
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                OpenOrCloseFngtrPopup(false, string.Empty);
                PerformAction(async () =>
               {
                   _siteValidateResponse = await _checkoutBusinessLogic.FNGTR(PhoneNumber);
                   PerformSITEAction();
               });
            }
        }
        private void EnterPressedOnPhoneNumber(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                FNGTR();
                MessengerInstance.Send(new CloseKeyboardMessage());
            }
        }

    }
}
