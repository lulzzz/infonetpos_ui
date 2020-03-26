using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Carwash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    /// <summary>
    /// Class to initiate the API calls
    /// </summary>
    public class CarwashSerializeManager :SerializeManager, ICarwashSerializeManager
    {

        private readonly ICarwashRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        public CarwashSerializeManager(ICarwashRestClient carwashRestClient, ICacheManager cacheManager)
        {
            _restClient = carwashRestClient;
            _cacheManager = cacheManager;
        }

   
        // method to initiate the getServerStatus API calls
        public async Task<bool> GetCarwasServerStatus()
        {
            var action = new GetServerStatusSerializeAction(_restClient);
            await PerformTask(action);
            if (action.ResponseValue.ToString() == "true")
            {
                return true;
            }
            else
            {
               return  false;
            } 
        }


        // method to initiate the validateCarwashCode API calls
        public async Task<bool> ValidateCarwashCode(string code)
        {
            var action = new ValidateCarwashSerializeAction(_restClient,code);
            await PerformTask(action);
            if (action.ResponseValue.ToString() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
