using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Kickback;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class FleetTenderVM : VMBase
    {
        private string _cardNumber;
        private string _expiryDate;
        private List<string> _languages;
        private int _selectedLanguageIndex;
        private bool _isAcceptButtonEnabled;
        private List<string> _promptMessages;
        private List<Error> _validationMessages = new List<Error>();
        private CardSwipeInformation _cardSwipeInformation;
        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;
        private readonly IKickBackBusinessLogic _kickBackBusinessLogic;
        private string _transactionType;
        private bool _performAptTender;
        private Dictionary<string, string> _messageDictionary = new Dictionary<string, string>();
        private string _tenderCode;
        private string _poNumber;
        private string _swipedCardNumber;
        private string _inputPinNumber;
        private string _amount;
        private List<int> _year;
        private List<int> _month;
        private bool _processForAmount;
        private bool _isGasKing { get; set; }
        private string _kickBackValue { get; set; }
        private double _kickbackPoints { get; set; }
        private bool _userInputForKickback;
        private bool _isFleet;
        private bool _isKickBackLinked;
        private CheckoutSummary _checkoutSummary;
        private bool _isInvalidLoyaltyCard;

        public List<int> Month
        {
            get { return _month; }
            set
            {
                _month = value;
                RaisePropertyChanged(nameof(Month));
            }
        }


        public List<int> Year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged(nameof(Year));
            }
        }


        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    RaisePropertyChanged(nameof(Amount));
                }
            }
        }

        private Stopwatch _timer = new Stopwatch();

        public bool IsAcceptButtonEnabled
        {
            get { return _isAcceptButtonEnabled; }
            set
            {
                if (_isAcceptButtonEnabled != value)
                {
                    _isAcceptButtonEnabled = value;
                    RaisePropertyChanged(nameof(IsAcceptButtonEnabled));
                }
            }
        }

        public int SelectedLanguageIndex
        {
            get { return _selectedLanguageIndex; }
            set
            {
                if (_selectedLanguageIndex != value)
                {
                    _selectedLanguageIndex = value;
                    RaisePropertyChanged(nameof(SelectedLanguageIndex));
                }
            }
        }
        public List<string> Languages
        {
            get { return _languages; }
            set
            {
                if (_languages != value)
                {
                    _languages = value;
                    RaisePropertyChanged(nameof(Languages));
                }
            }
        }
        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                if (_cardNumber != value)
                {
                    _cardNumber = value;
                    if (!CacheBusinessLogic.IsFleetCardRequired)
                    {
                        IsAcceptButtonEnabled = true;
                    }
                    else
                    {
                        IsAcceptButtonEnabled = !string.IsNullOrEmpty(_cardNumber);
                    }
                    RaisePropertyChanged(nameof(CardNumber));
                }
            }
        }

        public RelayCommand BackCommand { get; set; }
        public RelayCommand<object> ExpiryDateSelectedCommand { get; set; }
        public RelayCommand<object> EnterPressedOnTextBoxCommand { get; set; }
        public RelayCommand CompleteFleetPaymentCommand { get; set; }
        public RelayCommand CardNumberEnteredCommand { get; set; }

        public FleetTenderVM(ICheckoutBusinessLogic checkoutBusinessLogic,
             IKickBackBusinessLogic kickBackBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            _kickBackBusinessLogic = kickBackBusinessLogic;
            InitializeCommands();
            UnregisterMessage();
            RegisterMessages();
        }

        private void UnregisterMessage()
        {
            MessengerInstance.Unregister<SetFleetMessage>(this, "SetFleetMessage",
                 SetFleetMessages);
            MessengerInstance.Unregister<CardSwipeInformation>(this, "FleetCardSwiped",
               PaymentByFleetTenderCardSwiped);
            MessengerInstance.Unregister<CardSwipeInformation>(this, "ShowPrivateRestrictionsForAccount", ShowPrivateRestrictionsForAccount);
        }

        private void SetFleetMessages(SetFleetMessage message)
        {
            _swipedCardNumber = message.CardNumber;
            _tenderCode = message.TenderCode;
            _transactionType = message.TransactionType;
            _performAptTender = message.PerformAptTender;
            _kickbackPoints = message.KickbackPoints;
            _kickBackValue = message.KickBackValue;
            _isGasKing = message.IsGasKing;
            if (message.Amount == "" || message.Amount == null)
            {
                Amount = message.OutStandingAmount;
            }
            else
            {
                Amount = message.Amount;
            }
            _isFleet = message.IsFleet;
            _isKickBackLinked = message.IsKickBackLinked;
            _isInvalidLoyaltyCard = message.IsInvalidLoyaltyCard;
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<SetFleetMessage>(this, "SetFleetMessage", SetFleetMessages);
            MessengerInstance.Register<CardSwipeInformation>(this, "FleetCardSwiped", PaymentByFleetTenderCardSwiped);
            MessengerInstance.Register<CardSwipeInformation>(this, "ShowPrivateRestrictionsForAccount", ShowPrivateRestrictionsForAccount);
        }

        private void ShowPrivateRestrictionsForAccount(CardSwipeInformation cardInfo)
        {
            ResetVM();
            _swipedCardNumber = cardInfo?.CardNumber;
            _cardSwipeInformation = cardInfo;
            _processForAmount = true;
            _inputPinNumber = _cardSwipeInformation.Pin;
            AskForPin(_cardSwipeInformation.Pin, true);
        }

        private void InitializeCommands()
        {
            EnterPressedOnTextBoxCommand = new RelayCommand<object>(EnterPressedOnTextBox);
            BackCommand = new RelayCommand(NavigateService.Instance.NavigateToTenderScreen);
            ExpiryDateSelectedCommand = new RelayCommand<object>(ExpiryDateSelected);
            CompleteFleetPaymentCommand = new RelayCommand(() => PerformAction(CompleteFleetPayment));
            CardNumberEnteredCommand = new RelayCommand(CardNumberEntered);
            PopupService.PopupInstance.YesCommandOfPoNumberPopup = new RelayCommand(() =>
            {
                PerformAskForPin(_inputPinNumber);
            });

            PopupService.PopupInstance.NoCommandOfPoNumberPopup = new RelayCommand(() =>
            {
                ClosePopupWithTextBox();
                ShowNotification(ApplicationConstants.InvalidPin, NavigateToTender,
                        NavigateToTender,
                        ApplicationConstants.ButtonWarningColor);
            });
        }

        private void CardNumberEntered()
        {
            CardNumber = new Helper().GetTrack2Data(CardNumber);
        }

        private void EnterPressedOnTextBox(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                PopupService.PopupInstance.YesCommandOfPoNumberPopup.Execute(null);
            }
        }

        private async Task CompleteFleetPayment()
        {
            if (!string.IsNullOrEmpty(CardNumber))
            {
                _cardSwipeInformation = await _checkoutBusinessLogic.GetCardInformation(
                  Helper.EncodeToBase64(CardNumber), _transactionType);
                _inputPinNumber = _cardSwipeInformation.Pin;
                AskForPin(_cardSwipeInformation.Pin);
            }
            else
            {
                PaymentByCard();
            }
        }

        private void PaymentByFleetTenderCardSwiped(CardSwipeInformation cardSwipeInformation)
        {
            _cardSwipeInformation = cardSwipeInformation;
            _inputPinNumber = _cardSwipeInformation.Pin;

            PerformKickBack();
        }

        #region KickBack
        private void PerformKickBack()
        {
            if (_isInvalidLoyaltyCard)
            {
                ShowNotification(ApplicationConstants.InvalidLoyaltyCard,
                    () =>
                    {
                        AskForPin(_cardSwipeInformation.Pin);
                    },
                    () =>
                    {
                        AskForPin(_cardSwipeInformation.Pin);
                    });
            }
            else if (_isKickBackLinked && _kickbackPoints >= CacheBusinessLogic.KickbackRedeemMsg)
            {
                VerifyKickBack();
            }
            else
            {
                AskForPin(_cardSwipeInformation.Pin);
            }
        }

        private void VerifyKickBack()
        {
            SetKickBackBalanceInCache(_kickBackValue);
            VerifyKickBack(_kickbackPoints, _kickBackValue);
        }

        private void KickbackNumberEntered(object s)
        {
            if (Helper.IsEnterKey(s))
            {
                CloseKickBackNumberPopup();
                VerifyKickBack();
            }
        }

        private void CloseKickBackNumberPopup()
        {
            PopupService.IsKickbackNumberPopupOpen = false;
            PopupService.IsPopupOpen = false;
        }

        private void VerifyKickBack(double balancePoints, string value)
        {
            var kickBackMessage = string.Format(ApplicationConstants.VerifyKickbackMessage,
                balancePoints, value);

            ShowConfirmationMessage(kickBackMessage,
             () =>
             {
                 CloseKickBackNumberPopup();
                 CheckKickBackResponse(true);
             },
            () =>
            {
                CloseKickBackNumberPopup();
                CheckKickBackResponse(false);
            },
            () =>
            {
                CloseKickBackNumberPopup();
                CheckKickBackResponse(false);
            });
        }

        private async Task CheckKickBackResponse(bool response)
        {
            _userInputForKickback = response;
            CacheBusinessLogic.IsKickBack = true;
            if (!response)
            {
                CacheBusinessLogic.KickbackAmount = null;
            }

            PerformAction(async () =>
            {
                _checkoutSummary = await _checkoutBusinessLogic.SaleSummary(false, false,
                    CacheBusinessLogic.KickbackAmount);

                var checkKickBackresponse = await _kickBackBusinessLogic.CheckKickBackResponse(response);

                AskForPin(_cardSwipeInformation.Pin);
            });
        }
        #endregion

        private void SetKickBackBalanceInCache(string value)
        {
            CacheBusinessLogic.KickbackAmount = Helper.GetDoubleValue(value);
        }

        private void PaymentByCard()
        {
            PerformAction(async () =>
            {
                try
                {
                    var tenderSummary = new TenderSummary();
                    if (!string.IsNullOrEmpty(_swipedCardNumber))
                    {
                        tenderSummary = await _checkoutBusinessLogic.
                      PaymentByCard(_tenderCode,Amount, _transactionType,
                       Helper.EncodeToBase64(_swipedCardNumber), _poNumber);
                    }
                    else if (_cardSwipeInformation != null)
                    {
                        tenderSummary = await _checkoutBusinessLogic.
                              PaymentByCard(_tenderCode,Amount, _transactionType,
                             Helper.EncodeToBase64(CardNumber), _poNumber);
                        _cardSwipeInformation = null;
                    }
                    else
                    {
                        tenderSummary = await _checkoutBusinessLogic.UpdateTender(_tenderCode, null, _transactionType);
                    }
                    _swipedCardNumber = string.Empty;
                    NavigateToTender();
                    MessengerInstance.Send(tenderSummary, "UpdateTenderSummary");
                }
                finally
                {
                    _timer.Stop();
                    Log.Info(string.Format("Time taken in saving profile prompt and payment by card call is {0}ms ",
                        _timer.ElapsedMilliseconds));
                }
            });
        }

        private void AskForPin(string pin, bool processForAmount = false)
        {
            if (_cardSwipeInformation.AskPin)
            {
                if (!PopupService.PopupInstance.IsPopupOpen)
                {
                    PopupService.PopupInstance.IsPopupOpen = true;
                    PopupService.PopupInstance.IsPopupWithTextBoxOpen = true;
                    PopupService.PopupInstance.PasswordRevelMode = false;
                    PopupService.PopupInstance.Title = ApplicationConstants.EnterPinNumber;
                }
            }
            else
            {
                ShowPOMessages();
            }
        }

        private void PerformAskForPin(string pin)
        {
            if (PopupService.PopupInstance.TextValueOfPopupWithTextBox.Equals(pin))
            {
                ClosePopupWithTextBox();
                ShowPOMessages();
            }
            else
            {
                ClosePopupWithTextBox();
                ShowNotification(ApplicationConstants.InvalidPin,
                    NavigateToTender,
                    NavigateToTender,
                    ApplicationConstants.ButtonWarningColor);
            }
        }

        private void ShowPOMessages()
        {
            if (!string.IsNullOrEmpty(_cardSwipeInformation.PoMessage))
            {
                PopupService.PopupInstance.Title = _cardSwipeInformation.PoMessage;
                PopupService.PopupInstance.IsPopupOpen = true;
                PopupService.PopupInstance.IsPopupWithTextBoxOpen = true;
                PopupService.PopupInstance.PasswordRevelMode = false;

                PopupService.PopupInstance.YesCommandOfPoNumberPopup = new RelayCommand(() =>
                {
                    ShowValidationMessage();
                });

                PopupService.PopupInstance.NoCommandOfPoNumberPopup = new RelayCommand(() =>
                {
                    ClosePopupWithTextBox();
                    var popupMessage = string.Format(ApplicationConstants.MissingRequired,
                       _cardSwipeInformation.ProfileId, ApplicationConstants.PORequired);

                    ShowNotification(popupMessage, NavigateToTender, NavigateToTender,
                        ApplicationConstants.ButtonWarningColor);
                });
            }
            else
            {
                ShowValidationMessages();
            }
        }


        private void ShowValidationMessage()
        {
            if (!string.IsNullOrEmpty(PopupService.PopupInstance.TextValueOfPopupWithTextBox))
            {
                _messageDictionary.Add(PopupService.PopupInstance.Title,
                    PopupService.PopupInstance.TextValueOfPopupWithTextBox);

                ClosePopupWithTextBox();
                ShowValidationMessages();
            }
        }

        private void ShowValidationMessages()
        {
            PopupService.PopupInstance.YesCommandOfPoNumberPopup = new RelayCommand(ShowValidation);

            PopupService.PopupInstance.CloseCommand = new RelayCommand(RemoveValidation);

            PopupService.PopupInstance.NoCommandOfPoNumberPopup = new RelayCommand(RemoveValidation);

            if (_cardSwipeInformation.ProfileValidations?.Count > 0)
            {
                _validationMessages = (from i in _cardSwipeInformation.ProfileValidations
                                       select new Error
                                       {
                                           Message = i.Message,
                                           MessageType = (MessageType)i.MessageType
                                       }).ToList();

                if (_validationMessages.First().MessageType == MessageType.YesNo)
                {
                    ShowConfirmationMessage(_validationMessages.First().Message,
                       ShowMessages,
                       NavigateToTender,
                       NavigateToTender);
                }
                else
                {
                    ShowNotification(_validationMessages.First().Message,
                        NavigateToTender,
                        NavigateToTender,
                        ApplicationConstants.ButtonWarningColor);
                }
            }
            else
            {
                ShowMessages();
            }
        }

        private void RemoveValidation()
        {
            if (_promptMessages?.Count > 0)
            {
                ClosePopupWithTextBox();
                _promptMessages.RemoveAt(0);
                ShowMessage();
            }
            else
            {
                PromptMessage();
            }
        }

        private void ShowValidation()
        {
            if (_promptMessages?.Count > 0)
            {
                var message = _promptMessages.First();

                if (!string.IsNullOrEmpty(PopupService.PopupInstance.TextValueOfPopupWithTextBox) &&
                    string.IsNullOrEmpty(_messageDictionary.Keys.FirstOrDefault(x => x == message)))
                {
                    _messageDictionary.Add(message, PopupService.PopupInstance.TextValueOfPopupWithTextBox);
                    _promptMessages.RemoveAt(0);
                    ShowMessage();
                }
            }
            else
            {
                PromptMessage();
            }
        }

        private void NavigateToTender()
        {
            NavigateService.Instance.NavigateToTenderScreen();
            ResetCollections();
        }

        private void ResetCollections()
        {
            _messageDictionary.Clear();
            _validationMessages.Clear();
        }

        private void ClosePopupWithTextBox()
        {
            PopupService.PopupInstance.IsPopupOpen = false;
            PopupService.PopupInstance.IsPopupWithTextBoxOpen = false;
            PopupService.PopupInstance.TextValueOfPopupWithTextBox = string.Empty;
        }

        private void ShowMessages()
        {
            PopupService.PopupInstance.PasswordRevelMode = false;
            _promptMessages = _cardSwipeInformation.PromptMessages.Distinct().ToList();
            PromptMessage();
        }

        private void PromptMessage()
        {
            if (_promptMessages?.Count > 0)
            {
                OpenMessagePopup();
            }
            else
            {
                ClosePopupWithTextBox();

                PerformAction(async () =>
                {
                    _timer.Restart();
                    try
                    {
                        _poNumber = await _checkoutBusinessLogic.SaveProfilePrompt(
                        Helper.EncodeToBase64(!string.IsNullOrEmpty(_swipedCardNumber) ? _swipedCardNumber : CardNumber),
                        _cardSwipeInformation.ProfileId,
                        _messageDictionary);
                        if (_processForAmount)
                        {
                            MessengerInstance.Send(_poNumber, "CompleteAccountPaymentAfterValidations");
                        }
                        else if (!_performAptTender)
                        {
                            PaymentByCard();
                        }
                        else
                        {
                            CacheBusinessLogic.IsKickBack = true;
                            MessengerInstance.Send(new KickBackForTenderScreenMessage
                            {
                                PoNumber = _poNumber,
                                IsKickBack = true,
                                OutstandingAmount = _checkoutSummary?.TenderSummary?.OutstandingAmount
                            }, "CompleteAptTenderTransaction");

                            _checkoutSummary = null;
                        }
                    }
                    finally
                    {
                        // Navigating to tender only if APT tender needs not to be performed
                        if (!_performAptTender)
                        {
                            NavigateToTender();
                        }
                        _performAptTender = false;
                        _timer.Stop();
                    }
                });
            }
        }

        private void OpenMessagePopup()
        {
            if (!PopupService.PopupInstance.IsPopupOpen)
            {
                PopupService.PopupInstance.IsPopupOpen = true;
                PopupService.PopupInstance.IsPopupWithTextBoxOpen = true;
                PopupService.PopupInstance.Title = _promptMessages.First();
                PopupService.PopupInstance.IsPassword = true;
            }
        }

        private void ShowMessage(bool processForAmount = false)
        {
            PopupService.PopupInstance.IsPopupOpen = false;
            PopupService.PopupInstance.IsPopupWithTextBoxOpen = false;

            ClosePopupWithTextBox();
            PromptMessage();
        }

        private void ExpiryDateSelected(dynamic obj)
        {
            _expiryDate = obj.NewDate.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        internal void ResetVM()
        {
            Languages = new List<string>
            {
                "English",
                "French"
            };

            Month = new List<int>
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };

            Year = new List<int>();

            for (int i = DateTime.UtcNow.Year; i <= DateTime.UtcNow.Year + 20; i++)
            {
                Year.Add(i);
            }

            IsAcceptButtonEnabled = !CacheBusinessLogic.IsFleetCardRequired;
            Amount = string.Empty;
            _inputPinNumber = string.Empty;
            _tenderCode = string.Empty;
            _transactionType = string.Empty;
            _promptMessages = new List<string>();
            _validationMessages = new List<Error>();
            CardNumber = string.Empty;
            _messageDictionary = new Dictionary<string, string>();
            SelectedLanguageIndex = 0;
            _poNumber = string.Empty;
            _swipedCardNumber = string.Empty;
            _userInputForKickback = false;
            PopupService.PopupInstance.TextValueOfPopupWithTextBox = string.Empty;
            _processForAmount = false;
        }
    }
}
