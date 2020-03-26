using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Payment
{
    public class FleetVM : VMBase
    {
        private readonly IPaymentBussinessLogic _paymentBussinessLogic;
        private string _heading;
        private string _cardNumber;
        private string _amount;
        private string _swipedCardNumber;
        private string _authTokenForFleetPayment;
        private bool _isAcceptButtonEnabled;

        public bool IsAcceptButtonEnabled
        {
            get { return _isAcceptButtonEnabled; }
            set
            {
                _isAcceptButtonEnabled = value;
                RaisePropertyChanged(nameof(IsAcceptButtonEnabled));
            }
        }


        public string SwipedCardNumber
        {
            get { return _swipedCardNumber; }
            set
            {
                _swipedCardNumber = value;
                if (!string.IsNullOrEmpty(_swipedCardNumber) && AllowSwipe)
                {
                    CardNumber = new Helper().GetTrack2Data(SwipedCardNumber);
                }
            }
        }


        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = Helper.SelectAllDecimalValue(value, _amount);
                IsAcceptButtonEnabled = !string.IsNullOrEmpty(CardNumber)
                     && !string.IsNullOrEmpty(Amount);
                RaisePropertyChanged(nameof(Amount));
            }
        }
        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                IsAcceptButtonEnabled = !string.IsNullOrEmpty(CardNumber)
                    && !string.IsNullOrEmpty(Amount);
                RaisePropertyChanged(nameof(CardNumber));
            }
        }
        public string Heading
        {
            get { return _heading; }
            set
            {
                _heading = value;
                RaisePropertyChanged(nameof(Heading));
            }
        }
        public bool AllowSwipe { get; set; }

        public RelayCommand AcceptPaymentCommand { get; set; }
        public RelayCommand<object> EnterPressedOnAmountCommand { get; set; }

        public FleetVM(IPaymentBussinessLogic paymentBussinessLogic)
        {
            _paymentBussinessLogic = paymentBussinessLogic;
            InitializeCommands();
        }


        private void InitializeCommands()
        {
            AcceptPaymentCommand = new RelayCommand(() => PerformAction(AcceptPayment));
            EnterPressedOnAmountCommand = new RelayCommand<object>((s) => EnterPressedOnAmount(s));
        }

        private void EnterPressedOnAmount(object s)
        {
            if (Helper.IsEnterKey(s))
            {
                PerformAction(AcceptPayment);
            }
        }

        private async Task AcceptPayment()
        {
            var timer = new Stopwatch();
            timer.Restart();

            try
            {
                CacheBusinessLogic.AuthKey = _authTokenForFleetPayment;

                var response = await _paymentBussinessLogic.PaymentByFleet(
                  Helper.EncodeToBase64(CardNumber), Amount,
                  false);
                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send<CheckoutSummary>(response, "PaymentByFleet");
            }
            finally
            {
                CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
                timer.Stop();
                Log.Info(string.Format("Time taken in fleet payment is {0}ms ", timer.ElapsedMilliseconds));
            }
        }

        private void ClearCardNumberAndAmount()
        {
            Amount = string.Empty;
            CardNumber = string.Empty;
        }

        internal void ResetVM()
        {
            AllowSwipe = false;
            Heading = string.Empty;
            SwipedCardNumber = string.Empty;
            PerformAction(ValidateFleet);
            ClearCardNumberAndAmount();
        }

        private async Task ValidateFleet()
        {
            try
            {
                var response = await _paymentBussinessLogic.VerifyFleet();

                if (!string.IsNullOrEmpty(response.Message?.Message))
                {
                    ShowNotification(response.Message.Message,
                        null,
                        null,
                        null);
                }

                _authTokenForFleetPayment = CacheBusinessLogic.AuthKey;

                if (CacheBusinessLogic.FramePriorSwitchUserNavigation == "PaymentByFleet")
                {
                    CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                    CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
                }


                Heading = response.Caption;
                AllowSwipe = response.AllowSwipe;
            }
            catch (SwitchUserException ex)
            {
                ShowNotification(ex.Error.Message,
                        SwitchUserForPaymentByFleet,
                        SwitchUserForPaymentByFleet,
                        ApplicationConstants.ButtonWarningColor);
            }
        }
        private void SwitchUserForPaymentByFleet()
        {
            CacheBusinessLogic.FramePriorSwitchUserNavigation = "PaymentByFleet";
            NavigateService.Instance.NavigateToLogout();
        }
    }
}
