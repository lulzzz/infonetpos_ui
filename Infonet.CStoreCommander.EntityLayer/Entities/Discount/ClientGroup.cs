using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Discount
{
    public class ClientGroup
    {
        public string DiscountName { get; set; }
        public float DiscountRate { get; set; }
        public string DiscountType { get; set; }
        public string Footer { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
