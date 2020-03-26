using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.UI.Model.Cash;
using Infonet.CStoreCommander.UI.Model.Stock;
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
    public class CashDrawVM : VMBase
    {
        private readonly ICashBusinessLogic _cashBusinessLogic;
        private readonly IReasonListBussinessLogic _reasonListBussinessLogic;
        private readonly IReportsBussinessLogic _reportsBussinessLogic;

        private CashDrawTypesModel _cashDrawTypes;
        private bool _isBillsVisible;
        private bool _isRollVisible;
        private string _total;
        private decimal _totalCurrencyValue;
        private string _reasonCode;
        private bool _isPrintReceiptOn;
        private bool _isCompletedEnabled;
        private bool _isPrintReceiptEnabled;

        public bool IsPrintReceiptEnabled
        {
            get { return _isPrintReceiptEnabled; }
            set
            {
                _isPrintReceiptEnabled = value;
                RaisePropertyChanged(nameof(IsPrintReceiptEnabled));
            }
        }

        public bool IsPrintReceiptOn
        {
            get { return _isPrintReceiptOn; }
            set
            {
                _isPrintReceiptOn = value;
                RaisePropertyChanged(nameof(IsPrintReceiptOn));
            }
        }

        public bool IsCompleteEnabled
        {
            get { return _isCompletedEnabled; }
            set
            {
                _isCompletedEnabled = value;
                RaisePropertyChanged(nameof(IsCompleteEnabled));
            }
        }


        public ObservableCollection<CashDrawModel> CashDrawModelList { get; set; }

        public string Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged(nameof(Total));
            }
        }

        public bool IsRollsVisible
        {
            get { return _isRollVisible; }
            set
            {
                _isRollVisible = value;
                RaisePropertyChanged(nameof(IsRollsVisible));
            }
        }

        public bool IsBillsVisible
        {
            get { return _isBillsVisible; }
            set
            {
                _isBillsVisible = value;
                RaisePropertyChanged(nameof(IsBillsVisible));
            }
        }

        public CashDrawTypesModel CashDrawTypes
        {
            get { return _cashDrawTypes; }
            set
            {
                _cashDrawTypes = value;
                RaisePropertyChanged(nameof(CashDrawTypes));
            }
        }

        public RelayCommand<object> MessageItemClickedCommand { get; private set; }
        public RelayCommand GetCashDrawTypesCommand;
        public RelayCommand CompleteCashDrawCommand;
        public RelayCommand<object> IncreaseCashDrawValueCommand { get; set; }
        public RelayCommand<object> DecreaseCashDrawValueCommand { get; set; }

        public CashDrawVM(IReasonListBussinessLogic reasonListBussinessLogic,
            IReportsBussinessLogic reportsBussinessLogic,
            ICashBusinessLogic cashBusinessLogic)
        {
            _reasonListBussinessLogic = reasonListBussinessLogic;
            _cashBusinessLogic = cashBusinessLogic;
            _reportsBussinessLogic = reportsBussinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            MessageItemClickedCommand = new RelayCommand<object>(GetReasonForCompleteCashDraw);
            CompleteCashDrawCommand = new RelayCommand(CompleteCashDraw);
            DecreaseCashDrawValueCommand = new RelayCommand<object>((s) => DecreaseCashDrawValue(s));
            IncreaseCashDrawValueCommand = new RelayCommand<object>((s) => IncreaseCashDrawValue(s));
            GetCashDrawTypesCommand = new RelayCommand(() => PerformAction(GetCashDrawTypes));
        }

        private void GetReasonForCompleteCashDraw(dynamic reason)
        {
            _reasonCode = reason.Code;
            CloseReasonPopup();
            PerformAction(CompleteCashDrawAsync);
        }

        private void CompleteCashDraw()
        {
            if (CacheBusinessLogic.UseReasonForCashDraw)
            {
                PerformAction(async () =>
                {
                    await GetReasonListAsync(EntityLayer.ReasonType.cashDraw,
                  MessageItemClickedCommand);
                });
            }
            else
            {
                PerformAction(CompleteCashDrawAsync);
            }
        }

        private async Task CompleteCashDrawAsync()
        {
            var registerNumber = await new Helper().GetRegisterNumber();
            var completeCashDrawModel = new CompleteCashDraw
            {
                Amount = _totalCurrencyValue,
                Bills = ConvertUIBillToEnitity(),
                Coins = ConvertUICoinToEnitity(),
                DrawReason = CacheBusinessLogic.UseReasonForCashDraw ? _reasonCode :
                                string.Empty,
                RegisterNumber = registerNumber,
                TillNumber = CacheBusinessLogic.TillNumber
            };

            var response = await _cashBusinessLogic.CompleteCashDraw(completeCashDrawModel);

            // Opening the cash drawer when amount is greater than zero
            if (_totalCurrencyValue > 0)
            {
                base.OpenCashDrawer();
            }
            
            if (IsPrintReceiptOn)
            {
                await PerformPrint(response);
            }

            NavigateService.Instance.NavigateToHome();
        }

        private List<Currency> ConvertUIBillToEnitity()
        {
            var UIModel = new List<Currency>();
            foreach (var bill in CashDrawTypes.Bills.Where(x => x.Quantity > 0))
            {
                UIModel.Add(new Currency
                {
                    CurrencyName = bill.Description,
                    Value = bill.Value,
                    Quantity = bill.Quantity
                });
            }
            return UIModel;
        }

        private List<Currency> ConvertUICoinToEnitity()
        {
            var UIModel = new List<Currency>();
            foreach (var coin in CashDrawTypes.Coins.Where(x => x.Quantity > 0))
            {
                UIModel.Add(new Currency
                {
                    CurrencyName = coin.Description,
                    Value = coin.Value,
                    Quantity = coin.Quantity
                });
            }
            return UIModel;
        }

        private void DecreaseCashDrawValue(dynamic s)
        {
            var currency = FindSelectedCurrencyInstance(s);

            if (currency.Quantity > 0)
            {
                currency.Quantity--;
                _totalCurrencyValue -= currency.Value;
                Total = _totalCurrencyValue.ToString(CultureInfo.InvariantCulture);
                UpdateCashDrawList(currency);
                if (currency.Quantity == 0)
                {
                    RemoveCashDrawList(currency);
                }
            }
        }

        private void IncreaseCashDrawValue(dynamic s)
        {
            var currency = FindSelectedCurrencyInstance(s);

            if (currency.Quantity < 99)
            {
                currency.Quantity++;
                _totalCurrencyValue += currency.Value;
                Total = _totalCurrencyValue.ToString(CultureInfo.InvariantCulture);
                UpdateCashDrawList(currency);
            }
        }

        private void RemoveCashDrawList(ProductDataModel currency)
        {
            if (CashDrawModelList.Any(x => x.Tender.Equals(currency.Description)))
            {
                var cashDrawModel = CashDrawModelList.FirstOrDefault(x => x.Tender.Equals(currency.Description));
                CashDrawModelList.Remove(cashDrawModel);
            }

            IsCompleteEnabled = IsCashTenderEmpty();
        }

        private bool IsCashTenderEmpty()
        {
            return CashDrawModelList.Count > 0;
        }

        private void UpdateCashDrawList(ProductDataModel currency)
        {
            if (CashDrawModelList.Any(x => x.Tender.Equals(currency.Description)))
            {
                CashDrawModelList.FirstOrDefault(x => x.Tender.Equals(currency.Description)).Amount
                    = currency.Quantity * currency.Value;
            }
            else
            {
                CashDrawModelList.Add(new CashDrawModel
                {
                    Value = currency.Value.ToString(CultureInfo.InvariantCulture),
                    Amount = currency.Value * currency.Quantity,
                    Tender = currency.Description
                });

                IsCompleteEnabled = IsCashTenderEmpty();
            }
        }

        private ProductDataModel FindSelectedCurrencyInstance(dynamic s)
        {
            var currency = new ProductDataModel();
            var bill = CashDrawTypes.Bills.Any(x => x.Description == s);
            var roll = CashDrawTypes.Coins.Any(x => x.Description == s);

            if (bill)
            {
                currency = CashDrawTypes.Bills.FirstOrDefault(x => x.Description.Equals(s));

            }
            else if (roll)
            {
                currency = CashDrawTypes.Coins.FirstOrDefault(x => x.Description.Equals(s));
            }

            return currency;
        }

        private async Task GetCashDrawTypes()
        {
            var response = await _cashBusinessLogic.GetCashDrawType();

            var tempCashDrawType = new CashDrawTypesModel();

            IsBillsVisible = response.Bills.Count > 0;
            IsRollsVisible = response.Coins.Count > 0;

            #region populate Coins
            foreach (var coin in response.Coins)
            {
                tempCashDrawType.Coins.Add(new ProductDataModel
                {
                    Description = coin.CurrencyName,
                    StockCode = coin.CurrencyName,
                    Quantity = 0,
                    ImageSource = new BitmapImage
                    {
                        UriSource = Helper.IsValidURI(coin.Image) ? new Uri(coin.Image) :
                        null
                    },
                    Value = coin.Value
                });
            }
            #endregion

            #region populate Bills
            foreach (var bill in response.Bills)
            {
                tempCashDrawType.Bills.Add(new ProductDataModel
                {
                    Description = bill.CurrencyName,
                    StockCode = bill.CurrencyName,
                    Quantity = 0,
                    ImageSource = new BitmapImage
                    {
                        UriSource = Helper.IsValidURI(bill.Image) ? new Uri(bill.Image) :
                        null
                    },
                    Value = bill.Value
                });
            }
            #endregion

            CashDrawTypes = tempCashDrawType;
        }

        private async Task GetReasonListAsync(EntityLayer.ReasonType reasonEnum, RelayCommand<object> reasonSelectCommand)
        {
            if (!PopupService.IsPopupOpen)
            {
                PopupService.PopupInstance.ReasonList?.Clear();
                var response = await _reasonListBussinessLogic.GetReasonListAsync(reasonEnum.ToString());

                foreach (var reason in response.Reasons)
                {
                    PopupService.PopupInstance.ReasonList.Add(new Reasons
                    {
                        Code = reason.Code,
                        Description = reason.Description
                    });
                }

                PopupService.PopupInstance.Title = response.ReasonTitle;
                PopupService.PopupInstance.MessageItemClicked = reasonSelectCommand;
                PopupService.PopupInstance.IsPopupOpen = true;
                PopupService.PopupInstance.IsReasonPopupOpen = true;

                PopupService.PopupInstance.CloseCommand = new
                    RelayCommand(CloseReasonPopup);
            }
        }

        internal void ResetVM()
        {
            if (CacheBusinessLogic.ForcePrintReceipt)
            {
                IsPrintReceiptEnabled = false;
                IsPrintReceiptOn = true;
            }
            else
            {
                IsPrintReceiptEnabled = true;
                IsPrintReceiptOn = false;
            }

            _totalCurrencyValue = 0M;
            Total = "0.00";
            IsCompleteEnabled = false;
            CashDrawModelList = new ObservableCollection<CashDrawModel>();
            CashDrawTypes = new CashDrawTypesModel();
        }
    }
}
