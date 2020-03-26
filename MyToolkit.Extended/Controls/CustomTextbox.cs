using System.Linq;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MyToolkit.Extended.Controls
{
    public enum NumericKeyType
    {
        None,
        Amount,
        Number,
        Card,
        StockCodeNumber
    }

    public class CustomTextBox : TextBox
    {
        private bool _isShiftPressed = false;
        private string _oldValue;

        #region Constants
        private const string _number = "Number";
        private const string _decimal = "190";
        private const string _negativeSign = "189";
        private const string _semiColon = "186";
        private const string _equalSign = "187";
        private const string _questionMark = "191";
        #endregion

        public CustomTextBox()
        {
            this.TabIndex = -1;
            KeyUp -= OnKeyUp;
            KeyUp += OnKeyUp;

            KeyDown -= OnKeyDown;
            KeyDown += OnKeyDown;

            GotFocus -= Focused;
            GotFocus += Focused;

            LostFocus -= FocusLost;
            LostFocus += FocusLost;
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().ToUpper().Equals("SHIFT"))
            {
                if (e.KeyStatus.RepeatCount == 1)
                {
                    _isShiftPressed = true;
                }
                else
                {
                    _isShiftPressed = false;
                }
            }

            if (NumericKeyType != NumericKeyType.None && e.Key != VirtualKey.Enter)
            {
                FilterText(Text, e.Key, e);
            }
        }

        // Filters text according to numeric type
        private string FilterText(string text, VirtualKey key, KeyRoutedEventArgs e)
        {
            // Apart from Number, Decimal point and negative neglect everything

            switch (NumericKeyType)
            {
                case NumericKeyType.Number:
                    if (_isShiftPressed ||
                        !key.ToString().StartsWith(_number))
                    {
                        e.Handled = true;
                        return text;
                    }
                    break;
                case NumericKeyType.Amount:
                    if (_isShiftPressed ||
                        (!key.ToString().StartsWith(_number)
                        && (key.ToString() != _decimal && key != VirtualKey.Decimal) &&
                        (key.ToString() != _negativeSign && key != VirtualKey.Subtract)))
                    {
                        e.Handled = true;
                        return text;
                    }
                    break;
                case NumericKeyType.Card:
                    if (_isShiftPressed && key.ToString() == _questionMark)
                    {
                        break;
                    }

                    if (_isShiftPressed ||
                        (!key.ToString().StartsWith(_number)
                        && (key.ToString() != _decimal && key != VirtualKey.Decimal)
                        && (key.ToString() != _negativeSign && key != VirtualKey.Subtract)
                        && key.ToString() != _semiColon
                        && key.ToString() != _equalSign))
                    {
                        e.Handled = true;
                        return text;
                    }
                    break;

                case NumericKeyType.StockCodeNumber:
                    if (_isShiftPressed && key.ToString() == _questionMark)
                    {
                        break;
                    }

                    if (key.ToString() == _semiColon)
                    {
                        e.Handled = true;
                        return text;
                    }
                    break;
                default:
                    break;
            }


            // If negative is pressed in between then also ignore it
            if (NumericKeyType != NumericKeyType.StockCodeNumber)
            {
                if (SelectionStart != 0 && key.ToString() == _negativeSign)
                {
                    e.Handled = true;
                    return text;
                }
            }

            // Discard any second decimal point
            if (text.Count(x => x == '.') > 0 && key.ToString() == _decimal)
            {
                e.Handled = true;
                return text;
            }
            return text;
        }

        private void FocusLost(object sender, RoutedEventArgs e)
        {
            if (UpdateOnlyOnEnter)
            {
                Text = _oldValue;
            }
        }

        private void Focused(object sender, RoutedEventArgs e)
        {
            // Retain value of the Text box when focused
            _oldValue = Text;
        }

        private void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().ToUpper().Equals("SHIFT"))
            {
                _isShiftPressed = false;
            }

            VirtualKey pressedKey;

            // Look if Enter is pressed
            if (e.OriginalKey == VirtualKey.Enter)
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();

                if (EnterPressedCommand != null)
                {
                    EnterPressedCommand.Execute(e);
                }
            }
        }

        public ICommand EnterPressedCommand
        {
            get { return (ICommand)GetValue(EnterPressedCommandProperty); }
            set
            {
                if (value != null)
                {
                    SetValue(EnterPressedCommandProperty, value);
                }
            }
        }

        public static readonly DependencyProperty EnterPressedCommandProperty =
            DependencyProperty.Register(nameof(EnterPressedCommand),
                typeof(ICommand), typeof(CustomTextBox),
                new PropertyMetadata(null));

        public bool UpdateOnlyOnEnter
        {
            get { return (bool)GetValue(UpdateOnlyOnEnterProperty); }
            set
            {
                SetValue(UpdateOnlyOnEnterProperty, value);
            }
        }

        public static readonly DependencyProperty UpdateOnlyOnEnterProperty =
            DependencyProperty.Register(nameof(UpdateOnlyOnEnter),
                typeof(bool), typeof(CustomTextBox),
                new PropertyMetadata(false));

        public bool SetFocusOn
        {
            get { return (bool)GetValue(SetFocusOnProperty); }
            set
            {
                SetValue(SetFocusOnProperty, value);
                if (value)
                {
                    this.Focus(FocusState.Keyboard);
                }
            }
        }

        public static readonly DependencyProperty SetFocusOnProperty =
            DependencyProperty.Register(nameof(SetFocusOn),
                typeof(bool), typeof(CustomTextBox),
                new PropertyMetadata(false));

        public NumericKeyType NumericKeyType
        {
            get { return (NumericKeyType)GetValue(NumericKeyTypeProperty); }
            set
            {
                SetValue(NumericKeyTypeProperty, value);
            }
        }

        public static readonly DependencyProperty NumericKeyTypeProperty =
            DependencyProperty.Register(nameof(NumericKeyType),
                typeof(bool), typeof(CustomTextBox),
                new PropertyMetadata(NumericKeyType.None));
    }
}