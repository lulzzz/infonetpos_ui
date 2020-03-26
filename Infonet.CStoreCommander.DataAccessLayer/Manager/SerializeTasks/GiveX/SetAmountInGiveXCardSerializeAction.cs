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
    public class SetAmountInGiveXCardSerializeAction : SerializeAction
    {
        private readonly IGiveXRestClient _givexRestClient;
        private readonly GiveXCard _giveXCardModel;

        public SetAmountInGiveXCardSerializeAction(IGiveXRestClient givexRestClient,
            GiveXCard giveXCardModel) 
            : base("SetAmountInGiveXCard")
        {
            _givexRestClient = givexRestClient;
            _giveXCardModel = giveXCardModel;
        }

        protected async override Task<object> OnPerform()
        {
            var givexcardContract = new Mapper().MapGivexCard(_giveXCardModel);
            var givexcard = JsonConvert.SerializeObject(givexcardContract);
            var content = new StringContent(givexcard, Encoding.UTF8, ApplicationJSON);

            var response = await _givexRestClient.SetAmount(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapGivexSaleContract(data);
                    return new Mapper().MapGivexSaleContract(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
