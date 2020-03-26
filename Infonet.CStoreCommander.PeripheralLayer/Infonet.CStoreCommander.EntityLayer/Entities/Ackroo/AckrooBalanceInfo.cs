using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Ackroo
{
   public class AckrooBalanceInfo
    {
        public string TransactionStatus { get; set; }
        public string ResponseMessage { get; set; }
        public string Active { get; set; }
        public string LoyaltyBalace { get; set; }
        public string GiftBalance { get; set; }
        public string Increment { get; set; }
        public string AmountFunded { get; set; }
        public List<AckrooItem> Categories { get; set; }
    }
}
