namespace Infonet.CStoreCommander.DataAccessLayer.Utility
{
    /// <summary>
    /// Class containing the URLs for the APIs
    /// </summary>
    public static class Urls
    {
#if DEBUG
        private const string BaseUrl = "http://localhost:52287/api/v1/";
        //private const string BaseUrl = "http://Infonet.CStoreService.com/api/v1/";
#else
        private const string BaseUrl = "http://Infonet.CStoreService.com/api/v1/";
        //private const string BaseUrl = "http://172.16.0.42/api/v1/";
#endif
        internal const string RegisterSettings = BaseUrl + "system/getDeviceInfo?registerNumber={0}";

        internal const string ActiveTills = BaseUrl + "tills/activeTills";
        internal const string LoginPolicies = BaseUrl + "policy/login?ipAddress={0}";
        internal const string ActiveShifts = BaseUrl + "tills/activeShifts";
        internal const string Login = BaseUrl + "login/";
        internal const string GetPassword = BaseUrl + "login/getPassword?userName={0}";

        internal const string HotCategory = BaseUrl + "stock/hotProducts?pageIndex={0}&pageSize={1}";
        internal const string HotProductPages = BaseUrl + "stock/getHotProductPages";
        internal const string HotProducts = BaseUrl + "stock/getHotProducts?pageId={0}";
        internal const string InitializeNewSale = BaseUrl + "sale/new?tillNumber={0}&registerNumber={1}";

        internal const string AddCustomer = BaseUrl + "customer/loyalty/add";
        internal const string GetAllCustomers = BaseUrl + "customer?pageSize={0}&pageIndex={1}";
        internal const string GetAllLoyaltyCustomers = BaseUrl + "customer/loyalty?pageSize={0}&pageIndex={1}";
        internal const string SearchCustomers = BaseUrl + "customer/search?searchTerm={0}&tillNumber={1}&saleNumber={2}&pageSize={3}&pageIndex={4}";
        internal const string SearchLoyaltyCustomers = BaseUrl + "customer/loyalty/search?searchTerm={0}&saleNumber={1}&tillNumber={2}&pageSize={3}&pageIndex={4}";
        internal const string SetCustomerForSale = BaseUrl + "sale/setCustomer?customerCode={0}&tillNumber={1}&saleNumber={2}&registerNumber={3}";
        internal const string SetLoyalityCustomer = BaseUrl + "customer/loyalty/set";
        internal const string GetCustomerByCard = BaseUrl + "customer/getByCard";

        internal const string GetAllTaxes = BaseUrl + "tax/getTaxes";
        internal const string AddProduct = BaseUrl + "stock/add";
        internal const string GetStockItems = BaseUrl + "stock/items?pageSize={0}&pageIndex={1}";
        internal const string SearchStockItems = BaseUrl + "stock/search?searchTerm={0}&pageSize={1}&pageIndex={2}";

        internal const string GetBottlesUrl = BaseUrl + "bottle/getBottles?pageId={0}";
        internal const string BottleReturnUrl = BaseUrl + "bottle/returns";

        internal const string GetReasonList = BaseUrl + "reason/getReason?reason={0}";

        internal const string VoidSale = BaseUrl + "sale/void";
        internal const string ValidateVoidSale = BaseUrl + "sale/validateVoid?saleNumber={0}&tillNumber={1}";
        internal const string GetAllSuspendedSale = BaseUrl + "sale/suspendedSales?tillNumber={0}";

