using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.UI.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.UI.Utility
{
    public class PopupService : ViewModelBase
    {
        private bool _isPopupOpen;
        private bool _isAlertPopupOpen;
        private bool _isConfirmationPopupOpen;
        private bool _isReasonPopupOpen;
        private bool _isCheckoutOptionsOpen;
        private bool _isReturnsPopupOpen;
        private bool _isOkButtonEnabled;
        private bool _isYesButtonEnabled;
        private bool _isNoButtonEnabled;
        private bool _isEnvelopePopupOpen;
        private bool _isGstPstPopupOpen;
        private bool _isQitePopupOpen;
        private bool _isPurchaseOrderPopupOpen;
        private string _message;
        private string _title;
        private string _continue;
        private string _yesButtonText;
        private string _noButtonText;
        private bool _isMessagepopupOpen;
        private bool _isPumpOptionsPopupOpen;
        private bool _isPopupWithTextBoxOpen;
        private string _textValueOfPopupWithTextBox;
        private bool _passwordRevelMode;
        private bool _isTaxExemptionPopupOpen;
        private bool _isEmergencyPopupOpen;
        private bool _isKickbackPopupOpen;
        private bool _isKickbackNumberPopupOpen;
        private string _kickBackNumber;
        private string _customKickBackMessage;
        private bool _isCarwashPopupOpen;
        private string _carwashCode;
        private bool _isAckTenderPopOpen;
        private bool _isAckBalancePopOpen;
        private bool _isFuelDiscountPopOpen;

        public bool IsFuelDiscountPopupOpen
        {
            get { return _isFuelDiscountPopOpen; }
            set { Set(nameof(IsFuelDiscountPopupOpen), ref _isFuelDiscountPopOpen, value); }
        }
        public bool IsAckBalacePopOpen
        {
            get { return _isAckBalancePopOpen; }
            set { Set(nameof(IsAckBalacePopOpen), ref _isAckBalancePopOpen, value); }
        }
        public bool IsAckTenderPopOpen
        {
            get { return _isAckTenderPopOpen; }
            set { Set(nameof(IsAckTenderPopOpen), ref _isAckTenderPopOpen, value); }
        }
        public string CustomKickbackMessage
        {
            get { return _customKickBackMessage; }
            set { Set(nameof(CustomKickbackMessage), ref _customKickBackMessage, value); }
        }

        public string KickBackNumber
        {
            get { return _kickBackNumber; }
            set { Set(nameof(KickBackNumber), ref _kickBackNumber, value); }
        }

        public bool IsKickbackNumberPopupOpen
        {
            get { return _isKickbackNumberPopupOpen; }
            set { Set(nameof(IsKickbackNumberPopupOpen), ref _isKickbackNumberPopupOpen, value); }
        }

        public bool IsKickbackBalancePopupOpen
        {
            get { return _isKickbackPopupOpen; }
            set { Set(nameof(IsKickbackBalancePopupOpen), ref _isKickbackPopupOpen, value); }
        }

        public bool IsCarwashPopupOpen
        {
            get { return _isCarwashPopupOpen; }
            set { Set(nameof(IsCarwashPopupOpen), ref _isCarwashPopupOpen, value); }
        }

        public string CarwashCode
        {
            get { return _carwashCode; }
            set { Set(nameof(CarwashCode), ref _carwashCode, value); }
        }
        

        public bool IsTaxExemptionPopupOpen
        {
            get { return _isTaxExemptionPopupOpen; }
            set
            {
                _isTaxExemptionPopupOpen = value;
                RaisePropertyChanged(nameof(IsTaxExemptionPopupOpen));
            }
        }

        public bool PasswordRevelMode
        {
            get { return _passwordRevelMode; }
            set
            {
                _passwordRevelMode = value;
                RaisePropertyChanged(nameof(PasswordRevelMode));
            }
        }

        private ICommand _yesConfirmationCommand;
        private ICommand _okCommand;
        private ICommand _noConfirmationCommand;
        private ICommand _closeCommand;
        private ICommand _messageItemClicked;
        private ICommand _thirdButtonCommand;
        private ICommand _yesCommandOfPoNumberPopup;
        private ICommand _noCommandOfPoNumberPopup;

        private SolidColorBrush _yesButtonColor;
        private SolidColorBrush _noButtonColor;
        private SolidColorBrush _okButtonColor;
        private string _thirdButtonText;
        private SolidColorBrush _thirdButtonColor;
        private bool _isThirdButtonVisible;
        private bool _isFngtrPopupOpen;
        private bool _isText = true;
        private bool _isPassword;


        public bool IsText
        {
            get { return _isText; }
            set
            {
                if (_isText != value)
                {
                    _isText = value;
                    RaisePropertyChanged(nameof(IsText));
                }
            }
        }

        public bool IsPassword
        {
            get { return _isPassword; }
            set
            {
                if (_isPassword != value)
                {
                    _isPassword = value;
                    IsText = !_isPassword;
                    RaisePropertyChanged(nameof(IsPassword));
                }
            }
        }



        public bool IsFngtrPopupOpen
        {
            get { return _isFngtrPopupOpen; }
            set
            {
                if (_isFngtrPopupOpen != value)
                {
                    _isFngtrPopupOpen = value;
                    RaisePropertyChanged(nameof(IsFngtrPopupOpen));
                }
            }
        }

        public string TextValueOfPopupWithTextBox
        {
            get { return _textValueOfPopupWithTextBox; }
            set
            {
                if (_textValueOfPopupWithTextBox != value)
                {
                    _textValueOfPopupWithTextBox = value;
                    RaisePropertyChanged(nameof(TextValueOfPopupWithTextBox));
                }
            }
        }

        public bool IsPopupWithTextBoxOpen
        {
            get { return _isPopupWithTextBoxOpen; }
            set
            {
                if (_isPopupWithTextBoxOpen != value)
                {
                    _isPopupWithTextBoxOpen = value;
                    RaisePropertyChanged(nameof(IsPopupWithTextBoxOpen));
                }
            }
        }

        public bool IsThirdButtonVisible
        {
            get { return _isThirdButtonVisible; }
            set
            {
                if (value != _isThirdButtonVisible)
                {
                    _isThirdButtonVisible = value;
                    RaisePropertyChanged(nameof(IsThirdButtonVisible));
                }
            }
        }

        public SolidColorBrush ThirdButtonColor
        {
            get { return _thirdButtonColor; }
            set
            {
                if (_thirdButtonColor != value)
                {
                    _thirdButtonColor = value;
                    RaisePropertyChanged(nameof(ThirdButtonColor));
                }
            }
        }

        public string ThirdButtonText
        {
            get { return _thirdButtonText; }
            set
            {
                if (_thirdButtonText != value)
                {
                    _thirdButtonText = value;
                    RaisePropertyChanged(nameof(ThirdButtonText));
                }
            }
        }

        private static PopupService _popupInstance = new PopupService();
        private List<string> _openedPopups;

        public static PopupService PopupInstance
        {
            get
            {
                return _popupInstance;
            }
        }

        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set
            {
                if (_isPopupOpen != value)
                {
                    _isPopupOpen = value;
                    if (!_isPopupOpen)
                    {
                        MessengerInstance.Send(new SetFocusOnGridMessage { });
                    }
                }
            }
        }

        public bool IsNoButtonEnabled
        {
            get { return _isNoButtonEnabled; }
            set
            {
                _isNoButtonEnabled = value;
                RaisePropertyChanged(nameof(IsNoButtonEnabled));
            }
        }

        public bool IsMessagePopupOpen
        {
            get { return _isMessagepopupOpen; }
            set
            {
                _isMessagepopupOpen = value;
                RaisePropertyChanged(nameof(IsMessagePopupOpen));
            }
        }

        public bool IsYesbuttonEnabled
        {
            get { return _isYesButtonEnabled; }
            set
            {
                _isYesButtonEnabled = value;
                RaisePropertyChanged(nameof(IsYesbuttonEnabled));
            }
        }

        public bool IsPurchaseOrderPopupOpen
        {
            get { return _isPurchaseOrderPopupOpen; }
            set
            {
                _isPurchaseOrderPopupOpen = value;
                RaisePropertyChanged(nameof(IsPurchaseOrderPopupOpen));
            }
        }

        public bool IsPumpOptionsPopupOpen
        {
            get { return _isPumpOptionsPopupOpen; }
            set
            {
                _isPumpOptionsPopupOpen = value;
                RaisePropertyChanged(nameof(IsPumpOptionsPopupOpen));
            }
        }

        public bool IsEmergencyPopupOpen
        {
            get { return _isEmergencyPopupOpen; }
            set
            {
                if (_isEmergencyPopupOpen != value)
                {
                    _isEmergencyPopupOpen = value;
                    RaisePropertyChanged(nameof(IsEmergencyPopupOpen));
                }
            }
        }
        private ICommand _kickbackNumberEnteredCommand;

        public ICommand KickBackNumberEnteredCommand
        {
            get { return _kickbackNumberEnteredCommand; }
            set
            {
                _kickbackNumberEnteredCommand = value;
                RaisePropertyChanged(nameof(KickBackNumberEnteredCommand));
            }
        }

        public ICommand NoCommandOfPoNumberPopup
        {
            get { return _noCommandOfPoNumberPopup; }
            set
            {
                _noCommandOfPoNumberPopup = value;
                RaisePropertyChanged(nameof(NoCommandOfPoNumberPopup));
            }
        }

        public ICommand YesCommandOfPoNumberPopup
        {
            get { return _yesCommandOfPoNumberPopup; }
            set
            {
                _yesCommandOfPoNumberPopup = value;
                RaisePropertyChanged(nameof(YesCommandOfPoNumberPopup));
            }
        }

        public ICommand MessageItemClicked
        {
            get { return _messageItemClicked; }
            set
            {
                _messageItemClicked = value;
                RaisePropertyChanged(nameof(MessageItemClicked));
            }
        }

        public ICommand NoConfirmationCommand
        {
            get { return _noConfirmationCommand; }
            set
            {
                _noConfirmationCommand = value;
                RaisePropertyChanged(nameof(NoConfirmationCommand));
            }
        }


        public ICommand ThirdButtonCommand
        {
            get { return _thirdButtonCommand; }
            set
            {
                _thirdButtonCommand = value;
                RaisePropertyChanged(nameof(ThirdButtonCommand));
            }
        }

        public ICommand YesConfirmationCommand
        {
            get { return _yesConfirmationCommand; }
            set
            {
                _yesConfirmationCommand = value;
                RaisePropertyChanged(nameof(YesConfirmationCommand));
            }
        }

        public ICommand OkCommand
        {
            get { return _okCommand; }
            set
            {
                _okCommand = value;
                RaisePropertyChanged(nameof(OkCommand));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand;
            }
            set
            {
                _closeCommand = value;
                RaisePropertyChanged(nameof(CloseCommand));
            }
        }

        public string Continue
        {
            get { return _continue; }
            set
            {
                _continue = value;
                RaisePropertyChanged(nameof(Continue));
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string YesButtonText
        {
            get { return _yesButtonText; }
            set
            {
                _yesButtonText = value;
                RaisePropertyChanged(nameof(YesButtonText));
            }
        }

        public string NoButtonText
        {
            get { return _noButtonText; }
            set
            {
                _noButtonText = value;
                RaisePropertyChanged(nameof(NoButtonText));
            }
        }

        public SolidColorBrush YesButtonColor
        {
            get { return _yesButtonColor; }
            set
            {
                _yesButtonColor = value;
                RaisePropertyChanged(nameof(YesButtonColor));
            }
        }

        public SolidColorBrush NoButtonColor
        {
            get { return _noButtonColor; }
            set
            {
                _noButtonColor = value;
                RaisePropertyChanged(nameof(NoButtonColor));
            }
        }

        public SolidColorBrush OkButtonColor
        {
            get { return _okButtonColor; }
            set
            {
                _okButtonColor = value;
                RaisePropertyChanged(nameof(OkButtonColor));
            }
        }

        public bool IsConfirmationPopupOpen
        {
            get { return _isConfirmationPopupOpen; }
            set
            {
                _isConfirmationPopupOpen = value;
                RaisePropertyChanged(nameof(IsConfirmationPopupOpen));
            }
        }

        public bool IsAlertPopupOpen
        {
            get { return _isAlertPopupOpen; }
            set
            {
                _isAlertPopupOpen = value;
                RaisePropertyChanged(nameof(IsAlertPopupOpen));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }

        public bool IsReasonPopupOpen
        {
            get { return _isReasonPopupOpen; }
            set
            {
                _isReasonPopupOpen = value;
                RaisePropertyChanged(nameof(IsReasonPopupOpen));
            }
        }

        public bool IsCheckoutOptionsOpen
        {
            get { return _isCheckoutOptionsOpen; }
            set
            {
                _isCheckoutOptionsOpen = value;
                RaisePropertyChanged(nameof(IsCheckoutOptionsOpen));
                if (!_isCheckoutOptionsOpen)
                {
                    MessengerInstance.Send(new SetFocusOnGridMessage());
                }
            }
        }

        public bool IsReturnsPopupOpen
        {
            get { return _isReturnsPopupOpen; }
            set
            {
                _isReturnsPopupOpen = value;
                RaisePropertyChanged(nameof(IsReturnsPopupOpen));
                if (!_isReturnsPopupOpen)
                {
                    MessengerInstance.Send(new SetFocusOnGridMessage());
                }
            }
        }

        public bool IsOkButtonEnabled
        {
            get { return _isOkButtonEnabled; }
            set
            {
                _isOkButtonEnabled = value;
                RaisePropertyChanged(nameof(IsOkButtonEnabled));
            }
        }

        public bool IsEnvelopeOpen
        {
            get { return _isEnvelopePopupOpen; }
            set
            {
                _isEnvelopePopupOpen = value;
                RaisePropertyChanged(nameof(IsEnvelopeOpen));
            }
        }

        public bool IsGstPstPopupOpen
        {
            get { return _isGstPstPopupOpen; }
            set
            {
                _isGstPstPopupOpen = value;
                RaisePropertyChanged(nameof(IsGstPstPopupOpen));
            }
        }

        public bool IsQitePopupOpen
        {
            get { return _isQitePopupOpen; }
            set
            {
                _isQitePopupOpen = value;
                RaisePropertyChanged(nameof(IsQitePopupOpen));
            }
        }

        public ObservableCollection<Reasons> ReasonList { get; set; }
            = new ObservableCollection<Reasons>();

        public void CloseCurrentPopupWithStateSave()
        {
            _openedPopups = new List<string>();
            foreach (var prop in typeof(PopupService).GetProperties().Where(x => x.Name.Contains("Open") && x.PropertyType == typeof(bool)))
            {
                var value = (bool)prop.GetValue(PopupService.PopupInstance, null);
                if (value)
                {
                    _openedPopups.Add(prop.Name);
                    prop.SetValue(PopupService.PopupInstance, false);
                }
            }
        }

        internal void RestoreLastOpenedPopup()
        {
            foreach (var popup in _openedPopups)
            {
                typeof(PopupService).GetProperty(popup).SetValue(PopupService.PopupInstance, true);
            }
            _openedPopups = new List<string>();
        }
    }
}
