using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Discount;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IFuelDiscountBusinessLogic
    {
        Task<IEnumerable<ClientGroup>> GetDiscountItemsAsync();
        Task<string> GetFuelCodesAsync();
        
        
    }
}
