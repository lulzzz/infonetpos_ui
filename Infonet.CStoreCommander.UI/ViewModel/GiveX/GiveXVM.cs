using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.UI.Model.Stock;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Threading.Tasks;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.UI.Messages;
using System.Globalization;

namespace Infonet.CStoreCommander.UI.ViewModel.GiveX
{
    /// <summary>
    /// View model for GiveX Screen
    /// </summary>
    public class GiveXVM : VMBase
    {
        #region Private Variables
        private bool _isExistingvisible;
        private bool _isNewCardVisible;
        private bool _isDeactivateVisible;
        private bool _isActiveVisible;
        private bool _allowAdjustmentForGiveX;
        private bool _isSubmitButtonEnable;
        private bool _isCloseBatchEnabled;
        private string _stockCodeForGivexCard;
        #endregion

        private readonly IGiveXBussinessLogic _givexBussinessLogic;

        #region Public Variables
        public bool IsCloseBatchEnabled
        {
            get { return _isCloseBatchEnabled; }
            set
            {
                _isCloseBatchEnabled = value;
                RaisePropertyChanged(nameof(IsCloseBatchEnabled));
            }
        }
        public bool IsSubmitButtonEnable
        {
            get { return _isSubmitButtonEnable; }
            set
            {
                _isSubmitButtonEnable = value;
                RaisePropertyChanged(nameof(IsSubmitButtonEnable));
            }
        }
        public bool IsActiveVisible
        {
            get { return _isActiveVisible; }
            set
            {
                _isActiveVisible = value;
                RaisePropertyChanged(nameof(IsActiveVisible));
            }
        }
        public bool IsDeactivateVisible
        {
            get { return _isDeactivateVisible; }
            set
            {
                _isDeactivateVisible = value;
                RaisePropertyChanged(nameof(IsDeactivateVisible));
            }
        }
        public bool IsNewCardVisible
        {
            get { return _isNewCardVisible; }
            set
            {
                _isNewCardVisible = value;
                RaisePropertyChanged(nameof(IsNewCardVisible));
            }
        }
        public bool IsExistingCardVisible
        {
            get { return _isExistingvisible; }
            set
            {
                _isExistingvisible = value;
                RaisePropertyChanged(nameof(IsExistingCardVisible));
            }
        }
        public bool AllowAdjustmentForGiveX
        {
            get { return _allowAdjustmentForGiveX; }
            set
            {
                _allowAdjustmentForGiveX = value;
                RaisePropertyChanged(nameof(AllowAdjustmentForGiveX));
            }
        }
        #endregion

        #region Properties
        private string _givexCardNumber;
        private string _amount;
        private string _addAmount;
        private string _balance;

        public string Balance
        {
            get { return _balance; }
            set
            {
                _balance = Helper.SelectDecimalValue(value, _balance);
                RaisePropertyChanged(nameof(Balance));
            }
        }
        public string AddAmount
        {
            get { return _addAmount; }
            set
            {
                _addAmount = Helper.SelectDecimalValue(value, _addAmount);
                RaisePropertyChanged(nameof(AddAmount));
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = Helper.SelectDecimalValue(value, _amount);
                RaisePropertyChanged(nameof(Amount));
            }
        }
        public string GivexCardNumber
        {
            get { return _givexCardNumber; }
            set
            {
                _givexCardNumber = value;
                RaisePropertyChanged(nameof(GivexCardNumber));
            }
        }
        #endregion

