using Infonet.CStoreCommander.UI.Model.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.Messages
{
    public class PSMessage
    {
        public string StoreGUI { get; set; }
        public string UPCNumber { get; set; }
        public string SaleAmount { get; set; }
        public SaleModel CurrentSale { get; set; }
    }
}