        #region Sale
        internal const string UnsuspendSale = BaseUrl + "sale/unsuspend?saleNumber={0}&tillNumber={1}";
        internal const string SuspendSale = BaseUrl + "sale/suspend?saleNumber={0}&tillNumber={1}";
        internal const string GetSaleList = BaseUrl + "returnSale/GetAllSales?pageIndex={0}&pageSize={1}";
        internal const string SearchSaleList = BaseUrl + "returnSale/searchSale?pageIndex={0}&pageSize={1}&searchTerm={2}&saledate={3}";
        internal const string ReturnSale = BaseUrl + "returnSale/return";
        internal const string GetSaleBySaleNumber = BaseUrl + "returnSale/getSale?SaleNumber={0}&TillNumber={1}";
        internal const string ReturnSaleItems = BaseUrl + "returnSale/returnItems";
        #endregion
        internal const string GetPSTransactions = BaseUrl + "paymentsource/getPSTransactions?TILL_NUM={0}&SALE_NO={1}&PastDays={2}";
        internal const string GetPSRefundInfo = BaseUrl + "paymentsource/getRefundInfo?TransID={0}&SALE_NO={1}&TILL_NUM={2}";
        internal const string GetPSLogos = BaseUrl + "paymentsource/getPSLogos";
        internal const string SavePSTransactionID = BaseUrl + "paymentsource/savePSTansID?TILL_NUM={0}&SALE_NO={1}&LINE_NUM={2}&TransID={3}";
        internal const string GetPSVoucherInfo = BaseUrl + "paymentsource/getPSVoucherInfo?ProdName={0}";
        internal const string GetPSTransactionID = BaseUrl + "paymentsource/getPSTransactionID";
        internal const string GetPSProfile = BaseUrl + "paymentsource/getPSProfile";
        internal const string GetDownloadedFiles = BaseUrl + "paymentsource/getNewFile";
        internal const string GetPSProducts = BaseUrl + "paymentsource/getPSProducts";
        internal const string GetFuelCodes = BaseUrl + "discount/getFuelCodes";
        internal const string GetAllDiscounts = BaseUrl + "discount/getDiscounts";
        internal const string VerifyTaxExempt = BaseUrl + "tenders/verifyTaxExempt?saleNumber={0}&tillNumber={1}&registerNumber={2}";
        internal const string ValidateAITE = BaseUrl + "tenders/aite/validate";
        internal const string GstPstExempt = BaseUrl + "tenders/aite/gstPstExempt";
        internal const string AffixBarcode = BaseUrl + "tenders/aite/affixBarCode";
        internal const string RemoveSiteTax = BaseUrl + "tenders/site/removeTax";
        internal const string ValidateSite = BaseUrl + "tenders/site/validate";
        internal const string ValidateQite = BaseUrl + "tenders/qite/validate";
        internal const string OverLimitDetails = BaseUrl + "tenders/overLimitDetails?saleNumber={0}&tillNumber={1}";
        internal const string OverrideLimitDetails = BaseUrl + "tenders/overRideLimitDetails?saleNumber={0}&tillNumber={1}&treatyNumber={2}&treatyName={3}";
        internal const string OverrideLimitOverride = BaseUrl + "tenders/overRideLimit/override";
        internal const string CompleteOverrideLimit = BaseUrl + "tenders/overRideLimit/complete?saleNumber={0}&tillNumber={1}&registerNumber={2}";
        internal const string CompleteOverLimit = BaseUrl + "tenders/overLimit/complete";
        internal const string GetGiftCertificates = BaseUrl + "tenders/getGiftCerts";
        internal const string SaveGiftCertificates = BaseUrl + "tenders/saveGiftCertificates";
        internal const string SaleSummary = BaseUrl + "tenders/salesummary?kickBackAmount={0}";
        internal const string FNGTR = BaseUrl + "tenders/site/fngtr";
        internal const string SaveSignature = BaseUrl + "signature/save?saleNumber={0}&tillNumber={1}";

        internal const string CheckKickBackResponse = BaseUrl + "kickback/checkKickBackResponse?response={0}&tillNumber={1}&registerNumber={2}&saleNumber={3}";
        internal const string VerifyKickBack = BaseUrl + "tenders/verifyKickBack?PointCardNumber={0}&PhoneNumber={1}&registerNumber={2}&tillNumber={3}&saleNumber={4}";
        internal const string CheckKickBackBalance = BaseUrl + "kickback/checkKickBackBalance?tillNumber={0}&saleNumber={1}&pointCardNum={2}";
        internal const string ValidateGasKing = BaseUrl + "kickback/validateGasKing?tillNumber={0}&saleNumber={1}&registerNumber={2}";


        internal const string VerifyStock = BaseUrl + "sale/items/verifyAdd";

        internal const string AddStockToSale = BaseUrl + "sale/items/add";
        internal const string UpdateSaleLine = BaseUrl + "sale/items/update/";
        internal const string RemoveSaleLine = BaseUrl + "sale/items/remove?tillNumber={0}&saleNumber={1}&lineNumber={2}";
        internal const string WriteOff = BaseUrl + "sale/writeOff";


