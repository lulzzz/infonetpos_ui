using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Model.Checkout;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class OverLimitVM : VMBase
    {
        #region Private variables
        private bool _isTobaccoReason;
        private bool _isGasReason;
        private bool _isPropaneReason;
        private bool _isCompleteOverLimitEnabled;
        private bool _showTobaccoExplanation;
        private bool _hideTobaccoExplanation;
        private bool _showGasExplanation;
        private bool _hideGasExplanation;
        private bool _showPropaneExplanation;
        private bool _hidePropaneExplanation;
        private ExplanationCodeModel _tobaccoReason;
        private ExplanationCodeModel _gasReason;
        private ExplanationCodeModel _propaneReason;
        private string _tobaccoExplanation;
        private string _gasolineExplanation;
        private string _propaneExplanation;
        private string _tobaccoLocation;
        private string _gasLocation;
        private string _propaneLocation;
        private CheckoutSummary _checkoutSummary = new CheckoutSummary();
        private List<ExplanationCodeModel> _tobaccoReasons;
        private List<ExplanationCodeModel> _gasolineReasons;
        private List<ExplanationCodeModel> _propaneReasons;
        private ObservableCollection<TaxExemptSaleLineModel> _overLimitDetails;
        private DateTimeOffset _tobaccoDate;
        private DateTimeOffset _gasDate;
        private DateTimeOffset _propaneDate;

        public DateTimeOffset PropaneDate
        {
            get { return _propaneDate; }
            set
            {
                if (_propaneDate != value)
                {
                    _propaneDate = value;
                    RaisePropertyChanged(nameof(PropaneDate));
                }
            }
        }

        public DateTimeOffset GasDate
        {
            get { return _gasDate; }
            set
            {
                if (_gasDate != value)
                {
                    _gasDate = value;
                    RaisePropertyChanged(nameof(GasDate));
                }
            }
        }
        public DateTimeOffset TobaccoDate
        {
            get { return _tobaccoDate; }
            set
            {
                if (_tobaccoDate != value)
                {
                    _tobaccoDate = value;
                    RaisePropertyChanged(nameof(TobaccoDate));
                }
            }
        }
        #endregion

        #region Properties
        public bool IsTobaccoReason
        {
            get { return _isTobaccoReason; }
            set
            {
                _isTobaccoReason = value;
                RaisePropertyChanged(nameof(IsTobaccoReason));
            }
        }

        public bool IsGasReason
        {
            get { return _isGasReason; }
            set
            {
                _isGasReason = value;
                RaisePropertyChanged(nameof(IsGasReason));
            }
        }

        public bool IsPropaneReason
        {
            get { return _isPropaneReason; }
            set
            {
                _isPropaneReason = value;
                RaisePropertyChanged(nameof(IsPropaneReason));
            }
        }

        public bool IsCompleteOverLimitEnabled
        {
            get { return _isCompleteOverLimitEnabled; }
            set
            {
                _isCompleteOverLimitEnabled = value;
                RaisePropertyChanged(nameof(IsCompleteOverLimitEnabled));
            }
        }

        public ExplanationCodeModel TobaccoReason
        {
            get { return _tobaccoReason; }
            set
            {
                _tobaccoReason = value;
                if (_tobaccoReason != null && (_tobaccoReason.Code == 0 || _tobaccoReason.Code == 1 || _tobaccoReason.Code == 2))
                {
                    ShowTobaccoExplanation = true;
                    HideTobaccoExplanation = false;
                }
                else
                {
                    ShowTobaccoExplanation = false;
                    HideTobaccoExplanation = true;
                }
                RaisePropertyChanged(nameof(TobaccoReason));
            }
        }

        public ExplanationCodeModel GasReason
        {
            get { return _gasReason; }
            set
            {
                _gasReason = value;
                if (_gasReason != null && (_gasReason.Code == 0 || _gasReason.Code == 1 || _gasReason.Code == 2))
                {
                    ShowGasExplanation = true;
                    HideGasExplanation = false;
                }

                else
                {
                    ShowGasExplanation = false;
                    HideGasExplanation = true;
                }
                RaisePropertyChanged(nameof(GasReason));
            }
        }

        public ExplanationCodeModel PropaneReason
        {
            get { return _propaneReason; }
            set
            {
                _propaneReason = value;
                if (_propaneReason != null && (_propaneReason.Code == 0 || _propaneReason.Code == 1 || _propaneReason.Code == 2))
                {
                    ShowPropaneExplanation = true;
                    HidePropaneExplanation = false;
                }

                else
                {
                    ShowPropaneExplanation = false;
                    HidePropaneExplanation = true;
                }
                RaisePropertyChanged(nameof(PropaneReason));
            }
        }

        public string TobaccoExplanation
        {
            get { return _tobaccoExplanation; }
            set
            {
                _tobaccoExplanation = value;
                RaisePropertyChanged(nameof(TobaccoExplanation));
            }
        }

        public string GasolineExplanation
        {
            get { return _gasolineExplanation; }
            set
            {
                _gasolineExplanation = value;
                RaisePropertyChanged(nameof(GasolineExplanation));
            }
        }

        public string PropaneExplanation
        {
            get { return _propaneExplanation; }
            set
            {
                _propaneExplanation = value;
                RaisePropertyChanged(nameof(PropaneExplanation));
            }
        }

        public List<ExplanationCodeModel> TobaccoReasons
        {
            get { return _tobaccoReasons; }
            set
            {
                _tobaccoReasons = value;
                RaisePropertyChanged(nameof(TobaccoReasons));
            }
        }

        public List<ExplanationCodeModel> GasolineReasons
        {
            get { return _gasolineReasons; }
            set
            {
                _gasolineReasons = value;
                RaisePropertyChanged(nameof(GasolineReasons));
            }
        }

        public List<ExplanationCodeModel> PropaneReasons
        {
            get { return _propaneReasons; }
            set
            {
                _propaneReasons = value;
                RaisePropertyChanged(nameof(PropaneReasons));
            }
        }

        public ObservableCollection<TaxExemptSaleLineModel> OverLimitDetails
        {
            get { return _overLimitDetails; }
            set
            {
                _overLimitDetails = value;
                RaisePropertyChanged(nameof(OverLimitDetails));
            }
        }

        public bool ShowTobaccoExplanation
        {
            get { return _showTobaccoExplanation; }
            set
            {
                _showTobaccoExplanation = value;
                RaisePropertyChanged(nameof(ShowTobaccoExplanation));
            }
        }
        public bool HideTobaccoExplanation
        {
            get { return _hideTobaccoExplanation; }
            set
            {
                _hideTobaccoExplanation = value;
                RaisePropertyChanged(nameof(HideTobaccoExplanation));
            }
        }
        public bool ShowGasExplanation
        {
            get { return _showGasExplanation; }
            set
            {
                _showGasExplanation = value;
                RaisePropertyChanged(nameof(ShowGasExplanation));
            }
        }
        public bool HideGasExplanation
        {
            get { return _hideGasExplanation; }
            set
            {
                _hideGasExplanation = value;
                RaisePropertyChanged(nameof(HideGasExplanation));
            }
        }
        public bool ShowPropaneExplanation
        {
            get { return _showPropaneExplanation; }
            set
            {
                _showPropaneExplanation = value;
                RaisePropertyChanged(nameof(ShowPropaneExplanation));
            }
        }
        public bool HidePropaneExplanation
        {
            get { return _hidePropaneExplanation; }
            set
            {
                _hidePropaneExplanation = value;
                RaisePropertyChanged(nameof(HidePropaneExplanation));
            }
        }
        #endregion

        #region Commands
        public RelayCommand LoadOverLimitDetailsCommand { get; private set; }
        public RelayCommand CompleteOverLimitCommand { get; private set; }
        public RelayCommand BackPageCommand { get; private set; }
        public RelayCommand<object> TobaccoDateCommand { get; private set; }
        public RelayCommand<object> GasDateCommand { get; private set; }
        public RelayCommand<object> PropaneDateCommand { get; private set; }

        public string TobaccoLocation
        {
            get { return _tobaccoLocation; }
            set
            {
                _tobaccoLocation = value;
                RaisePropertyChanged(nameof(TobaccoLocation));
            }
        }

        public string GasLocation
        {
            get { return _gasLocation; }
            set
            {
                _gasLocation = value;
                RaisePropertyChanged(nameof(GasLocation));
            }
        }

        public string PropaneLocation
        {
            get { return _propaneLocation; }
            set
            {
                _propaneLocation = value;
                RaisePropertyChanged(nameof(PropaneLocation));
            }
        }
        #endregion

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        public OverLimitVM(ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            InitilaizeCommands();
        }

        private void InitilaizeCommands()
        {
            BackPageCommand = new RelayCommand(() => PerformAction(OpenSaleSummary));
            LoadOverLimitDetailsCommand = new RelayCommand(() => PerformAction(LoadOverLimitDetails));
            CompleteOverLimitCommand = new RelayCommand(CompleteOverLimit);
            TobaccoDateCommand = new RelayCommand<object>(SetTobbacoDate);
            GasDateCommand = new RelayCommand<object>(SetGasDate);
            PropaneDateCommand = new RelayCommand<object>(SetPropaneDate);
        }

        private void SetPropaneDate(dynamic obj)
        {
            _propaneDate = obj.NewDate.Date;
        }

        private void SetGasDate(dynamic obj)
        {
            _gasDate = obj.NewDate.Date;
        }

        private void SetTobbacoDate(dynamic obj)
        {
            _tobaccoDate = obj.NewDate.Date;
        }

        private async Task OpenSaleSummary()
        {
            var response = await _checkoutBusinessLogic.SaleSummary(isAiteValidated: true,
                isSiteValidated: false);
            NavigateService.Instance.NavigateToSaleSummary();
            MessengerInstance.Send(response);
        }

        public void ReInitialize()
        {
            TobaccoReason = null;
            GasReason = null;
            PropaneReason = null;
            TobaccoExplanation = string.Empty;
            GasolineExplanation = string.Empty;
            PropaneExplanation = string.Empty;
            ShowTobaccoExplanation = ShowGasExplanation = ShowPropaneExplanation = true;
            HideGasExplanation = HidePropaneExplanation = HideTobaccoExplanation = false;
            GasLocation = PropaneLocation = TobaccoLocation = string.Empty;

            GasDate = PropaneDate = TobaccoDate = DateTimeOffset.Now;
        }

        private async Task LoadOverLimitDetails()
        {
            var overLimitDetails = await _checkoutBusinessLogic.OverLimitDetails();
            IsGasReason = overLimitDetails.IsGasReasons;
            IsTobaccoReason = overLimitDetails.IsTobaccoReasons;
            IsPropaneReason = overLimitDetails.IsPropaneReasons;
            TobaccoReasons = (from t in overLimitDetails.TobaccoReasons
                              select new ExplanationCodeModel
                              {
                                  Code = t.ExplanationCode,
                                  Reason = t.Reason
                              }).ToList();
            GasolineReasons = (from t in overLimitDetails.GasReasons
                               select new ExplanationCodeModel
                               {
                                   Code = t.ExplanationCode,
                                   Reason = t.Reason
                               }).ToList();
            PropaneReasons = (from t in overLimitDetails.PropaneReasons
                              select new ExplanationCodeModel
                              {
                                  Code = t.ExplanationCode,
                                  Reason = t.Reason
                              }).ToList();
            OverLimitDetails = new ObservableCollection<TaxExemptSaleLineModel>
            (from t in overLimitDetails.TaxExemptSale
             select new TaxExemptSaleLineModel
             {
                 ExemptedTax = t.ExemptedTax,
                 Product = t.Product,
                 Quantity = t.Quantity,
                 QuotaLimit = t.QuotaLimit,
                 QuotaUsed = t.QuotaUsed,
                 RegularPrice = t.RegularPrice,
                 TaxFreePrice = t.TaxFreePrice,
                 Type = t.Type
             });
        }

        private void CompleteOverLimit()
        {
            PerformAction(async () =>
            {
                try
                {
                    var response = await _checkoutBusinessLogic.CompleteOverLimit(
                        IsTobaccoReason ? TobaccoReason?.Reason : IsGasReason ? GasReason?.Reason : PropaneReason?.Reason,
                        IsTobaccoReason ? TobaccoExplanation : IsGasReason ? GasolineExplanation : PropaneExplanation,
                        IsTobaccoReason ? TobaccoLocation : IsGasReason ? GasLocation : PropaneLocation,
                        IsTobaccoReason ? Convert.ToDateTime(TobaccoDate.ToString(CultureInfo.InvariantCulture),CultureInfo.InvariantCulture) :
                        IsGasReason ? Convert.ToDateTime(GasDate.ToString(CultureInfo.InvariantCulture),CultureInfo.InvariantCulture) :
                        Convert.ToDateTime(PropaneDate.ToString(CultureInfo.InvariantCulture),CultureInfo.InvariantCulture));

                    OpenCheckoutPage(response);
                }
                catch (SwitchUserException ex)
                {
                    ShowConfirmationMessage(ex.Message, null,
                        () => PerformAction(async () =>
                        {
                            var response = await _checkoutBusinessLogic.SaleSummary(isAiteValidated: true,
                                isSiteValidated: false);
                            OpenCheckoutPage(response);
                        }),
                        () =>
                        PerformAction(async () =>
                        {
                            var response = await _checkoutBusinessLogic.SaleSummary(isAiteValidated: true,
                                isSiteValidated: false);
                            OpenCheckoutPage(response);
                        }));
                }
            });
        }

        private void OpenCheckoutPage(CheckoutSummary response)
        {
            _checkoutSummary.SaleSummary = response.SaleSummary;
            _checkoutSummary.TenderSummary = response.TenderSummary;
            NavigateService.Instance.NavigateToSaleSummary();
            MessengerInstance.Send(_checkoutSummary);
        }
    }
}