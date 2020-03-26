using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.EntityLayer.Entities.PaymentSource
{
    public class PSProfile
    {
        public string GroupNumber { get; set; }
        public string ProductVersion { get; set; }
        public string EffectiveDate { get; set; }
        public string TerminalId { get; set; }
        public string PSpwd { get; set; }
        public string MID { get; set; }
        public string URL { get; set; }
    }
}
