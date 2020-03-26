using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.UI.Model.Checkout;
using Infonet.CStoreCommander.UI.Model.FuelPrice;
using Infonet.CStoreCommander.UI.Model.FuelPump;
using Infonet.CStoreCommander.UI.Model.HotCategory;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.UI.Model.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using BottleReturnSaleLineModel = Infonet.CStoreCommander.UI.Model.Sale.BottleReturnSaleLineModel;
using BottleReturnSaleModel = Infonet.CStoreCommander.UI.Model.Sale.BottleReturnSaleModel;
using SaleLineModel = Infonet.CStoreCommander.UI.Model.Sale.SaleLineModel;

namespace Infonet.CStoreCommander.UI.Utility
{
    public static class Extensions
    {
        public static EntityLayer.Entities.Sale.BottleReturnSale ToEntity(this BottleReturnSaleModel sale)
        {
            if (sale == null)
            {
                return null;
            }

            return new EntityLayer.Entities.Sale.BottleReturnSale
            {
                Register = sale.Register,
                SaleNumber = sale.SaleNumber,
                TillNumber = sale.TillNumber,
                TotalAmount = sale.TotalAmount,
                SaleLines = (from s in sale.SaleLines
                             select s.ToEntity()).ToList()
            };
        }

        public static EntityLayer.Entities.Sale.BottleReturnSaleLine ToEntity(this BottleReturnSaleLineModel saleLine)
        {
            if (saleLine == null)
            {
                return null;
            }

            return new EntityLayer.Entities.Sale.BottleReturnSaleLine
            {
                Description = saleLine.Description,
                Price = decimal.Parse(saleLine.Price, NumberStyles.Any, CultureInfo.InvariantCulture),
                StockCode = saleLine.StockCode,
                Quantity = saleLine.Quantity,
                Amount = decimal.Parse(saleLine.Amount, NumberStyles.Any, CultureInfo.InvariantCulture),
                Discount = saleLine.Discount,
                LineNumber = saleLine.LineNumber
            };
        }

        public static SaleModel ToModel(this Sale sale)
        {
            if (sale == null)
            {
                return null;
            }

            return new SaleModel
            {
                Customer = sale.CustomerName,
                SaleNumber = sale.SaleNumber,
                Summary = sale.Summary,
                TillNumber = sale.TillNumber,
                TotalAmount = sale.TotalAmount,
                EnableExactChange = sale.EnableExactChange,
                EnableWriteOffButton = sale.EnableWriteOffButton,
                SaleLines = new ObservableCollection<SaleLineModel>(
                    from sl in sale.SaleLines
                    select new SaleLineModel
                    {
                        Description = sl.Description,
                        Price = sl.Price,
                        Quantity = sl.Quantity,
                        Amount = sl.Amount,
                        Discount = !string.IsNullOrEmpty(sl.Discount) ?
                        (sl.DiscountType.Equals("%") ? sl.Discount + sl.DiscountType :
                        sl.DiscountType + sl.DiscountRate) : string.Empty,
                        Code = sl.StockCode,
                        Dept = sl.Dept,
                        AllowDiscountChange = sl.AllowDiscountChange,
                        AllowDiscoutReason = sl.AllowDiscountReason,
                        AllowPriceReason = sl.AllowPriceReason,
                        AllowReturnReason = sl.AllowReturnReason,
                        AllowPriceChange = sl.AllowPriceChange,
                        AllowQuantityChange = sl.AllowQuantityChange,
                        AllowStockCodeChange = false,
                        LineNumber = sl.LineNumber
                    }),
                SaleLineError = sale.Errors,
                LineDisplay = sale.LineDisplay
            };
        }

        public static TenderModel ToModel(this Tender tender)
        {
            if (tender == null)
            {
                return null;
            }

            return new TenderModel
            {
                AmountEntered = tender.AmountEntered,
                AmountValue = tender.AmountValue,
                Code = tender.TenderCode,
                // TODO: Move this to Converter
                Image = new BitmapImage
                {
                    UriSource = Helper.IsValidURI(tender.ImageSource) ?
                    new Uri(tender.ImageSource) :
                        null
                },
                MaximumValue = tender.MaximumValue,
                MinimumValue = tender.MinimumValue,
                Name = tender.TenderName,
                Class = tender.TenderClass,
                IsEnabled = tender.IsEnabled
            };
        }

