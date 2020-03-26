using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface ICarwashRestClient
    {
        Task<HttpResponseMessage> ValidateCarwashCode(string code);

        Task<HttpResponseMessage> GetCarwasServerStatus();
    }
}
