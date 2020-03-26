using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class CouponVM : VMBase
    {
        #region Private variables
        private string _number;
        private string _amount;
        private string _tenderCode;
        #endregion

        #region Properties
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                RaisePropertyChanged(nameof(Number));
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }
        #endregion

        #region Commands
        public RelayCommand AcceptCommand { get; private set; }
        public RelayCommand GetSaleSummaryCommand { get; set; }
        #endregion

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        public CouponVM(ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            InitializeCommands();
            RegisterMessages();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<string>(this,
                "SetSelectedTenderCode", SetSelectedTenderCode);
            MessengerInstance.Register<string>(this,
                "SetOutstandingAmount", SetOutstandingAmount);
        }

        private void SetOutstandingAmount(string amount)
        {
            Amount = amount;
        }

        private void SetSelectedTenderCode(string tenderCode)
        {
            _tenderCode = tenderCode;
        }

        private void InitializeCommands()
        {
            AcceptCommand = new RelayCommand(AcceptCoupon);
            GetSaleSummaryCommand = new RelayCommand(() => PerformAction(GetSaleSummary));
        }


        private async Task GetSaleSummary()
        {
            var checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
            NavigateService.Instance.NavigateToTenderScreen();
            MessengerInstance.Send(checkoutSummary);
        }

        private void AcceptCoupon()
        {
            PerformAction(async () =>
            {
                var tenderSummary =
                await _checkoutBusinessLogic.PaymentByCoupon(Number, _tenderCode);

                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send(tenderSummary, "UpdateTenderSummary");
            });
        }

        private void ShowErrors(List<Error> errors)
        {
            if (errors == null || errors.Count == 0)
            {
                return;
            }

            var message = errors[0].Message;
            errors.RemoveAt(0);

            ShowNotification(message,
                () => { ShowErrors(errors); },
                () => { ShowErrors(errors); },
                ApplicationConstants.ButtonWarningColor);
        }

        public void ReInitialize()
        {
            Number = string.Empty;
            Amount = string.Empty;
        }
    }
}
