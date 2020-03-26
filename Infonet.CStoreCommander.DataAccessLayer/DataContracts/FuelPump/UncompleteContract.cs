using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class UncompleteSaleContract
    {
        public int pumpId { get; set; }
        public int positionId { get; set; }
        public int saleNumber { get; set; }
        public string prepayAmount { get; set; }
        public string prepayVolume { get; set; }
        public string usedAmount { get; set; }
        public string usedVolume { get; set; }
        public int grade { get; set; }
        public string unitPrice { get; set; }
        public int salePosition { get; set; }
        public int saleGrade { get; set; }
        public string regPrice { get; set; }
        public int mop { get; set; }
    }

    public class UncompletePrepayLoadContract
    {
        public bool isDeleteEnabled { get; set; }
        public bool isChangeEnabled { get; set; }
        public bool isOverPaymentEnabled { get; set; }
        public bool isDeleteVisible { get; set; }
        public string caption { get; set; }
        public List<UncompleteSaleContract> unCompleteSale { get; set; }
    }

    public class UncompletePrepayChangePostContract
    {
        public int pumpId { get; set; }
        public int saleNum { get; set; }
        public int tillNumber { get; set; }
        public string finishAmount { get; set; }
        public string finishQty { get; set; }
        public string finishPrice { get; set; }
        public string prepayAmount { get; set; }
        public string positionId { get; set; }
        public int gradeId { get; set; }
    }
    
    public class UncompletePrepayChangeContract
    {
        public string changeDue { get; set; }
        public bool openDrawer { get; set; }
        public ReportContract taxExemptReceipt { get; set; }
    }

    public class OverPaymentContract
    {
        public bool openDrawer { get; set; }
        public ReportContract taxExemptReceipt { get; set; }
    }
}
