using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.EntityLayer.Entities.PaymentSource
{
    public class PSTransaction
    {
        public string TransactionID { get; set; }
        public string SALE_DATE { get; set; }
        public string STOCK_CODE { get; set; }
        public string DESCRIPT { get; set; }
        public string Amount { get; set; }
    }
}
