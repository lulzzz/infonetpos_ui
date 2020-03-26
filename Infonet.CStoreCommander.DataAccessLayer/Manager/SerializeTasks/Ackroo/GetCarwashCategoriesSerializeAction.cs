using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Ackroo
{
    public class GetCarwashCategoriesSerializeAction : SerializeAction
    {
        private readonly IAckrooRestClient _IAckrooRestClient;
        public GetCarwashCategoriesSerializeAction(IAckrooRestClient restclient)
            :base("GetCarwashCategories")
        {
            _IAckrooRestClient = restclient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _IAckrooRestClient.GetCarwashCategories();
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = DeSerializer.MapAckrCarwashContract(data);
                    return Mapper.MapAckrCarwash(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
