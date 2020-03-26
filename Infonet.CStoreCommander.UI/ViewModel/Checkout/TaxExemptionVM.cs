using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.Messages;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    /// <summary>
    /// View model for Tax exemption screens
    /// </summary>
    public class TaxExemptionVM : VMBase
    {
        #region Private variables
        private TaxExemptVerification _verifyTaxExempt;
        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;
        #endregion

        public TaxExemptionVM(ICheckoutBusinessLogic checkoutBusinessLogic,
            IReportsBussinessLogic reportsBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            _reportsBusinessLogic = reportsBusinessLogic;
            RegisterMessages();
        }


        private void RegisterMessages()
        {
            MessengerInstance.Register<TaxExemptVerification>(this,
                SetVerificationStatus);
        }

        private void SetVerificationStatus(TaxExemptVerification verifyTax)
        {
            _verifyTaxExempt = verifyTax;
            PerformAction(async () =>
            {
                _verifyTaxExempt = await _checkoutBusinessLogic.VerifyTaxExempt();
                if (!string.IsNullOrEmpty(_verifyTaxExempt.ConfirmMessage?.Message))
                {
                    ShowConfirmationMessage(_verifyTaxExempt.ConfirmMessage.Message,
                        CompletePayment);
                }
                else
                {
                    CheckForTaxExemptions();
                }
            });
        }

        private void CompletePayment()
        {
            PerformAction(async () =>
            {
                var response = await _checkoutBusinessLogic.CompletePayment(TransactionType.Sale.ToString(),
                   false);

                NavigateService.Instance.NavigateToHome();
                MessengerInstance.Send(response.Sale.ToModel(), "UpdateSale");

                PerformPrint(response.Receipts);
            });
        }

        private async void CheckForTaxExemptions()
        {
            if (_verifyTaxExempt.ProcessAite)
            {
                NavigateService.Instance.NavigateToAITE();
            }
            else if (_verifyTaxExempt.ProcessSiteSale ||
                _verifyTaxExempt.ProcessSiteSaleRemoveTax ||
                _verifyTaxExempt.ProcessSiteReturn)
            {
                NavigateService.Instance.NavigateToSITE();

                var SITEMessage = new SITEMessage
                {
                    TreatyNumber = _verifyTaxExempt.TreatyNumber,
                    PermitNumberVisible = _verifyTaxExempt.ProcessSiteSaleRemoveTax,
                    TreatyName = _verifyTaxExempt.TreatyName
                };

                MessengerInstance.Send(SITEMessage,
                    "PermitNumberVisible");
            }
            else if (_verifyTaxExempt.ProcessQite)
            {
                PopupService.PopupInstance.IsPopupOpen = true;
                PopupService.PopupInstance.IsQitePopupOpen = true;
            }
            else
            {
                PerformAction(async () =>
                {
                    var checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
                    NavigateService.Instance.NavigateToSaleSummary();
                    MessengerInstance.Send(checkoutSummary);
                });
            }
        }
    }
}
