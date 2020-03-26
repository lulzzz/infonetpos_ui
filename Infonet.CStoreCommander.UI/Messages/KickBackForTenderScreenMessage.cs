using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.Messages
{
    public class KickBackForTenderScreenMessage
    {
        public string PoNumber { get; set; }
        public bool IsKickBack { get; set; }
        public string OutstandingAmount { get; set; }
    }
}
