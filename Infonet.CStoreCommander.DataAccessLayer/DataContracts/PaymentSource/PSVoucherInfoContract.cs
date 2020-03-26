using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.PaymentSource
{
    public class PSVoucherInfoContract
    {
        public PSVoucherContract voucher { get; set; }
        public List<PSLogoContract> logos { get; set; }
    }
}