        #region Commands
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CloseBatchCommand { get; set; }
        public RelayCommand ActivateCommand { get; set; }
        public RelayCommand DeactivateCommand { get; set; }
        public RelayCommand SetCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand OpenGivexReportCommand { get; set; }
        public RelayCommand<object> AddEnteredCommand { get; set; }
        public RelayCommand<object> CardNumberEnteredCommand { get; set; }
        public RelayCommand<object> BalanceEnteredCommand { get; set; }
        public RelayCommand<object> AmountEnteredCommand { get; set; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="givexBussinessLogic">GiveX Business logic Manager</param>
        public GiveXVM(IGiveXBussinessLogic givexBussinessLogic)
        {
            _givexBussinessLogic = givexBussinessLogic;
            MessengerInstance.Register<StockModel>(this,
                "AddGivexCardForSaleLine", AddGivexCardForSaleLine);
            InitilizeCommands();
            InitializeData();
        }

        /// <summary>
        /// Initializes the data 
        /// </summary>
        private void InitializeData()
        {
            AllowAdjustmentForGiveX = CacheBusinessLogic.AllowAdjustmentForGiveX;
            DisableExistingCard();
            DisableNewCard();
        }

        /// <summary>
        /// Initializes all the commands
        /// </summary>
        private void InitilizeCommands()
        {
            ActivateCommand = new RelayCommand(() => PerformAction(ActivateGivexCardAsync));
            DeactivateCommand = new RelayCommand(() => PerformAction(DeactivateGivexCardAsync));
            SetCommand = new RelayCommand(() => PerformAction(SetBalanceAsync));
            AddCommand = new RelayCommand(() => PerformAction(AddAmountAsync));
            SubmitCommand = new RelayCommand(() => PerformAction(SubmitGivexCardNumberAsync));
            CloseBatchCommand = new RelayCommand(() => PerformAction(CloseBatchAsync));
            AmountEnteredCommand = new RelayCommand<object>((args) => AmountEntered(args));
            AddEnteredCommand = new RelayCommand<object>((args) => AddAmountEntered(args));
            BalanceEnteredCommand = new RelayCommand<object>((args) => BalanceEntered(args));
            CardNumberEnteredCommand = new RelayCommand<object>((s) => CardNumberEntered(s));
            OpenGivexReportCommand = new RelayCommand(NavigateService.Instance.NavigateToGivexReport);
        }

        /// <summary>
        /// Method to handle enter event in Amount textbox
        /// </summary>
        /// <param name="args"></param>
        private void AmountEntered(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                //  MessengerInstance.Send(new CloseKeyboardMessage());
                PerformAction(ActivateGivexCardAsync);
            }
        }

        /// <summary>
        /// Method to handle enter event in Add Amount textbox
        /// </summary>
        /// <param name="args"></param>
        private void AddAmountEntered(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                // MessengerInstance.Send(new CloseKeyboardMessage());
                PerformAction(AddAmountAsync);
            }
        }

