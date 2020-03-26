using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Model
{
    public class PumpDetailModel : ViewModelBase
    {
        private int _pumpNumber;
        private string _status;
        private Uri _source;
        private SolidColorBrush _criticalLevelColor;
        private string _criticalLevelContent;
        private ObservableCollection<string> pumpOptions;
        private string _basketLableCaption;
        private bool _enableStackBasketBotton;
        private bool _enableBasketBotton;
        private int _selectedPumpOptionIndex;
        private string _payPumpOrPrepay;
        private bool _isPumpOptionCBVisible;
        private string _basketButtonCaption;
        private int _selectedCurrentStackIndex;
        private ObservableCollection<Stacked> _stacked;
        private string _pumpButtonCaption;

        public string PumpButtonCaption
        {
            get { return _pumpButtonCaption; }
            set
            {
                if (_pumpButtonCaption != value)
                {
                    _pumpButtonCaption = value?.ToUpper();
                    RaisePropertyChanged(nameof(PumpButtonCaption));
                }
            }
        }

        public ObservableCollection<Stacked> Stacked
        {
            get { return _stacked; }
            set
            {
                if (_stacked != value)
                {
                    _stacked = value;
                    RaisePropertyChanged(nameof(Stacked));
                }
            }
        }

        public int SelectedCurrentStackIndex
        {
            get { return _selectedCurrentStackIndex; }
            set
            {
                if (value != _selectedCurrentStackIndex)
                {
                    _selectedCurrentStackIndex = value;
                    RaisePropertyChanged(nameof(SelectedCurrentStackIndex));
                }
            }
        }

        public string BasketButtonCaption
        {
            get { return _basketButtonCaption; }
            set
            {
                if (value != _basketButtonCaption)
                {
                    _basketButtonCaption = value;

                    CriticalLevelColor = string.IsNullOrEmpty(BasketButtonCaption)
                                       ? new SolidColorBrush(Color.FromArgb(255, 105, 121, 126))
                                       : new SolidColorBrush(Color.FromArgb(255, 241, 241, 241));

                    CheckForCurrentStack();

                    RaisePropertyChanged(nameof(BasketButtonCaption));
                }
            }
        }

        private void CheckForCurrentStack()
        {
            if (!string.IsNullOrEmpty(_basketButtonCaption) &&
                !string.IsNullOrEmpty(_basketLableCaption))
            {
                EnableStackBasketBotton = true;
            }
            else
            {
                EnableStackBasketBotton = false;
            }
        }

        public bool IsPumpOptionCBVisible
        {
            get { return _isPumpOptionCBVisible; }
            set
            {
                if (_isPumpOptionCBVisible != value)
                {
                    _isPumpOptionCBVisible = value;
                    RaisePropertyChanged(nameof(IsPumpOptionCBVisible));
                }
            }
        }

        public string PayPumpOrPrepay
        {
            get { return _payPumpOrPrepay; }
            set
            {
                if (_payPumpOrPrepay != value)
                {
                    _payPumpOrPrepay = value;
                    RaisePropertyChanged(nameof(PayPumpOrPrepay));
                }
            }
        }

        public int SelectedPumpOptionIndex
        {
            get { return _selectedPumpOptionIndex; }
            set
            {
                if (value != _selectedPumpOptionIndex)
                {
                    _selectedPumpOptionIndex = value;
                    RaisePropertyChanged(nameof(SelectedPumpOptionIndex));
                }
            }
        }

        public bool EnableBasketBotton
        {
            get { return _enableBasketBotton; }
            set
            {
                if (value != _enableBasketBotton)
                {
                    _enableBasketBotton = value;
                    RaisePropertyChanged(nameof(EnableBasketBotton));
                }
            }
        }

        public bool EnableStackBasketBotton
        {
            get { return _enableStackBasketBotton; }
            set
            {
                if (value != _enableStackBasketBotton)
                {
                    if (value == true)
                    {
                        Stacked = new ObservableCollection<Stacked>
                        {
                            new Stacked {Heading = ApplicationConstants.Current,Content = _basketButtonCaption},
                            new Stacked{Heading = ApplicationConstants.Next,Content = _basketLableCaption}
                         };
                    }
                    _enableStackBasketBotton = value;
                    RaisePropertyChanged(nameof(EnableStackBasketBotton));
                }
            }
        }

        public string BasketLabelCaption
        {
            get { return _basketLableCaption; }
            set
            {
                if (value != _basketLableCaption)
                {
                    _basketLableCaption = value;
                    CheckForCurrentStack();
                    RaisePropertyChanged(nameof(BasketLabelCaption));
                }
            }
        }

        public ObservableCollection<string> PumpOptions
        {
            get { return pumpOptions; }
            set
            {
                pumpOptions = value;
                RaisePropertyChanged(nameof(PumpOptions));
            }
        }

        public int PumpNumber
        {
            get { return _pumpNumber; }
            set
            {
                if (_pumpNumber != value)
                {
                    _pumpNumber = value;
                    RaisePropertyChanged(nameof(PumpNumber));
                }
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status == null || (_status.ToUpper() != value.ToUpper()))
                {
                    _status = value.ToUpper();
                    IsPumpOptionCBVisible = _status?.ToUpper() != PumpStatuses.CALLING.ToString() ||
                        (CanCashierAuthorize && _status?.ToUpper() == PumpStatuses.IDLE.ToString());
                    RaisePropertyChanged(nameof(Status));
                }
            }
        }

        public Uri Source
        {
            get { return _source; }
            set
            {
                if (_source != value)
                {
                    _source = value;
                    RaisePropertyChanged(nameof(Source));
                }
            }
        }

        public SolidColorBrush CriticalLevelColor
        {
            get { return _criticalLevelColor; }
            set
            {
                if (_criticalLevelColor != value)
                {
                    _criticalLevelColor = value;
                    RaisePropertyChanged(nameof(CriticalLevelColor));
                }
            }
        }

        public string CriticalLevelContent
        {
            get { return _criticalLevelContent; }
            set
            {
                if (_criticalLevelContent != value)
                {
                    _criticalLevelContent = value;
                    RaisePropertyChanged(nameof(CriticalLevelContent));
                }
            }
        }

        public bool CanCashierAuthorize { get; set; }
    }

    public class Stacked : ViewModelBase
    {
        private string _content;
        private string _heading;

        public string Heading
        {
            get { return _heading; }
            set
            {
                _heading = value;
                RaisePropertyChanged(nameof(Heading));
            }
        }


        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

    }

    public class BigPumpsModel : ViewModelBase
    {
        private string _pumpId;
        private bool _isPumpVisible;
        private string _pumpLabel;
        private string _pumpMessage;
        private string _amount;

        public string Amount
        {
            get { return _amount; }
            set
            {
                if (value != _amount)
                {
                    _amount = value;
                    RaisePropertyChanged(nameof(Amount));
                }
            }
        }


        public string PumpMessage
        {
            get { return _pumpMessage; }
            set
            {
                if (value != _pumpMessage)
                {
                    _pumpMessage = value;
                    RaisePropertyChanged(nameof(PumpMessage));
                }
            }
        }

        public string PumpLabel
        {
            get { return _pumpLabel; }
            set
            {
                if (value != _pumpLabel)
                {
                    _pumpLabel = value;
                    RaisePropertyChanged(nameof(PumpLabel));
                }
            }
        }


        public bool IsPumpVisible
        {
            get { return _isPumpVisible; }
            set
            {
                if (value != _isPumpVisible)
                {
                    _isPumpVisible = value;
                    RaisePropertyChanged(nameof(IsPumpVisible));
                }
            }
        }


        public string PumpId
        {
            get { return _pumpId; }
            set
            {
                if (value != _pumpId)
                {
                    _pumpId = value;
                    RaisePropertyChanged(nameof(PumpId));
                }
            }
        }
    }

    public enum PumpStatuses
    {
        CALLING,
        IDLE,
        AUTHORIZED,
        INACTIVE,
        STOPPED,
        PUMPING
    }
}
