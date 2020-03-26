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
    public class GiftCertificateVM : VMBase
    {
        #region Private variables
        private string _totalAmount;
        private string _certificateNumber;
        private List<GiftCertificateModel> _allGiftCertificates;
        private ObservableCollection<GiftCertificateModel> _giftCertificates;
        private GiftCertificateModel _giftCertificate;
        private GiftCertificateModel _giftCertificateToAdd;
        private ObservableCollection<GiftCertificateModel> _selectedGiftCertificates;
        #endregion

        #region Public Properties
        public ObservableCollection<GiftCertificateModel> GiftCertificates
        {
            get { return _giftCertificates; }
            set
            {
                _giftCertificates = value;
                RaisePropertyChanged(nameof(GiftCertificates));
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

        public string CertificateNumber
        {
            get { return _certificateNumber; }
            set
            {
                _certificateNumber = value;
                RaisePropertyChanged(nameof(CertificateNumber));
            }
        }

        public GiftCertificateModel GiftCertificate
        {
            get { return _giftCertificate; }
            set
            {
                _giftCertificate = value;
                RaisePropertyChanged(nameof(GiftCertificate));
            }
        }

        public GiftCertificateModel GiftCertificateToAdd
        {
            get { return _giftCertificateToAdd; }
            set
            {
                _giftCertificateToAdd = value;
                RaisePropertyChanged(nameof(GiftCertificateToAdd));
            }
        }
        #endregion

        #region Commands
        public RelayCommand SearchCertificateNumberCommand { get; private set; }
        public RelayCommand<object> SaveGiftCertificatesCommand { get; private set; }
        public RelayCommand GetSaleSummaryCommand { get; set; }

        public ObservableCollection<GiftCertificateModel> SelectedGiftCertificates
        {
            get
            {
                return _selectedGiftCertificates;
            }

            set
            {
                _selectedGiftCertificates = value;
                decimal totalAmount = 0M;

                if (_selectedGiftCertificates != null)
                {
                    foreach (var giftCertificate in _selectedGiftCertificates)
                    {
                        decimal selectedAmount = 0M;
                        var amount = giftCertificate.Amount;
                        decimal.TryParse(amount, NumberStyles.Any, CultureInfo.InvariantCulture, out selectedAmount);
                        totalAmount += selectedAmount;
                    }
                }

                TotalAmount = totalAmount.ToString(CultureInfo.InvariantCulture);
            }
        }
        #endregion

        private readonly IGiftCertificateBusinessLogic _giftCertificateBusinessLogic;
        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        public GiftCertificateVM(IGiftCertificateBusinessLogic giftCertificateBusinessLogic,
            ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _giftCertificateBusinessLogic = giftCertificateBusinessLogic;
            _checkoutBusinessLogic = checkoutBusinessLogic;
            InitializeCommands();
            RegisterMessages();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<GiftCertificateSelectedMessage>(this,
                LoadGiftCertificates);
        }

        private void InitializeCommands()
        {
            GetSaleSummaryCommand = new RelayCommand(() => PerformAction(GetSaleSummary));
            SearchCertificateNumberCommand = new RelayCommand(SearchCertificateNumber);
            SaveGiftCertificatesCommand = new RelayCommand<object>(SaveGiftCertificates);
        }

        private void SaveGiftCertificates(dynamic args)
        {
            PerformAction(async () =>
            {
                var timer = new Stopwatch();
                timer.Restart();
                try
                {
                    var giftCertificates = new List<GiftCertificate>();

                    if (SelectedGiftCertificates?.Count > 0)
                    {
                        foreach (var giftCertificate in SelectedGiftCertificates)
                        {
                            giftCertificates.Add(new GiftCertificate
                            {
                                Amount = decimal.Parse(giftCertificate.Amount, NumberStyles.Any, CultureInfo.InvariantCulture),
                                Number = giftCertificate.Number
                            });
                        }

                        var tenderSummary =
                        await _giftCertificateBusinessLogic.SaveGiftCertificates(giftCertificates);
                        NavigateService.Instance.NavigateToTenderScreen();
                        MessengerInstance.Send(tenderSummary, "UpdateTenderSummary");
                    }
                }
                finally
                {
                    timer.Stop();
                    Log.Info(string.Format("Time taken in saving gift certificate is {0}ms ", timer.ElapsedMilliseconds));
                }
            });
        }

        private async Task GetSaleSummary()
        {
            var checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
            NavigateService.Instance.NavigateToTenderScreen();
            MessengerInstance.Send(checkoutSummary);
        }


        private void SearchCertificateNumber()
        {
            var certificate = _allGiftCertificates.FirstOrDefault(x => x.Number == CertificateNumber);
            if (certificate != null)
            {
                var giftCertificate = GiftCertificates.FirstOrDefault(x => x.Number == certificate.Number);
                if (certificate.IsExpired)
                {
                    var message = string.Format(ApplicationConstants.GiftCertificateExpiredMessage,
                        certificate.Number);
                    CertificateNumber = string.Empty;
                    ShowNotification(message,
                        null,
                        null,
                        ApplicationConstants.ButtonWarningColor);
                }
                else
                {
                    GiftCertificateToAdd = giftCertificate;
                }
            }
            else {
                var message = string.Format(ApplicationConstants.GiftCertificateNotFound, CertificateNumber);
                CertificateNumber = string.Empty;
                ShowNotification(message,
                    null,
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
        }

        private void LoadGiftCertificates(GiftCertificateSelectedMessage message)
        {
            PerformAction(async () =>
            {
                try
                {
                    _allGiftCertificates = new List<GiftCertificateModel>(
                        from g in await _giftCertificateBusinessLogic.GetGiftCertificates(message.Amount, message.TenderCode,
                        message.TransactionType)
                        select new GiftCertificateModel
                        {
                            Amount = g.Amount.ToString(CultureInfo.InvariantCulture),
                            IsExpired = g.IsExpired,
                            Number = g.Number,
                            SoldDate = g.SoldOn
                        });
                    GiftCertificates = new ObservableCollection<GiftCertificateModel>(_allGiftCertificates.Where(x => !x.IsExpired));
                }
                catch (ApiDataException ex)
                {
                    ShowNotification(ex.Message,
                        NavigateService.Instance.NavigateToSaleSummary,
                        NavigateService.Instance.NavigateToSaleSummary,
                        ApplicationConstants.ButtonWarningColor);
                }
            });
        }

        public void ReInitialize()
        {
            _allGiftCertificates = new List<GiftCertificateModel>();
            GiftCertificates = new ObservableCollection<GiftCertificateModel>();
            GiftCertificate = null;
            CertificateNumber = string.Empty;
            TotalAmount = "0";
        }
    }
}
