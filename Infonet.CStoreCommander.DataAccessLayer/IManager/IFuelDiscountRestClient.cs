using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IFuelDiscountRestClient
    {
        Task<HttpResponseMessage> GetFuelDiscountItemsAsyc();
        Task<HttpResponseMessage> GetFuelCodesAsync();
    }
}
