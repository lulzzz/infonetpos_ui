using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Carwash
{
    /// <summary>
    /// class to get the data recieved from APIs
    /// </summary>
    public class ValidateCarwashSerializeAction : SerializeAction
    {

        private readonly ICarwashRestClient _restClient;
        private readonly string _carwashCode;

        public ValidateCarwashSerializeAction(ICarwashRestClient restClient , string carwashCode) : base("getServerStatus")
        {
            _restClient = restClient;
            _carwashCode = carwashCode;
        }

        //method to recieve the data feom validateCarwash API
        protected async override Task<Object> OnPerform()
        {
            var response = await _restClient.ValidateCarwashCode(_carwashCode);
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
