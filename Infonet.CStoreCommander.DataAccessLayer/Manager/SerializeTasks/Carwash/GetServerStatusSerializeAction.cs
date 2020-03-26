using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Carwash
{
    /// <summary>
    /// class to get the data recieved from APIs
    /// </summary>
    public class GetServerStatusSerializeAction : SerializeAction
    {
        private readonly ICarwashRestClient _restClient;

        public GetServerStatusSerializeAction(ICarwashRestClient restClient):base("getServerStatus")
        {
            _restClient = restClient;
        }
        
        //method to get the data from getServerStatus API
        protected async override Task<Object> OnPerform()
        {
            var response =  await _restClient.GetCarwasServerStatus();
            var data = response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return data.Result;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
