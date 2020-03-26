using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Discount;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IFuelDiscountSerializeManager
    {
        Task<IEnumerable<ClientGroup>> GetDiscountItemsAsync();
        Task<string> GetFuelCodesAsync();
    }
}
