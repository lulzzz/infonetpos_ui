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
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Settings.Maintenance;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock.HotCategory;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.System;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Theme;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities;
using Infonet.CStoreCommander.EntityLayer.Entities.Ackroo;
using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Customer;
using Infonet.CStoreCommander.EntityLayer.Entities.DipInput;
using Infonet.CStoreCommander.EntityLayer.Entities.Discount;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.Kickback;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using Infonet.CStoreCommander.EntityLayer.Entities.Logout;
using Infonet.CStoreCommander.EntityLayer.Entities.Payment;
using Infonet.CStoreCommander.EntityLayer.Entities.PaymentSource;
using Infonet.CStoreCommander.EntityLayer.Entities.Payout;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.EntityLayer.Entities.Reprint;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using Infonet.CStoreCommander.EntityLayer.Entities.System;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Infonet.CStoreCommander.DataAccessLayer.Utility
{
    public class Mapper
    {
        internal List<ClientGroup> MapClientGroups(List<ClientGroupContract> clientgrps)
        {
            if (clientgrps == null)
                return null;
            var olist = (from c in clientgrps
                         select new ClientGroup
                         {
                             GroupId = c.groupId,
                             GroupName = c.groupName,
                             DiscountType = c.discountType,
                             DiscountRate = c.discountRate,
                             Footer = c.footer,
                             DiscountName = c.discountName
                         }).ToList();
            return olist;
        }
        internal EntityLayer.Entities.Login.LoginPolicy MapLoginPolicies(LoginPolicyContract loginPolicy)
        {
            return new EntityLayer.Entities.Login.LoginPolicy
            {
                PosID = loginPolicy.policies.posId,
                ProvideTillFloat = loginPolicy.policies.provideTillFloat,
                UsePredefinedTillNumber = loginPolicy.policies.usePredefinedTillNumber,
                UseShifts = loginPolicy.policies.useShifts,
                WindowsLogin = loginPolicy.policies.windowsLogin,
                PosLanguage = loginPolicy.policies.posLanguage,
                Message = loginPolicy.message,
                AutoShiftPick = loginPolicy.policies.autoShiftPick,
                KeypadFormat = loginPolicy.policies.keypadFormat
            };
        }

        internal Login MapLogin(LoginContract loginContract)
        {
            var login = new Login();

            if (loginContract != null)
            {
                login.AuthToken = loginContract.authToken;
                login.TrainerCaption = loginContract.trainerCaption;
            }

            return login;
        }

        internal ErrorMessageWithCaption MapErrorWithCaption(ErrorWithCaptionContract contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new ErrorMessageWithCaption
            {
                Caption = contract.caption,
                Error = MapError(contract.error),
                FuelPriceReport = MapReport(contract.fuelPriceReport),
                PriceReport = MapReport(contract.priceReport)
            };
        }

        internal VerifyByAccount VerifyByAccount(VerifyByAccountGetContract verifyByAccount)
        {
            if (verifyByAccount != null)
            {
                return new VerifyByAccount
                {
                    IsPurchaseOrderRequired = verifyByAccount.isPurchaseOrderRequired,
                    IsMutiliPO = verifyByAccount.isMutiliPO,
                    CreditMessage = new EntityLayer.Entities.Common.Error
                    {
                        Message = verifyByAccount.creditMessage?.message
                    },
                    OverrideArLimitMessage = new EntityLayer.Entities.Common.Error
                    {
                        Message = verifyByAccount.overrideArLimitMessage?.message
                    },
                    UnauthorizedMessage = new EntityLayer.Entities.Common.Error
                    {
                        Message = verifyByAccount.unauthorizedMessage?.message
                    },
                    CardSummary = MapCardTenderInformation(verifyByAccount.cardSummary)
                };
            }
            return null;
        }

        internal List<GiftCertificate> MapGiftCertificates(List<GiftCertificateContract> giftCertificates)
        {
            if (giftCertificates == null)
            {
                return null;
            }

            return (from g in giftCertificates
                    select new GiftCertificate
                    {
                        Amount = decimal.Parse(g.amount, NumberStyles.Any, CultureInfo.InvariantCulture),
                        ExpiresOn = g.expiresOn,
                        IsExpired = g.isExpired,
                        Number = g.number,
                        SoldOn = g.soldOn
                    }).ToList();
        }

        internal List<StoreCredit> MapStoreCredit(List<GiftCertificateContract> storeCredit)
        {
            if (storeCredit == null)
            {
                return null;
            }

            return (from g in storeCredit
                    select new StoreCredit
                    {
                        Amount = decimal.Parse(g.amount, NumberStyles.Any, CultureInfo.InvariantCulture),
                        ExpiresOn = g.expiresOn,
                        IsExpired = g.isExpired,
                        Number = g.number,
                        SoldOn = g.soldOn
                    }).ToList();
        }

        internal List<ARCustomer> MapCustomers(List<ARCustomerContract> customers)
        {
            var customerList = new List<ARCustomer>();
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    customerList.Add(MapARCustomer(customer));
                }
            }
            return customerList;
        }

        internal Customer MapCustomeFromContract(CustomerContract responseValue)
        {
            return new Customer
            {
                Code = responseValue.code,
                LoyaltyNumber = responseValue.loyaltyNumber,
                Name = responseValue.name,
                PhoneNumber = responseValue.phone,
                Points = responseValue.loyaltyPoints
            };
        }

        internal ARCustomer MapARCustomer(ARCustomerContract customer)
        {
            return new ARCustomer
            {
                Balance = customer.balance.StartsWith("-") ? string.Format("{0} (Credit)", customer.balance) : customer.balance,
                Code = customer.code,
                CreditLimit = customer.creditLimit,
                Name = customer.name,
                Phone = customer.phone
            };
        }

        internal SaleSummary MapSaleSummary(List<SaleSummaryLineContract> saleSummary)
        {
            if (saleSummary == null)
            {
                return new SaleSummary();
            }

            return new SaleSummary
            {
                Summary = (from s in saleSummary
                           select new SaleSummaryLine
                           {
                               Key = s.key,
                               Value = s.value
                           }).ToList()
            };
        }

        internal UpdateTenderGet MapUpdatedTendersGet(UpdatedTenderGetContract updatedTendersGet)
        {
            if (updatedTendersGet != null)
            {
                return new UpdateTenderGet
                {
                    TenderedAmount = updatedTendersGet.tenderedAmount,
                    Tenders = MapTenders(new TendersContract
                    {
                        tenders = updatedTendersGet.tenders
                    })
                };
            }
            else
            {
                return null;
            }
        }

        internal SiteValidate MapSiteValidate(SiteValidateContract siteValidateContract)
        {
            if (siteValidateContract == null)
            {
                return null;
            }
            return new SiteValidate
            {
                IsFrmOverrideLimit = siteValidateContract.isFrmOverrideLimit,
                PermitNumber = siteValidateContract.permitNumber,
                SaleSummary = MapSaleSummary(siteValidateContract.saleSummary),
                TenderSummary = MapTenderSummary(siteValidateContract.tenderSummary),
                Tenders = MapTenders(siteValidateContract.tenders),
                TreatyCustomerName = siteValidateContract.treatyCustomerName,
                TreatyNumber = siteValidateContract.treatyNumber,
                FngtrMessage = siteValidateContract.fngtrMessage,
                IsFngtr = siteValidateContract.isFngtr,
                RequireSignature = siteValidateContract.requireSignature
            };
        }

        internal TenderSummary MapTenderSummary(TenderSummaryContract tenderSummary)
        {
            if (tenderSummary == null)
            {
                return null;
            }

            return new TenderSummary
            {
                DisplayNoReceiptButton = tenderSummary.displayNoReceiptButton,
                EnableCompletePayment = tenderSummary.enableCompletePayment,
                EnableRunAway = tenderSummary.enableRunAway,
                Summary1 = tenderSummary.summary1,
                Summary2 = tenderSummary.summary2,
                IssueStoreCreditMessage = tenderSummary.issueStoreCreditMessage,
                OutstandingAmount = tenderSummary.outstandingAmount,
                Tenders = MapTenders(tenderSummary.tenders),
                EnablePumpTest = tenderSummary.enablePumpTest,
                Report = MapReports(tenderSummary.receipts),
                Messages = tenderSummary.messages != null ?
                    (from e in tenderSummary.messages
                     select MapError(e)).ToList() :
                     new List<EntityLayer.Entities.Common.Error>(),
                VendorCoupons = MapVendorCoupon(tenderSummary.vendorCoupons),
                LineDisplay = MapLineDisplay(tenderSummary.customerDisplay)
            };
        }

        private List<SaleVendorCoupon> MapVendorCoupon(List<SaleVendorCouponContract> vendorCoupons)
        {
            var vendors = new List<SaleVendorCoupon>();

            if (vendorCoupons != null)
            {
                vendors = (from v in vendorCoupons
                           select new SaleVendorCoupon
                           {
                               Coupon = v.coupon,
                               SerialNumber = v.serialNumber
                           }).ToList();
            }

            return vendors;
        }

        internal AiteValidate MapValidateAITE(AiteValidateContract validateAite)
        {
            if (validateAite == null)
            {
                return null;
            }

            return new AiteValidate
            {
                BarCode = validateAite.barCode,
                CardHolderName = validateAite.aiteCardHolderName,
                CardNumber = validateAite.aiteCardNumber,
                IsOverLimit = validateAite.isFrmOverLimit,
                SaleSummary = MapSaleSummary(validateAite.saleSummary),
                TenderSummary = MapTenderSummary(validateAite.tenderSummary),
                Tenders = MapTenders(validateAite.tenders)
            };
        }

        internal StockPrice MapStockPrice(StockPriceContract stockpriceContract)
        {
            if (stockpriceContract != null)
            {
                var stockPrice = new StockPrice
                {
                    SpecialPriceTypes = stockpriceContract.specialPriceTypes,
                    AvailableQuantity = CheckForNullValues(stockpriceContract.availableQuantity),
                    Description = CheckForNullValues(stockpriceContract.description),
                    FirstUnitPrice = MapFirstPriceUnit(stockpriceContract),
                    FromDate = CheckForNullValues(stockpriceContract.fromDate),
                    IncrementalPrice = MapIncrementalPrice(stockpriceContract),
                    IsActiveVendorPrice = stockpriceContract.isActiveVendorPrice,
                    IsAddButtonVisible = stockpriceContract.isAddButtonVisible,
                    IsAvQtyVisible = stockpriceContract.isAvQtyVisible,
                    IsChangePriceEnable = stockpriceContract.isChangePriceEnable,
                    IsEndDateChecked = stockpriceContract.isEndDateChecked,
                    IsPerDollarChecked = stockpriceContract.isPerDollarChecked,
                    IsPerPercentageChecked = stockpriceContract.isPerPercentageChecked,
                    IsPriceVisible = stockpriceContract.isPriceVisible,
                    IsRemoveButtonVisible = stockpriceContract.isRemoveButtonVisible,
                    IsSpecialPricingVisible = stockpriceContract.isSpecialPricingVisible,
                    IsTaxExemptVisible = stockpriceContract.isTaxExemptVisible,
                    IsToDateVisible = stockpriceContract.isToDateVisible,
                    PriceTypeText = CheckForNullValues(stockpriceContract.priceTypeText),
                    RegularPriceText = CheckForNullValues(stockpriceContract.regularPriceText),
                    SalePrice = MapSalePrice(stockpriceContract),
                    StockCode = CheckForNullValues(stockpriceContract.stockCode),
                    TaxExemptAvailable = CheckForNullValues(stockpriceContract.taxExemptAvailable),
                    TaxExemptPrice = CheckForNullValues(stockpriceContract.taxExemptPrice),
                    ToDate = CheckForNullValues(stockpriceContract.toDate),
                    VendorId = CheckForNullValues(stockpriceContract.vendorId),
                    XForPrice = MapXForPrice(stockpriceContract),
                    LineDisplay = MapLineDisplay(stockpriceContract.customerDisplay),
                    Message = stockpriceContract.message
                };

                return stockPrice;
            }
            return null;
        }
        private string CheckForNullValues(string response)
        {
            return string.IsNullOrEmpty(response) ? string.Empty : response;
        }

        private XForPrice MapXForPrice(StockPriceContract stockpriceContract)
        {
            var xForPrice = new XForPrice();
            if (stockpriceContract?.xForPrice != null)
            {
                xForPrice.Columns = stockpriceContract.xForPrice.columns;
                xForPrice.ColumnText = stockpriceContract.xForPrice.columnText;
                xForPrice.ColumnText2 = stockpriceContract.xForPrice.columnText2;

                if (stockpriceContract.xForPrice.xForPriceGrids != null)
                {
                    var xForPriceGrids = new List<XForPriceGrids>();
                    foreach (var xForPriceGrid in stockpriceContract.xForPrice.xForPriceGrids)
                    {
                        xForPriceGrids.Add(new XForPriceGrids
                        {
                            Column1 = xForPriceGrid.column1,
                            Column2 = xForPriceGrid.column2,
                            Column3 = xForPriceGrid.column3
                        });
                    }
                    xForPrice.CommonGrids = xForPriceGrids;
                }
            }
            return xForPrice;
        }

        private SalePrice MapSalePrice(StockPriceContract stockpriceContract)
        {
            var tempSalePrice = new SalePrice();
            if (stockpriceContract?.salePrice != null)
            {
                tempSalePrice.Columns = stockpriceContract.salePrice.columns;
                tempSalePrice.ColumnText = stockpriceContract.salePrice.columnText;

                if (stockpriceContract.salePrice.salePrices != null)
                {
                    var salePrices = new List<SalePrices>();
                    foreach (var salePrice in stockpriceContract.salePrice.salePrices)
                    {
                        salePrices.Add(new SalePrices
                        {
                            Column1 = salePrice.column1,
                            Column2 = salePrice.column2,
                            Column3 = salePrice.column3
                        });
                    }
                    tempSalePrice.CommonGrids = salePrices;
                }
            }
            return tempSalePrice;
        }

        internal Theme MapTheme(ThemeContract themeContract)
        {
            if (themeContract == null)
            {
                return null;
            }

            return new Theme
            {
                BackgroundColor1Dark = themeContract.backgroundColor1Dark,
                BackgroundColor1Light = themeContract.backgroundColor1Light,
                BackgroundColor2 = themeContract.backgroundColor2,
                ButtonBackgroundColor = themeContract.headerBackgroundColor,
                ButtonBottomColor = themeContract.buttonFooterColor,
                ButtonBottomConfirmationColor = themeContract.buttonFooterConfirmationColor,
                ButtonBottomWarningColor = themeContract.buttonFooterWarningColor,
                ButtonForegroundColor = themeContract.headerForegroundColor,
                HeaderBackgroundColor = themeContract.headerBackgroundColor,
                HeaderForegroundColor = themeContract.headerForegroundColor,
                LabelTextForegroundColor = themeContract.labelTextForegroundColor
            };
        }

        private IncrementalPrice MapIncrementalPrice(StockPriceContract stockpriceContract)
        {
            var tempIncrementalPrice = new IncrementalPrice();

            if (stockpriceContract?.incrementalPrice != null)
            {
                tempIncrementalPrice.Columns = stockpriceContract.incrementalPrice.columns;
                tempIncrementalPrice.ColumnText = stockpriceContract.incrementalPrice.columnText;
                tempIncrementalPrice.ColumnText2 = stockpriceContract.incrementalPrice.columnText2;
                tempIncrementalPrice.ColumnText3 = stockpriceContract.incrementalPrice.columnText3;

                if (stockpriceContract.incrementalPrice.incrementalPriceGrids != null)
                {
                    var incrementalPriceGrids = new List<IncrementalPriceGrids>();
                    foreach (var incrementalPriceGrid in stockpriceContract.incrementalPrice.incrementalPriceGrids)
                    {
                        incrementalPriceGrids.Add(new IncrementalPriceGrids
                        {
                            Column1 = incrementalPriceGrid.column1,
                            Column2 = incrementalPriceGrid.column2,
                            Column3 = incrementalPriceGrid.column3
                        });
                    }
                    tempIncrementalPrice.CommonGrids = incrementalPriceGrids;
                }
            }
            return tempIncrementalPrice;
        }

        private FirstUnitPrice MapFirstPriceUnit(StockPriceContract stockpriceContract)
        {
            var firstUnitPrice = new FirstUnitPrice();
            if (stockpriceContract?.firstUnitPrice != null)
            {
                firstUnitPrice.Columns = stockpriceContract.firstUnitPrice.columns;
                firstUnitPrice.ColumnText = stockpriceContract.firstUnitPrice.columnText;
                firstUnitPrice.ColumnText2 = stockpriceContract.firstUnitPrice.columnText2;

                if (stockpriceContract.firstUnitPrice.firstUnitPriceGrids != null)
                {
                    var firstUnitPriceGrids = new List<FirstUnitPriceGrids>();
                    foreach (var firstUnitPriceGrid in stockpriceContract.firstUnitPrice.firstUnitPriceGrids)
                    {
                        firstUnitPriceGrids.Add(new FirstUnitPriceGrids
                        {
                            Column1 = firstUnitPriceGrid.column1,
                            Column2 = firstUnitPriceGrid.column2,
                            Column3 = firstUnitPriceGrid.column3
                        });
                    }
                    firstUnitPrice.CommonGrids = firstUnitPriceGrids;
                }
            }
            return firstUnitPrice;
        }

        internal Report MapReport(ReportContract saleCountReportContract)
        {
            var saleCountReportModel = new Report();
            if (saleCountReportContract != null && saleCountReportContract.reportContent != null)
            {
                saleCountReportModel.ReportName = saleCountReportContract.reportName;

                var data = Convert.FromBase64String(saleCountReportContract.reportContent);
                saleCountReportModel.ReportContent = Encoding.UTF8.GetString(data);
                saleCountReportModel.Copies = saleCountReportContract.copies;

            }
            return saleCountReportModel;
        }

        internal VerifyKickback MapVerifyKickback(VerifyKickbackContract verifyKickbackContract)
        {
            var verifyKickback = new VerifyKickback();

            if (verifyKickbackContract != null)
            {
                verifyKickback.BalancePoints = verifyKickbackContract.balancePoints;
                verifyKickback.Value = verifyKickbackContract.value;
                verifyKickback.Verify = verifyKickbackContract.verify;
            }

            return verifyKickback;
        }

        internal List<Report> MapReports(List<ReportContract> reportContract)
        {
            var reportModel = new List<Report>();
            if (reportContract != null)
            {
                foreach (var report in reportContract)
                {
                    reportModel.Add(MapReport(report));
                }
            }
            return reportModel;
        }

        internal ExactChange MapExactChange(ExactChangeContract exactChangeContract)
        {
            var exactChange = new ExactChange
            {
                IsRefund = exactChangeContract.isRefund,
                OpenCashDrawer = exactChangeContract.openCashDrawer,
                LimitExceedMessage = exactChangeContract.limitExceedMessage,
                LineDisplays = (from c in exactChangeContract.customerDisplays
                                select MapLineDisplay(c)).ToList()
            };

            if (exactChangeContract != null)
            {
                if (exactChangeContract.newSale != null)
                {
                    exactChange.NewSale = MapSale(exactChangeContract.newSale);
                }

                if (exactChangeContract.paymentReceipt != null)
                {
                    exactChange.Report = MapReport(exactChangeContract.paymentReceipt);
                }
            }

            return exactChange;
        }

        internal WriteOff MapWriteOff(WriteOffContract result)
        {
            if (result == null)
            {
                return null;
            }

            var data = Convert.FromBase64String(result.writeOffReceipt.reportContent);
            return new WriteOff
            {
                Sale = MapSale(result.newSale),
                Receipt = MapReport(result.writeOffReceipt),
                LineDisplay = MapLineDisplay(result.customerDisplay)
            };
        }

        internal GiveXCardBalance MapGiveXCardBalance(GiveXBalanceContract contract)
        {
            if (contract != null)
            {
                return new GiveXCardBalance
                {
                    Balance = decimal.Parse(contract.balance, NumberStyles.Any, CultureInfo.InvariantCulture),
                    CardNumber = contract.cardNumber,
                    Report = MapReport(contract.receipt)
                };
            }
            return null;
        }

        internal object MapChangePasswordSuccess(ChangePasswordSuccessContract parsedData)
        {
            if (parsedData != null)
            {
                return new Success
                {
                    Message = parsedData.error.message,
                    IsSuccess = parsedData.success
                };
            }
            else
                return null;
        }

        /// <summary>
        /// Method to map[ success model with success contract
        /// </summary>
        /// <param name="parsedData"></param>
        /// <returns></returns>
        internal Success MapSuccess(SuccessContract parsedData)
        {
            if (parsedData != null)
            {
                return new Success
                {
                    Message = parsedData.message,
                    IsSuccess = parsedData.success
                };
            }
            else
                return null;
        }

        /// <summary>
        /// Method to map hotProductModel with HotProductContract
        /// </summary>
        /// <param name="hotProducts"></param>
        /// <returns></returns>
        internal List<HotProductModel> MapHotProduct(List<HotProductContract> hotProducts)
        {
            var hotProductsModel = new List<HotProductModel>();
            if (hotProducts != null)
            {
                foreach (var product in hotProducts)
                {
                    hotProductsModel.Add(new HotProductModel
                    {
                        ButtonId = product.buttonId,
                        DefaultQuantity = product.defaultQuantity,
                        Description = product.description,
                        Image = product.imageUrl,
                        StockCode = product.stockCode
                    });
                }
            }
            return hotProductsModel;
        }

        internal CustomerContract MapCustomer(Customer customer)
        {
            return new CustomerContract()
            {
                code = customer.Code,
                loyaltyNumber = customer.LoyaltyNumber,
                name = customer.Name,
                phone = customer.PhoneNumber,
                loyaltyPoints = customer.Points
            };
        }

        internal TaxCodeModel MapTaxCodes(List<string> taxCodes)
        {
            return new TaxCodeModel()
            {
                TaxCode = taxCodes
            };
        }

        internal List<StockModel> MapStocks(List<StockContract> stockItems)
        {
            if (stockItems == null)
            {
                return null;
            }

            return (from s in stockItems
                    select new StockModel
                    {
                        AlternateCode = s.alternateCode,
                        Description = s.description,
                        RegularPrice = string.IsNullOrEmpty(s.regularPrice) ? default(decimal?) :
                        decimal.Parse(s.regularPrice, NumberStyles.Any, CultureInfo.InvariantCulture),
                        StockCode = s.stockCode
                    }).ToList();
        }

        internal List<Customer> MapCustomers(List<CustomerContract> customers)
        {
            if (customers == null)
            {
                return null;
            }

            return (from c in customers
                    select new Customer
                    {
                        Code = c.code,
                        LoyaltyNumber = c.loyaltyNumber,
                        Name = c.name,
                        PhoneNumber = c.phone,
                        Points = c.loyaltyPoints
                    }).ToList();
        }

        internal List<HotProductPageModel> MapCategories(List<HotProductPageContract> categoriesContract)
        {
            var categories = new List<HotProductPageModel>();
            foreach (var categoryContract in categoriesContract)
            {
                var products = new List<HotProductModel>();

                if (categoryContract != null)
                    categories.Add(new HotProductPageModel
                    {
                        PageId = categoryContract.pageId,
                        PageName = categoryContract.pageName
                    });
            }
            return categories;
        }

        internal EntityLayer.Entities.Common.Error MapError(ErrorContract errorContract)
        {
            if (errorContract != null)
            {
                return new EntityLayer.Entities.Common.Error
                {
                    ShutDownPos = errorContract.shutDownPOS,
                    Message = errorContract.error.message,
                    MessageType = (MessageType)errorContract.error.messageType
                };
            }

            return null;
        }

        internal EntityLayer.Entities.Common.Error MapError(DataContracts.Common.Error errorContract)
        {
            if (errorContract != null)
            {
                return new EntityLayer.Entities.Common.Error
                {
                    ShutDownPos = false,
                    Message = errorContract.message,
                    MessageType = (MessageType)errorContract.messageType
                };
            }

            return null;
        }

        internal EntityLayer.Entities.Common.Error MapError(InternalServerErrorContract errorContract)
        {
            if (errorContract != null)
            {
                return new EntityLayer.Entities.Common.Error
                {
                    ShutDownPos = true,
                    Message = errorContract.error.message,
                    MessageType = (MessageType)errorContract.error.messageType
                };
            }

            return null;
        }

        internal ActiveTills MapTills(TillsContract tillsContract)
        {
            if (tillsContract == null)
            {
                return null;
            }

            var tills = new ActiveTills
            {
                Tills = tillsContract.tills.Select(x => x.tillNumber).ToList(),
                CashFloat = decimal.Parse(tillsContract.cashFloat, NumberStyles.Any, CultureInfo.InvariantCulture),
                ShutDownPOS = tillsContract.shutDownPOS,
                ForceTill = tillsContract.forceTill,
                Message = tillsContract.message.message,
                ShiftNumber = tillsContract.shiftNumber,
                ShiftDate = tillsContract.shiftDate,
                IsTrainer = tillsContract.isTrainer
            };
            return tills;
        }

        internal ActiveShifts MapActiveShifts(ActiveShiftsContract shiftContract)
        {
            if (shiftContract == null)
            {
                return null;
            }
            var shifts = new ActiveShifts
            {
                Shifts = new List<Shifts>(),
                CashFloat = decimal.Parse(shiftContract.cashFloat, NumberStyles.Any, CultureInfo.InvariantCulture),
                ForceShift = shiftContract.forceShift,
                IsShiftUsedForDay = shiftContract.shiftsUsedForDay
            };

            foreach (var shift in shiftContract.shifts)
            {
                shifts.Shifts.Add(new Shifts
                {
                    DisplayFormat = shift.displayFormat,
                    ShiftNumber = shift.shiftNumber,
                    ShiftDate = shift.shiftDate
                });
            }
            return shifts;
        }

        internal string MapStockCode(StockCodeContract data)
        {
            if (data != null)
            {
                return data.stockCode;
            }
            return null;
        }

        internal BottleReturn MapBottleReturn(BottleReturnContract bottleReturnContract)
        {
            if (bottleReturnContract == null)
            {
                return null;
            }

            return new BottleReturn
            {
                ChangeDue = bottleReturnContract.changeDue,
                IsRefund = bottleReturnContract.isRefund,
                OpenCashDrawer = bottleReturnContract.openCashDrawer,
                Receipt = MapReport(bottleReturnContract.paymentReceipt),
                Sale = MapSale(bottleReturnContract.newSale),
                LineDisplay = MapLineDisplay(bottleReturnContract.customerDisplay)
            };
        }

        internal VoidSale MapVoidSale(VoidSaleResponseContract voidSaleResponseContract)
        {

            if (voidSaleResponseContract == null)
            {
                return null;
            }

            return new VoidSale
            {
                ChangeDue = voidSaleResponseContract.changeDue,
                IsRefund = voidSaleResponseContract.isRefund,
                OpenCashDrawer = voidSaleResponseContract.openCashDrawer,
                Receipt = MapReport(voidSaleResponseContract.paymentReceipt),
                Sale = MapSale(voidSaleResponseContract.newSale),
                LineDisplays = (from c in voidSaleResponseContract.customerDisplays
                                select MapLineDisplay(c)).ToList()
            };
        }

        private LineDisplayModel MapLineDisplay(LineDisplayContract contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new LineDisplayModel
            {
                NonOposTexts = contract.nonOposTexts,
                OposText1 = contract.oposText1,
                OposText2 = contract.oposText2
            };
        }

        internal Sale MapSale(SaleContract saleContract)
        {
            var saleModel = new Sale
            {
                SaleNumber = saleContract.saleNumber,
                TillNumber = saleContract.tillNumber,
                Summary = saleContract.summary,
                TotalAmount = saleContract.totalAmount,
                EnableWriteOffButton = saleContract.enableWriteOffButton,
                CustomerName = saleContract.customer,
                SaleLines = new List<SaleLine>(),
                Errors = new List<EntityLayer.Entities.Common.Error>(),
                EnableExactChange = saleContract.enableExactChange,
                LineDisplay = MapLineDisplay(saleContract.customerDisplayText),
                HasCarwashProducts = saleContract.hasCarwashProducts
                
            };

            if (saleContract.saleLineErrors != null)
            {
                foreach (var saleLineErrors in saleContract?.saleLineErrors)
                {
                    saleModel.Errors.Add(new Mapper().MapError(saleLineErrors));
                }
            }

            if (saleContract.saleLines != null)
            {
                foreach (var saleLine in saleContract.saleLines)
                {
                    saleModel.SaleLines.Add(new SaleLine
                    {
                        Amount = saleLine.amount,
                        Discount = saleLine.discountRate,
                        DiscountRate = saleLine.discountRate,
                        DiscountType = saleLine.discountType,
                        LineNumber = saleLine.lineNumber,
                        Price = saleLine.price,
                        Quantity = saleLine.quantity,
                        StockCode = saleLine.stockCode,
                        TotalAmount = !string.IsNullOrEmpty(saleLine.totalAmount) ? decimal.Parse(saleLine.totalAmount, NumberStyles.Any, CultureInfo.InvariantCulture) : 0,
                        Description = saleLine.description,
                        AllowDiscountChange = saleLine.allowDiscountChange,
                        AllowDiscountReason = saleLine.allowDiscountReason,
                        AllowPriceChange = saleLine.allowPriceChange,
                        AllowPriceReason = saleLine.allowPriceReason,
                        AllowQuantityChange = saleLine.allowQuantityChange,
                        AllowReturnReason = saleLine.allowReturnReason,
                        //Tony 03/19/2019
                        Dept = saleLine.dept
                        //End
                    });
                }
            }
            return saleModel;
        }

        internal StockContract MapStock(StockModel stock)
        {
            return new StockContract
            {
                description = stock.Description,
                regularPrice = stock.RegularPrice.HasValue ? stock.RegularPrice.Value.ToString(CultureInfo.InvariantCulture) : null,
                stockCode = stock.StockCode,
                taxCodes = stock.TaxCodes.TaxCode
            };
        }

        internal BottleSaleContract MapBottleReturnSale(BottleReturnSale sale)
        {
            return new BottleSaleContract
            {
                tillNumber = sale.TillNumber,
                saleNumber = sale.SaleNumber,
                amount = sale.TotalAmount.ToString(CultureInfo.InvariantCulture),
                registerNumber = sale.Register,
                bottles = (from b in sale.SaleLines
                           select new BottleContract
                           {
                               product = b.StockCode,
                               quantity = b.Quantity.ToString(CultureInfo.InvariantCulture),
                               lineNumber = b.LineNumber,
                               price = b.Price.ToString(CultureInfo.InvariantCulture),
                               amount = b.Amount.ToString(CultureInfo.InvariantCulture)
                           }).ToList()
            };
        }

        /// <summary>
        /// Method to Map reason ReasonListContract with  ReasonsListModel
        /// </summary>
        /// <param name="reasonsListContract"></param>
        /// <returns></returns>
        internal ReasonsList MapReasonsList(ReasonListContract reasonsListContract)
        {
            var reasonsListModel = new ReasonsList
            {
                ReasonTitle = reasonsListContract.reasonTitle,
                Reasons = new List<Reasons>()
            };

            foreach (var reason in reasonsListContract.reasons)
            {
                reasonsListModel.Reasons.Add(new Reasons
                {
                    Code = reason.code,
                    Description = reason.description
                });
            }
            return reasonsListModel;
        }

        /// <summary>
        /// Method to map ReasonModel with VoidSaleContract
        /// </summary>
        /// <param name="reasonModel"></param>
        /// <returns></returns>
        internal VoidSaleContract MapReason(Reasons reasonModel, ICacheManager cacheManager)
        {
            if (reasonModel != null)
            {
                return new VoidSaleContract
                {
                    voidReason = reasonModel.Code?.ToString(),
                    tillNumber = cacheManager.TillNumberForSale,
                    saleNumber = cacheManager.SaleNumber
                };
            }
            else
            {
                return new VoidSaleContract
                {
                    tillNumber = cacheManager.TillNumberForSale,
                    saleNumber = cacheManager.SaleNumber
                };
            }
        }

        /// <summary>
        /// Method to map Add item to sale with AddStockToSaleContract
        /// </summary>
        /// <returns></returns>
        internal AddStockToSaleContract MapAddStockToSale(
            int tillNumber, int saleNumber, int registerNumber,
            string stockCode, float quantity, bool isReturn, bool isManuallyAdded,
            GiftCard giftCard)
        {
            return new AddStockToSaleContract
            {
                saleNumber = saleNumber,
                stockCode = stockCode,
                tillNumber = tillNumber,
                quantity = quantity.ToString(CultureInfo.InvariantCulture),
                isReturnMode = isReturn,
                registerNumber = registerNumber,
                isManuallyAdded = isManuallyAdded,
                giftCard = giftCard != null ? new GiftCardContract
                {
                    price = giftCard.Price,
                    cardNumber = giftCard.CardNumber,
                    giftNumber = giftCard.GiftNumber
                } : null
            };
        }

        /// <summary>
        /// Method to map all policies 
        /// </summary>
        /// <param name="policies"></param>
        /// <returns></returns>
        internal Policy MapPolicies(PolicyContract policies)
        {
            if (policies != null)
            {
                return new Policy
                {
                    AddStockItemNotFoundInList = policies.addStockItemNotFoundInList,
                    AllowPayout = policies.allowPayout,
                    ConfirmDeleteLineItem = policies.confirmDeleteLineItem,
                    DefaultCustomerCode = policies.defaultCustomerCode,
                    DefaultMemberCodeForNonMembers = policies.defaultMemberCodeForNonMembers,
                    DipInputTime = policies.dipInputTime,
                    DisplayCustomerDetails = policies.displayCustomerDetails,
                    ForceAuthorizationOnVoid = policies.forceAuthorizationOnVoid,
                    OperatorBottleReturnLimit = policies.operatorBottleReturnLimit,
                    OperatorCanChangePrice = policies.operatorCanChangePrice,
                    OperatorCanChangeQuantity = policies.operatorCanChangeQantity,
                    OperatorCanGiveDiscount = policies.operatorCanGiveDiscount,
                    OperatorCanReturnBottle = policies.operatorCanReturnBottle,
                    OperatorCanReturnSale = policies.operatorCanReturnSale,
                    AllowAdjustmentForGiveX = policies.allowAdjustmentForGiveX,
                    OperatorCanScanCustomerCard = policies.operatorCanScanCustomerCard,
                    OperatorCanSuspendOrUnsuspendSales = policies.operatorCanSuspendOrUnsuspendSales,
                    OperatorCanSwipeCustomerCard = policies.operatorCanSwipeCustomerCard,
                    OperatorCanSwipeMemberCodeAtPump = policies.operatorCanSwipeMemberCodeAtPump,
                    OperatorCanUseARCustomer = policies.operatorCanUseARCustomer,
                    OperatorCanUseCustomer = policies.operatorCanUseCustomer,
                    OperatorCanVoidSale = policies.operatorCanVoidSale,
                    PrintReceiptForVoidAndReturn = policies.printReceiptForVoidAndReturn,
                    ReasonForPayout = policies.reasonForPayout,
                    ShareSuspendSale = policies.shareSuspendSale,
                    ShowCustomerNoteOnOverlimit = policies.showCustomerNoteOnOverlimit,
                    SupportDipInput = policies.supportDipInput,
                    SupportKitsInPurchase = policies.supportKitsInPurchase,
                    SuspendEmptySales = policies.suspendEmptySales,
                    TenderNameforARAccount = policies.tenderNameforARAccount,
                    UseCustomerDiscountCode = policies.useCustomerDiscountCode,
                    UseProductDiscount = policies.useProductDiscount,
                    UseReasonForVoid = policies.useReasonForVoid,
                    OperatorCanAddStock = policies.operatorCanAddStock,
                    OperatorCanUseLoyalty = policies.operatorCanUseLoyalty,
                    UserCanChangePassword = policies.userCanChangePassword,
                    RequirePasswordOnChangeUser = policies.requirePasswordOnChangeUser,
                    CertificateType = policies.certificateType,
                    FreezeTillAutomatically = policies.freezeTillAutomatically,
                    IdleIntervalAfterAppFreezes = policies.idleIntervalAfterAppFreezes,
                    OperatorCanDrawCash = policies.operatorCanDrawCash,
                    CashDrawReceiptCopies = policies.cashDrawReceiptCopies,
                    CashDropReceiptCopies = policies.cashDropReceiptCopies,
                    OperatorCanDropCash = policies.operatorCanDropCash,
                    AskForCashDropReason = policies.askForCashDropReason,
                    RequireEnvelopNumber = policies.requireEnvelopNumber,
                    UseReasonForCashDraw = policies.useReasonForCashDraw,
                    ForcePrintReceipt = policies.forcePrintReceipt,
                    DelayInNewSale = policies.delayInNewSale,
                    BaseCurrency = policies.baseCurrency,
                    EnableMsgInput = policies.enableMsgInput,
                    CouponTender = policies.couponTender,
                    IsFuelOnlySystem = policies.isFuelOnlySystem,
                    IsPosOnlySystem = policies.isPosOnlySystem,
                    IsPayAtPumpOn = policies.payAtPumpEnabled,
                    IsPostPayOn = policies.postPayEnabled,
                    IsPrePayOn = policies.prepayEnabled,
                    AllowSwipeScan = policies.allowSwipeScan,
                    FuelPriceNotificationCount = policies.fuelPriceNotificationCount,
                    FuelPriceNotificationTimeInterval = policies.fuelPriceNotificationTimeInterval,
                    SupportFuelPriceFromHO = policies.supportFuelPriceFromHO,
                    SupportCashCreditpricing = policies.supportCashCreditpricing,
                    TaxExemption = policies.taxExemption,
                    PumpSpace = policies.pumpSpace,
                    SwitchUserOnEachSale = policies.switchUserOnEachSale,
                    // From here Cache manager is not updated
                    AllowPOSMinimize = policies.allowPOSMinimize,
                    ArpayReceiptCopies = policies.arpayReceiptCopies,
                    BottleReturnReceiptCopies = policies.bottleReturnReceiptCopies,
                    ForceGiftCertificate = policies.forceGiftCertificate,
                    GiftCertificateNumbered = policies.giftCertificateNumbered,
                    GiftTender = policies.giftTender,
                    OperatorCanOpenCashDrawer = policies.operatorCanOpenCashDrawer,
                    OperatorIsTrainer = policies.operatorIsTrainer,
                    PaymentReceiptCopies = policies.paymentReceiptCopies,
                    PayoutReceiptCopies = policies.payoutReceiptCopies,
                    RefundReceiptCopies = policies.refundReceiptCopies,
                    SupportsTaxExampt = policies.supportsTaxExampt,
                    UseReasonForCashDrawer = policies.useReasonForCashDrawer,
                    EnableExactChange = policies.enableExactChange,
                    CheckSC = policies.checkSC,
                    IsFuelPricingGrouped = policies.isFuelPricingGrouped,
                    IsFuelPriceDisplayUsed = policies.isFuelPriceDisplayUsed,
                    IsPriceIncrementEnabled = policies.isPriceIncrementEnabled,
                    IsTaxExemptionPricesEnabled = policies.isTaxExemptionPricesEnabled,
                    StayOnFuelPricePage = policies.stayOnFuelPricePage,
                    RequireToGetCustomerName = policies.requireToGetCustomerName,
                    UserCanPerformManualSales = policies.userCanPerformManualSales,
                    RequireSignature = policies.requireSignature,
                    IsFleetCardRequired = policies.isFleetCardRequired,
                    ClickDelayForPumps = policies.clickDelayForPumps,
                    KickbackRedeemMsg = policies.kickbackRedeemMsg,
                    SupportKickback = policies.supportKickback,
                    CustomKickbackmsg = policies.customKickbackmsg,
                    IsCarwashSupported = policies.isCarwashSupported,
                    IsCarwashIntegrated = policies.isCarwashIntegrated,

                    displayCustGrpID = policies.displayCustGrpID,  // done by Tony 07/17/2018
                    isTDRS_FUELDISCSupported = policies.isTDRS_FUELDISCSupported,  // done by Tony 07/17/2018
                    isFuelDiscountSupported = policies.isFuelDiscountSupported,    // done by Tony 07/17/2018
                    //Done by Tony 10/11/2018
                    SupportPSInet = policies.supportPSInet,
                    PSINet_Type = policies.psiNet_Type,
                    VERSION = policies.version,  //Tony 09/05/2019
                    //Done by Tony 12/19/2018
                    RECEIPT_TYPE = policies.receipT_TYPE, //done by Tony 07/26/2019
                    REWARDS_Enabled = policies.rewardS_Enabled,
                    REWARDS_Gift = policies.rewardS_Gift,
                    REWARDS_TpsIp = policies.rewardS_TpsIp,
                    REWARDS_TpsPort = policies.rewardS_TpsPort,
                    REWARDS_Timeout = policies.rewardS_Timeout,
                    REWARDS_Carwash = policies.rewardS_Carwash,
                    REWARDS_CWGIFT = policies.rewardS_CWGIFT,
                    REWARDS_CWPKG = policies.rewardS_CWPKG,
                    REWARDS_DefaultLoyal = policies.rewardS_DefaultLoyal,
                    REWARDS_Message = policies.rewardS_Message,
                    FuelDept = policies.fuelDept  //done by Tony 05/22/2019
                };
            }

            return null;
        }

        internal List<BottleDetail> MapBottles(List<BottleContract> bottles)
        {
            if (bottles == null)
            {
                return null;
            }

            return (from b in bottles
                    select new BottleDetail
                    {
                        Description = b.description,
                        DefaultQuantity = b.defaultQuantity,
                        Product = b.product,
                        Price = decimal.Parse(b.price, NumberStyles.Any, CultureInfo.InvariantCulture),
                        Amount = decimal.Parse(b.amount, NumberStyles.Any, CultureInfo.InvariantCulture),
                        Image = b.imageUrl
                    }).ToList();
        }

        /// <summary>
        /// Method to map change password contract
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        internal ChangePasswordContract MapChangePassword(ICacheManager cacheManager, string password, string confirmPassword)
        {
            return new ChangePasswordContract
            {
                confirmPassword = password,
                password = confirmPassword,
                userName = cacheManager.UserName
            };
        }

        /// <summary>
        /// Method to map user model with user contract
        /// </summary>
        /// <param name="_userModel"></param>
        /// <returns></returns>
        internal UserContract MapUserContract(User _userModel)
        {
            if (_userModel != null)
            {
                return new UserContract
                {
                    floatAmount = _userModel.FloatAmount.ToString(CultureInfo.InvariantCulture),
                    password = _userModel.Password,
                    posId = _userModel.PosId,
                    shiftNumber = _userModel.ShiftNumber,
                    tillNumber = _userModel.TillNumber,
                    userName = _userModel.UserName,
                    unauthorizedAccess = _userModel.UnauthorizedAccess
                };
            }
            return null;
        }

        /// <summary>
        /// method to map SuspendedSaleContract with SuspendSaleModel
        /// </summary>
        /// <param name="suspendedSales"></param>
        /// <returns>List of SuspendSaleModel</returns>
        internal List<EntityLayer.Entities.Sale.SuspendedSale> MapSuspendedSales(SuspendedSaleContract suspendedSales)
        {
            var suspendedSaleModelList = new List<EntityLayer.Entities.Sale.SuspendedSale>();
            if (suspendedSales != null)
            {
                foreach (var suspendedSale in suspendedSales.suspendedSale)
                {
                    suspendedSaleModelList.Add(MapSuspendedSale(suspendedSale));
                }
            }
            return suspendedSaleModelList;
        }

        /// <summary>
        /// method to map suspended sale with suspended sale model
        /// </summary>
        /// <param name="suspendedSale"></param>
        /// <returns>SuspendSaleModel</returns>
        internal EntityLayer.Entities.Sale.SuspendedSale MapSuspendedSale(DataContracts.Sale.SuspendedSale suspendedSale)
        {
            if (suspendedSale != null)
            {
                return new EntityLayer.Entities.Sale.SuspendedSale
                {
                    Customer = suspendedSale.customer,
                    SaleNumber = suspendedSale.saleNumber,
                    Till = suspendedSale.tillNumber,
                    LineDisplay = MapLineDisplay(suspendedSale.customerDisplayText)
                };
            }
            return null;
        }

        /// <summary>
        /// Method to map sale list contract with salelistModel
        /// </summary>
        /// <param name="saleListContract"></param>
        /// <returns></returns>
        internal List<SaleList> MapSaleList(List<SaleListContract> saleListContract)
        {
            var saleListModel = new List<SaleList>();

            if (saleListContract?.Count > 0)
            {
                foreach (var sale in saleListContract)
                {
                    saleListModel.Add(new SaleList
                    {
                        Date = Convert.ToDateTime(sale.date).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                        SaleNumber = sale.saleNumber,
                        TillNumber = sale.tillNumber,
                        Time = Convert.ToDateTime(sale.time).ToString("h:mm tt", CultureInfo.InvariantCulture),
                        TotalAmount = sale.totalAmount,
                        AllowCorrection = sale.allowCorrection,
                        AllowReason = sale.allowReason
                    });
                }
            }
            return saleListModel;

        }

        internal VerifyStock MapVerifyStock(VerifyStockContract verifyStock)
        {
            if (verifyStock == null)
            {
                return null;
            }

            return new VerifyStock
            {
                RestrictionPage = new RestrictionPage
                {
                    Description = verifyStock.restrictionPage.description,
                    OpenRestrictionPage = verifyStock.restrictionPage.openRestrictionPage
                },
                RegularPriceMessage = MapError(verifyStock.regularPriceMessage),
                QuantityMessage = MapError(verifyStock.quantityMessage),
                GiveXPage = new GiveXPage
                {
                    StockCode = verifyStock.givexPage.stockCode,
                    OpenGiveXPage = verifyStock.givexPage.openGivexPage,
                    RegularPrice = verifyStock.givexPage.regularPrice
                },
                GiftCertificatePage = new GiftCertificatePage
                {
                    StockCode = verifyStock.giftCertPage.stockCode,
                    OpenGiftCertificatePage = verifyStock.giftCertPage.openGiftCertPage,
                    RegularPrice = verifyStock.giftCertPage.regularPrice,
                    GiftNumber = verifyStock.giftCertPage.giftNumber
                },
                PSInetPage = new PSInetPage
                {
                    OpenPSInetPage = verifyStock.psInetPage.openPSInetPage,
                    StockCode = verifyStock.psInetPage.stockCode,
                    RegularPrice = verifyStock.psInetPage.regularPrice
                },
                CanManuallyEnterProduct = verifyStock.canManuallyEnterProduct,
                AddStockPage = new AddStockPage
                {
                    StockCode = verifyStock.addStockPage.stockCode,
                    OpenAddStockPage = verifyStock.addStockPage.openAddStockPage
                },
                ManuallyEnterMessage = verifyStock.manuallyEnterMessage
            };
        }

        internal UpdateSaleLineContract MapUpdateSaleLineToSale(int saleNumber,
            int tillNumber, int lineNumber, byte registerNumber,
            string discount, string discountType, string quantity,
            string price, string reason, string reasonType)
        {
            return new UpdateSaleLineContract
            {
                saleNumber = saleNumber,
                tillNumber = tillNumber,
                quantity = quantity,
                registerNumber = registerNumber,
                price = price,
                lineNumber = lineNumber,
                discount = discount,
                discountType = discountType,
                reasonCode = reason,
                reasonType = reasonType
            };
        }

        internal GiveXCardContract MapGivexCard(GiveXCard _giveXCardModel)
        {
            if (_giveXCardModel != null)
            {
                return new GiveXCardContract
                {
                    amount = _giveXCardModel.GivexPrice.ToString(CultureInfo.InvariantCulture),
                    givexCardNumber = _giveXCardModel.GivexCardNumber,
                    givexPrice = _giveXCardModel.GivexPrice.ToString(CultureInfo.InvariantCulture),
                    saleNumber = _giveXCardModel.SaleNumber,
                    stockCode = _giveXCardModel.StockCodeForGivexCard,
                    tillNumber = _giveXCardModel.TillNumber
                };
            }
            return null;
        }

        internal List<Department> MapDepartments
            (List<DepartmentContract> departmentContract)
        {
            var departments = new List<Department>();
            if (departmentContract != null)
            {
                foreach (var department in departmentContract)
                {
                    departments.Add(new Department
                    {
                        ID = department.departmentId,
                        Name = department.departmentName
                    });
                }
            }
            return departments;
        }

        internal List<EntityLayer.Entities.Reports.Till> MapTill(List<TillContract> tillContract)
        {
            var tills = new List<EntityLayer.Entities.Reports.Till>();
            if (tillContract != null)
            {
                foreach (var till in tillContract)
                {
                    tills.Add(new EntityLayer.Entities.Reports.Till
                    {
                        ID = till.id,
                        Number = till.tillNumber
                    });
                }
            }
            return tills;
        }

        internal List<EntityLayer.Entities.Reports.Shift> MapShift(List<ShiftContract> shiftContract)
        {
            var shifts = new List<EntityLayer.Entities.Reports.Shift>();
            if (shiftContract != null)
            {
                foreach (var shift in shiftContract)
                {
                    shifts.Add(new EntityLayer.Entities.Reports.Shift
                    {
                        ID = shift.id,
                        Number = shift.shiftNumber
                    });
                }
            }
            return shifts;
        }

        internal FlashReport MapFlashReport(FlashReportContract flashReportContract)
        {
            var flashReport = new FlashReport();

            if (flashReportContract != null)
            {
                flashReport.Report = MapReport(flashReportContract?.report);

                if (flashReportContract.totals != null)
                {
                    flashReport.Totals = new Totals
                    {
                        Charges = flashReportContract.totals.charges,
                        InvoiceDiscount = flashReportContract.totals.invoiceDiscount,
                        LineDiscount = flashReportContract.totals.lineDiscount,
                        ProductSales = flashReportContract.totals.productSales,
                        Refunded = flashReportContract.totals.refunded,
                        SalesAfterDiscount = flashReportContract.totals.salesAfterDiscount,
                        Taxes = flashReportContract.totals.taxes,
                        TotalsReceipts = flashReportContract.totals.totalsReceipts
                    };
                }

                if (flashReportContract.departments?.Count > 0)
                {
                    foreach (var department in flashReportContract.departments)
                    {
                        var departments = new Departments
                        {
                            Department = department.department,
                            Description = department.description,
                            NetSales = department.netSales
                        };

                        flashReport.Departments.Add(departments);
                    }
                }
            }
            return flashReport;
        }

        internal TaxExemptVerification MapTaxExemptVerification(TaxExemptVerificationContract taxExemptVerification)
        {
            if (taxExemptVerification == null)
            {
                return null;
            }

            return new TaxExemptVerification
            {
                ConfirmMessage = MapError(taxExemptVerification.confirmMessage),
                ProcessAite = taxExemptVerification.processAite,
                ProcessQite = taxExemptVerification.processQite,
                ProcessSiteReturn = taxExemptVerification.processSiteReturn,
                ProcessSiteSale = taxExemptVerification.processSiteSale,
                ProcessSiteSaleRemoveTax = taxExemptVerification.processSiteSaleRemoveTax,
                TreatyName = taxExemptVerification.treatyName,
                TreatyNumber = taxExemptVerification.treatyNumber
            };
        }

        internal CashDrawTypes MapCashDrawTypes(CashDrawTypeContract cashDrawContract)
        {
            var cashDrawTypes = new CashDrawTypes
            {
                Coins = new List<Coins>(),
                Bills = new List<Bills>()
            };

            if (cashDrawContract?.coins != null)
            {
                foreach (var coin in cashDrawContract.coins)
                {
                    cashDrawTypes.Coins.Add(new Coins
                    {
                        CurrencyName = coin.currencyName,
                        Image = coin.image,
                        Value = decimal.Parse(coin.value, CultureInfo.InvariantCulture)
                    });
                }
            }

            if (cashDrawContract?.bills != null)
            {
                foreach (var bill in cashDrawContract.bills)
                {
                    cashDrawTypes.Bills.Add(new Bills
                    {
                        CurrencyName = bill.currencyName,
                        Image = bill.image,
                        Value = decimal.Parse(bill.value, CultureInfo.InvariantCulture)
                    });
                }
            }
            return cashDrawTypes;
        }

        internal QiteValidate MapQiteValidate(ValidateQiteContract qiteContract)
        {
            if (qiteContract == null)
            {
                return null;
            }

            return new QiteValidate
            {
                BandMemberName = qiteContract.bandMemberName,
                BandMember = qiteContract.bandMember,
                SaleSummary = MapSaleSummary(qiteContract.saleSummary),
                TenderSummary = MapTenderSummary(qiteContract.tenderSummary),
                Tenders = MapTenders(qiteContract.tenders)
            };
        }

        internal List<Tender> MapTenders(List<TenderContract> tenders)
        {
            return MapTenders(new TendersContract
            {
                tenders = tenders
            });
        }

        internal List<Tender> MapTenders(TendersContract tenderContract)
        {
            var tenders = new List<Tender>();
            if (tenderContract != null)
            {
                foreach (var tender in tenderContract.tenders)
                {
                    tenders.Add(new Tender
                    {
                        AmountEntered = tender.amountEntered,
                        AmountValue = tender.amountValue,
                        MaximumValue = decimal.Parse(tender.maximumValue, NumberStyles.Any, CultureInfo.InvariantCulture),
                        MinimumValue = decimal.Parse(tender.minimumValue, NumberStyles.Any, CultureInfo.InvariantCulture),
                        TenderCode = tender.tenderCode,
                        TenderName = tender.tenderName,
                        ImageSource = tender.image,
                        TenderClass = tender.tenderClass,
                        IsEnabled = tender.isEnabled
                    });
                }
            }
            return tenders;
        }

        internal List<CashButtons> MapCashButtons(List<CashButtonContract> cashButtons)
        {
            var cashButtonList = new List<CashButtons>();

            if (cashButtons != null)
            {
                foreach (var cashButton in cashButtons)
                {
                    cashButtonList.Add(new CashButtons
                    {
                        Button = cashButton.button,
                        Value = cashButton.value
                    });
                }
            }
            return cashButtonList;
        }

        internal OverLimitDetails MapOverLimitDetails(OverLimitDetailsContract overlimitDetails)
        {
            if (overlimitDetails == null)
            {
                return null;
            }

            return new OverLimitDetails
            {
                IsGasReasons = overlimitDetails.isGasReasons,
                IsPropaneReasons = overlimitDetails.isPropaneReasons,
                IsTobaccoReasons = overlimitDetails.isTobaccoReasons,
                GasReasons = MapExplanationReason(overlimitDetails.gasReasons),
                PropaneReasons = MapExplanationReason(overlimitDetails.propaneReasons),
                TobaccoReasons = MapExplanationReason(overlimitDetails.tobaccoReasons),
                TaxExemptSale = (from t in overlimitDetails.taxExemptSale
                                 select new TaxExemptSaleLine
                                 {
                                     ExemptedTax = t.exemptedTax,
                                     Product = t.product,
                                     Quantity = t.quantity,
                                     QuotaLimit = t.quotaLimit,
                                     QuotaUsed = t.quotaUsed,
                                     RegularPrice = t.regularPrice,
                                     TaxFreePrice = t.taxFreePrice,
                                     Type = t.type
                                 }).ToList()
            };
        }

        private List<ExplanationReason> MapExplanationReason(List<ExplanationContract> explanations)
        {
            if (explanations == null || explanations.Count == 0)
            {
                return new List<ExplanationReason>();
            }

            return (from e in explanations
                    select new ExplanationReason
                    {
                        ExplanationCode = e.explanationCode,
                        Reason = e.reason
                    }).ToList();
        }

        internal OverrideLimitDetails MapOverrideLimitDetails(OverrideLimitDetailsContract overrideLimitDetails)
        {
            if (overrideLimitDetails == null)
            {
                return null;
            }

            return new OverrideLimitDetails
            {
                Caption = overrideLimitDetails.caption,
                PurchaseItems = (from p in overrideLimitDetails.purchaseItems
                                 select new PurchaseItem
                                 {
                                     Amount = p.amount,
                                     DisplayQuota = p.displayQuota,
                                     EquivalentQuantity = p.equivalentQuantity,
                                     Price = p.price,
                                     ProductId = p.productId,
                                     ProductTypeId = p.productTypeId,
                                     Quantity = p.quantity,
                                     QuotaLimit = p.quotaLimit,
                                     QuotaUsed = p.quotaUsed,
                                     FuelOverLimitText = p.fuelOverLimitText
                                 }).ToList(),
                OverrideCodes = MapOverrideCodes(overrideLimitDetails.overrideCodes)
            };
        }

        private List<OverrideCode> MapOverrideCodes(List<OverrideCodeContract> overrideCodes)
        {
            if (overrideCodes == null || overrideCodes.Count == 0)
            {
                return new List<OverrideCode>();
            }

            return (from o in overrideCodes
                    select new OverrideCode
                    {
                        Codes = o.codes,
                        RowId = o.rowId
                    }).ToList();
        }

        internal CheckoutSummary MapCheckoutSummary(CheckoutSummaryContract saleSummary)
        {
            if (saleSummary == null)
            {
                return null;
            }

            return new CheckoutSummary
            {
                SaleSummary = MapSaleSummary(saleSummary.saleSummary),
                TenderSummary = MapTenderSummary(saleSummary.tenderSummary)
            };
        }

        internal CommonPaymentComplete MapCommonCompletePayment(CommonPaymentCompleteContract paymentComplete)
        {
            if (paymentComplete == null)
            {
                return null;
            }

            return new CommonPaymentComplete
            {
                ChangeDue = paymentComplete.changeDue,
                IsRefund = paymentComplete.isRefund,
                OpenCashDrawer = paymentComplete.openCashDrawer,
                Receipt = MapReport(paymentComplete.paymentReceipt),
                Sale = MapSale(paymentComplete.newSale),
                LimitExceedMessage = paymentComplete.limitExceedMessage,
                LineDisplay = (from c in paymentComplete.customerDisplays
                               select MapLineDisplay(c)).ToList()
            };
        }

        internal CompletePayment MapCompletePayment(PaymentCompleteContract paymentComplete)
        {
            if (paymentComplete == null)
            {
                return null;
            }

            return new CompletePayment
            {
                ChangeDue = paymentComplete.changeDue,
                IsRefund = paymentComplete.isRefund,
                OpenCashDrawer = paymentComplete.openCashDrawer,
                Receipts = MapReports(paymentComplete.receipts),
                Sale = MapSale(paymentComplete.newSale),
                LimitExceedMessage = paymentComplete.limitExceedMessage,
                KickabckServerError = paymentComplete.kickabckServerError,
                LineDisplay = (from c in paymentComplete.customerDisplays
                               select MapLineDisplay(c)).ToList()
            };
        }


        internal CardSwipeInformation MapCardTenderInformation(TenderInformationContract tenderInformationContract)
        {
            if (tenderInformationContract == null)
            {
                return null;
            }

            var card = new CardSwipeInformation
            {
                Amount = tenderInformationContract.amount,
                Caption = tenderInformationContract.caption,
                CardNumber = tenderInformationContract.cardNumber,
                TenderCode = tenderInformationContract.tenderCode,
                AskPin = tenderInformationContract.askPin,
                Pin = tenderInformationContract.pin,
                ProfileId = tenderInformationContract.profileId,
                PromptMessages = tenderInformationContract.promptMessages,
                PoMessage = tenderInformationContract.poMessage,
                IsArCustomer = tenderInformationContract.isArCustomer,
                TenderClass = tenderInformationContract.tenderClass,
                IsGasKing = tenderInformationContract.isGasKing,
                KickBackValue = tenderInformationContract.kickBackValue,
                KickbackPoints = tenderInformationContract.kickbackPoints,
                IsKickBackLinked = tenderInformationContract.isKickBackLinked,
                IsFleet = tenderInformationContract.isFleet,
                IsInvalidLoyaltyCard = tenderInformationContract.isInvalidLoyaltyCard
            };

            if (tenderInformationContract?.profileValidations != null)
            {
                card.ProfileValidations = (from p in tenderInformationContract.profileValidations
                                           select new ProfileValidations
                                           {
                                               Message = p.message,
                                               MessageType = p.messageType
                                           }).ToList();
            }

            CardType cardType;
            Enum.TryParse(tenderInformationContract.cardType, true, out cardType);
            card.CardType = cardType;
            return card;
        }

        internal List<DipInput> MapDipInput(List<DipInputContract> dipInputResponse)
        {
            var dipInputs = new List<DipInput>();

            if (dipInputResponse != null)
            {
                return (from d in dipInputResponse
                        select new DipInput
                        {
                            DipValue = d.dipValue,
                            Grade = d.grade,
                            GradeId = d.gradeId,
                            TankId = d.tankId
                        }).ToList();
            }

            return dipInputs;
        }


        internal List<EntityLayer.Entities.Message.Message> MapMessages(List<MessageContract> responseValue)
        {
            var messageList = new List<EntityLayer.Entities.Message.Message>();

            foreach (var m in responseValue)
            {
                messageList.Add(new EntityLayer.Entities.Message.Message
                {
                    ActualMessage = m.message,
                    Caption = m.caption.Replace("\r\n", " "),
                    Index = m.index.ToString()
                });
            }

            return messageList;
        }

        internal VendorPayout MapVenderPayout(VendorPayoutContract responseValue)
        {
            if (responseValue != null)
            {
                var vendorPayout = new VendorPayout
                {
                    Message = new PayoutMessage
                    {
                        ActualMessage = responseValue.message.message,
                        MessageType = responseValue.message.messageType
                    },
                    Reasons = (from r in responseValue.reasons
                               select new Reasons
                               {
                                   Code = r.code,
                                   Description = r.description
                               }).ToList(),
                    Taxes = (from t in responseValue.taxes
                             select new Tax
                             {
                                 Amount = t.amount,
                                 Code = t.code,
                                 Description = t.description
                             }).ToList(),
                    Vendors = (from v in responseValue.vendors
                               select new Vendors
                               {
                                   Code = v.code,
                                   Name = v.name
                               }).ToList()
                };
                return vendorPayout;
            }
            return null;
        }

        internal PaymentByFleet MapPaymentByFleet(FleetContract paymentByFleet)
        {
            if (paymentByFleet != null)
            {
                return new PaymentByFleet
                {
                    Caption = paymentByFleet.caption,
                    AllowSwipe = paymentByFleet.allowSwipe,
                    Message = new FleetMessage
                    {
                        Message = paymentByFleet.message?.message,
                        MessageType = paymentByFleet.message.messageType
                    }
                };
            }
            return null;
        }

        internal List<ReportName> MapReprintReportName(List<ReportNameContract> reportNameContract)
        {
            var reportNames = new List<ReportName>();

            if (reportNameContract != null)
            {
                foreach (var report in reportNameContract)
                {
                    reportNames.Add(new ReportName
                    {
                        DateEnabled = report.dateEnabled,
                        IsEnabled = report.isEnabled,
                        ReportType = report.reportType,
                        Name = report.reportName
                    });
                }
            }
            return reportNames;
        }

        internal ReprintReportSale MapReprintReportSale(ReprintReportSaleContract reportSaleContract)
        {
            var ReprintReportSale = new ReprintReportSale();

            if (reportSaleContract != null)
            {
                if (reportSaleContract.isCloseBatchSale)
                {
                    ReprintReportSale.IsCloseBatchSale = true;
                    ReprintReportSale.CloseBatchSales = new List<CloseBatchSales>();

                    foreach (var closeBatchSale in reportSaleContract.closeBatchSales)
                    {
                        var data = Convert.FromBase64String(closeBatchSale.report);

                        var tempCloseBatchSale = new CloseBatchSales
                        {
                            BatchNumber = closeBatchSale.batchNumber,
                            Date = closeBatchSale.date,
                            Report = Encoding.UTF8.GetString(data),
                            TerminalId = closeBatchSale.terminalId,
                            Time = closeBatchSale.time
                        };

                        ReprintReportSale.CloseBatchSales.Add(tempCloseBatchSale);
                    }
                }
                else if (reportSaleContract.isPayAtPumpSale)
                {
                    ReprintReportSale.IsPayAtPumpSale = true;
                    ReprintReportSale.PayAtPumpSales = new List<PayAtPumpSale>();

                    foreach (var payAtPumpSale in reportSaleContract.payAtPumpSales)
                    {
                        var tempPayAtPumpSale = new PayAtPumpSale
                        {
                            Amount = payAtPumpSale.amount,
                            Date = payAtPumpSale.date,
                            Grade = payAtPumpSale.grade,
                            Pump = payAtPumpSale.pump,
                            Time = payAtPumpSale.time,
                            Volume = payAtPumpSale.volume,
                            SaleNumber = payAtPumpSale.saleNumber
                        };

                        ReprintReportSale.PayAtPumpSales.Add(tempPayAtPumpSale);
                    }
                }
                else if (reportSaleContract.isPayInsideSale)
                {
                    ReprintReportSale.IsPayInsideSale = true;
                    ReprintReportSale.PayInsideSales = new List<PayInsideSales>();

                    foreach (var payInsideSale in reportSaleContract.payInsideSales)
                    {
                        var tempPayInsideSales = new PayInsideSales
                        {
                            Amount = payInsideSale.amount,
                            SaleNumber = payInsideSale.saleNumber,
                            Customer = payInsideSale.customer,
                            SoldOn = payInsideSale.soldOn,
                            Time = payInsideSale.time
                        };

                        ReprintReportSale.PayInsideSales.Add(tempPayInsideSales);
                    }
                }
                else if (reportSaleContract.isPaymentSale)
                {
                    ReprintReportSale.IsPaymentSale = true;
                    ReprintReportSale.PaymentSales = new List<PaymentSales>();

                    foreach (var closeBatchSale in reportSaleContract.paymentSales)
                    {
                        var tempPaymentSales = new PaymentSales
                        {
                            SoldOn = closeBatchSale.soldOn,
                            Amount = closeBatchSale.amount,
                            SaleNumber = closeBatchSale.saleNumber,
                            Time = closeBatchSale.time
                        };

                        ReprintReportSale.PaymentSales.Add(tempPaymentSales);
                    }
                }
            }

            return ReprintReportSale;
        }

        internal InitializeFuelPump MapInitializeFuelPump(
            InitalizeFuelPumpContract fuelPumpsContract)
        {
            var fuelPumps = new InitializeFuelPump();


            if (fuelPumpsContract != null)
            {
                fuelPumps = new InitializeFuelPump
                {
                    IsCurrentEnabled = fuelPumpsContract.isCurrentEnabled,
                    IsErrorEnabled = fuelPumpsContract.isErrorEnabled,
                    IsFinishEnabled = fuelPumpsContract.isFinishEnabled,
                    IsFuelPriceEnabled = fuelPumpsContract.isFuelPriceEnabled,
                    IsManualEnabled = fuelPumpsContract.isManualEnabled,
                    IsPrepayEnabled = fuelPumpsContract.isPrepayEnabled,
                    IsPropaneEnabled = fuelPumpsContract.isPropaneEnabled,
                    IsResumeButtonEnabled = fuelPumpsContract.isResumeButtonEnabled,
                    IsStopButtonEnabled = fuelPumpsContract.isStopButtonEnabled,
                    IsTierLevelEnabled = fuelPumpsContract.isTierLevelEnabled
                };

                if (fuelPumpsContract.bigPumps != null)
                {
                    fuelPumps.BigPumps = (from b in fuelPumpsContract.bigPumps
                                          select new BigPumps
                                          {
                                              Amount = b.amount,
                                              IsPumpVisible = b.isPumpVisible,
                                              PumpId = b.pumpId,
                                              PumpLabel = b.pumpLabel,
                                              PumpMessage = b.pumpMessage
                                          }).ToList();
                }

                if (fuelPumpsContract.pumps != null)
                {
                    fuelPumps.Pumps = (from f in fuelPumpsContract.pumps
                                       select new PumpStatus
                                       {
                                           BasketButtonCaption = f.basketButtonCaption,
                                           BasketButtonVisible = f.basketButtonVisible,
                                           BasketLabelCaption = f.basketLabelCaption,
                                           PrepayText = f.prepayText,
                                           PumpButtonCaption = f.pumpButtonCaption,
                                           PumpId = f.pumpId,
                                           Status = f.status != null ? f.status : string.Empty,
                                           PayPumporPrepay = f.payPumporPrepay,
                                           EnableBasketButton = f.enableBasketButton,
                                           EnableStackBasketButton = f.enableStackBasketButton,
                                           CanCashierAuthorize = f.canCashierAuthorize
                                       }).ToList();
                }
            }

            return fuelPumps;
        }

        internal PumpMessage MapPumpMessage(DataContracts.Common.Error contract)
        {
            var headOfficeMessage = new PumpMessage();

            if (contract != null)
            {
                headOfficeMessage.Message = contract.message;
                headOfficeMessage.MessageType = contract.messageType.ToString();
            }

            return headOfficeMessage;
        }

        internal VendorCoupon MapVendorCoupon(VendorCouponContract vendorCouponContract)
        {
            if (vendorCouponContract != null)
            {
                return new VendorCoupon
                {
                    DefaultCoupon = vendorCouponContract.defaultCoupon,
                    SaleVendorCoupons = (from v in vendorCouponContract.saleVendorCoupons
                                         select new SaleVendorCoupon
                                         {
                                             Coupon = v.coupon,
                                             SerialNumber = v.serialNumber
                                         }).ToList()
                };
            }

            return new VendorCoupon
            {
                DefaultCoupon = string.Empty,
                SaleVendorCoupons = new List<SaleVendorCoupon>()
            };
        }

        internal Register MapRegister(RegisterContract contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Register
            {
                CashDrawer = new CashDrawer
                {
                    Name = contract.cashDrawer.name,
                    OpenCode = contract.cashDrawer.openCode,
                    UseCashDrawer = contract.cashDrawer.useCashDrawer,
                    UseOposCashDrawer = contract.cashDrawer.useOposCashDrawer
                },
                CustomerDisplay = new CustomerDisplay
                {
                    DisplayCode = contract.customerDisplay.displayCode,
                    DisplayLen = contract.customerDisplay.displayLen,
                    Name = contract.customerDisplay.name,
                    Port = contract.customerDisplay.port,
                    UseCustomerDisplay = contract.customerDisplay.useCustomerDisplay,
                    UseOposCustomerDisplay = contract.customerDisplay.useOposCustomerDisplay
                },
                Msr = new Msr
                {
                    Name = contract.msr.name,
                    UseMsr = contract.msr.useMsr,
                    UseOposMsr = contract.msr.useOposMsr
                },
                Receipt = new ReceiptPrinter
                {
                    Name = contract.receipt.name,
                    ReceiptDriver = contract.receipt.receiptDriver,
                    UseOposReceiptPrinter = contract.receipt.useOposReceiptPrinter,
                    UseReceiptPrinter = contract.receipt.useReceiptPrinter
                },
                Report = new ReportPrinter
                {
                    Driver = contract.report.driver,
                    Font = contract.report.font,
                    FontSize = contract.report.fontSize,
                    Name = contract.report.name,
                    UseOposReportPrinter = contract.report.useOposReportPrinter,
                    UseReportPrinter = contract.report.useReportPrinter
                },
                Scanner = new Scanner
                {
                    Name = contract.scanner.name,
                    Port = contract.scanner.port,
                    Setting = contract.scanner.setting,
                    UseOposScanner = contract.scanner.useOposScanner,
                    UseScanner = contract.scanner.useScanner
                }
            };
        }

        internal CancelTender MapCancelTender(CancelTendersContract contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new CancelTender
            {
                Success = contract.success,
                LineDisplay = MapLineDisplay(contract.customerDisplay)
            };
        }

        internal List<Sounds> MapSounds(SoundContract soundContract)
        {
            var sounds = new List<Sounds>();

            if (soundContract != null)
            {
                if (soundContract.deviceSounds != null)
                {
                    sounds.AddRange(MapSoundEntity(soundContract.deviceSounds));
                }
                if (soundContract.pumpSounds != null)
                {
                    sounds.AddRange(MapSoundEntity(soundContract.pumpSounds));
                }
                if (soundContract.systemSounds != null)
                {
                    sounds.AddRange(MapSoundEntity(soundContract.systemSounds));
                }
            }

            return sounds;
        }

        private List<Sounds> MapSoundEntity(List<SoundEntityContract> soundEntity)
        {
            var sounds = new List<Sounds>();

            foreach (var sound in soundEntity)
            {
                var tempSound = new Sounds
                {
                    File = sound.file,
                    Name = sound.name
                };

                sounds.Add(tempSound);
            }

            return sounds;
        }

        internal GetPumpAction MapPumpAction(GetPumpActionContract pumpAction)
        {
            var getPumpAction = new GetPumpAction();

            if (pumpAction != null)
            {
                getPumpAction.Amount = pumpAction.amount;
                getPumpAction.IsPumpVisible = pumpAction.isPumpVisible;
                getPumpAction.PumpId = pumpAction.pumpId;
                getPumpAction.PumpLabel = pumpAction.pumpLabel;
                getPumpAction.PumpMessage = pumpAction.pumpMessage;
            }
            return getPumpAction;
        }

        internal TierLevel MapTierLevel(TierLevelContract contract)
        {
            if (contract != null)
            {
                contract.levels = contract.levels ?? new List<LevelContract>();
                contract.tiers = contract.tiers ?? new List<TierContract>();

                var tierLevel = new TierLevel
                {
                    PageCaption = contract.pageCaption,
                    PumpTierLevels = MapPumpTierLevel(contract.pumpTierLevels),
                    Levels = (from l in contract.levels
                              select new Level
                              {
                                  LevelId = l.levelId,
                                  LevelName = l.levelName
                              }).ToList(),
                    Tiers = (from t in contract.tiers
                             select new Tier
                             {
                                 TierId = t.tierId,
                                 TierName = t.tierName
                             }).ToList(),
                    Message = new TierLevelMessage
                    {
                        Message = contract.message == null ? string.Empty : contract.message.message,
                        MessageType = contract.message == null ? 0 : contract.message.messageType
                    }
                };

                return tierLevel;
            }
            else
            {
                return null;
            }
        }

        private List<PumpTierLevel> MapPumpTierLevel(List<PumpTierLevelContract> pumpTierLevels)
        {
            if (pumpTierLevels != null)
            {
                var tierLevels = new List<PumpTierLevel>();

                foreach (var tierLevel in pumpTierLevels)
                {
                    tierLevels.Add(new PumpTierLevel
                    {
                        LevelId = tierLevel.levelId,
                        LevelName = tierLevel.levelName,
                        PumpId = tierLevel.pumpId,
                        TierId = tierLevel.tierId,
                        TierName = tierLevel.tierName
                    });
                }

                return tierLevels;
            }
            else
            {
                return null;
            }
        }

        internal ValidateTillClose MapValidateTill(ValidateTillCloseContract validateTillContract)
        {
            if (validateTillContract == null)
            {
                return null;
            }

            return new ValidateTillClose
            {
                ProcessTankDip = validateTillContract.processTankDip,
                CloseTillMessage = MapValidateCloseTillMessage(validateTillContract.closeTillMessage),
                EndSaleSessionMessage = MapValidateCloseTillMessage(validateTillContract.endSaleSessionMessage),
                PrepayMessage = MapValidateCloseTillMessage(validateTillContract.prepayMessage),
                ReadTotalizerMessage = MapValidateCloseTillMessage(validateTillContract.readTotalizerMessage),
                SuspendSaleMessage = MapValidateCloseTillMessage(validateTillContract.suspendSaleMessage),
                TankDipMessage = MapValidateCloseTillMessage(validateTillContract.tankDipMessage)
            };
        }

        private GenericMessage MapValidateCloseTillMessage(GenericMessageContract message)
        {
            if (message == null)
            {
                return new GenericMessage();
            }
            else
            {
                return new GenericMessage
                {
                    Message = message.message,
                    MessageType = message.messageType
                };
            }
        }

        internal CloseTill MapTillClose(CloseTillContract closeTillContract)
        {
            if (closeTillContract == null)
            {
                return null;
            }
            else
            {
                return new CloseTill
                {
                    ShowBillCoins = closeTillContract.showBillCoins,
                    ShowEnteredField = closeTillContract.showEnteredField,
                    ShowSystemField = closeTillContract.showSystemField,
                    ShowDifferenceField = closeTillContract.showDifferenceField,
                    BillCoins = MapBillCoins(closeTillContract.billCoins),
                    Tenders = MapCloseTillTenders(closeTillContract.tenders),
                    Total = closeTillContract.total,
                    LineDisplay = MapLineDisplay(closeTillContract.customerDisplay)
                };
            }
        }

        private List<CloseTillTenders> MapCloseTillTenders(List<CloseTillTendersContract> tenders)
        {
            if (tenders == null)
            {
                return null;
            }
            else
            {
                return (from t in tenders
                        select new CloseTillTenders
                        {
                            Count = t.count,
                            Difference = t.difference,
                            Entered = t.entered,
                            System = t.system,
                            Tender = t.tender
                        }).ToList();
            }
        }

        private List<BillCoins> MapBillCoins(List<BillCoinsContract> billCoins)
        {
            if (billCoins == null)
            {
                return null;
            }
            else
            {
                return (from b in billCoins
                        select new BillCoins
                        {
                            Description = b.description,
                            Value = b.value.Equals("0.00") ? string.Empty : b.value,
                            Amount = b.amount.Equals("0") ? string.Empty : b.amount
                        }).ToList();
            }
        }

        internal List<PropaneGrade> MapPropaneGrades(List<PropaneGradesContract> contract)
        {
            if (contract != null)
            {
                return (from i in contract
                        select new PropaneGrade
                        {
                            FullName = i.fullName,
                            Id = i.id,
                            Shortname = i.shortname,
                            StockCode = i.stockCode
                        }).ToList();
            }
            return null;
        }

        internal List<LoadPumps> MapPropanePumps(List<LoadPumpsContract> propanePumpsContract)
        {
            if (propanePumpsContract != null)
            {
                return (from i in propanePumpsContract
                        select new LoadPumps
                        {
                            Id = i.id,
                            Name = i.name,
                            PositionId = i.positionId
                        }).ToList();
            }

            return null;
        }

        internal FuelPrices MapFuelPrices(FuelPricesContract contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new FuelPrices
            {
                CanReadTotalizer = contract.canReadTotalizer,
                CanSelectPricesToDisplay = contract.canSelectPricesToDisplay,
                Caption = contract.caption,
                IsCashPriceEnabled = contract.isCashPriceEnabled,
                IsCreditPriceEnabled = contract.isCreditPriceEnabled,
                IsErrorEnabled = contract.isErrorEnabled,
                IsExitEnabled = contract.isExitEnabled,
                IsPricesToDisplayChecked = contract.isPricesToDisplayChecked,
                IsPricesToDisplayEnabled = contract.isPricesToDisplayEnabled,
                IsReadTankDipChecked = contract.isReadTankDipChecked,
                IsReadTankDipEnabled = contract.isReadTankDipEnabled,
                IsReadTotalizerChecked = contract.isReadTotalizerChecked,
                IsReadTotalizerEnabled = contract.isReadTotalizerEnabled,
                IsTaxExemptedCashPriceEnabled = contract.isTaxExemptedCashPriceEnabled,
                IsTaxExemptedCreditPriceEnabled = contract.isTaxExemptedCreditPriceEnabled,
                IsTaxExemptionVisible = contract.isTaxExemptionVisible,
                IsIncrementEnabled = contract.isIncrementEnabled,
                Prices = (from fp in contract.fuelPrices
                          select MapFuelPrice(fp)).ToList(),
                Report = MapReport(contract.report)
            };
        }

        internal Price MapFuelPrice(FuelPriceContract contract)
        {
            if (contract == null)
            {
                return null;
            }

            return new Price
            {
                Row = contract.row,
                CashPrice = contract.cashPrice,
                CreditPrice = contract.creditPrice,
                Grade = contract.grade,
                GradeId = contract.gradeId,
                Level = contract.level,
                LevelId = contract.levelId,
                TaxExemptedCashPrice = contract.taxExemptedCashPrice,
                TaxExemptedCreditPrice = contract.taxExemptedCreditPrice,
                Tier = contract.tier,
                TierId = contract.tierId,
            };
        }

        internal PriceToDisplay MapPricesToDisplay(PricesToDisplayContract prices)
        {
            if (prices == null)
            {
                return null;
            }

            return new PriceToDisplay
            {
                Grades = prices.grades,
                GradesState = MapComboBoxStates(prices.gradesState),
                Levels = prices.levels,
                LevelsState = MapComboBoxStates(prices.levelsState),
                Tiers = prices.tiers,
                TiersState = MapComboBoxStates(prices.tiersState)
            };
        }

        private List<PriceToDisplayComboBox> MapComboBoxStates(List<ComboboxStateContract> states)
        {
            if (states?.Count == 0)
            {
                return new List<PriceToDisplayComboBox>();
            }

            return (from t in states
                    select new PriceToDisplayComboBox
                    {
                        IsEnabled = t.isEnabled,
                        SelectedValue = t.selectedValue
                    }).ToList();
        }

        internal GivexSaleCard MapGivexSaleContract(DeactivateGivexCardContract givexSaleContract)
        {
            if (givexSaleContract != null)
            {
                return new GivexSaleCard
                {
                    Sale = MapSale(givexSaleContract.sale),
                    Report = MapReport(givexSaleContract.receipt)
                };
            }
            return null;
        }

        internal FinishTillClose MapFinishTillClose(FinishTillCloseContract finishTiilCloseContract)
        {
            if (finishTiilCloseContract != null)
            {
                return new FinishTillClose
                {
                    LineDisplay = MapLineDisplay(finishTiilCloseContract.lcdMessage),
                    Message = new GenericMessage
                    {
                        Message = finishTiilCloseContract.message?.message,
                        MessageType = finishTiilCloseContract.message == null ? 0 : finishTiilCloseContract.
                        message.messageType
                    },
                    Reports = MapReports(finishTiilCloseContract.reports)
                };
            }
            else
            {
                return null;
            }
        }

        internal PriceIncrementDecrement MapIncrementsAndDecrements(PriceIncrementDecrementContract prices)
        {
            if (prices == null)
            {
                return null;
            }

            return new PriceIncrementDecrement
            {
                IsCreditEnabled = prices.isCreditEnabled,
                PriceDecrements = (from d in prices.priceDecrements
                                   select MapPriceDecrement(d)).ToList(),
                PriceIncrements = (from d in prices.priceIncrements
                                   select MapPriceIncrement(d)).ToList(),
            };
        }

        internal PriceDecrement MapPriceDecrement(PriceDecrementContract obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new PriceDecrement
            {
                Cash = obj.cash,
                Credit = obj.credit,
                LevelId = obj.levelId,
                Row = obj.row,
                TierId = obj.tierId,
                TierLevel = obj.tierLevel
            };
        }

        internal SetPriceDecrement MapSetPriceDecrement(SetPriceDecrementContract obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new SetPriceDecrement
            {
                Price = MapPriceDecrement(obj.price),
                Report = MapReport(obj.report)
            };
        }

        internal SetPriceIncrement MapSetPriceIncrement(SetPriceIncrementContract obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new SetPriceIncrement
            {
                Price = MapPriceIncrement(obj.price),
                Report = MapReport(obj.report)
            };
        }

        internal PriceIncrement MapPriceIncrement(PriceIncrementContract obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new PriceIncrement
            {
                Cash = obj.cash,
                Credit = obj.credit,
                Row = obj.row,
                Grade = obj.grade,
                GradeId = obj.gradeId
            };
        }

        internal OverPayment MapOverPayment(OverPaymentContract contract)
        {
            if (contract != null)
            {
                return new OverPayment
                {
                    OpenDrawer = contract.openDrawer,
                    TaxExemptReceipt = MapReport(contract.taxExemptReceipt)
                };
            }
            return null;
        }

        internal UncompletePrepayChange MapUncompletePrepayChange(UncompletePrepayChangeContract
            contract)
        {
            if (contract != null)
            {
                return new UncompletePrepayChange
                {
                    ChangeDue = contract.changeDue,
                    OpenDrawer = contract.openDrawer,
                    TaxExemptReceipt = MapReport(contract.taxExemptReceipt)
                };
            }
            return null;
        }

        internal UncompletePrepayLoad MapUncompletePrepayLoad(UncompletePrepayLoadContract contract)
        {
            if (contract != null)
            {
                return new UncompletePrepayLoad
                {
                    Caption = contract.caption,
                    IsChangeEnabled = contract.isChangeEnabled,
                    IsDeleteEnabled = contract.isDeleteEnabled,
                    IsDeleteVisible = contract.isDeleteVisible,
                    IsOverPaymentEnabled = contract.isOverPaymentEnabled,
                    UncompleteSale = MapUncompleteSale(contract.unCompleteSale)
                };
            }

            return null;
        }

        private List<UncompleteSale> MapUncompleteSale(List<UncompleteSaleContract> uncompleteSale)
        {
            if (uncompleteSale != null)
            {
                return (from i in uncompleteSale
                        select new UncompleteSale
                        {
                            Grade = i.grade,
                            Mop = i.mop,
                            PositionId = i.positionId,
                            PrepayAmount = i.prepayAmount,
                            PrepayVolume = i.prepayVolume,
                            PumpId = i.pumpId,
                            RegPrice = i.regPrice,
                            SaleGrade = i.saleGrade,
                            SaleNumber = i.saleNumber,
                            SalePosition = i.salePosition,
                            UnitPrice = i.unitPrice,
                            UsedAmount = i.usedAmount,
                            UsedVolume = i.usedVolume
                        }).ToList();
            }
            return new List<UncompleteSale>();
        }

        internal GiveXReport MapGiveXReport(GiveXReportContract giveXReportContract)
        {
            var giveXReport = new GiveXReport();

            if (giveXReportContract != null)
            {
                if (giveXReportContract.closeBatchReport != null)
                {
                    giveXReport.CloseBatchReport = MapReport(giveXReportContract?.closeBatchReport);
                }

                if (giveXReportContract.reportDetails != null)
                {
                    giveXReport.ReportDetails = (from r in giveXReportContract.reportDetails
                                                 select new ReportDetail
                                                 {
                                                     BatchDate = r.batchDate,
                                                     BatchTime = r.batchTime,
                                                     CashOut = r.cashOut,
                                                     Id = r.id,
                                                     Report = r.report
                                                 }).ToList();
                }
            }

            return giveXReport;
        }

        internal KickBackBalancePoint MapKickBackbalancePoints(BalancePointContract kickbackBalancePoint)
        {
            if (kickbackBalancePoint != null)
            {
                return new KickBackBalancePoint
                {
                    BalancePoint = kickbackBalancePoint.balancePoints
                };
            }

            return new KickBackBalancePoint();
        }

        internal ValidateGasKing MapValidateGasKing(ValidateGasKingContract gasKingResponse)
        {
            if (gasKingResponse != null)
            {
                return new ValidateGasKing
                {
                    IsKickBackLinked = gasKingResponse.isKickBackLinked,
                    PointsReedemed = gasKingResponse.pointsReedemed,
                    Value = gasKingResponse.value
                };
            }

            return new ValidateGasKing();
        }
        //Tony 03/19/2019
        internal static List<Carwash> MapAckrCarwash(List<CarwashContract> cwc)
        {
            if (cwc == null)
                return null;
            var olist = (from c in cwc
                         select new Carwash
                         {
                             UnitId = c.unitId,
                             Category = c.category
                         }).ToList();
            return olist;
        }
        internal static PSVoucherInfo MapPSVoucherInfo(PSVoucherInfoContract psvcinfo)
        {
            PSVoucherInfo psvc = new PSVoucherInfo();
            psvc.Voucher = new PSVoucher
            {
                ProdName = psvcinfo.voucher.prodName,
                Voucher = psvcinfo.voucher.voucher,
                Lines = psvcinfo.voucher.lines
            };
            if (psvcinfo.logos != null)
            {
                psvc.Logos = new List<PSLogo>();
                PSLogo pslogo;
                foreach (var c in psvcinfo.logos)
                {
                    pslogo = new PSLogo
                    {
                        BMAP = c.bmap,
                        ImageString = c.imageString,
                        ImageFileName = c.imageFileName
                    };
                    psvc.Logos.Add(pslogo);

                }

            }
            return psvc;
        }
        internal static PSRefund MapPSRefund(PSRefundContract psrt)
        {
            return new PSRefund()
            {
                UpcNumber = psrt.upcNumber,
                Amount = psrt.amount,
                Name = psrt.name
            };
        }
        internal static PSProfile MapPSProfile(PSProfileContract psprf)
        {
            return new PSProfile
            {
                GroupNumber = psprf.groupNumber,
                ProductVersion = psprf.productVersion,
                EffectiveDate = psprf.effectiveDate,
                TerminalId = psprf.terminalId,
                PSpwd = psprf.pSpwd,
                MID = psprf.mid,
                URL = psprf.url
            };
        }
        
        internal List<PSProduct> MapPSProd(List<PSProductContract> psprods)
        {
            if (psprods == null)
                return null;
            var olist = (from c in psprods
                         select new PSProduct
                         {
                             UpcNumber = c.upcNumber,
                             Name = c.name,
                             ProductCode = c.productCode,
                             AmountLimit = c.amountLimit,
                             StoreGUI = c.storeGUI,
                             Description = c.description,
                             Amount = c.amount,
                             AmtDisplay = c.amtDisplay,
                             CategoryName = c.categoryName
                         }).ToList();
            return olist;
        }
        internal List<PSTransaction> MapPSTransactions(List<PSTransactionContract> pstrans)
        {
            if (pstrans == null)
                return null;
            var olist = (from c in pstrans
                         select new PSTransaction
                         {
                             Amount = c.amount,
                             DESCRIPT = c.descript,
                             SALE_DATE = string.Format("{0:MM-dd-yyyy}", DateTime.Parse(c.salE_DATE)),
                             STOCK_CODE = c.stocK_CODE,
                             TransactionID = c.transactionID
                         }).ToList();
            return olist;
        }
        internal List<PSLogo> MapPSLogo(List<PSLogoContract> pslogos)
        {
            if (pslogos == null)
                return null;
            var olist = (from c in pslogos
                         select new PSLogo
                         {
                             BMAP = c.bmap,
                             ImageString = c.imageString,
                             ImageFileName = c.imageFileName
                         }).ToList();
            return olist;
        }
        //End
    }
}
