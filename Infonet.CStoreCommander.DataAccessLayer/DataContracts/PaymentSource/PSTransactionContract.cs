using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.PaymentSource
{
    public class PSTransactionContract
    {
        public string transactionID { get; set; }
        public string salE_DATE { get; set; }
        public string stocK_CODE { get; set; }
        public string descript { get; set; }
        public string amount { get; set; }
    }
}
