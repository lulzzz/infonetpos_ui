using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Sale
{
    public class UnsuspendSaleVM : VMBase
    {
        private SuspendSaleModel _selectedSale;
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

        public SuspendSaleModel SelectedSale
        {
            get { return _selectedSale; }
            set
            {
                _selectedSale = value;
                IsAcceptButtonEnable = _selectedSale != null;
                RaisePropertyChanged(nameof(SelectedSale));
            }
        }
        private ObservableCollection<SuspendSaleModel> _suspendedSales;
        public ObservableCollection<SuspendSaleModel> SuspendedSales
        {
            get { return _suspendedSales; }
            set
            {
                _suspendedSales = value;
                RaisePropertyChanged(nameof(SuspendedSales));
            }
        }

        public RelayCommand LoadAllSuspendSaleCommand;
        public RelayCommand UnsuspendSaleCommand;
        private readonly ISaleBussinessLogic _saleBussinessLogic;

        public UnsuspendSaleVM(ISaleBussinessLogic saleBussinessLogic)
        {
            _saleBussinessLogic = saleBussinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            UnsuspendSaleCommand = new RelayCommand(() => PerformAction(UnsuspendSale));
            LoadAllSuspendSaleCommand = new RelayCommand(LoadAllSuspendSale);
        }

        /// <summary>
        /// method to unsuspend sale
        /// </summary>
        /// <returns></returns>
        private async Task UnsuspendSale()
        {
            var startTime = DateTime.Now;
            try
            {
                if (SelectedSale != null)
                {
                    var result = await _saleBussinessLogic.UnsuspendSale(SelectedSale.SaleNumber);
                    var sale = result.ToModel();
                    MessengerInstance.Send(sale, "UpdateSale");
                    NavigateService.Instance.NavigateToHome();
                }

            }
            finally
            {
                var endTime = DateTime.Now;
                _log.Info(string.Format("Time Taken In Unsuspend Sale is {0}ms", (endTime - startTime).TotalMilliseconds));
            }
        }


        /// <summary>
        /// Method to get all suspended sale
        /// </summary>
        /// <returns></returns>
        private async void LoadAllSuspendSale()
        {
            try
            {
                LoadingService.ShowLoadingStatus(true);
                var response = await _saleBussinessLogic.GetAllSuspendSales();
                var suspendedSales = new ObservableCollection<SuspendSaleModel>();
                foreach (var sale in response)
                {
                    suspendedSales.Add(new SuspendSaleModel
                    {
                        Customer = sale.Customer,
                        SaleNumber = sale.SaleNumber,
                        Till = sale.Till
                    });
                }
                SuspendedSales = new ObservableCollection<SuspendSaleModel>(suspendedSales);
            }
            catch (UserNotAuthorizedException ex)
            {
                Log.Warn(ex);
                NavigateService.Instance.NavigateToLogin();
            }
            catch (InternalServerException ex)
            {
                Log.Error(ex);
                ShowNotification(ex.Error.Message, NavigateService.Instance.NavigateToHome,
                    NavigateService.Instance.NavigateToHome, ApplicationConstants.ButtonWarningColor);
            }
            catch (ApiDataException ex)
            {
                Log.Warn(ex);
                ShowNotification(ex.Error.Message, NavigateService.Instance.NavigateToHome,
                    NavigateService.Instance.NavigateToHome, ApplicationConstants.ButtonWarningColor);
            }
            finally
            {
                LoadingService.ShowLoadingStatus(false);
            }
        }
    }
}
