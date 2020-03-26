using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    /// <summary>
    /// class to call the carwash APIs
    /// </summary>
    public class CarwashRestClient : ICarwashRestClient
    {
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="cacheManager"></param>
        public CarwashRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        
        /// <summary>
        /// method to call the carwash server API
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetCarwasServerStatus()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetCarwashServerStatus, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to call the validate carwash API
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ValidateCarwashCode(string code)
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.ValidateCarwashCode + "?code=" + code, _cacheManager.AuthKey);
        }
    }
}
