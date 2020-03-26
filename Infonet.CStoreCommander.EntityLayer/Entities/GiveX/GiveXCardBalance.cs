using Infonet.CStoreCommander.EntityLayer.Entities.Reports;

namespace Infonet.CStoreCommander.EntityLayer.Entities.GiveX
{
    public class GiveXCardBalance
    {
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsExistingCard { get; set; }
        public Report Report{ get; set; }
    }
}
