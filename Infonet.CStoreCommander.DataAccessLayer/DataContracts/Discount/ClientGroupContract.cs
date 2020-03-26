using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Discount
{
    public class ClientGroupContract
    {
        public string discountName { get; set; }
        public float discountRate { get; set; }
        public string discountType { get; set; }
        public string footer { get; set; }
        public string groupId { get; set; }
        public string groupName { get; set; }
    }
}
