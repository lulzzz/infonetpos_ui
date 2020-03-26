using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class AiteVM : VMBase
    {
        #region Private variables
        private bool _aiteValidateEnabled;
        private bool _affixBarCodeEnabled;
        private bool _isAiteDoneEnabled;
        private bool _cardNumberEnabled;
        private bool _isCardNumberEntered;
        private bool _isSubmitTreatyEnabled;
        private string _cardNumber;
        private string _cardholderName;
        private string _barCode;
        private string _treatyNumber;
        private bool _isGstPstTaxExemptEnabled;

        public bool IsGstPstTaxExemptEnabled
        {
            get { return _isGstPstTaxExemptEnabled; }
            set
            {
                _isGstPstTaxExemptEnabled = value;
                RaisePropertyChanged(nameof(IsGstPstTaxExemptEnabled));
            }
        }


        private AiteValidate _aiteValidate;
        private CheckoutSummary _checkoutSummary = new CheckoutSummary();
        #endregion

        #region Properties
        public bool AiteValidateEnabled
        {
            get { return _aiteValidateEnabled; }
            set
            {
                _aiteValidateEnabled = value;
                RaisePropertyChanged(nameof(AiteValidateEnabled));
            }
        }

        public bool AffixBarCodeEnabled
        {
            get { return _affixBarCodeEnabled; }
            set
            {
                _affixBarCodeEnabled = value;
                RaisePropertyChanged(nameof(AffixBarCodeEnabled));
            }
        }

        public bool IsAiteDoneEnabled
        {
            get { return _isAiteDoneEnabled; }
            set
            {
                _isAiteDoneEnabled = value;
                RaisePropertyChanged(nameof(IsAiteDoneEnabled));
            }
        }

        public bool CardNumberEnabled
        {
            get { return _cardNumberEnabled; }
            set
            {
                _cardNumberEnabled = value;
                RaisePropertyChanged(nameof(CardNumberEnabled));
            }
        }

        public bool IsSubmitTreatyEnabled
        {
            get { return _isSubmitTreatyEnabled; }
            set
            {
                _isSubmitTreatyEnabled = value;
                RaisePropertyChanged(nameof(IsSubmitTreatyEnabled));
            }
        }

        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                _isCardNumberEntered = true;
                EnableDisableAiteButtons();
                RaisePropertyChanged(nameof(CardNumber));
            }
        }

        public string CardholderName
        {
            get { return _cardholderName; }
            set
            {
                _cardholderName = value;
                RaisePropertyChanged(nameof(CardholderName));
            }
        }

        public string BarCode
        {
            get { return _barCode; }
            set
            {
                _barCode = value;
                _isCardNumberEntered = false;
                EnableDisableAiteButtons();
                RaisePropertyChanged(nameof(BarCode));
            }
        }

        public string TreatyNumber
        {
            get { return _treatyNumber; }
            set
            {
                _treatyNumber = value;
                IsSubmitTreatyEnabled = !string.IsNullOrEmpty(_treatyNumber);
                RaisePropertyChanged(nameof(TreatyNumber));
            }
        }
        #endregion

        #region Commands
        public RelayCommand ValidateAiteCommand { get; private set; }
        public RelayCommand OpenGstPstPopupCommand { get; private set; }
        public RelayCommand ValidateGstPstCommand { get; private set; }
        public RelayCommand AiteDoneCommand { get; private set; }
        public RelayCommand AffixBarCodeCommand { get; private set; }
        public RelayCommand GetSaleSummaryCommand { get; set; }
        #endregion

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        public AiteVM(ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ValidateAiteCommand = new RelayCommand(ValidateAite);
            OpenGstPstPopupCommand = new RelayCommand(OpenGstPstPopup);
            ValidateGstPstCommand = new RelayCommand(VaidateGstPst);
            AiteDoneCommand = new RelayCommand(CompleteAite);
            AffixBarCodeCommand = new RelayCommand(AffixBarCode);
            GetSaleSummaryCommand = new RelayCommand(() => PerformAction(GetSaleSummary));
        }


        private async Task GetSaleSummary()
        {
            var checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
            NavigateService.Instance.NavigateToTenderScreen();
            MessengerInstance.Send(new CloseKeyboardMessage());
            MessengerInstance.Send(checkoutSummary);
        }

        public void ReInitialize()
        {
            AffixBarCodeEnabled = false;
            IsAiteDoneEnabled = false;
            CardNumberEnabled = true;
            IsSubmitTreatyEnabled = false;
            CardNumber = string.Empty;
            CardholderName = string.Empty;
            BarCode = string.Empty;
            TreatyNumber = string.Empty;
            MessengerInstance.Send(true, "ResetSaleSummary");
            AiteValidateEnabled = IsGstPstTaxExemptEnabled = true;
        }

        private void EnableDisableAiteButtons()
        {
            IsAiteDoneEnabled = !string.IsNullOrEmpty(CardholderName);
        }

        private void ValidateAite()
        {
            PerformAction(async () =>
            {
                var timer = new Stopwatch();
                timer.Restart();

                try
                {
                    _aiteValidate = await _checkoutBusinessLogic.ValidateAITE(CardNumber,
                        BarCode, _isCardNumberEntered);
                    ProcessAite();
                }
                catch (Exception)
                {
                    ReInitialize();
                    throw;
                }
                finally
                {
                    timer.Stop();
                    Log.Info(string.Format("Time taken in Validate Aite is {0}ms ", timer.ElapsedMilliseconds));
                }
            });
        }

        private void ProcessAite()
        {
            _checkoutSummary.SaleSummary = _aiteValidate.SaleSummary;
            _checkoutSummary.TenderSummary = _aiteValidate.TenderSummary;
            CardholderName = _aiteValidate.CardHolderName;
            IsGstPstTaxExemptEnabled = CardNumberEnabled = false;
            BarCode = _aiteValidate.BarCode;
            CardNumber = _aiteValidate.CardNumber;
            AiteValidateEnabled = false;
            AffixBarCodeEnabled = string.IsNullOrEmpty(BarCode);
            IsAiteDoneEnabled = true;
        }

        private void OpenGstPstPopup()
        {
            PopupService.IsPopupOpen = true;
            PopupService.IsGstPstPopupOpen = true;
            TreatyNumber = string.Empty;
            PopupService.CloseCommand = new RelayCommand(() =>
            {
                PopupService.IsPopupOpen = false;
                PopupService.IsGstPstPopupOpen = false;
            });
        }

        private void VaidateGstPst()
        {
            if (!string.IsNullOrEmpty(TreatyNumber))
            {
                PerformAction(async () =>
                {
                    PopupService.IsGstPstPopupOpen = false;
                    PopupService.IsPopupOpen = false;
                    _aiteValidate = await _checkoutBusinessLogic.GstPstTaxExempt(TreatyNumber);
                    ProcessAite();
                    NavigateService.Instance.NavigateToSaleSummary();
                    MessengerInstance.Send(new CloseKeyboardMessage());
                    MessengerInstance.Send(_checkoutSummary);
                });
            }
        }

        private void CompleteAite()
        {
            if (_aiteValidate.IsOverLimit)
            {
                NavigateService.Instance.NavigateToOverLimitScreen();
            }
            else
            {
                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send(_checkoutSummary);
            }
            MessengerInstance.Send(new CloseKeyboardMessage());
        }

        private void AffixBarCode()
        {
            if (!string.IsNullOrEmpty(BarCode))
            {
                PerformAction(async () =>
                {
                    await _checkoutBusinessLogic.AffixBarcode(CardNumber,
                        BarCode);
                });
            }
        }

    }
}