        internal const string Policies = BaseUrl + "policy/getAll?registerNumber={0}";
        internal const string RefreshPolicies = BaseUrl + "policy/refresh?tillNumber={0}&saleNumber={1}";
        internal const string ChangePassword = BaseUrl + "login/changePassword";
        internal const string ChangeUser = BaseUrl + "login/changeUser";

        internal const string GetActiveTheme = BaseUrl + "themes/active?ipAddress={0}";

        internal const string MaintenanceCloseBatch = BaseUrl + "maintenance/closeBatch";
        internal const string Initialize = BaseUrl + "maintenance/initialize";
        internal const string Prepay = BaseUrl + "maintenance/prepay?newState={0}";
        internal const string PostPay = BaseUrl + "maintenance/postPay?newState={0}";
        internal const string GetFuelVolume = BaseUrl + "fuelPump/propane/GetFuelVolume";
        #region Ackroo
        internal const string GetAckrooStockCode = BaseUrl + "ackroo/getAValidAckrooStock";
        internal const string GetCarwashCategories = BaseUrl + "ackroo/getCarwashCategories";
        internal const string GetAckrooCarwashStockCode = BaseUrl + "ackroo/getAckrooCarwashStockCode?sDesc={0}";
        #endregion
        #region GiveX
        internal const string GiveXBalance = BaseUrl + "givex/balance?givexCardNumber={0}&saleNumber={1}&tillNumber={2}";
        internal const string DeactivateGivexCard = BaseUrl + "givex/deactivate";
        internal const string CloseBatch = BaseUrl + "givex/closeBatch";
        internal const string ActivateGiveXCard = BaseUrl + "givex/activate";
        internal const string AddAmount = BaseUrl + "givex/Increase";
        internal const string AdjustAmount = BaseUrl + "givex/adjust";
        internal const string GetGivexStockCode = BaseUrl + "givex/getStockCode";
        internal const string GetGivexReport = BaseUrl + "givex/report?reportDate={0}";

        #endregion

        internal const string LogoutUser = BaseUrl + "tills/logout";

        #region Report
        internal const string GetReceiptHeader = BaseUrl + "report/getReceiptHeader";
        internal const string GetAllDepartment = BaseUrl + "department/getAllDepartments";
        internal const string GetAllTill = BaseUrl + "tills/getAllTills";
        internal const string GetAllShift = BaseUrl + "tills/getAllShifts";
        internal const string GetSaleCountReport = BaseUrl + "report/getSaleSummaryReport?departmentId={0}&tillNumber={1}&shiftNumber={2}&loggedTillNumber={3}";
        internal const string GetFlashReport = BaseUrl + "report/getFlashReport?tillNumber={0}";
        internal const string GetTillAuditReport = BaseUrl + "report/getTillAuditReport?tillNumber={0}";
        internal const string GetKickBackBalanceReport = BaseUrl + "report/printKickbackReport?points={0}";

        #endregion

        internal const string ExactChange = BaseUrl + "payment/byExactCash";

        #region Cash
        internal const string GetCashDrawTypes = BaseUrl + "cash/getDrawTypes";
        internal const string CompleteCashDraw = BaseUrl + "cash/completeDraw";
        internal const string GetAllTenders = BaseUrl + "tenders/getTenders?transactionType={0}&saleNumber={1}&tillNumber={2}&billTillClose={3}&dropReason={4}";
        internal const string GetCashButtons = BaseUrl + "cash/getCashButtons";
        internal const string UpdateTenders = BaseUrl + "cash/updateTenders";
        internal const string UpdateTender = BaseUrl + "tenders/updateTenders?isAmountEnteredManually={0}";
        internal const string CompleteCashDrop = BaseUrl + "cash/completeDrop";
        internal const string OpenCashDrawer = BaseUrl + "cash/openCashDrawer";
        #endregion

        #region Price
        internal const string PriceCheckByCode = BaseUrl + "priceCheck/getStockPrices?stockCode={0}&tillNumber={1}&saleNumber={2}&registerNumber={3}";
        internal const string EditRegularPrice = BaseUrl + "priceCheck/updateRegularPrice";
        internal const string EditSpecialPrice = BaseUrl + "priceCheck/updateSpecialPrice";
        #endregion

        internal const string CancelTender = BaseUrl + "tenders/cancelTenders?saleNumber={0}&tillNumber={1}&transactionType={2}";
        internal const string CompletePayment = BaseUrl + "payment/complete";
        internal const string RunAway = BaseUrl + "payment/runAway";
        internal const string PaymentByCard = BaseUrl + "payment/byCard";
        internal const string PaymentByGivex = BaseUrl + "tenders/saveGivex";
        internal const string PaymentByCoupon = BaseUrl + "payment/byCoupon";

