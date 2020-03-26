using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Cash
{
    public class GetCashDrawTypesSerializeAction : SerializeAction
    {
        private readonly ICashRestClient _cashRestClient;

        public GetCashDrawTypesSerializeAction(ICashRestClient cashRestClient) 
            : base("GetCashDrawTypes")
        {
            _cashRestClient = cashRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _cashRestClient.GetCashDrawTypes();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var cashDrawTypes = new DeSerializer().MapCashDrawTypes(data);
                    return new Mapper().MapCashDrawTypes(cashDrawTypes);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
