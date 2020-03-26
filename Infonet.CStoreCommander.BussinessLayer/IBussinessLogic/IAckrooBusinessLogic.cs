using Infonet.CStoreCommander.EntityLayer.Entities.Ackroo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IAckrooBusinessLogic
    {
        Task<string> GetAckrooStockCode();
        Task<List<Carwash>> GetCarwashCategories();
        Task<string> GetAckrooCarwashStockCode(string sDesc);
    }
}
