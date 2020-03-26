using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Logout
{
    public class ValidateTillCloseContract
    {
        public GenericMessageContract prepayMessage { get; set; }
        public GenericMessageContract suspendSaleMessage { get; set; }
        public GenericMessageContract closeTillMessage { get; set; }
        public GenericMessageContract readTotalizerMessage { get; set; }
        public GenericMessageContract tankDipMessage { get; set; }
        public GenericMessageContract endSaleSessionMessage { get; set; }
        public bool processTankDip { get; set; }
    }

    public class GenericMessageContract
    {
        public string message { get; set; }
        public int messageType { get; set; }
    }

    public class CloseTillContract
    {
        public bool showBillCoins { get; set; }
        public bool showEnteredField { get; set; }
        public bool showSystemField { get; set; }
        public bool showDifferenceField { get; set; }
        public List<BillCoinsContract> billCoins { get; set; }
        public List<CloseTillTendersContract> tenders { get; set; }
        public string total { get; set; }
        public LineDisplayContract customerDisplay { get; set; }
    }

    public class BillCoinsContract
    {
        public string description { get; set; }
        public string value { get; set; }
        public string amount { get; set; }
    }

    public class CloseTillTendersContract
    {
        public string tender { get; set; }
        public string count { get; set; }
        public string entered { get; set; }
        public string system { get; set; }
        public string difference { get; set; }
    }

    public class UpdatedTenderContract
    {
        public string name { get; set; }
        public string entered { get; set; }
    }

    public class UpdatedBillCoinContract
    {
        public string description { get; set; }
        public string amount { get; set; }
    }

    public class UpdateTillCloseContract
    {
        public int tillNumber { get; set; }
        public UpdatedTenderContract updatedTender { get; set; }
        public UpdatedBillCoinContract updatedBillCoin { get; set; }
    }

    public class FinishTillCloseContract
    {
        public List<ReportContract> reports { get; set; }
        public LineDisplayContract lcdMessage { get; set; }
        public GenericMessageContract message { get; set; }
    }
}