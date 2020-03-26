using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Logout
{
    public class ValidateTillClose
    {
        public GenericMessage PrepayMessage { get; set; }
        public GenericMessage SuspendSaleMessage { get; set; }
        public GenericMessage CloseTillMessage { get; set; }
        public GenericMessage ReadTotalizerMessage { get; set; }
        public GenericMessage TankDipMessage { get; set; }
        public GenericMessage EndSaleSessionMessage { get; set; }
        public bool ProcessTankDip { get; set; }
    }

    public class GenericMessage
    {
        public string Message { get; set; }
        public int MessageType { get; set; }
    }

    public class CloseTill
    {
        public bool ShowBillCoins { get; set; }
        public bool ShowEnteredField { get; set; }
        public bool ShowSystemField { get; set; }
        public bool ShowDifferenceField { get; set; }
        public List<BillCoins> BillCoins { get; set; }
        public List<CloseTillTenders> Tenders { get; set; }
        public string Total { get; set; }
        public LineDisplayModel LineDisplay { get; set; }
    }

    public class BillCoins
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public string Amount { get; set; }
    }

    public class CloseTillTenders
    {
        public string Tender { get; set; }
        public string Count { get; set; }
        public string Entered { get; set; }
        public string System { get; set; }
        public string Difference { get; set; }
    }

    public class UpdatedTender
    {
        public string Name { get; set; }
        public string Entered { get; set; }
    }

    public class UpdatedBillCoin
    {
        public string Description { get; set; }
        public string Amount { get; set; }
    }

    public class FinishTillClose
    {
        public List<Report> Reports { get; set; }
        public LineDisplayModel LineDisplay { get; set; }
        public GenericMessage Message { get; set; }
    }
}