        internal const string GetAllARCustomer = BaseUrl + "customer/ar?pageIndex={0}&pageSize={1}";
        internal const string SearchARCustomer = BaseUrl + "customer/arsearch?searchTerm={0}&pageIndex={1}&pageSize={2}";
        internal const string SaveARPayment = BaseUrl + "tenders/arPayment";
        internal const string GetCardInformation = BaseUrl + "tenders/getCardInformation";
        internal const string GetARCustomerByCustomerCode = BaseUrl + "customer/ar/getByCard";
        internal const string GetTreatyName = BaseUrl + "tenders/getTreatyName?treatyNumber={0}&captureMethod={1}";

        internal const string GetStoreCredit = BaseUrl + "tenders/getStoreCredits";
        internal const string SaveStoreCredit = BaseUrl + "tenders/saveStoreCredits";

        #region Account
        internal const string PaymentByAccount = BaseUrl + "payment/byAccount";
        internal const string VerifyByAccount = BaseUrl + "payment/verifyByAccount";
        internal const string VerfifyMultiPO = BaseUrl + "payment/validatePO?saleNumber={0}&tillNumber={1}&purchaseOrder={2}";
        #endregion

        #region DipInput
        internal const string DipInputGet = BaseUrl + "dipInput/get";
        internal const string DipInputPrint = BaseUrl + "dipInput/print?tillNumber={0}&shiftNumber={1}&registerNumber={2}";
        internal const string SaveDipInput = BaseUrl + "dipInput/save";
        #endregion

        internal const string GetMessage = BaseUrl + "message";
        internal const string AddMessage = BaseUrl + "message/add";

        #region Payout
        internal const string GetVendorPayout = BaseUrl + "payout/getVendorPayout";
        internal const string PayoutComplete = BaseUrl + "payout/complete";
        internal const string ValidateFleet = BaseUrl + "payout/validateFleet";
        internal const string FleetPayment = BaseUrl + "payout/fleetPayment";
        #endregion

        #region Reprint
        internal const string GetReprintReportNames = BaseUrl + "report/getReprintReportNames";
        internal const string GetReprintReport = BaseUrl + "report/getReprintReport?saleNumber={0}&saleDate={1}&reportType={2}";
        internal const string GetReprintSales = BaseUrl + "report/sales?reportType={0}&date={1}";
        #endregion

        #region Fuel Pump
        internal const string InitializeFuelPump = BaseUrl + "fuelPump/intialize?tillNumber={0}";
        internal const string GetHeadOfficeNotification = BaseUrl + "fuelPump/getHeadOfficeNotification";
        internal const string GetPumpStatus = BaseUrl + "fuelPump/getPumpsStatus?tillNumber={0}";
        internal const string LoadGrades = BaseUrl + "fuelPump/loadGrades?pumpId={0}&switchPrepay={1}&tillNumber={2}";
        internal const string GetVendorCoupon = BaseUrl + "payment/getVendorCoupon?saleNumber={0}&tillNumber={1}&tenderCode={2}";
        internal const string AddVendorCoupon = BaseUrl + "payment/addVendorCoupon";
        internal const string RemoveVendorCoupon = BaseUrl + "payment/removeVendorCoupon";
        internal const string PaymentVendorCoupon = BaseUrl + "payment/byVendorCoupon";
        internal const string ResumeAll = BaseUrl + "fuelPump/resumeall";
        internal const string StopAll = BaseUrl + "fuelPump/stopall";
        internal const string StopBroadcast = BaseUrl + "fuelPump/stopBroadcast";
        internal const string CheckError = BaseUrl + "system/checkError";

        internal const string AddPrepay = BaseUrl + "fuelPump/prepay/add";
        internal const string DeletePrepay = BaseUrl + "fuelPump/prepay/delete";
        internal const string SwitchPrepay = BaseUrl + "fuelPump/prepay/switch";

