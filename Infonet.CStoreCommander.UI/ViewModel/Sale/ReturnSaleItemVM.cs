using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Sale
{
    public class ReturnSaleItemVM : VMBase
    {
        private readonly ISaleBussinessLogic _saleBussinessLogic;
        private readonly IReasonListBussinessLogic _reasonListBussinessLogic;

        private SaleModel _saleModel;
        private List<int> _selectedSaleLineItems { get; set; }
        private List<Error> _hashForSaleErrorPopup;
        private SaleLineModel _selectedSaleModel;
        private bool _isReturnEnabled;
        private string _reasonCode;
        public bool IsReturnEnabled
        {
            get { return _isReturnEnabled; }
            set
            {
                _isReturnEnabled = value;
                RaisePropertyChanged(nameof(IsReturnEnabled));
            }
        }

        public SaleLineModel SelectedSaleModel
        {
            get { return _selectedSaleModel; }
            set
            {
                _selectedSaleModel = value;
                IsReturnEnabled = _selectedSaleModel != null && CacheBusinessLogic.IsCurrentSaleEmpty;
                RaisePropertyChanged(nameof(SaleModel));
            }
        }

        public RelayCommand<object> MessageItemClickedCommand { get; private set; }
        public RelayCommand<object> ReturnSaleItemsCommand { get; set; }
        public SaleModel SaleModel
        {
            get { return _saleModel; }
            set
            {
                _saleModel = value;
                RaisePropertyChanged(nameof(SaleModel));
            }
        }


        public ReturnSaleItemVM(ISaleBussinessLogic saleBussinessLogic,
            IReasonListBussinessLogic reasonListBussinessLogic)
        {
            _reasonListBussinessLogic = reasonListBussinessLogic;
            _saleBussinessLogic = saleBussinessLogic;
            InitializeCommands();
            InitializeData();
            MessengerInstance.Register<SaleModel>(this, "ReturnSaleItem", UpdateRetunSale);

        }

        private void UpdateRetunSale(SaleModel saleModel)
        {
            SaleModel = saleModel;
            _hashForSaleErrorPopup.AddRange(SaleModel.SaleLineError);
            ShowSaleErrorPopups();
        }

        private void ShowSaleErrorPopups()
        {
            if (_hashForSaleErrorPopup?.Count > 0)
            {
                ShowNotification(_hashForSaleErrorPopup.FirstOrDefault().Message,
                  ShowSaleErrorPopups,
                  ShowSaleErrorPopups,
                  ApplicationConstants.ButtonWarningColor);
                _hashForSaleErrorPopup.RemoveAt(0);
            }
        }

        private void InitializeData()
        {
            SaleModel = new SaleModel();
            IsReturnEnabled = _selectedSaleLineItems != null && CacheBusinessLogic.IsCurrentSaleEmpty;

            _selectedSaleLineItems = new List<int>();
            _hashForSaleErrorPopup = new List<Error>();
        }

        public void ReInitialize()
        {
            IsReturnEnabled = _selectedSaleLineItems != null && CacheBusinessLogic.IsCurrentSaleEmpty;
        }

        private void InitializeCommands()
        {
            MessageItemClickedCommand = new RelayCommand<object>(ReturnSaleWithReason);
            ReturnSaleItemsCommand = new RelayCommand<object>((s) => ReturnSaleItems(s));
        }

        private void ReturnSaleWithReason(dynamic obj)
        {
            _reasonCode = obj.Code;
            CloseReasonPopup();
            PerformAction(ReturnSaleItems);
        }

        private void ReturnSaleItems(dynamic args)
        {
            var allowReason = false;
            var flag = true;
            _selectedSaleLineItems.Clear();

            foreach (var saleLineItem in args)
            {
                if (saleLineItem.AllowReturnReason && flag)
                {
                    allowReason = true;
                    flag = !flag;
                }
                _selectedSaleLineItems.Add(saleLineItem.LineNumber);
            }
            if (allowReason)
            {
                PerformAction(async () => { await GetReasonListAsync(EntityLayer.ReasonType.refunds, MessageItemClickedCommand); });
            }
            else
            {
                PerformAction(ReturnSaleItems);
            }
        }

        /// <summary>
        /// Method to get reason list
        /// </summary>
        /// <returns></returns>
        private async Task GetReasonListAsync(EntityLayer.ReasonType reasonEnum, RelayCommand<object> reasonSelectCommand)
        {
            if (!PopupService.IsPopupOpen)
            {
                PopupService.ReasonList?.Clear();
                var response = await _reasonListBussinessLogic.GetReasonListAsync(reasonEnum.ToString());

                foreach (var reason in response.Reasons)
                {
                    PopupService.ReasonList.Add(new Reasons
                    {
                        Code = reason.Code,
                        Description = reason.Description
                    });
                }

                PopupService.Title = response.ReasonTitle;
                PopupService.MessageItemClicked = reasonSelectCommand;
                PopupService.IsPopupOpen = true;
                PopupService.IsReasonPopupOpen = true;

                PopupService.CloseCommand = new RelayCommand(CloseReasonPopup);
            }
        }


        private async Task ReturnSaleItems()
        {
            var response = await _saleBussinessLogic.ReturnSaleItems(CacheBusinessLogic.TillNumber,
                SaleModel.TillNumber, SaleModel.SaleNumber,
                _selectedSaleLineItems, _reasonCode, EntityLayer.ReasonType.refunds.ToString());
            MessengerInstance.Send(response.ToModel(), "UpdateSale");
            NavigateService.Instance.NavigateToHome();
        }
    }
}
