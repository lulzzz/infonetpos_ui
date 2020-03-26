using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.UI.Model.FuelPump;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.Messages;

namespace Infonet.CStoreCommander.UI.ViewModel.PumpOptions
{
    public class FinishVM : VMBase
    {
        private IFuelPumpBusinessLogic _fuelBusinessLogic;
        private UncompletePrepayModel _uncompletePrepayModel;
        private UncompleteSaleModel _selectedUncompleteSaleModel;

        public UncompleteSaleModel SelectedUncompleteSaleModel
        {
            get { return _selectedUncompleteSaleModel; }
            set
            {
                if (value != _selectedUncompleteSaleModel)
                {
                    _selectedUncompleteSaleModel = value;
                    RaisePropertyChanged(nameof(SelectedUncompleteSaleModel));
                }
            }
        }


        public UncompletePrepayModel UncompletePrepayModel
        {
            get { return _uncompletePrepayModel; }
            set
            {
                if (value != _uncompletePrepayModel)
                {
                    _uncompletePrepayModel = value;
                    RaisePropertyChanged(nameof(UncompletePrepayModel));
                }
            }
        }

        public RelayCommand CompleteChangeCommand { get; set; }
        public RelayCommand CompleteOverPaymentCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public FinishVM(IFuelPumpBusinessLogic fuelBusinessLogic)
        {
            _fuelBusinessLogic = fuelBusinessLogic;
            RegisterMessages();
            initializeCommands();
        }
        

        private void initializeCommands()
        {
            DeleteCommand = new RelayCommand(() =>
            {
                PerformAction(Delete);
            });

            CompleteChangeCommand = new RelayCommand(() =>
            {
                PerformAction(CompleteChange);
            });

            CompleteOverPaymentCommand = new RelayCommand(() =>
            {
                PerformAction(CompleteOverPayment);
            });

            BackCommand = new RelayCommand(PerformHomeNavigation);
        }

        private async Task CompleteOverPayment()
        {
            var response = await _fuelBusinessLogic.UncompleteOverPayment(SelectedUncompleteSaleModel.PumpId,
                  SelectedUncompleteSaleModel.UsedAmount, SelectedUncompleteSaleModel.UsedVolume,
                  SelectedUncompleteSaleModel.RegPrice, SelectedUncompleteSaleModel.PrepayAmount,
                  SelectedUncompleteSaleModel.PositionId, SelectedUncompleteSaleModel.Grade, SelectedUncompleteSaleModel.SaleNumber);

            PerformActionWithoutLoader(async () =>
            {
                var pumpsStatus = await _fuelBusinessLogic.InitializeFuelPump(true, CacheBusinessLogic.TillNumberForSale);
                MessengerInstance.Send(pumpsStatus);
            }, false);

            await PerformPrint(response.TaxExemptReceipt);

            if (response.OpenDrawer)
            {
                OpenCashDrawer();
            }

            PerformHomeNavigation();
        }

        private void PerformHomeNavigation()
        {
            NavigateService.Instance.NavigateToHome();
            MessengerInstance.Send<ResetPumpOptionMessage>(null);
            MessengerInstance.Send<EnableFuelOptionButtonMessage>(new EnableFuelOptionButtonMessage
            {
                EnableFuelOptionButton = true
            });
        }

        private async Task CompleteChange()
        {
            var response = await _fuelBusinessLogic.UncompletePrepayChange(SelectedUncompleteSaleModel.UsedAmount,
                SelectedUncompleteSaleModel.RegPrice, SelectedUncompleteSaleModel.UsedVolume,
                SelectedUncompleteSaleModel.Grade,
                SelectedUncompleteSaleModel.PositionId.ToString(), SelectedUncompleteSaleModel.PrepayAmount,
                SelectedUncompleteSaleModel.PumpId, SelectedUncompleteSaleModel.SaleNumber);

            PerformActionWithoutLoader(async () =>
            {
                var pumpsStatus = await _fuelBusinessLogic.InitializeFuelPump(true, CacheBusinessLogic.TillNumberForSale);
                MessengerInstance.Send(pumpsStatus);
            }, false);

            await PerformPrint(response.TaxExemptReceipt);

            if(UncompletePrepayModel.UncompleteSale.Count == 1)
            {
                MessengerInstance.Send(new PumpOptionRemoveMessage
                {
                    RemoveFinishOption = true,
                    RemoveManualOption = false
                });
            }

            if (response.OpenDrawer)
            {
                OpenCashDrawer();
            }
            if (!string.IsNullOrEmpty(response.ChangeDue))
            {
                ShowNotification(response.ChangeDue,
                    NavigateService.Instance.NavigateToHome,
                    NavigateService.Instance.NavigateToHome,
                    ApplicationConstants.ButtonConfirmationColor);
            }
            else
            {
                PerformHomeNavigation();
            }
        }

        private async Task Delete()
        {
            var response = await _fuelBusinessLogic.DeleteUncomplete(SelectedUncompleteSaleModel.PumpId);
            MessengerInstance.Send(response.ToModel(), "UpdateSale");
            PerformHomeNavigation();
        }

        public void ResetVM()
        {
            UncompletePrepayModel = new UncompletePrepayModel();

            PerformAction(async () =>
            {
                var response = await _fuelBusinessLogic.UncompletePrepayLoad();
                MapUIControl(response);

                if (UncompletePrepayModel.UncompleteSale?.Count > 0)
                {
                    SelectedUncompleteSaleModel = UncompletePrepayModel.UncompleteSale.FirstOrDefault();
                }
            });
        }

        private void MapUIControl(UncompletePrepayLoad response)
        {
            UncompletePrepayModel = new UncompletePrepayModel
            {
                Caption = response.Caption,
                IsChangeEnabled = response.IsChangeEnabled,
                IsDeleteEnabled = response.IsDeleteEnabled,
                IsDeleteVisible = response.IsDeleteVisible,
                IsOverPaymentEnabled = response.IsOverPaymentEnabled,
                UncompleteSale = MapUncompleteSale(response.UncompleteSale)
            };
        }

        private ObservableCollection<UncompleteSaleModel> MapUncompleteSale(List<UncompleteSale> uncompleteSale)
        {
            var uncompleteSaleModel = (from i in uncompleteSale
                                       select new UncompleteSaleModel
                                       {
                                           Grade = i.Grade,
                                           Mop = i.Mop,
                                           PositionId = i.PositionId,
                                           PrepayAmount = i.PrepayAmount,
                                           PrepayVolume = i.PrepayVolume,
                                           PumpId = i.PumpId,
                                           RegPrice = i.RegPrice,
                                           SaleGrade = i.SaleGrade,
                                           SaleNumber = i.SaleNumber,
                                           SalePosition = i.SalePosition,
                                           UnitPrice = i.UnitPrice,
                                           UsedAmount = i.UsedAmount,
                                           UsedVolume = i.UsedVolume
                                       }).ToList();

            return new ObservableCollection<UncompleteSaleModel>(uncompleteSaleModel);
        }

        private void RegisterMessages()
        {
            //  MessengerInstance.Register<>(this, Finish);
        }
    }
}
