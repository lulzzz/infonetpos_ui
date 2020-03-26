using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.EntityLayer.Entities.PaymentSource
{
    public class PSRefund
    {
        public string UpcNumber { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
    }
}
