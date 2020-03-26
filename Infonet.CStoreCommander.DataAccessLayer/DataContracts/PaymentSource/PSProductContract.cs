using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.PaymentSource
{
    public class PSProductContract
    {
        public string upcNumber { get; set; }
        public string name { get; set; }
        public string productCode { get; set; }
        public string amountLimit { get; set; }
        public string storeGUI { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string amtDisplay { get; set; }
        public string categoryName { get; set; }
    }
}
