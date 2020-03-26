using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.PaymentSource
{
    public class PSRefundContract
    {
        public string upcNumber { get; set; }
        public string name { get; set; }
        public string amount { get; set; }

    }
}
