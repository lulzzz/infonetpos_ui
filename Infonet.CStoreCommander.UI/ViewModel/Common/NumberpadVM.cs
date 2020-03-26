using GalaSoft.MvvmLight.Command;
using System;
using System.Globalization;

namespace Infonet.CStoreCommander.UI.ViewModel.Common
{
    /// <summary>
    /// View model for Number pad
    /// </summary>
    public class NumberpadVM : VMBase
    {
        private string _inputText;
        private int currency = 0;

        public string InputText
        {
            get { return _inputText; }
            set
            {
                if (IsValidInput(value))
                {
                    _inputText = value;
                }
                RaisePropertyChanged(nameof(InputText));
            }
        }

        public RelayCommand<object> NumberTappedCommand { get; set; }
        public RelayCommand<object> CurrencyTappedCommand { get; set; }
        public RelayCommand MinusTappedCommand { get; set; }
        public RelayCommand BackSapceCommand { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public NumberpadVM()
        {
           // InputText = "";
            InitializeCommands();
            RegisterMessages();
        }

        /// <summary>
        /// Initializes all the commands
        /// </summary>
        private void InitializeCommands()
        {
            CurrencyTappedCommand = new RelayCommand<object>((s) => CurrencyTapped(s));
            MinusTappedCommand = new RelayCommand(MinusTapped);
            NumberTappedCommand = new RelayCommand<object>((args) => ChangeNumber(args));
            BackSapceCommand = new RelayCommand(BackSpaceTapped);
        }

        private void CurrencyTapped(dynamic args)
        {
            var decimalValueOfInputText = ConvertToDecimal(InputText);
            var decimalValueOfCurrency = ConvertToDecimal(args);
            var sum = decimalValueOfCurrency + decimalValueOfInputText;
            InputText = sum.ToString();
            MessengerInstance.Send(InputText, "CurrencyTapped");
        }

        private decimal ConvertToDecimal(dynamic inputText)
        {
            var result = 0M;
            Decimal.TryParse(inputText.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            return result;
        }

        /// <summary>
        /// Minus button is tapped
        /// </summary>
        private void MinusTapped()
        {
            InputText += "-";
            if (InputText.EndsWith("-") && InputText.Length > 1)
            {
                InputText = InputText.Replace("-", "");
            }
        }

        /// <summary>
        /// Registers all the messages listened by the view model
        /// </summary>
        private void RegisterMessages()
        {
            MessengerInstance.Register<bool>(this, "ResetNumberPadVM", ResetNumberPadVM);
            MessengerInstance.Register<string>(this, "SetQuantiyUsingNumberPad", UpdateText);
        }


        private void UpdateText(string quantity)
        {
            InputText = quantity != null && quantity != "0" ? quantity : string.Empty;
        }

        /// <summary>
        /// Back space is tapped
        /// </summary>
        private void BackSpaceTapped()
        {
            if (!string.IsNullOrEmpty(InputText))
            {
                var lenghtOfString = InputText.Length;
                InputText = lenghtOfString == 1 ? string.Empty : InputText.Remove(lenghtOfString - 1);
            }
        }

        private void ChangeNumber(dynamic args)
        {
            InputText += args;
        }

        private bool IsValidInput(string value)
        {
            if ((value.StartsWith("+") || value.StartsWith("-")) && value.Length == 1)
                return true;

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            try
            {
                var decimalValue = 0M;
                decimalValue = decimal.Parse(value, CultureInfo.InvariantCulture);
                if (value.Contains("."))
                {
                    var x = value.Split('.')[1];
                    return x.Length > 5 ? false : true;
                }
                return (decimalValue / 9999) > 1 || (decimalValue / 9999) < -1 ? false : true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal void ReInitialize()
        {
            InputText = string.Empty;
        }

        private void ResetNumberPadVM(bool obj)
        {
            currency = 0;
        }
    }
}
