using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.UI.Model.Checkout;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class OverrideLimitVM : VMBase
    {
        #region Private variables
        private bool _isOverrideCodeSelected;
        private bool _isOverrideDone;
        private string _document;
        private string _overrideCode;
        private string _details;
        private string _caption;
        private string _maxLimitMessage;
        private List<string> _overrideCodes;
        private List<OverrideCode> _allOverrideCodes;
        private PurchaseItemModel _selectedOverrideLimit;
        private ObservableCollection<PurchaseItemModel> _overrideLimitDetails;
        #endregion

        #region Properties
        public bool IsOverrideDone
        {
            get { return _isOverrideDone; }
            set
            {
                _isOverrideDone = value;
                if (_isOverrideDone)
                {
                    IsOverrideCodeSelected = false;
                }
                RaisePropertyChanged(nameof(IsOverrideDone));
                RaisePropertyChanged(nameof(IsOverrideNotDone));
            }
        }

        public bool IsOverrideNotDone
        {
            get { return !_isOverrideDone; }
        }

        public ObservableCollection<PurchaseItemModel> OverrideLimitDetails
        {
            get { return _overrideLimitDetails; }
            set
            {
                _overrideLimitDetails = value;
                if (OverrideLimitDetails != null)
                {
                    SelectedOverrideLimit = OverrideLimitDetails.FirstOrDefault();
                }
                RaisePropertyChanged(nameof(OverrideLimitDetails));
            }
        }

        public PurchaseItemModel SelectedOverrideLimit
        {
            get { return _selectedOverrideLimit; }
            set
            {
                _selectedOverrideLimit = value;
                IsOverrideCodeSelected = SelectedOverrideLimit != null;
                UpdatesOverrideCodes();
                RaisePropertyChanged(nameof(SelectedOverrideLimit));
            }
        }

        public bool IsOverrideCodeSelected
        {
            get { return _isOverrideCodeSelected; }
            set
            {
                _isOverrideCodeSelected = value;
                RaisePropertyChanged(nameof(IsOverrideCodeSelected));
            }
        }

        public string Document
        {
            get { return _document; }
            set
            {
                _document = value;
                RaisePropertyChanged(nameof(Document));
            }
        }

        public string OverrideCode
        {
            get { return _overrideCode; }
            set
            {
                _overrideCode = value;
                RaisePropertyChanged(nameof(OverrideCode));
            }
        }

        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                RaisePropertyChanged(nameof(Details));
            }
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                RaisePropertyChanged(nameof(Caption));
            }
        }

        public string MaxLimitMessage
        {
            get { return _maxLimitMessage; }
            set
            {
                if (_maxLimitMessage != value)
                {
                    _maxLimitMessage = value;
                    IsTaxFreeLimitNotReached = string.IsNullOrEmpty(_maxLimitMessage);
                    IsTaxFreeLimitReached = !string.IsNullOrEmpty(_maxLimitMessage);
                    RaisePropertyChanged(nameof(MaxLimitMessage));
                    RaisePropertyChanged(nameof(IsTaxFreeLimitNotReached));
                    RaisePropertyChanged(nameof(IsTaxFreeLimitReached));
                }
            }
        }

        public bool IsTaxFreeLimitNotReached;

        public bool IsTaxFreeLimitReached;

        public List<string> OverrideCodes
        {
            get { return _overrideCodes; }
            set
            {
                _overrideCodes = value;
                RaisePropertyChanged(nameof(OverrideCodes));
            }
        }
        #endregion

        #region Commands
        public RelayCommand OverrideLimitOverrideCommand { get; private set; }
        public RelayCommand CompleteOverrideLimitCommand { get; private set; }
        public RelayCommand LoadOverrideLimitDetailsCommand { get; private set; }
        public RelayCommand<object> OverrideCodeChangedCommand { get; private set; }
        public RelayCommand BackPageCommand { get; private set; }
        #endregion

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        public OverrideLimitVM(ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
           
            BackPageCommand = new RelayCommand(NavigateToSaleSummary);
            OverrideLimitOverrideCommand = new RelayCommand(OverrideLimitOverride);
            CompleteOverrideLimitCommand = new RelayCommand(CompleteOverrideLimit);
            LoadOverrideLimitDetailsCommand = new RelayCommand(LoadOverrideLimitDetails);
            OverrideCodeChangedCommand = new RelayCommand<object>(OverrideCodeChanged);
        }

        private void NavigateToSaleSummary()
        {
            
            ShowNotification(ApplicationConstants.OverrideLimitMessage,
                OpenSaleSummary,
                OpenSaleSummary,
                ApplicationConstants.ButtonWarningColor);
            
        }

        private async void OpenSaleSummary()
        {
            
            PerformAction(async () =>
            {
                var response = await _checkoutBusinessLogic.SaleSummary(isAiteValidated: false,
                    isSiteValidated: true);
                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send(response);
            });
        }

        private void CompleteOverrideLimit()
        {
            PerformAction(async () =>
            {
                var summary = await _checkoutBusinessLogic.CompleteOverrideLimit();
                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send(summary);
            });
           
        }

        private void OverrideLimitOverride()
        {
            PerformAction(async () =>
            {
                var success = await _checkoutBusinessLogic.OverrideLimitOverride(
                    (OverrideLimitDetails.IndexOf(SelectedOverrideLimit) + 1).ToString(),
                    OverrideCode,
                    Document,
                    Details);

                IsOverrideDone = success;
            });
        }

        private void UpdatesOverrideCodes()
        {
            var index = OverrideLimitDetails.IndexOf(SelectedOverrideLimit);
          
            //    OverrideCodes = _allOverrideCodes
            //        .FirstOrDefault(x => x.RowId == index + 1)?.Codes;
  
            if (index >= 0)
            {
                OverrideCodes = _allOverrideCodes[index].Codes;
            }
            MaxLimitMessage = SelectedOverrideLimit?.MaxLimitMessage;
        }

        private void OverrideCodeChanged(object obj)
        {
            OverrideCode = obj.ToString();
        }

        private void LoadOverrideLimitDetails()
        {
            PerformAction(async () =>
            {
                var overrideLimitDetails = await _checkoutBusinessLogic.OverrideLimitDetails();
                _allOverrideCodes = overrideLimitDetails.OverrideCodes;
                OverrideLimitDetails = new ObservableCollection<PurchaseItemModel>(
                    from o in overrideLimitDetails.PurchaseItems
                    select new PurchaseItemModel
                    {
                        Amount = o.Amount,
                        DisplayQuota = o.DisplayQuota,
                        EquivalentQuantity = o.EquivalentQuantity,
                        Id = o.ProductId,
                        Price = o.Price,
                        Quantity = o.Quantity,
                        QuotaLimit = o.QuotaLimit,
                        QuotaUsed = o.QuotaUsed,
                        TypeId = o.ProductTypeId,
                        MaxLimitMessage = o.FuelOverLimitText
                    });

                SelectedOverrideLimit = OverrideLimitDetails.FirstOrDefault();

                Caption = overrideLimitDetails.Caption;
            });
        }

        public void ReInitialize()
        {
            OverrideLimitDetails = new ObservableCollection<PurchaseItemModel>();
            IsOverrideDone = false;
            OverrideCode = string.Empty;
            Document = string.Empty;
            Details = string.Empty;
            MaxLimitMessage = string.Empty;
        }
    }
}
