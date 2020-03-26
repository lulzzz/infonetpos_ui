using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Globalization;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class GiveXTenderVM : VMBase
    {
        #region Private variables
        private bool _isCardNumberEnabled;
        private bool _isSwipedFromCard;
        private bool _askPin;
        private string _number;
        private string _amount;
        private string _cardPinData;
        private string _pin;
        private string _transactionType;
        private string _outStandingAmount;
        private string _tenderCode;
        #endregion

        #region Properties
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                RaisePropertyChanged(nameof(Number));
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public bool IsCardNumberEnabled
        {
            get { return _isCardNumberEnabled; }
            set
            {
                _isCardNumberEnabled = value;
                RaisePropertyChanged(nameof(IsCardNumberEnabled));
            }
        }

        public string Pin
        {
            get { return _pin; }
            set
            {
                _pin = value;
                RaisePropertyChanged(nameof(Pin));
            }
        }

        public bool AskPin
        {
            get { return _askPin; }
            set
            {
                _askPin = value;
                RaisePropertyChanged(nameof(AskPin));
            }
        }
        #endregion

        #region Commands
        public RelayCommand AcceptCommand { get; private set; }
        public RelayCommand GetSaleSummaryCommand { get; set; }
        #endregion

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;

        public GiveXTenderVM(ICheckoutBusinessLogic checkoutBusinessLogic,
            IReportsBussinessLogic reportsBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            _reportsBusinessLogic = reportsBusinessLogic;
            InitializeCommands();
            RegisterMessages();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<GiveXSelectedMessage>(this,
                "SetGiveXMessage", SetGiveXMessage);
            MessengerInstance.Register<CardSwipeInformation>(this,
                "PerformTransactionForGivex", SetSwipedCardDetails);
        }

        private void SetGiveXMessage(GiveXSelectedMessage message)
        {
            _tenderCode = message.TenderCode;
            _outStandingAmount = message.Amount != null ? message.Amount.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            _transactionType = message.TransactionType;
            Amount = message.OutStandingAmount;
            Number = new Helper().GetTrack2Data(message.CardNumber);
        }

        private void SetSwipedCardDetails(CardSwipeInformation cardDetails)
        {
            Number = cardDetails.CardNumber;
            _outStandingAmount = cardDetails.Amount;
            IsCardNumberEnabled = false;
            _isSwipedFromCard = true;
            _cardPinData = cardDetails.Pin;
            AskPin = cardDetails.AskPin;
        }

        private void InitializeCommands()
        {
            AcceptCommand = new RelayCommand(AcceptGiveXTender);
            GetSaleSummaryCommand = new RelayCommand(() => PerformAction(GetSaleSummary));
        }

        private async Task GetSaleSummary()
        {
            NavigateService.Instance.NavigateToTenderScreen();
        }

        private void AcceptGiveXTender()
        {
            MessengerInstance.Send(new CloseKeyboardMessage());
            if (ValidateData())
            {
                PerformAction(async () =>
                {
                    decimal amount = 0;
                    decimal? amountToSend = null;
                    decimal.TryParse(_outStandingAmount, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);

                    Number = new Helper().GetTrack2Data(Number);
                    var tenderSummary =
                     await _checkoutBusinessLogic.PaymentByGivex(Number,
                    string.IsNullOrEmpty(_outStandingAmount) ?
                        amountToSend : amount,
                    _transactionType, _tenderCode);
                    await PrintGiveXReceipt(tenderSummary.Report);

                    NavigateService.Instance.NavigateToSaleSummary();
                    MessengerInstance.Send(tenderSummary, "UpdateTenderSummary");
                });
            }
            else
            {
                // TODO: Ipsit - Get message from Smriti
                ShowNotification("",
                    null,
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
        }

        private async Task PrintGiveXReceipt(List<Report> reports)
        {
            PerformPrint(reports);
        }

        private bool ValidateData()
        {
            return !(_isSwipedFromCard && (Pin != _cardPinData && AskPin));
        }

        public void ReInitialize()
        {
            Number = string.Empty;
            Amount = string.Empty;
            IsCardNumberEnabled = true;
            _isSwipedFromCard = false;
            AskPin = false;
        }
    }
}