        public static List<HotProductEntity> ToEntities(this ObservableCollection<HotProductModel> hotProducts)
        {
            return (from h in hotProducts
                    select new HotProductEntity
                    {
                        PageId = h.PageId,
                        PageName = h.PageName,
                        ProductDetails = (from c in h.ProductDetails
                                          select new HotProductDetailEntity
                                          {
                                              DefaultQuantity = c.DefaultQuantity,
                                              Description = c.Description,
                                              Id = c.Id,
                                              ImageSource = c.ImageSource,
                                              Name = c.Name,
                                              Quantity = c.Quantity,
                                              StockCode = c.StockCode
                                          }).ToList()
                    }).ToList();
        }

        public static ObservableCollection<HotProductModel> ToModel(this List<HotProductEntity> hotProducts)
        {
            return new ObservableCollection<HotProductModel>
                (from h in hotProducts
                 select new HotProductModel
                 {
                     PageId = h.PageId,
                     PageName = h.PageName,
                     ProductDetails = new ObservableCollection<ProductDataModel>(
                         from c in h.ProductDetails
                         select new ProductDataModel
                         {
                             DefaultQuantity = c.DefaultQuantity,
                             Description = c.Description,
                             Id = c.Id,
                             ImageSource = c.ImageSource,
                             Name = c.Name,
                             Quantity = c.Quantity,
                             StockCode = c.StockCode
                         })
                 });
        }

        public static Price ToEntity(this FuelPriceModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new Price
            {
                Row = model.Row,
                CashPrice = model.CashPrice,
                CreditPrice = model.CreditPrice,
                Grade = model.Grade,
                GradeId = model.GradeId,
                Level = model.Level,
                LevelId = model.LevelId,
                TaxExemptedCashPrice = model.TaxExemptedCashPrice,
                TaxExemptedCreditPrice = model.TaxExemptedCreditPrice,
                Tier = model.Tier,
                TierId = model.TierId
            };
        }

        public static FuelPriceModel ToModel(this Price model)
        {
            if (model == null)
            {
                return null;
            }

            return new FuelPriceModel
            {
                Row = model.Row,
                CashPrice = model.CashPrice,
                CreditPrice = model.CreditPrice,
                Grade = model.Grade,
                GradeId = model.GradeId,
                Level = model.Level,
                LevelId = model.LevelId,
                TaxExemptedCashPrice = model.TaxExemptedCashPrice,
                TaxExemptedCreditPrice = model.TaxExemptedCreditPrice,
                Tier = model.Tier,
                TierId = model.TierId
            };
        }

        public static FuelPricesModel ToModel(this FuelPrices prices)
        {
            if (prices == null)
            {
                return null;
            }

            return new FuelPricesModel
            {
                Prices = new ObservableCollection<FuelPriceModel>
                (from p in prices.Prices
                 select new FuelPriceModel
                 {
                     Row = p.Row,
                     CashPrice = p.CashPrice,
                     CreditPrice = p.CreditPrice,
                     Grade = p.Grade,
                     GradeId = p.GradeId,
                     Level = p.Level,
                     LevelId = p.LevelId,
                     TaxExemptedCashPrice = p.TaxExemptedCashPrice,
                     TaxExemptedCreditPrice = p.TaxExemptedCreditPrice,
                     Tier = p.Tier,
                     TierId = p.TierId
                 }),
                IsGrouped = prices.IsGrouped,
                Report = prices.Report.ReportContent,
                CanReadTotalizer = prices.CanReadTotalizer,
                CanSelectPricesToDisplay = prices.CanSelectPricesToDisplay,
                Caption = prices.Caption,
                IsCashPriceEnabled = prices.IsCashPriceEnabled,
                IsCreditPriceEnabled = prices.IsCreditPriceEnabled,
                IsErrorEnabled = prices.IsErrorEnabled,
                IsExitEnabled = prices.IsExitEnabled,
                IsPricesToDisplayChecked = prices.IsPricesToDisplayChecked,
                IsPricesToDisplayEnabled = prices.IsPricesToDisplayEnabled,
                IsReadTankDipChecked = prices.IsReadTankDipChecked,
                IsReadTankDipEnabled = prices.IsReadTankDipEnabled,
                IsReadTotalizerChecked = prices.IsReadTotalizerChecked,
                IsReadTotalizerEnabled = prices.IsReadTotalizerEnabled,
                IsTaxExemptedCashPriceEnabled = prices.IsTaxExemptedCashPriceEnabled,
                IsTaxExemptedCreditPriceEnabled = prices.IsTaxExemptedCreditPriceEnabled,
                IsTaxExemptionVisible = prices.IsTaxExemptionVisible,
            };
        }

