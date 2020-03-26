using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.PaymentSource
{
    public class PSVoucherContract
    {
        public string prodName { get; set; }
        public int lines { get; set; }
        public string voucher { get; set; }
    }
}
