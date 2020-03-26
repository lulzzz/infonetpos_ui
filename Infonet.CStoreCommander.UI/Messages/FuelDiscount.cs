using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infonet.CStoreCommander.UI.Model.Sale;

namespace Infonet.CStoreCommander.UI.Messages
{
    public class FuelDiscount
    {
        
        public string DiscoutType { get; set; }
        public string DiscountName { get; set; }
        public string Reason { get; set; }
        public string CustGrpID { get; set; }
        public List<SaleLineModel> FuelLines { get; set; }
        public double DiscountRate { get; set; }
    }
}
