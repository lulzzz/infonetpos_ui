using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.Messages
{
    public class AckrooTenderPaymentMessage
    {
        public string TenderCode { get; set; }
        public decimal Amount { get; set; }
    }
}
