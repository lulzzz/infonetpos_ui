using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IAckrooRestClient
    {
        Task<HttpResponseMessage> GetAckrooStockCode();
        Task<HttpResponseMessage> GetCarwashCategories();
        Task<HttpResponseMessage> GetAckrooCarwashStockCode(string sDesc);
    }
}
