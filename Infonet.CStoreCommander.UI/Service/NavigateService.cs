using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.View;
using Infonet.CStoreCommander.UI.View.Ackroo;
using Infonet.CStoreCommander.UI.View.CashManager;
using Infonet.CStoreCommander.UI.View.Checkout;
using Infonet.CStoreCommander.UI.View.Checkout.TaxExempt;
using Infonet.CStoreCommander.UI.View.Common;
using Infonet.CStoreCommander.UI.View.Customer;
using Infonet.CStoreCommander.UI.View.DipInput;
using Infonet.CStoreCommander.UI.View.Fuel_Pricing;
using Infonet.CStoreCommander.UI.View.GiveX;
using Infonet.CStoreCommander.UI.View.Login;
using Infonet.CStoreCommander.UI.View.Payments;
using Infonet.CStoreCommander.UI.View.Prepay;
using Infonet.CStoreCommander.UI.View.PSInet;
using Infonet.CStoreCommander.UI.View.PumpOptions;
using Infonet.CStoreCommander.UI.View.PumpOptions.Fuel_Pricing;
using Infonet.CStoreCommander.UI.View.PumpOptions.Propane;
using Infonet.CStoreCommander.UI.View.Reports;
using Infonet.CStoreCommander.UI.View.Reports.ReportOptions;
using Infonet.CStoreCommander.UI.View.Reprint;
using Infonet.CStoreCommander.UI.View.Sale;
using Infonet.CStoreCommander.UI.View.Settings;
using Infonet.CStoreCommander.UI.View.Settings.MaintenanceOptions;
using Infonet.CStoreCommander.UI.View.Stock;
using Infonet.CStoreCommander.UI.View.TierLevel;
using Infonet.CStoreCommander.UI.ViewModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Service
{
    /// <summary>
    /// Contains methods for navigation through Application
    /// </summary>
    public class NavigateService
    {
        private static readonly Lazy<NavigateService> _lazy = new Lazy<NavigateService>(() => new NavigateService());

        internal Frame SecondFrame { get; set; } = new Frame();
        internal Frame FirstFrame { get; set; } = new Frame();
        internal Frame RootFrame { get; set; } = Window.Current.Content as Frame;
        internal Frame MaintenanceFrame { get; set; } = new Frame();
        internal Frame PaymentFrame { get; set; } = new Frame();
        internal Frame AckrooFrame { get; set; } = new Frame();
        internal Frame PaymentSourceFrame { get; set; } = new Frame();
        internal Frame ReportsFrame { get; set; } = new Frame();
        internal Frame MasterFrame { get; set; } = new Frame();
        internal Frame FuelPricingFrame { get; set; } = new Frame();
        internal Frame firstFrameOpenedPriorFreezeScreen { get; set; } = new Frame();
        internal Frame secondFrameOpenedPriorFreezeScreen { get; set; } = new Frame();
        internal Frame internalFrameOpenedPriorFreezeScreen { get; set; } = new Frame();

        internal string ChildOfMasterFrame { get; set; }

        internal void NavigateToMasterFrame()
        {
            Navigate(typeof(MasterPage));
        }

        internal bool IsReasonPopupOpen { get; set; }
        internal bool IsCheckoutOptionsOpen { get; set; }
        internal bool IsEnvelopeOpen { get; set; }
        internal bool IsGstPstPopupOpen { get; set; }
        internal bool IsQitePopupOpen { get; set; }
        internal bool IsAlertPopupOpen { get; set; }
        internal bool IsConfirmationPopupOpen { get; set; }
        internal bool IsReturnsPopupOpen { get; set; }
        internal bool IsMessagePopupOpen { get; set; }
        internal bool IsNavigatedFromFreezeScreen { get; set; }
        internal bool IsPurchaseOrderPopupOpen { get; set; }
        internal bool IsPumpOptionsPopupOpen { get; set; }
        internal bool IsPopupWithTextBoxOpen { get; set; }
        internal bool IsTaxExemptionPopupOpen { get; set; }
        internal bool IsFngtrPopupOpen { get; set; }
        internal static NavigateService Instance => _lazy.Value;

        public Frame Frame { get; set; }

        internal Tuple<Type, object> PreviousFrame { get; set; }

        internal void Navigate(Type pageType)
        {
            Frame.Navigate(pageType);
        }

        internal void OpenSalesCountReport()
        {
            NavigateReportsFrame(typeof(SalesCount));
        }

        internal void Navigate(Type pageType, Type masterPageType)
        {
            var masterPage = Frame.Content as Page;
            if (masterPage != null && masterPage.GetType() != masterPageType)
            {
                Frame.Navigate(masterPageType);
                masterPage = Frame.Content as Page;
            }
            var contentFrame = masterPage.FindName("ContentFrame") as Frame;
            contentFrame.Navigate(pageType);
        }

        internal void NavigateFirstFrame(Type frame)
        {
            if (NavigateService.Instance.FirstFrame?.Content?.GetType().Name != frame.Name)
            {
                Instance.Frame = Instance.FirstFrame;
                Instance.Navigate(frame);
                Instance.Frame = Instance.RootFrame;
            }
        }

        internal void NavigateSecondFrame(Type frame)
        {
            if (NavigateService.Instance.SecondFrame?.Content?.GetType().Name != frame.Name)
            {
                Instance.Frame = Instance.SecondFrame;
                Instance.Navigate(frame);
                Instance.Frame = Instance.RootFrame;
            }
        }

        internal void NavigateFuelPricingFrame(Type frame)
        {
            Instance.Frame = Instance.FuelPricingFrame;
            Instance.Navigate(frame);
            Instance.Frame = Instance.RootFrame;
        }

        internal void NavigateMaintenanceFrame(Type frame)
        {
            Instance.Frame = Instance.MaintenanceFrame;
            Instance.Navigate(frame);
            Instance.Frame = Instance.RootFrame;
        }

        internal void NavigatePaymentFrame(Type frame)
        {
            Instance.Frame = Instance.PaymentFrame;
            Instance.Navigate(frame);
            Instance.Frame = Instance.RootFrame;
        }

        internal void NavigateReportsFrame(Type frame)
        {
            Instance.Frame = Instance.ReportsFrame;
            Instance.Navigate(frame);
            Instance.Frame = Instance.RootFrame;
        }

        internal void NavigateToLastPrint()
        {
            NavigateFirstFrame(typeof(LastPrint));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateMasterFrame(Type frame)
        {
            Instance.Frame = Instance.MasterFrame;
            Instance.Navigate(frame);
            Instance.Frame = Instance.RootFrame;
        }

        internal void SecondFrameBackNavigation()
        {
            if (Instance.SecondFrame.CanGoBack)
            {
                Instance.SecondFrame.GoBack();
            }
        }

        internal void NavigateToPropaneGradeAmountNumberPad()
        {
            NavigateSecondFrame(typeof(AmountNumberPad));
        }

        internal void NavigateToGivexReport()
        {
            NavigateFirstFrame(typeof(GiveXReportGrid));
            NavigateSecondFrame(typeof(GivexReport));
        }

        internal void NavigateToCustomers()
        {
            NavigateFirstFrame(typeof(Customers));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToLoyaltyCustomers()
        {
            NavigateFirstFrame(typeof(LoyaltyCustomers));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToAddItemByCode(object stockCode)
        {
            NavigateSecondFrame(typeof(AddItemByCode));
        }

        internal void NavigateToTierlevelPage()
        {
            NavigateFirstFrame(typeof(TierLevel));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToMaintainence()
        {
            NavigateFirstFrame(typeof(Maintenance));
            NavigateSecondFrame(typeof(BlankView));
            NavigateMaintenanceFrame(typeof(ServicePump));
        }

        internal void NavigateToFuelPricingPage()
        {
            VMBase.LoadFuelPrices = true;
            NavigateFirstFrame(typeof(FuelPricing));
            NavigateFuelPricingFrame(typeof(BasePrice));
            NavigateSecondFrame(typeof(FuelReport));
        }

        internal void NavigateToPropaneGrade()
        {
            NavigateFirstFrame(typeof(PropaneGrade));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToPayment()
        {
            NavigateFirstFrame(typeof(Payment));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToNumericKeyPad()
        {
            NavigateSecondFrame(typeof(SetQuantityForHotCategories));
        }

        internal void NavigateToBottleReturnNumericKeyPad()
        {
            NavigateSecondFrame(typeof(NumberpadForBottleItems));
        }

        internal void NavigateToLogout()
        {
            NavigateMasterFrame(typeof(LogoutScreen));
        }

        internal void NavigateToLogin()
        {
            NavigateMasterFrame(typeof(LoginScreen));
        }

        internal void NavigateToExtendedScreen()
        {
            NavigateMasterFrame(typeof(ExtendedSplashScreen));
        }

        internal void NavigateToCloseTill()
        {
            NavigateMasterFrame(typeof(TillClose));
        }

        internal void RedirectToHome()
        {
            if (NavigateService.Instance.MasterFrame?.Content?.GetType().Name != nameof(HomeScreen))
            {
                NavigateMasterFrame(typeof(HomeScreen));
            }
        }

        internal void NavigateToHome()
        {
            if (NavigateService.Instance.FirstFrame?.Content?.GetType().Name != nameof(SaleGrid))
            {
                NavigateFirstFrame(typeof(SaleGrid));
            }
            if (NavigateService.Instance.SecondFrame?.Content?.GetType().Name != nameof(Products))
            {
                NavigateSecondFrame(typeof(Products));
            }
        }

        internal void NavigateToGiftCard()
        {
            NavigateFirstFrame(typeof(GiftCard));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateBack()
        {
            RedirectToHome();
        }

        internal void NavigateToAddCustomer()
        {
            NavigateFirstFrame(typeof(AddCustomer));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void ClearSecondFrame()
        {
            NavigateSecondFrame(typeof(BlankView));
        }


        internal void NavigateToStockSearch()
        {
            NavigateFirstFrame(typeof(SearchStock));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToAddStock()
        {
            NavigateFirstFrame(typeof(AddStock));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToBottleReturns()
        {
            NavigateFirstFrame(typeof(BottleReturns));
            //   NavigateToInnerFrame();
            NavigateSecondFrame(typeof(BottleItems));
        }

        internal void NavigateToUnsuspendedSale()
        {
            NavigateFirstFrame(typeof(UnsuspendSale));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToReturnSale()
        {
            NavigateFirstFrame(typeof(ReturnSale));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToReports()
        {
            NavigateFirstFrame(typeof(Reports));
        }

        internal void NavigateToReturnSaleItem()
        {
            NavigateFirstFrame(typeof(ReturnSaleItems));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToFreeze()
        {
            NavigateMasterFrame(typeof(Freezed));
        }

        internal void NavigateToGiveXPage()
        {
            NavigateFirstFrame(typeof(GiveX));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToTenderScreen()
        {
            //   NavigateToInnerFrame();
            NavigateSecondFrame(typeof(Tender));
        }

        internal void NavigateToSaleSummary()
        {
            NavigateFirstFrame(typeof(SalesSummary));
            NavigateToTenderScreen();
        }

        internal void NavigateToGiftCertificate()
        {
            NavigateFirstFrame(typeof(SalesSummary));
            NavigateSecondFrame(typeof(GiftCertificate));
        }

        internal void NavigateToCoupon()
        {
            NavigateFirstFrame(typeof(SalesSummary));
            NavigateSecondFrame(typeof(Coupon));
        }

        internal void NavigateToGiveXTender()
        {
            NavigateFirstFrame(typeof(SalesSummary));
            NavigateSecondFrame(typeof(GiveXTender));
        }

        internal void NavigateToPriceCheckPage()
        {
            NavigateFirstFrame(typeof(PriceCheck));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToCashDrop()
        {
            NavigateFirstFrame(typeof(CashDrop));
            NavigateSecondFrame(typeof(CashTender));
        }

        internal void NavigateToCashDraw()
        {
            NavigateFirstFrame(typeof(CashDraw));
            NavigateSecondFrame(typeof(CashType));
        }

        internal void NavigateToTillAuditReport()
        {
            NavigateReportsFrame(typeof(TillAuditReport));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToSaleCountReport()
        {
            NavigateSecondFrame(typeof(Report));
            NavigateReportsFrame(typeof(SalesCount));
        }

        internal void NavigateToFlashReport()
        {
            NavigateSecondFrame(typeof(Totals));
            NavigateReportsFrame(typeof(FlashReport));
        }

        internal void NavigateToAITE()
        {
            NavigateFirstFrame(typeof(SalesSummary));
            NavigateSecondFrame(typeof(AITE));
        }

        internal void NavigateToSITE()
        {
            NavigateFirstFrame(typeof(SalesSummary));
            NavigateSecondFrame(typeof(SITE));
        }

        internal void NavigateToSignatureCapture()
        {
            NavigateFirstFrame(typeof(SalesSummary));
            NavigateSecondFrame(typeof(SignatureScreen));
        }

        internal void NavigateToTenderNumberPad()
        {
            NavigateSecondFrame(typeof(TendersNumberPad));
        }

        internal void NavigateToDipReading()
        {
            NavigateFirstFrame(typeof(DipInput));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToTendersQuantityPad()
        {
            NavigateSecondFrame(typeof(TendersQuantityPad));
        }

        internal void NavigateToErrorPage()
        {
            NavigateFirstFrame(typeof(Errors));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToOverLimitScreen()
        {
            NavigateFirstFrame(typeof(TaxExemptOverLimit));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToOverrideLimitScreen()
        {
            NavigateFirstFrame(typeof(OverrideLimit));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToPayout()
        {
            NavigatePaymentFrame(typeof(Payouts));
        }

        internal void NavigateToFleet()
        {
            NavigatePaymentFrame(typeof(Fleet));
        }

        internal void NavigateToAR()
        {
            NavigatePaymentFrame(typeof(AR));
        }

        internal void NavigateToPrePay()
        {
            NavigateFirstFrame(typeof(Prepay));
            NavigateSecondFrame(typeof(PrepayNumberPad));
        }

        internal void ClearBackStack()
        {
            Instance.Frame = Instance.FirstFrame;
            Instance.Frame.BackStack.Clear();
            Instance.Frame = Instance.RootFrame;

            Instance.Frame = Instance.SecondFrame;
            Instance.Frame.BackStack.Clear();
            Instance.Frame = Instance.RootFrame;
        }

        internal void NavigateToReprint()
        {
            NavigateFirstFrame(typeof(Reprint));
            NavigateSecondFrame(typeof(ReceiptPreview));
        }

        internal void OpenedScreenPriorFreezeScreen()
        {
            switch (ChildOfMasterFrame)
            {
                case nameof(HomeScreen):
                    UnFreezeHomeScreen();
                    break;
                case nameof(LoginScreen):
                    NavigateToLogin();
                    break;
                case nameof(LogoutScreen):
                    NavigateToLogout();
                    break;
                default:
                    RedirectToHome();
                    break;
            }

            RestorePopup();
            IsNavigatedFromFreezeScreen = false;

            ResetAllValues();
        }

        private void ResetAllValues()
        {
            ChildOfMasterFrame = string.Empty;
            secondFrameOpenedPriorFreezeScreen = firstFrameOpenedPriorFreezeScreen = null;
        }

        private void UnFreezeHomeScreen()
        {

            RedirectToHome();
            if (firstFrameOpenedPriorFreezeScreen.SourcePageType != null)
            {
                var firstFrameContent = firstFrameOpenedPriorFreezeScreen.SourcePageType;
                NavigateFirstFrame(firstFrameContent);
            }
            if (secondFrameOpenedPriorFreezeScreen.SourcePageType != null)
            {
                var secondFrameContent = secondFrameOpenedPriorFreezeScreen.SourcePageType;
                NavigateSecondFrame(secondFrameContent);
            }

            if (internalFrameOpenedPriorFreezeScreen != null)
            {
                NavigateInternalFrame();
                internalFrameOpenedPriorFreezeScreen = null;
            }
        }

        private void NavigateInternalFrame()
        {
            switch (NavigateService.Instance.FirstFrame?.Content?.GetType()?.Name)
            {
                case nameof(Payment):
                    NavigatePaymentFrame(internalFrameOpenedPriorFreezeScreen.SourcePageType);
                    break;
                case nameof(Reports):
                    NavigateReportsFrame(internalFrameOpenedPriorFreezeScreen.SourcePageType);
                    break;
                case nameof(Maintenance):
                    NavigateMaintenanceFrame(internalFrameOpenedPriorFreezeScreen.SourcePageType);
                    break;
            }
        }

        private static void RestorePopup()
        {
            if (PopupService.PopupInstance.IsPopupOpen == true)
            {
                PopupService.PopupInstance.IsCheckoutOptionsOpen =
                NavigateService.Instance.IsCheckoutOptionsOpen;


                PopupService.PopupInstance.IsEnvelopeOpen =
                NavigateService.Instance.IsEnvelopeOpen;


                PopupService.PopupInstance.IsGstPstPopupOpen =
                NavigateService.Instance.IsGstPstPopupOpen;


                PopupService.PopupInstance.IsQitePopupOpen =
                NavigateService.Instance.IsQitePopupOpen;


                PopupService.PopupInstance.IsReasonPopupOpen =
                NavigateService.Instance.IsReasonPopupOpen;

                PopupService.PopupInstance.IsConfirmationPopupOpen =
                NavigateService.Instance.IsConfirmationPopupOpen;


                PopupService.PopupInstance.IsReturnsPopupOpen =
                NavigateService.Instance.IsReturnsPopupOpen;


                PopupService.PopupInstance.IsAlertPopupOpen =
                NavigateService.Instance.IsAlertPopupOpen;


                PopupService.PopupInstance.IsMessagePopupOpen =
                NavigateService.Instance.IsMessagePopupOpen;

                PopupService.PopupInstance.IsPurchaseOrderPopupOpen
                    = NavigateService.Instance.IsPurchaseOrderPopupOpen;

                PopupService.PopupInstance.IsPumpOptionsPopupOpen
                    = NavigateService.Instance.IsPumpOptionsPopupOpen;

                PopupService.PopupInstance.IsPopupWithTextBoxOpen
                 = NavigateService.Instance.IsPopupWithTextBoxOpen;

                PopupService.PopupInstance.IsTaxExemptionPopupOpen
                 = NavigateService.Instance.IsTaxExemptionPopupOpen;

                PopupService.PopupInstance.IsFngtrPopupOpen =
                    NavigateService.Instance.IsFngtrPopupOpen;
            }
        }

        internal void NavigateToStoreCredit()
        {
            SecondFrame.Navigate(typeof(StoreCredit));
        }

        public Frame GetInternalFrame()
        {
            switch (NavigateService.Instance.FirstFrame?.Content?.GetType()?.Name)
            {
                case nameof(Payment):
                    return PaymentFrame;
                case nameof(Reports):
                    return ReportsFrame;
                case nameof(Maintenance):
                    return MaintenanceFrame;
                default:
                    return null;
            }
        }

        internal void NavigateToFinish()
        {
            NavigateFirstFrame(typeof(Finish));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToBasePricing()
        {
            NavigateFuelPricingFrame(typeof(BasePrice));
            NavigateSecondFrame(typeof(FuelReport));
        }

        internal void NavigateToSaleVendorCoupon()
        {
            NavigateFirstFrame(typeof(SalesSummary));
            NavigateSecondFrame(typeof(VendorCoupon));
        }

        internal void NavigateToPaymentByFleet()
        {
            NavigateSecondFrame(typeof(FleetCard));
        }

        internal void NavigateToManualFuelSale()
        {
            if (NavigateService.Instance.FirstFrame?.Content?.GetType().Name != nameof(AddManual))
            {
                NavigateFirstFrame(typeof(AddManual));
            }
            if (NavigateService.Instance.SecondFrame?.Content?.GetType().Name != nameof(FuelPriceQuantityPad))
            {
                NavigateSecondFrame(typeof(FuelPriceQuantityPad));
            }
        }

        internal void NavigateToPricesToDisplay()
        {
            NavigateFirstFrame(typeof(FuelPricing));
            NavigateFuelPricingFrame(typeof(PricesToDisplay));
            NavigateSecondFrame(typeof(BlankView));
        }

        internal void NavigateToIncrements()
        {
            NavigateFirstFrame(typeof(FuelPricing));
            NavigateFuelPricingFrame(typeof(Increments));
            NavigateSecondFrame(typeof(Differences));
        }

        internal void NavigateToDifferences()
        {
            NavigateFirstFrame(typeof(FuelPricing));
            NavigateFuelPricingFrame(typeof(TaxExemptIncrements));
            NavigateSecondFrame(typeof(TaxExemptDifferences));
        }
        internal void NavigateAckrooFrame(Type frame)
        {
            Instance.Frame = Instance.AckrooFrame;
            Instance.Navigate(frame);
            Instance.Frame = Instance.RootFrame;
        }
        internal void NavigatePaymentSourceFrame(Type frame)
        {
            Instance.Frame = Instance.PaymentSourceFrame;
            Instance.Navigate(frame);
            Instance.Frame = Instance.RootFrame;
        }
        internal void NavigateToAkrooPage()
        {
            NavigateFirstFrame(typeof(Ackroo));
        }
        internal void NavigateToPSInetPage()
        {
            NavigateFirstFrame(typeof(PaymentSource));
        }
    }
}