        public static FuelPrices ToEntity(this FuelPricesModel prices)
        {
            if (prices == null)
            {
                return null;
            }

            return new FuelPrices
            {
                Prices =
                (from p in prices.Prices
                 select new Price
                 {
                     Row = p.Row,
                     CashPrice = p.CashPrice,
                     CreditPrice = p.CreditPrice,
                     Grade = p.Grade,
                     GradeId = p.GradeId,
                     Level = p.Level,
                     LevelId = p.LevelId,
                     TaxExemptedCashPrice = p.TaxExemptedCashPrice,
                     TaxExemptedCreditPrice = p.TaxExemptedCreditPrice,
                     Tier = p.Tier,
                     TierId = p.TierId
                 }).ToList(),
                CanReadTotalizer = prices.CanReadTotalizer,
                CanSelectPricesToDisplay = prices.CanSelectPricesToDisplay,
                Caption = prices.Caption,
                IsCashPriceEnabled = prices.IsCashPriceEnabled,
                IsCreditPriceEnabled = prices.IsCreditPriceEnabled,
                IsErrorEnabled = prices.IsErrorEnabled,
                IsExitEnabled = prices.IsExitEnabled,
                IsPricesToDisplayChecked = prices.IsPricesToDisplayChecked,
                IsPricesToDisplayEnabled = prices.IsPricesToDisplayEnabled,
                IsReadTankDipChecked = prices.IsReadTankDipChecked,
                IsReadTankDipEnabled = prices.IsReadTankDipEnabled,
                IsReadTotalizerChecked = prices.IsReadTotalizerChecked,
                IsReadTotalizerEnabled = prices.IsReadTotalizerEnabled,
                IsTaxExemptedCashPriceEnabled = prices.IsTaxExemptedCashPriceEnabled,
                IsTaxExemptedCreditPriceEnabled = prices.IsTaxExemptedCreditPriceEnabled,
                IsTaxExemptionVisible = prices.IsTaxExemptionVisible,
                IsGrouped = prices.IsGrouped
            };
        }

        public static PriceDecrement ToEntity(this DifferenceModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new PriceDecrement
            {
                Cash = model.Cash,
                Credit = model.Credit,
                LevelId = model.LevelId,
                Row = model.Row,
                TierId = model.TierId,
                TierLevel = model.TierLevel
            };
        }

        public static DifferenceModel ToModel(this PriceDecrement model)
        {
            if (model == null)
            {
                return null;
            }

            return new DifferenceModel
            {
                Cash = model.Cash,
                Credit = model.Credit,
                LevelId = model.LevelId,
                Row = model.Row,
                TierId = model.TierId,
                TierLevel = model.TierLevel
            };
        }

        public static PriceIncrement ToEntity(this IncrementModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new PriceIncrement
            {
                Cash = model.Cash,
                Credit = model.Credit,
                Row = model.Row,
                Grade = model.Grade,
                GradeId = model.GradeId
            };
        }

        public static IncrementModel ToModel(this PriceIncrement model)
        {
            if (model == null)
            {
                return null;
            }

            return new IncrementModel
            {
                Cash = model.Cash,
                Credit = model.Credit,
                Row = model.Row,
                Grade = model.Grade,
                GradeId = model.GradeId
            };
        }
    }

    public class HotProductEntity
    {
        public string PageName { get; set; }
        public int PageId { get; set; }
        public List<HotProductDetailEntity> ProductDetails { get; set; }
    }

    public class HotProductDetailEntity
    {
        public string Description { get; set; }

        public string StockCode { get; set; }

        public int DefaultQuantity { get; set; }

        public int Id { get; set; }

        public int Quantity { get; set; }

        public ImageSource ImageSource { get; set; }

        public string Name { get; set; }
    }
}
