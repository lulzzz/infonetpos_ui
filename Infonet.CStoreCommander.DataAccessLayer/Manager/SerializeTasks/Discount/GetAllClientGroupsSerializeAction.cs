using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Discount
{
    internal class GetAllClientGroupsSerializeAction : SerializeAction
    {
        private readonly IFuelDiscountRestClient _restClient;
        public GetAllClientGroupsSerializeAction(IFuelDiscountRestClient restClient):base("GetFuelDiscountItemsAsyc")
        {
            _restClient = restClient;
            
        }
        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GetFuelDiscountItemsAsyc();
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapClientGroups(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
