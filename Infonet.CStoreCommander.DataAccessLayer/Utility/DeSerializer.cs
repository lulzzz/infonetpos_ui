using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Ackroo;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Customer;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.DipInput;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Discount;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPrice;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.GiveX;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Kickback;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Login;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Logout;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Message;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payment;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.PaymentSource;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payout;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reprint;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock.HotCategory;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.System;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Theme;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Infonet.CStoreCommander.DataAccessLayer.Utility
{
    public class DeSerializer
    {
        internal List<ClientGroupContract> MapClientGroups(string data)
        {
            List<ClientGroupContract> clientgrps;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ClientGroupContract>));
                clientgrps = (List<ClientGroupContract>)serializer.ReadObject(stream);
            }

            return clientgrps;

        }
        internal TillsContract MapTills(string data)
        {
            TillsContract tillsContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(TillsContract));
                tillsContract = (TillsContract)serializer.ReadObject(stream);
            }

            return tillsContract;
        }

        internal OverPaymentContract MapOverPayment(string data)
        {
            OverPaymentContract overPaymentContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(OverPaymentContract));
                overPaymentContract = (OverPaymentContract)serializer.ReadObject(stream);
            }

            return overPaymentContract;
        }

        internal SetGroupPriceWithReport MapGroupPriceWithReport(string data)
        {
            SetGroupPriceWithReport contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SetGroupPriceWithReport));
                contract = (SetGroupPriceWithReport)serializer.ReadObject(stream);
            }

            return contract;
        }

        internal ErrorWithCaptionContract MapErrorWithCaption(string data)
        {
            ErrorWithCaptionContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ErrorWithCaptionContract));
                contract = (ErrorWithCaptionContract)serializer.ReadObject(stream);
            }

            return contract;
        }

        internal UncompletePrepayChangeContract MapUncompletePrepayChange(string data)
        {
            UncompletePrepayChangeContract uncompletePrepayChangeContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(UncompletePrepayChangeContract));
                uncompletePrepayChangeContract = (UncompletePrepayChangeContract)serializer.
                    ReadObject(stream);
            }

            return uncompletePrepayChangeContract;
        }

        internal GetPumpActionContract MapPumpAction(string data)
        {
            GetPumpActionContract getPumpAction;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(GetPumpActionContract));
                getPumpAction = (GetPumpActionContract)serializer.ReadObject(stream);
            }

            return getPumpAction;
        }

        internal VendorPayoutContract MapVendorPayout(string data)
        {
            VendorPayoutContract vendorPayoutContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(VendorPayoutContract));
                vendorPayoutContract = (VendorPayoutContract)serializer.ReadObject(stream);
            }

            return vendorPayoutContract;
        }

        internal string MapPONumber(string data)
        {
            string poNumber;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(string));
                poNumber = (string)serializer.ReadObject(stream);
            }

            return poNumber;
        }

        internal CompletePayoutContract MapCompletePayout(string data)
        {
            CompletePayoutContract completePayoutContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(CompletePayoutContract));
                completePayoutContract = (CompletePayoutContract)serializer.ReadObject(stream);
            }

            return completePayoutContract;
        }

        internal string MapTreatyNumber(string data)
        {
            String treatyName = string.Empty;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(String));
                treatyName = (String)serializer.ReadObject(stream);
            }

            return treatyName;
        }

        internal CustomerContract MapCustomer(string data)
        {
            CustomerContract customers;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(CustomerContract));
                customers = (CustomerContract)serializer.ReadObject(stream);
            }
            return customers;
        }

        internal VerifyByAccountGetContract VerifyByAccount(string data)
        {
            VerifyByAccountGetContract verifyByAccountGetContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(VerifyByAccountGetContract));
                verifyByAccountGetContract = (VerifyByAccountGetContract)serializer.ReadObject(stream);
            }
            return verifyByAccountGetContract;
        }

        internal ARCustomerContract MapARCustomer(string data)
        {
            ARCustomerContract arCustomerContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ARCustomerContract));
                arCustomerContract = (ARCustomerContract)serializer.ReadObject(stream);
            }

            return arCustomerContract;
        }

        internal List<ARCustomerContract> MapARCustomersList(string data)
        {
            List<ARCustomerContract> arCustomerContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ARCustomerContract>));
                arCustomerContract = (List<ARCustomerContract>)serializer.ReadObject(stream);
            }

            return arCustomerContract;
        }

        internal List<GiftCertificateContract> MapGiftCertificates(string data)
        {
            List<GiftCertificateContract> giftCertificates;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<GiftCertificateContract>));
                giftCertificates = (List<GiftCertificateContract>)serializer.ReadObject(stream);
            }

            return giftCertificates;
        }

        internal TenderSummaryContract MapTenderSummary(string data)
        {
            TenderSummaryContract tenderSummary;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(TenderSummaryContract));
                tenderSummary = (TenderSummaryContract)serializer.ReadObject(stream);
            }

            return tenderSummary;
        }

        internal CommonPaymentCompleteContract MapCommonCompletePayment(string data)
        {
            CommonPaymentCompleteContract paymentComplete;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(CommonPaymentCompleteContract));
                paymentComplete = (CommonPaymentCompleteContract)serializer.ReadObject(stream);
            }

            return paymentComplete;
        }

        internal PaymentCompleteContract MapCompletePayment(string data)
        {
            PaymentCompleteContract paymentComplete;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PaymentCompleteContract));
                paymentComplete = (PaymentCompleteContract)serializer.ReadObject(stream);
            }

            return paymentComplete;
        }

        internal ThemeContract MapTheme(string data)
        {
            ThemeContract theme;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ThemeContract));
                theme = (ThemeContract)serializer.ReadObject(stream);
            }

            return theme;
        }

        internal CheckoutSummaryContract MapCheckoutSummary(string data)
        {
            CheckoutSummaryContract checkoutSummary;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(CheckoutSummaryContract));
                checkoutSummary = (CheckoutSummaryContract)serializer.ReadObject(stream);
            }

            return checkoutSummary;
        }

        internal StockPriceContract MapStockPrice(string data)
        {
            StockPriceContract stockCodeContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(StockPriceContract));
                stockCodeContract = (StockPriceContract)serializer.ReadObject(stream);
            }

            return stockCodeContract;
        }

        internal OverrideLimitDetailsContract MapOverrideLimitDetails(string data)
        {
            OverrideLimitDetailsContract overrideLimitDetails;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(OverrideLimitDetailsContract));
                overrideLimitDetails = (OverrideLimitDetailsContract)serializer.ReadObject(stream);
            }

            return overrideLimitDetails;
        }

        internal OverLimitDetailsContract MapOverLimitDetails(string data)
        {
            OverLimitDetailsContract overLimitDetailsContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(OverLimitDetailsContract));
                overLimitDetailsContract = (OverLimitDetailsContract)serializer.ReadObject(stream);
            }

            return overLimitDetailsContract;
        }

        internal List<SaleSummaryLineContract> MapSaleSummary(string data)
        {
            List<SaleSummaryLineContract> saleSummaryContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<SaleSummaryLineContract>));
                saleSummaryContract = (List<SaleSummaryLineContract>)serializer.ReadObject(stream);
            }

            return saleSummaryContract;
        }

        internal ValidateQiteContract MapValidateQite(string data)
        {
            ValidateQiteContract qite;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ValidateQiteContract));
                qite = (ValidateQiteContract)serializer.ReadObject(stream);
            }
            return qite;
        }

        internal List<CashButtonContract> MapCashButtons(string data)
        {
            var cashButtonContract = new List<CashButtonContract>();
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<CashButtonContract>));
                cashButtonContract = (List<CashButtonContract>)serializer.ReadObject(stream);
            }

            return cashButtonContract;
        }

        internal UpdatedTenderGetContract MapUpdatedTendersGet(string data)
        {
            UpdatedTenderGetContract updatedTenderGetContract;

            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(UpdatedTenderGetContract));
                updatedTenderGetContract = (UpdatedTenderGetContract)serializer.ReadObject(stream);
            }

            return updatedTenderGetContract;
        }

        internal TendersContract MapTenders(string data)
        {
            TendersContract tenderContract;

            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(TendersContract));
                tenderContract = (TendersContract)serializer.ReadObject(stream);
            }

            return tenderContract;
        }

        internal bool MapAffixBarcode(string data)
        {
            var bytes = Encoding.Unicode.GetBytes(data);
            var response = false;
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SuccessContract));
                response = ((SuccessContract)serializer.ReadObject(stream)).success;
            }

            return response;
        }

        internal SiteValidateContract MapSiteValidate(string data)
        {
            var bytes = Encoding.Unicode.GetBytes(data);
            SiteValidateContract response;
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SiteValidateContract));
                response = (SiteValidateContract)serializer.ReadObject(stream);
            }

            return response;
        }

        internal AiteValidateContract MapValidateAITE(string data)
        {
            AiteValidateContract aiteValidateContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(AiteValidateContract));
                aiteValidateContract = (AiteValidateContract)serializer.ReadObject(stream);
            }
            return aiteValidateContract;
        }

        internal CashDrawTypeContract MapCashDrawTypes(string data)
        {
            CashDrawTypeContract cashDrawContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(CashDrawTypeContract));
                cashDrawContract = (CashDrawTypeContract)serializer.ReadObject(stream);
            }

            return cashDrawContract;
        }

        internal FlashReportContract MapFlashReport(string data)
        {
            FlashReportContract flashReportContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(FlashReportContract));
                flashReportContract = (FlashReportContract)serializer.ReadObject(stream);
            }

            return flashReportContract;
        }

        internal List<ShiftContract> MapShift(string data)
        {
            List<ShiftContract> shifts;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ShiftContract>));
                shifts = (List<ShiftContract>)serializer.ReadObject(stream);
            }

            return shifts;
        }

        internal ExactChangeContract MapExactChange(string data)
        {
            ExactChangeContract exactChangeContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ExactChangeContract));
                exactChangeContract = (ExactChangeContract)serializer.ReadObject(stream);
            }
            return exactChangeContract;
        }

        internal WriteOffContract MapWriteOff(string data)
        {
            WriteOffContract writeOffContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(WriteOffContract));
                writeOffContract = (WriteOffContract)serializer.ReadObject(stream);
            }
            return writeOffContract;
        }

        internal ReportContract MapReport(string data)
        {
            ReportContract saleCountReport;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ReportContract));
                saleCountReport = (ReportContract)serializer.ReadObject(stream);
            }

            return saleCountReport;
        }

        internal VerifyKickbackContract MapVerifyKickbackContract(string data)
        {
            VerifyKickbackContract verifyKickbackContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(VerifyKickbackContract));
                verifyKickbackContract = (VerifyKickbackContract)serializer.ReadObject(stream);
            }

            return verifyKickbackContract;
        }

        internal List<ReportContract> MapReports(string data)
        {
            List<ReportContract> reports;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ReportContract>));
                reports = (List<ReportContract>)serializer.ReadObject(stream);
            }

            return reports;
        }

        internal List<DepartmentContract> MapDepartments(string data)
        {
            List<DepartmentContract> departments;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<DepartmentContract>));
                departments = (List<DepartmentContract>)serializer.ReadObject(stream);
            }

            return departments;
        }

        internal List<TillContract> MapTill(string data)
        {
            List<TillContract> departments;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<TillContract>));
                departments = (List<TillContract>)serializer.ReadObject(stream);
            }

            return departments;
        }

        /// <summary>
        /// Method to deserialize data into GiveXBalanceContract
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal GiveXBalanceContract MapGiveXCardBalance(string data)
        {
            GiveXBalanceContract giveXBalanceContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(GiveXBalanceContract));
                giveXBalanceContract = (GiveXBalanceContract)serializer.ReadObject(stream);
            }
            return giveXBalanceContract;
        }

        /// <summary>
        /// method to deseralize data of SaleList api
        /// </summary>
        /// <param name="data"></param>
        /// <returns>list of sales</returns>
        internal List<SaleListContract> MapSaleList(string data)
        {
            List<SaleListContract> saleListContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<SaleListContract>));
                saleListContract = (List<SaleListContract>)serializer.ReadObject(stream);
            }
            return saleListContract;
        }

        /// <summary>
        /// method to deserialize suspended sales 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal SuspendedSaleContract MapSuspendedSale(string data)
        {
            SuspendedSaleContract suspendedSales;

            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SuspendedSaleContract));
                suspendedSales = (SuspendedSaleContract)serializer.ReadObject(stream);
            }

            return suspendedSales;
        }

        public ActiveShiftsContract MapShifts(string data)
        {
            ActiveShiftsContract shiftContract;

            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ActiveShiftsContract));
                shiftContract = (ActiveShiftsContract)serializer.ReadObject(stream);
            }
            return shiftContract;
        }

        internal ReasonListContract MapReasonList(string data)
        {
            ReasonListContract reasonsList;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var seralizer = new DataContractJsonSerializer(typeof(ReasonListContract));
                reasonsList = (ReasonListContract)seralizer.ReadObject(stream);
            }
            return reasonsList;
        }

        internal LoginPolicyContract MapLoginPolicies(string data)
        {
            LoginPolicyContract policy;
            var bytes = Encoding.Unicode.GetBytes(data);

            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(LoginPolicyContract));
                policy = (LoginPolicyContract)serializer.ReadObject(stream);
            }

            return policy;
        }

        internal LoginContract MapLogin(string data)
        {
            LoginContract loginContract;
            var bytes = Encoding.Unicode.GetBytes(data);

            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(LoginContract));
                loginContract = (LoginContract)serializer.ReadObject(stream);
            }

            return loginContract;
        }

        internal List<HotProductPageContract> MapHotProductPages(string data)
        {
            List<HotProductPageContract> categoriesContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<HotProductPageContract>));
                categoriesContract = (List<HotProductPageContract>)serializer.ReadObject(stream);
            }
            return categoriesContract;
        }

        internal ErrorContract MapError(string data)
        {
            ErrorContract errorContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ErrorContract));
                errorContract = (ErrorContract)serializer.ReadObject(stream);
            }
            return errorContract;
        }

        internal InternalServerErrorContract MapInternalServerError(string data)
        {
            InternalServerErrorContract errorContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(InternalServerErrorContract));
                errorContract = (InternalServerErrorContract)serializer.ReadObject(stream);
            }
            return errorContract;
        }

        internal SuccessContract MapSuccess(string data)
        {
            SuccessContract successContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SuccessContract));
                successContract = (SuccessContract)serializer.ReadObject(stream);
            }
            return successContract;
        }

        internal ChangePasswordSuccessContract MapChangePasswordSuccess(string data)
        {
            ChangePasswordSuccessContract successContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ChangePasswordSuccessContract));
                successContract = (ChangePasswordSuccessContract)serializer.ReadObject(stream);
            }
            return successContract;
        }

        internal SaleContract MapSale(string data)
        {
            SaleContract saleContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SaleContract));
                saleContract = (SaleContract)serializer.ReadObject(stream);
            }
            return saleContract;
        }

        internal DeactivateGivexCardContract MapGivexSaleContract(string data)
        {
            DeactivateGivexCardContract deactivateGivexCardContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(DeactivateGivexCardContract));
                deactivateGivexCardContract = (DeactivateGivexCardContract)serializer.ReadObject(stream);
            }
            return deactivateGivexCardContract;
        }


        internal VoidSaleResponseContract MapVoidSaleResponse(string data)
        {
            VoidSaleResponseContract voidSaledataContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(VoidSaleResponseContract));
                voidSaledataContract = (VoidSaleResponseContract)serializer.ReadObject(stream);
            }
            return voidSaledataContract;
        }

        internal BottleReturnContract MapBottleReturn(string data)
        {
            BottleReturnContract bottleReturnContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(BottleReturnContract));
                bottleReturnContract = (BottleReturnContract)serializer.ReadObject(stream);
            }
            return bottleReturnContract;
        }

        internal StockCodeContract MapStockCode(string data)
        {
            StockCodeContract givexStockCode;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(StockCodeContract));
                givexStockCode = (StockCodeContract)serializer.ReadObject(stream);
            }
            return givexStockCode;
        }

        internal List<CustomerContract> MapCustomers(string data)
        {
            List<CustomerContract> customers;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<CustomerContract>));
                customers = (List<CustomerContract>)serializer.ReadObject(stream);
            }
            return customers;
        }

        internal List<string> MapTaxes(string data)
        {
            TaxCodesContract taxes;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(TaxCodesContract));
                taxes = (TaxCodesContract)serializer.ReadObject(stream);
            }
            return taxes.taxCodes;
        }

        internal PolicyContract MapPolicies(string data)
        {
            PolicyContract policies;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PolicyContract));
                policies = (PolicyContract)serializer.ReadObject(stream);
            }
            return policies;
        }

        internal TaxExemptVerificationContract MapTaxExemptVerification(string data)
        {
            TaxExemptVerificationContract taxExemptVerification;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(TaxExemptVerificationContract));
                taxExemptVerification = (TaxExemptVerificationContract)serializer.ReadObject(stream);
            }
            return taxExemptVerification;
        }

        internal List<StockContract> MapStockItems(string data)
        {
            List<StockContract> stockItems;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<StockContract>));
                stockItems = (List<StockContract>)serializer.ReadObject(stream);
            }
            return stockItems;
        }

        /// <summary>
        /// Method to map hot products
        /// </summary>
        /// <param name="data"></param>
        /// <returns>list of hot products</returns>
        internal List<HotProductContract> MapHotProduct(string data)
        {
            List<HotProductContract> hotProduct;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<HotProductContract>));
                hotProduct = (List<HotProductContract>)serializer.ReadObject(stream);
            }
            return hotProduct;
        }

        /// <summary>
        /// Method to map Bottles
        /// </summary>
        /// <param name="data">JSON data</param>
        /// <returns>list of Bottles</returns>
        internal List<BottleContract> MapBottles(string data)
        {
            List<BottleContract> bottles;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<BottleContract>));
                bottles = (List<BottleContract>)serializer.ReadObject(stream);
            }
            return bottles;
        }

        /// <summary>
        /// method to deserialize api data to suspendSaleContract
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal SuspendedSale MapSuspendSale(string data)
        {
            SuspendedSale suspendedSale;

            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SuspendedSale));
                suspendedSale = (SuspendedSale)serializer.ReadObject(stream);
            }

            return suspendedSale;
        }

        internal VerifyStockContract MapVerifyStock(string data)
        {
            VerifyStockContract verifyStock;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(VerifyStockContract));
                verifyStock = (VerifyStockContract)serializer.ReadObject(stream);
            }
            return verifyStock;
        }

        internal TenderInformationContract MapGetCardInformation(string data)
        {
            TenderInformationContract tenderInformation;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(TenderInformationContract));
                tenderInformation = (TenderInformationContract)serializer.ReadObject(stream);
            }
            return tenderInformation;
        }

        internal List<DipInputContract> MapDipInput(string data)
        {
            List<DipInputContract> dipInputContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<DipInputContract>));
                dipInputContract = (List<DipInputContract>)serializer.ReadObject(stream);
            }
            return dipInputContract;
        }

        internal List<MessageContract> MapMessage(string data)
        {
            List<MessageContract> messageListContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<MessageContract>));
                messageListContract = (List<MessageContract>)serializer.ReadObject(stream);
            }
            return messageListContract;
        }

        internal FleetContract MapPaymentByFleet(string data)
        {
            FleetContract fleetContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(FleetContract));
                fleetContract = (FleetContract)serializer.ReadObject(stream);
            }
            return fleetContract;
        }

        internal List<ReportNameContract> MapReprintReportName(string data)
        {
            List<ReportNameContract> reportNameContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ReportNameContract>));
                reportNameContract = (List<ReportNameContract>)serializer.ReadObject(stream);
            }
            return reportNameContract;
        }

        internal ReprintReportSaleContract MapReprintReportSale(string data)
        {
            ReprintReportSaleContract reprintReportSaleContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ReprintReportSaleContract));
                reprintReportSaleContract = (ReprintReportSaleContract)serializer.ReadObject(stream);
            }
            return reprintReportSaleContract;
        }

        internal InitalizeFuelPumpContract MapInitializeFuelPump(string data)
        {
            InitalizeFuelPumpContract fuelPumpsContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(InitalizeFuelPumpContract));
                fuelPumpsContract = (InitalizeFuelPumpContract)serializer.ReadObject(stream);
            }
            return fuelPumpsContract;
        }

        internal Error MapPumpMessage(string data)
        {
            Error error;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(Error));
                error = (Error)serializer.ReadObject(stream);
            }
            return error;
        }
        //Tony 03/19/2019
        internal static List<CarwashContract> MapAckrCarwashContract(string data)
        {
            List<CarwashContract> olist = null;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<CarwashContract>));
                olist = (List<CarwashContract>)serializer.ReadObject(stream);
            }
            return olist;
        }
        //end
        internal List<string> MapGrades(string data)
        {
            List<string> grades;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<string>));
                grades = (List<string>)serializer.ReadObject(stream);
            }
            return grades;
        }

        internal VendorCouponContract MapVendorCoupon(string data)
        {
            VendorCouponContract vendorCouponContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(VendorCouponContract));
                vendorCouponContract = (VendorCouponContract)serializer.ReadObject(stream);
            }
            return vendorCouponContract;
        }

        internal string MapString(string data)
        {
            string pageName;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(string));
                pageName = (string)serializer.ReadObject(stream);
            }
            return pageName;
        }

        internal bool MapBool(string data)
        {
            bool successContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(bool));
                successContract = (bool)serializer.ReadObject(stream);
            }
            return successContract;
        }

        internal RegisterContract MapRegister(string data)
        {
            RegisterContract registerContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(RegisterContract));
                registerContract = (RegisterContract)serializer.ReadObject(stream);
            }
            return registerContract;
        }

        internal CancelTendersContract MapCancelTenders(string data)
        {
            CancelTendersContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(CancelTendersContract));
                contract = (CancelTendersContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal SoundContract MapSounds(string data)
        {
            SoundContract soundContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SoundContract));
                soundContract = (SoundContract)serializer.ReadObject(stream);
            }
            return soundContract;
        }

        internal TierLevelContract MapTierLevel(string data)
        {
            TierLevelContract tierLevelContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(TierLevelContract));
                tierLevelContract = (TierLevelContract)serializer.ReadObject(stream);
            }
            return tierLevelContract;
        }

        internal ValidateTillCloseContract MapValidateTill(string data)
        {
            ValidateTillCloseContract validateTillCloseContract;

            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ValidateTillCloseContract));
                validateTillCloseContract = (ValidateTillCloseContract)serializer.ReadObject(stream);
            }

            return validateTillCloseContract;
        }

        internal CloseTillContract MapTillClose(string data)
        {
            CloseTillContract closeTillContract;

            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(CloseTillContract));
                closeTillContract = (CloseTillContract)serializer.ReadObject(stream);
            }

            return closeTillContract;
        }

        internal string MapPassword(string data)
        {
            string password;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(string));
                password = (string)serializer.ReadObject(stream);
            }
            return password;
        }

        internal List<PropaneGradesContract> MapPropaneGrades(string data)
        {
            List<PropaneGradesContract> propaneGrades;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<PropaneGradesContract>));
                propaneGrades = (List<PropaneGradesContract>)serializer.ReadObject(stream);
            }
            return propaneGrades;
        }

        internal List<LoadPumpsContract> MapPropanePumps(string data)
        {
            List<LoadPumpsContract> loadPumpsContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<LoadPumpsContract>));
                loadPumpsContract = (List<LoadPumpsContract>)serializer.ReadObject(stream);
            }
            return loadPumpsContract;
        }

        internal FuelPricesContract MapFuelPrices(string data)
        {
            FuelPricesContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(FuelPricesContract));
                contract = (FuelPricesContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal FuelPriceContract MapFuelPrice(string data)
        {
            FuelPriceContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(FuelPriceContract));
                contract = (FuelPriceContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal string MapFuelPumpError(string data)
        {
            string contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(string));
                contract = (string)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal PricesToDisplayContract MapPricesToDisplay(string data)
        {
            PricesToDisplayContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PricesToDisplayContract));
                contract = (PricesToDisplayContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal FinishTillCloseContract MapFinishTillClose(string data)
        {
            FinishTillCloseContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(FinishTillCloseContract));
                contract = (FinishTillCloseContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal PriceIncrementDecrementContract MapIncrementsAndDecrements(string data)
        {
            PriceIncrementDecrementContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PriceIncrementDecrementContract));
                contract = (PriceIncrementDecrementContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal PriceDecrementContract MapPriceDecrement(string data)
        {
            PriceDecrementContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PriceDecrementContract));
                contract = (PriceDecrementContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal SetPriceDecrementContract MapSetPriceDecrement(string data)
        {
            SetPriceDecrementContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SetPriceDecrementContract));
                contract = (SetPriceDecrementContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal PriceIncrementContract MapPriceIncrement(string data)
        {
            PriceIncrementContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PriceIncrementContract));
                contract = (PriceIncrementContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal SetPriceIncrementContract MapSetPriceIncrement(string data)
        {
            SetPriceIncrementContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(SetPriceIncrementContract));
                contract = (SetPriceIncrementContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal UncompletePrepayLoadContract MapUncompletePrepayLoad(string data)
        {
            UncompletePrepayLoadContract contract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(UncompletePrepayLoadContract));
                contract = (UncompletePrepayLoadContract)serializer.ReadObject(stream);
            }
            return contract;
        }

        internal GiveXReportContract MapGiveXReport(string data)
        {
            GiveXReportContract giveXReportContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(GiveXReportContract));
                giveXReportContract = (GiveXReportContract)serializer.ReadObject(stream);
            }
            return giveXReportContract;
        }

        internal BalancePointContract MapKickbackBalancePoints(string data)
        {
            BalancePointContract balancePointContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(BalancePointContract));
                balancePointContract = (BalancePointContract)serializer.ReadObject(stream);
            }
            return balancePointContract;
        }

        internal ValidateGasKingContract MapValidateGasKing(string data)
        {
            ValidateGasKingContract validateGasKingContract;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(ValidateGasKingContract));
                validateGasKingContract = (ValidateGasKingContract)serializer.ReadObject(stream);
            }
            return validateGasKingContract;
        }
        //Tony 03/19/2019
        internal static List<string> MapListString(string data)
        {
            List<string> olist;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<string>));
                olist = (List<string>)serializer.ReadObject(stream);
            }
            return olist;
        }
        internal PSProfileContract MapPSProfile(string data)
        {
            var bytes = Encoding.Unicode.GetBytes(data);
            PSProfileContract pspf = null;
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PSProfileContract));
                pspf = (PSProfileContract)serializer.ReadObject(stream);
            }
            return pspf;
        }
        internal static PSRefundContract MapPSRefundInfo(string data)
        {
            var bytes = Encoding.Unicode.GetBytes(data);
            PSRefundContract psrt = null;
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PSRefundContract));
                psrt = (PSRefundContract)serializer.ReadObject(stream);

            }
            return psrt;
        }
        internal static PSVoucherInfoContract MapPSVoucherInfo(string data)
        {
            var bytes = Encoding.Unicode.GetBytes(data);
            PSVoucherInfoContract pspf = null;
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(PSVoucherInfoContract));
                pspf = (PSVoucherInfoContract)serializer.ReadObject(stream);
            }
            return pspf;
        }
        internal static List<PSLogoContract> MapPSLogos(string data)
        {
            List<PSLogoContract> olist;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<PSLogoContract>));
                olist = (List<PSLogoContract>)serializer.ReadObject(stream);
            }
            return olist;
        }

        internal List<PSProductContract> MapPSProds(string data)
        {
            List<PSProductContract> olist;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<PSProductContract>));
                olist = (List<PSProductContract>)serializer.ReadObject(stream);
            }
            return olist;
        }
        internal List<PSLogoContract> MapPSLogo(string data)
        {
            List<PSLogoContract> olist;
            var bytes = Encoding.Unicode.GetBytes(data);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<PSLogoContract>));
                olist = (List<PSLogoContract>)serializer.ReadObject(stream);
            }
            return olist;
        }
        internal List<PSTransactionContract> MapPSTransactions(string data)
        {
            var bytes = Encoding.Unicode.GetBytes(data);
            List<PSTransactionContract> psrt = null;
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<PSTransactionContract>));
                psrt = (List<PSTransactionContract>)serializer.ReadObject(stream);

            }
            return psrt;
        }
        //End
    }
}
