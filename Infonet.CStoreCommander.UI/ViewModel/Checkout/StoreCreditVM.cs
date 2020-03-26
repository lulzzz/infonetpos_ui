using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Checkout;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class StoreCreditVM : VMBase
    {
        private ObservableCollection<StoreCreditModel> _storeCredits;
        private ObservableCollection<StoreCreditModel> _selectedStoreCredit;
        private List<StoreCreditModel> _allStoreCredit;
        private string _transactionType;
        private string _tenderCode;
        private string _storeCreditNumber;
        private string _totalAmount;
        private bool _isAcceptButtonEnable;

        public bool IsAcceptButtonEnable
        {
            get { return _isAcceptButtonEnable; }
            set
            {
                _isAcceptButtonEnable = value;
                RaisePropertyChanged(nameof(IsAcceptButtonEnable));
            }
        }


        public string TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                RaisePropertyChanged(nameof(TotalAmount));
            }
        }

        public string StoreCreditNumber
        {
            get { return _storeCreditNumber; }
            set
            {
                _storeCreditNumber = value;
                RaisePropertyChanged(nameof(StoreCreditNumber));
            }
        }
        private StoreCreditModel _storeCreditToAdd;

        public StoreCreditModel StoreCreditToAdd
        {
            get { return _storeCreditToAdd; }
            set
            {
                _storeCreditToAdd = value;
                RaisePropertyChanged(nameof(StoreCreditToAdd));
            }
        }

        public ObservableCollection<StoreCreditModel> SelectedStoreCredit
        {
            get { return _selectedStoreCredit; }
            set
            {
                _selectedStoreCredit = value;
                IsAcceptButtonEnable = value?.Count > 0;
                decimal totalAmount = 0M;

                if (_selectedStoreCredit != null)
                {
                    foreach (var storeCredit in _selectedStoreCredit)
                    {
                        decimal selectedAmount = 0M;
                        var amount = storeCredit.Amount;
                        decimal.TryParse(amount, NumberStyles.Any, CultureInfo.InvariantCulture,out selectedAmount);
                        totalAmount += selectedAmount;
                    }
                }



                TotalAmount = totalAmount.ToString(CultureInfo.InvariantCulture);
            }
        }

        public ObservableCollection<StoreCreditModel> StoreCredits
        {
            get { return _storeCredits; }
            set
            {
                _storeCredits = value;
                RaisePropertyChanged(nameof(StoreCredits));
            }
        }

        public RelayCommand<object> SaveStoreCreditCommand;
        public RelayCommand SearchStoreCreditCommand;
        public RelayCommand GetSaleSummaryCommand { get; set; }

        private readonly IGiftCertificateBusinessLogic _giftCertificateBusinessLogic;
        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        public StoreCreditVM(IGiftCertificateBusinessLogic giftCertificateBusinessLogic,
            ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _giftCertificateBusinessLogic = giftCertificateBusinessLogic;
            _checkoutBusinessLogic = checkoutBusinessLogic;

            InitializeCommands();
            RegisterMessages();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<StoreCreditSelectedMessage>(this, LoadStoreCredit);
        }

        private void InitializeCommands()
        {
            GetSaleSummaryCommand = new RelayCommand(() => PerformAction(GetSaleSummary));
            SaveStoreCreditCommand = new RelayCommand<object>(SaveStoreCredit);
            SearchStoreCreditCommand = new RelayCommand(SearchStoreCredit);
        }

        private async Task GetSaleSummary()
        {
            var checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
            NavigateService.Instance.NavigateToTenderScreen();
            MessengerInstance.Send(checkoutSummary);
        }

        private void SearchStoreCredit()
        {
            var certificate = _allStoreCredit.FirstOrDefault(x => x.Number == StoreCreditNumber);
            if (certificate != null)
            {
                var giftCertificate = StoreCredits.FirstOrDefault(x => x.Number == certificate.Number);
                if (certificate.IsExpired)
                {
                    var message = string.Format(ApplicationConstants.GiftCertificateExpiredMessage,
                        certificate.Number);
                    StoreCreditNumber = string.Empty;
                    ShowNotification(message,
                        null,
                        null,
                        ApplicationConstants.ButtonWarningColor);
                }
                else
                {
                    StoreCreditToAdd = giftCertificate;
                }
            }
            else
            {
                var message = string.Format(ApplicationConstants.GiftCertificateNotFound, StoreCreditNumber);
                StoreCreditNumber = string.Empty;
                ShowNotification(message,
                    null,
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
        }

        private void SaveStoreCredit(dynamic args)
        {
            PerformAction(async () =>
            {
                var timer = new Stopwatch();
                timer.Restart();
                try
                {
                    var storeCredits = new List<StoreCredit>();

                    foreach (var item in args)
                    {
                        var storeCredit = item as StoreCreditModel;
                        storeCredits.Add(new StoreCredit
                        {
                            Amount = decimal.Parse(storeCredit.Amount, NumberStyles.Any, CultureInfo.InvariantCulture),
                            Number = storeCredit.Number
                        });
                    }

                    var tenderSummary = await _giftCertificateBusinessLogic.SaveStoreCredit(_transactionType, _tenderCode, storeCredits);
                    NavigateService.Instance.NavigateToTenderScreen();
                    MessengerInstance.Send(tenderSummary, "UpdateTenderSummary");
                }
                finally
                {
                    timer.Stop();
                    Log.Info(string.Format("Time taken in saving store credit is {0}ms ", timer.ElapsedMilliseconds));
                }
            });

        }

        private void LoadStoreCredit(StoreCreditSelectedMessage message)
        {
            PerformAction(async () =>
            {
                try
                {
                    _tenderCode = message.TenderCode;
                    _transactionType = message.TransactionType;
                    _allStoreCredit = new List<StoreCreditModel>(
                        from g in await _giftCertificateBusinessLogic.GetStoreCredit(_transactionType, message.TenderCode,
                        message.Amount.HasValue ? message.Amount.Value.ToString(CultureInfo.InvariantCulture) : null)
                        select new StoreCreditModel
                        {
                            Amount = g.Amount.ToString(CultureInfo.InvariantCulture),
                            IsExpired = g.IsExpired,
                            Number = g.Number,
                            SoldDate = g.SoldOn
                        });
                    StoreCredits = new ObservableCollection<StoreCreditModel>(_allStoreCredit.Where(x => !x.IsExpired));
                }
                catch (ApiDataException ex)
                {
                    ShowNotification(ex.Message,
                        NavigateService.Instance.NavigateToTenderScreen,
                        NavigateService.Instance.NavigateToTenderScreen,
                        ApplicationConstants.ButtonWarningColor);
                }
            });
        }

        internal void ReInitialize()
        {
            StoreCredits = new ObservableCollection<StoreCreditModel>();
            _allStoreCredit = new List<StoreCreditModel>();
            StoreCreditNumber = string.Empty;
            TotalAmount = "0";
        }
    }
}