        /// <summary>
        /// Method to handle enter event in Balance textbox
        /// </summary>
        /// <param name="args"></param>
        private void BalanceEntered(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                //  MessengerInstance.Send(new CloseKeyboardMessage());
                PerformAction(SetBalanceAsync);
            }
        }

        /// <summary>
        /// Method to handle enter event in cardNumber textbox
        /// </summary>
        /// <param name="args"></param>
        private void CardNumberEntered(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                MessengerInstance.Send(new CloseKeyboardMessage());
                PerformAction(SubmitGivexCardNumberAsync);
            }
        }

        /// <summary>
        /// Adds new GiveX card for Sale 
        /// </summary>
        /// <param name="stockModel"></param>
        private void AddGivexCardForSaleLine(StockModel stockModel)
        {
            IsSubmitButtonEnable = false;
            IsCloseBatchEnabled = false;
            _stockCodeForGivexCard = stockModel.StockCode;
            EnableNewCard();
        }

        /// <summary>
        /// Method to add amount 
        /// </summary>
        /// <returns>sale object</returns>
        private async Task AddAmountAsync()
        {
            var response = await _givexBussinessLogic.AddAmount(new Helper().GetTrack2Data(GivexCardNumber),
                StringToDecimalConverter(AddAmount), _stockCodeForGivexCard);
            CleanUpOperation(response);
        }

        /// <summary>
        /// method to set balance 
        /// </summary>
        /// <returns>sale object</returns>
        private async Task SetBalanceAsync()
        {
            var response = await _givexBussinessLogic.SetAmount(
                GivexCardNumber,
                StringToDecimalConverter(Balance),
                _stockCodeForGivexCard);
            CleanUpOperation(response);
        }

        /// <summary>
        /// method to deactivate GiveX Card
        /// </summary>
        /// <returns>sale </returns>
        private async Task DeactivateGivexCardAsync()
        {
            var response = await _givexBussinessLogic.DeactivateCard(
                GivexCardNumber,
                StringToDecimalConverter(Balance), _stockCodeForGivexCard);
            CleanUpOperation(response);
        }

        /// <summary>
        /// method to activate GiveX card
        /// </summary>
        /// <returns>sale object</returns>
        private async Task ActivateGivexCardAsync()
        {
            if (CacheBusinessLogic.IsGiveXCalledFromAddStock)
            {
                MessengerInstance.Send<GiftCard>(new GiftCard
                {
                    CardNumber = GivexCardNumber,
                    Price = Amount,
                    Quantity = 1
                }, "RegisterGiftCardModelFromGivexPage");
                ReInitialize();
            }
            else
            {
                var response = await _givexBussinessLogic.ActivateCard(
                    GivexCardNumber,
                    StringToDecimalConverter(Amount), _stockCodeForGivexCard);
                CleanUpOperation(response);
            }
        }

        /// <summary>
        /// Method to reset all values and navigating to home
        /// </summary>
        /// <param name="response"></param>
        private void CleanUpOperation(GivexSaleCard response)
        {
            var saleModel = response.Sale.ToModel();
            PerformPrint(response.Report);
            MessengerInstance.Send(saleModel, "UpdateSale");
            ReInitialize();
            NavigateService.Instance.NavigateToHome();
        }

        /// <summary>
        /// Method to close batch
        /// </summary>
        /// <returns>redirects to home</returns>
        private async Task CloseBatchAsync()
        {
            var report = await _givexBussinessLogic.CloseBatch();
            PerformPrint(report);

            ReInitialize();
            NavigateService.Instance.NavigateToHome();
        }

        /// <summary>
        /// Method to submit GiveX card number
        /// </summary>
        /// <returns></returns>
        private async Task SubmitGivexCardNumberAsync()
        {
            // Only select and display the Card number 
            GivexCardNumber = new Helper().GetTrack2Data(GivexCardNumber);

            var response = await _givexBussinessLogic.CardBalance(GivexCardNumber);
            if (response.Balance > 0)
            {
                Balance = response.Balance.ToString(CultureInfo.InvariantCulture);
                EnableExistingCard();
            }
            else //TODO: Change This condition once actual response is determined.
            {
                EnableNewCard();
            }
        }

        /// <summary>
        /// Utility Method to convert string to decimal
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        private decimal StringToDecimalConverter(string targetValue)
        {
            decimal result = 0M;
            decimal.TryParse(targetValue, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            return result;
        }

        /// <summary>
        /// Method to reset all values of GiveX
        /// </summary>
        internal void ReInitialize()
        {
            AllowAdjustmentForGiveX = CacheBusinessLogic.AllowAdjustmentForGiveX;
            IsCloseBatchEnabled = IsSubmitButtonEnable = true;
            GivexCardNumber = AddAmount = Amount = Balance = string.Empty;
            DisableExistingCard();
            DisableNewCard();
            GetGivexStockCode();
        }

        private void GetGivexStockCode()
        {
            PerformAction(async () =>
            {
                if (!CacheBusinessLogic.IsGiveXCalledFromAddStock
                && string.IsNullOrEmpty(_stockCodeForGivexCard))
                {
                    try
                    {
                        _stockCodeForGivexCard = await _givexBussinessLogic.GetGiveXStockCode();
                    }
                    catch (UserNotAuthorizedException ex)
                    {
                        Log.Warn(ex);
                        NavigateService.Instance.NavigateToLogin();
                    }
                    catch (InternalServerException ex)
                    {
                        Log.Warn(ex);
                        ShowNotification(ex.Error.Message,
                            NavigateService.Instance.NavigateToHome,
                            NavigateService.Instance.NavigateToHome,
                            ApplicationConstants.ButtonWarningColor);
                    }
                    catch (ApiDataException ex)
                    {
                        Log.Warn(ex);
                        ShowNotification(ex.Message,
                            NavigateService.Instance.NavigateToHome,
                            NavigateService.Instance.NavigateToHome,
                            ApplicationConstants.ButtonWarningColor);
                    }
                    catch (Exception ex)
                    {
                        Log.Info(Message, ex);
                        ShowNotification(ApplicationConstants.SomethingBadHappned,
                            NavigateService.Instance.NavigateToHome,
                            NavigateService.Instance.NavigateToHome,
                            ApplicationConstants.ButtonWarningColor);
                    }
                }
            });
        }

        #region Methods for visibility
        private void EnableExistingCard()
        {
            IsDeactivateVisible = IsExistingCardVisible = true;
            DisableNewCard();
        }

        private void EnableNewCard()
        {
            IsNewCardVisible = IsActiveVisible = true;
            DisableExistingCard();
        }

        private void DisableExistingCard()
        {
            IsDeactivateVisible = IsExistingCardVisible = false;
        }

        private void DisableNewCard()
        {
            IsNewCardVisible = IsActiveVisible = false;
        }
        #endregion
    }
}
