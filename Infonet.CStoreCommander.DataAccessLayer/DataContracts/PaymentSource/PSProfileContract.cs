using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.PaymentSource
{
    public class PSProfileContract
    {
        public string groupNumber { get; set; }
        public string productVersion { get; set; }
        public string effectiveDate { get; set; }
        public string terminalId { get; set; }
        public string pSpwd { get; set; }
        public string mid { get; set; }
        public string url { get; set; }
    }
}
