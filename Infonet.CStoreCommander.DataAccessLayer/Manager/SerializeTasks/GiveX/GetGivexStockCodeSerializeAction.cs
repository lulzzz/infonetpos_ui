using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.GiveX
{
    public class GetGivexStockCodeSerializeAction : SerializeAction
    {
        private readonly IGiveXRestClient _givexRestClient;
        public GetGivexStockCodeSerializeAction(IGiveXRestClient givexRestClient)
            : base("GetGivexStockCode")
        {
            _givexRestClient = givexRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _givexRestClient.getGivexStockCode();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapStockCode(data);
                    return new Mapper().MapStockCode(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
