using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.FuelPump
{
    public class OverPayment
    {
        public bool OpenDrawer { get; set; }
        public Report TaxExemptReceipt { get; set; }
    }

    public class UncompletePrepayChange
    {
        public string ChangeDue { get; set; }
        public bool OpenDrawer { get; set; }
        public Report TaxExemptReceipt { get; set; }
    }

    public class UncompleteSale
    {
        public int PumpId { get; set; }
        public int PositionId { get; set; }
        public int SaleNumber { get; set; }
        public string PrepayAmount { get; set; }
        public string PrepayVolume { get; set; }
        public string UsedAmount { get; set; }
        public string UsedVolume { get; set; }
        public int Grade { get; set; }
        public string UnitPrice { get; set; }
        public int SalePosition { get; set; }
        public int SaleGrade { get; set; }
        public string RegPrice { get; set; }
        public int Mop { get; set; }
    }

    public class UncompletePrepayLoad
    {
        public bool IsDeleteEnabled { get; set; }
        public bool IsChangeEnabled { get; set; }
        public bool IsOverPaymentEnabled { get; set; }
        public bool IsDeleteVisible { get; set; }
        public string Caption { get; set; }
        public List<UncompleteSale> UncompleteSale { get; set; }
    }
}
