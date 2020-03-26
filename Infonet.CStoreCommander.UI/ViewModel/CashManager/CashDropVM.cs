using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Model.Cash;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Infonet.CStoreCommander.UI.ViewModel.CashManager
{
    public class CashDropVM : VMBase
    {
        private string _envelopeNumber;
        private readonly ICashBusinessLogic _cashBussinessLogic;
        private readonly IReportsBussinessLogic _reportsBussinessLogic;
        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;
        private TenderModel _selectedTender;
        private ObservableCollection<TenderModel> _tenderList;
        private ObservableCollection<CashButtonModel> _cashButtons;
        private ObservableCollection<TenderModel> _selectedTenderList;
        private string _reason;
        private string _tenderTotalAmount;
        private bool _isSelectedTenderedEmpty;


        public TenderModel SelectedTender
        {
            get { return _selectedTender; }
            set
            {
                _selectedTender = value;
                RaisePropertyChanged(nameof(SelectedTender));
            }
        }
        public bool IsSelectedTenderEmpty
        {
            get { return _isSelectedTenderedEmpty; }
            set
            {
                _isSelectedTenderedEmpty = value;
                RaisePropertyChanged(nameof(IsSelectedTenderEmpty));
            }
        }
        public string TenderTotalAmount
        {
            get { return _tenderTotalAmount; }
            set
            {
                _tenderTotalAmount = value;
                RaisePropertyChanged(nameof(TenderTotalAmount));
            }
        }
        public ObservableCollection<TenderModel> SelectedTenderList
        {
            get { return _selectedTenderList; }
            set
            {
                _selectedTenderList = value;
                RaisePropertyChanged(nameof(SelectedTenderList));
            }
        }
        public ObservableCollection<CashButtonModel> CashButtons
        {
            get { return _cashButtons; }
            set
            {
                _cashButtons = value;
                RaisePropertyChanged(nameof(CashButtons));
            }

        }
        public ObservableCollection<TenderModel> TenderList
        {
            get { return _tenderList; }
            set
            {
                _tenderList = value;
                RaisePropertyChanged(nameof(TenderList));
            }
        }

        public RelayCommand CompleteTenderCommand { get; set; }
        public RelayCommand<object> OpenNumberPadCommand { get; set; }
        public RelayCommand<object> SetTenderAmountCommand { get; set; }

        public RelayCommand CancelTenderCommand { get; set; }

        public CashDropVM(IReportsBussinessLogic reportsBussinessLogic,
            ICheckoutBusinessLogic checkoutBusinessLogic,
            ICashBusinessLogic cashBussinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            _cashBussinessLogic = cashBussinessLogic;
            _reportsBussinessLogic = reportsBussinessLogic;
            MessengerInstance.Register<CashDropEnvelopeModel>(this,
                    "GetCashDropTenders", GetAllTenders);
            InitializeCommands();
        }

        private void InitializeData()
        {
            TenderList = new ObservableCollection<TenderModel>();
            SelectedTenderList = new ObservableCollection<TenderModel>();
            CashButtons = new ObservableCollection<CashButtonModel>();
            IsSelectedTenderEmpty = SelectedTenderList.Count > 0;
            PerformAction(GetCashButtons);
            TenderTotalAmount = "0.00";
        }

        private async Task GetCashButtons()
        {
            var response = await _cashBussinessLogic.GetCashButtons();
            CashButtons.Clear();
            foreach (var button in response)
            {
                CashButtons.Add(new CashButtonModel
                {
                    Button = button.Button,
                    Value = button.Value
                });
            }
        }

        private void InitializeCommands()
        {
            CancelTenderCommand = new RelayCommand(() => PerformAction(CancelTender));
            CompleteTenderCommand = new RelayCommand(() => PerformAction(CompleteTender));
            SetTenderAmountCommand = new RelayCommand<object>((s) => SetTenderAmount(s));
            OpenNumberPadCommand = new RelayCommand<object>((s) => OpenNumberPadForTenderItem(s));
        }

        private async Task CancelTender()
        {
            var response = await _checkoutBusinessLogic.CancelTenders(TransactionType.CashDrop.ToString());
            var sale = response.ToModel();
            MessengerInstance.Send(sale, "UpdateSale");

            MessengerInstance.Unregister<string>(this, "CurrencyTapped", CurrencyTapped);
            NavigateService.Instance.NavigateToHome();
        }

        private async Task CompleteTender()
        {
            var registerNumber = await new Helper().GetRegisterNumber();

            var completeCashDrop = new CompleteCashDrop
            {
                DropReason = _reason,
                EnvelopeNumber = _envelopeNumber,
                RegisterNumber = registerNumber,
                TillNumber = CacheBusinessLogic.TillNumber,
                Tenders = MapUIModelWithTender(SelectedTenderList)
            };

            var report = await _cashBussinessLogic.CompleteCashDrop(completeCashDrop);

            PerformPrint(report);            

            NavigateService.Instance.NavigateToHome();
            MessengerInstance.Unregister<string>(this, "CurrencyTapped", CurrencyTapped);
        }

        private void SetTenderAmount(dynamic s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var result = 0M;
                decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
                if (SelectedTender.MaximumValue < result && SelectedTender.MaximumValue != 0)
                {
                    var message = string.Format(ApplicationConstants.MaximumTenderValue,
                        SelectedTender.TenderName, SelectedTender.MaximumValue);

                    ShowConfirmationMessage(message,
                    () => UpdateTender(SelectedTender.MaximumValue),
                    () =>
                    {
                        SelectedTender = null;
                        NavigateService.Instance.SecondFrameBackNavigation();
                    },
                    null,
                    ApplicationConstants.ButtonFooterColor,
                    ApplicationConstants.ButtonFooterColor);
                }
                else
                {
                    UpdateTender(result);
                }
            }
        }

        private void UpdateTender(decimal amountEntered)
        {
            if (SelectedTenderList.Any(x => x.TenderCode == SelectedTender.TenderCode))
            {
                SelectedTenderList.First(x => x.TenderCode == SelectedTender.TenderCode).AmountEntered =
                    amountEntered.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                SelectedTender.AmountEntered = amountEntered.ToString(CultureInfo.InvariantCulture);
                SelectedTenderList.Add(SelectedTender);
            }

            var tenders = new List<Tender>();

            foreach (var tender in SelectedTenderList)
            {
                tenders.Add(new Tender
                {
                    AmountEntered = tender.AmountEntered,
                    TenderCode = tender.TenderCode
                });
            }

            var updatedTenders = new UpdateTenderPost
            {
                Tenders = tenders,
                DropReason = _reason
            };

            PerformAction(async () =>
            {
                try
                {
                    var response = await _cashBussinessLogic.UpdateTender(updatedTenders);
                    SelectedTenderList = MapTendersWithUITender(response.Tenders);
                    TenderTotalAmount = response.TenderedAmount;
                }
                catch (ApiDataException)
                {
                    SelectedTenderList.Remove(SelectedTender);
                    throw;
                }
                finally
                {
                    IsSelectedTenderEmpty = SelectedTenderList.Count > 0;
                    NavigateService.Instance.SecondFrameBackNavigation();
                }
            });
        }

        private void OpenNumberPadForTenderItem(dynamic s)
        {
            SelectedTender = SelectedTenderList.FirstOrDefault(x => x.TenderCode == s);
            if (SelectedTender == null)
            {
                SelectedTender = TenderList.FirstOrDefault(x => x.TenderCode == s);
            }
            NavigateService.Instance.NavigateToTenderNumberPad();

            var amountEntered = 0M;
            decimal.TryParse(SelectedTender?.AmountEntered, NumberStyles.Any, CultureInfo.InvariantCulture, out amountEntered);
            MessengerInstance.Send(true, "ResetNumberPadVM");
            MessengerInstance.Send(amountEntered.ToString(CultureInfo.InvariantCulture), "SetQuantiyUsingNumberPad");
        }

        private void GetAllTenders(CashDropEnvelopeModel cashDropEnvelope)
        {
            _envelopeNumber = cashDropEnvelope.EnvelopeNumber;
            _reason = cashDropEnvelope.Reason;
            InitializeData();
            PerformAction(async () =>
            {
                try
                {
                    var response = await _cashBussinessLogic.GetAllTenders(
                        TransactionType.CashDrop.ToString(),
                        false,
                        cashDropEnvelope.Reason);
                    TenderList = MapTendersWithUITender(response);
                }
                catch (Exception)
                {
                    NavigateService.Instance.NavigateToHome();
                    throw;
                }
            });
        }

        private ObservableCollection<TenderModel> MapTendersWithUITender(List<Tender> response)
        {
            var tenders = new ObservableCollection<TenderModel>();

            foreach (var tender in response)
            {
                tenders.Add(new TenderModel
                {
                    AmountEntered = tender.AmountEntered,
                    AmountValue = tender.AmountValue,
                    MaximumValue = tender.MaximumValue,
                    MinimumValue = tender.MinimumValue,
                    TenderCode = tender.TenderCode,
                    TenderName = tender.TenderName,
                    TenderClass = tender.TenderClass,
                    Quantity = 0,
                    Image = new BitmapImage
                    {
                        UriSource = Helper.IsValidURI(tender.ImageSource) ? new Uri(tender.ImageSource) :
                    null
                    }
                });
            }
            return tenders;
        }

        private List<Tender> MapUIModelWithTender(ObservableCollection<TenderModel> selectedTenders)
        {
            var tempTenders = new List<Tender>();

            foreach (var tender in selectedTenders)
            {
                tempTenders.Add(new Tender
                {
                    AmountEntered = tender.AmountEntered,
                    TenderCode = tender.TenderCode
                });
            }
            return tempTenders;
        }

        private void CurrencyTapped(string s)
        {
            if (SelectedTender.TenderClass.Equals(CacheBusinessLogic.BaseCurrency))
            {
                SetTenderAmount(s);
            }
        }

        internal void ResetVM()
        {
            base.OpenCashDrawer();
            MessengerInstance.Register<string>(this, "CurrencyTapped", CurrencyTapped);
        }
    }
}