        internal const string UpdateFuelPrice = BaseUrl + "fuelPump/updateFuelPrice?option={0}&counter={1}";
        internal const string AddBasket = BaseUrl + "fuelPump/basket/add";
        internal const string GetPumpAction = BaseUrl + "fuelPump/getPumpAction?pumpId={0}&stopPressed={1}&resumePressed={2}";
        internal const string LoadTierLevel = BaseUrl + "fuelPump/tierLevel/load";
        internal const string UpdateTierLevel = BaseUrl + "fuelPump/tierLevel/update";

        internal const string LoadPropaneGrade = BaseUrl + "fuelPump/propane/loadGrades";
        internal const string LoadPropanePumps = BaseUrl + "fuelPump/propane/loadPumps?gradeId={0}";
        internal const string PumpTest = BaseUrl + "payment/pumpTest";
        internal const string AddPropane = BaseUrl + "fuelPump/propane/add";

        internal const string AddManually = BaseUrl + "fuelPump/addManually";
        #endregion

        internal const string SaveProfilePrompt = BaseUrl + "tenders/saveProfilePrompts";

        internal const string GetSounds = BaseUrl + "system/getSounds";
        internal const string TaxExemption = BaseUrl + "sale/setTaxExemption";

        #region Till close
        internal const string CloseTill = BaseUrl + "tills/closeTill?tillNumber={0}&saleNumber={1}";
        internal const string ReadTankDip = BaseUrl + "tills/readTankDip?tillNumber={0}";
        internal const string EndShift = BaseUrl + "tills/endShift?tillNumber={0}&saleNumber={1}";
        internal const string ValidateTillClose = BaseUrl + "tills/validateTillClose?tillNumber={0}&saleNumber={1}";
        internal const string UpdateTillClose = BaseUrl + "tills/updateTillClose";
        internal const string FinishTillClose = BaseUrl + "tills/finishClose?tillNumber={0}&registerNumber={1}&readTankDip={2}&readTotaliser={3}";
        #endregion

        #region Fuel Price
        internal const string GroupFuelPrices = BaseUrl + "fuelPrice/loadGroupBasePrices";
        internal const string FuelPrices = BaseUrl + "fuelPrice/loadBasePrices";
        internal const string ReadTotalizer = BaseUrl + "fuelPrice/readTotalizer?tillNumber={0}";
        internal const string SetGroupBasePrice = BaseUrl + "fuelPrice/setGroupBasePrice";
        internal const string SaveGroupBasePrices = BaseUrl + "fuelPrice/saveBaseGroupPrices";
        internal const string SetBasePrice = BaseUrl + "fuelPrice/setBasePrice";
        internal const string VerifyBasePrices = BaseUrl + "fuelPrice/verifyBasePrices";
        internal const string VerifyGroupBasePrices = BaseUrl + "fuelPrice/verifyGroupBasePrices";
        internal const string SaveBasePrices = BaseUrl + "fuelPrice/saveBasePrices";
        internal const string LoadPricesToDisplay = BaseUrl + "fuelPrice/loadPricesToDisplay";
        internal const string SavePricesToDisplay = BaseUrl + "fuelPrice/savePricesToDisplay";
        internal const string LoadPriceIncrementsAndDecrements = BaseUrl + "fuelPrice/loadPriceIncrementDecrement?taxExempt={0}";
        internal const string SetPriceDecrement = BaseUrl + "fuelPrice/setPriceDecrement";
        internal const string SetPriceIncrement = BaseUrl + "fuelPrice/setPriceIncrement";
        #endregion

        internal const string GetError = BaseUrl + "system/getError";
        internal const string ClearError = BaseUrl + "system/clearError";
        internal const string UncompleteOverPayment = BaseUrl + "fuelPump/uncompletePrepay/overpayment?pumpId={0}&saleNum={1}&tillNumber={2}&finishAmount={3}&finishQty={4}&finishPrice={5}&prepayAmount={6}&positionId={7}&gradeId={8}";
        internal const string UncompletePrepayChange = BaseUrl + "fuelPump/uncompletePrepay/change";
        internal const string UncompletePrepayLoad = BaseUrl + "fuelPump/uncompletePrepay/load?tillNumber={0}";
        internal const string UncompleteDelete = BaseUrl + "uncompletePrepay/delete?pumpId={0}&saleNum={1}&tillNumber={2}";

        #region Carwash

        internal const string GetCarwashServerStatus = BaseUrl + "Carwash/getServerStatus";
        internal const string ValidateCarwashCode = BaseUrl + "Carwash/verifyCarwash";
        #endregion

    }

}

