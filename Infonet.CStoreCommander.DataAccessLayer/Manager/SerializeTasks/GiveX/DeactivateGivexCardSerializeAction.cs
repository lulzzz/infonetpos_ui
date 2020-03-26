using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.GiveX
{
    public class DeactivateGivexCardSerializeAction : SerializeAction
    {
        private readonly IGiveXRestClient _restClient;
        private readonly GiveXCard _giveXDeactivateCardModel;

        public DeactivateGivexCardSerializeAction(IGiveXRestClient givexRestClient,
            GiveXCard giveXDeactivateCardModel) : 
            base("DeactivateGivexCard")
        {
            _restClient = givexRestClient;
            _giveXDeactivateCardModel = giveXDeactivateCardModel;
        }

        protected async override Task<object> OnPerform()
        {
            var givexcardContract = new Mapper().MapGivexCard(_giveXDeactivateCardModel);
            var givexcard = JsonConvert.SerializeObject(givexcardContract);
            var content = new StringContent(givexcard, Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.DeactivateGivexcard(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var givexSaleContract = new DeSerializer().MapGivexSaleContract(data);
                    return new Mapper().MapGivexSaleContract(givexSaleContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
