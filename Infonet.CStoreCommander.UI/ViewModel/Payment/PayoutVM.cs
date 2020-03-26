using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Model.Payout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Payout;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.Service;
using System.Diagnostics;
using System.Globalization;

namespace Infonet.CStoreCommander.UI.ViewModel.Payment
{
    public class PayoutVM : VMBase
    {
        private readonly IPayoutBusinessLogic _payoutbusinessLogic;
        private readonly IReportsBussinessLogic _reportsBussinessLogic;
        private bool _isReasonEnable;
        private ObservableCollection<VendorModel> _vendorList;
        private VendorModel _selectedVendor;
        private Dictionary<string, string> _reasons;
        private int _selectedReasonIndex;
        private ObservableCollection<TaxModel> _taxes;
        private string _amount;
        private bool _isPrintOn;
        private string _searchText;
        private List<Vendors> _localVendorList;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));
            }
        }
        public bool IsPrintOn
        {
            get { return _isPrintOn; }
            set
            {
                _isPrintOn = value;
                RaisePropertyChanged(nameof(IsPrintOn));
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = Helper.SelectAllDecimalValue(value, _amount);
                RaisePropertyChanged(nameof(Amount));
            }
        }
        public ObservableCollection<TaxModel> Taxes
        {
            get { return _taxes; }
            set
            {
                _taxes = value;
                RaisePropertyChanged(nameof(Taxes));
            }
        }
        public int SelectedReasonIndex
        {
            get { return _selectedReasonIndex; }
            set
            {
                _selectedReasonIndex = value;
                RaisePropertyChanged(nameof(SelectedReasonIndex));
            }
        }
        public Dictionary<string, string> Reasons
        {
            get { return _reasons; }
            set
            {
                _reasons = value;
                RaisePropertyChanged(nameof(Reasons));
            }
        }
        public VendorModel SelectedVendor
        {
            get { return _selectedVendor; }
            set
            {
                _selectedVendor = value;
                RaisePropertyChanged(nameof(SelectedVendor));
            }
        }
        public ObservableCollection<VendorModel> VendorList
        {
            get { return _vendorList; }
            set
            {
                _vendorList = value;
                RaisePropertyChanged(nameof(VendorList));
            }
        }
        public bool IsReasonEnable
        {
            get { return _isReasonEnable; }
            set
            {
                _isReasonEnable = value;
                RaisePropertyChanged(nameof(IsReasonEnable));
            }
        }

        public RelayCommand CompletePaymentCommand { get; set; }
        public RelayCommand SearchVendorCommand { get; set; }

        public PayoutVM(IPayoutBusinessLogic payoutbusinessLogic,
            IReportsBussinessLogic reportsBussinessLogic)
        {
            _payoutbusinessLogic = payoutbusinessLogic;
            _reportsBussinessLogic = reportsBussinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CompletePaymentCommand = new RelayCommand(() => PerformAction(CompletePayment));
            SearchVendorCommand = new RelayCommand(SearchVendor);
        }

        private void SearchVendor()
        {
            var tempVendors = (from v in _localVendorList
                               where v.Name.ToLower().Contains(SearchText.ToLower()) ||
                               v.Code.ToLower().Contains(SearchText.ToLower())
                               select new VendorModel
                               {
                                   Code = v.Code,
                                   Name = v.Name
                               }).ToList();

            VendorList = new ObservableCollection<VendorModel>(tempVendors);

            if (VendorList.Count > 0)
            {
                SelectedVendor = VendorList.First();
            }
        }

        private async Task CompletePayment()
        {
            var timer = new Stopwatch();
            timer.Restart();

            try
            {
                var reasonCode = string.Empty;

                var taxes = (from t in Taxes
                             where Convert.ToDecimal(t.Amount, CultureInfo.InvariantCulture) != 0M
                             select new Tax
                             {
                                 Amount = t.Amount,
                                 Code = t.Code,
                                 Description = t.Description
                             }).ToList();

                if (SelectedReasonIndex > -1)
                {
                    reasonCode = Reasons?.ElementAt(SelectedReasonIndex).Key;
                }

                var response = await _payoutbusinessLogic.CompletePayout(taxes, SelectedVendor?.Code,
                    Amount, reasonCode);

                if (response.OpenCashDrawer)
                {
                    base.OpenCashDrawer();
                }

                foreach (var lineDisplayText in response.LineDisplay)
                {
                    WriteToLineDisplay(lineDisplayText);
                }

                if (IsPrintOn)
                {
                    PerformPrint(response.Receipt);
                }

                NavigateService.Instance.NavigateToHome();
                MessengerInstance.Send(response.Sale.ToModel(), "UpdateSale");
            }
            finally
            {
                timer.Stop();
                Log.Info(string.Format("Time taken in payout is {0}ms ", timer.ElapsedMilliseconds));
            }
        }

        private void GetVendorPayout()
        {
            PerformAction(async () =>
            {
                var response = await _payoutbusinessLogic.GetVendorPayout();
                _localVendorList = response.Vendors;
                PopulateVendors(_localVendorList);

                if (CacheBusinessLogic.ReasonForPayout)
                {
                    PopulateReasons(response.Reasons);
                }
                PopulateTaxes(response.Taxes);
            });

        }

        private void PopulateTaxes(List<Tax> taxes)
        {
            var tempTaxes = (from t in taxes
                             select new TaxModel
                             {
                                 Amount = t.Amount,
                                 Code = t.Code,
                                 Description = t.Description
                             }).ToList();
            
            Taxes = new ObservableCollection<TaxModel>(tempTaxes);
        }

        private void PopulateVendors(List<Vendors> vendors)
        {
            var tempVendors = (from v in vendors
                               select new VendorModel
                               {
                                   Code = v.Code,
                                   Name = v.Name
                               }).ToList();

            VendorList = new ObservableCollection<VendorModel>(tempVendors);
        }

        private void PopulateReasons(List<Reasons> reasons)
        {
            var tempReasons = new Dictionary<string, string>();

            foreach (var till in reasons)
            {
                tempReasons.Add(till.Code, till.Description);
            }
            
            Reasons = tempReasons;
        }

        internal void ResetVM()
        {
            IsReasonEnable = CacheBusinessLogic.ReasonForPayout;
            _localVendorList = new List<Vendors>();
            SelectedVendor = null;
            Taxes = null;
            SelectedReasonIndex = -1;
            GetVendorPayout();
            SearchText = string.Empty;
            Amount = string.Empty;
            IsPrintOn = false;
        }

    }
}
