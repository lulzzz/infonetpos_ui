using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.DipInputs
{
    public class GetDipInputSerializeAction : SerializeAction
    {
        private readonly IDipInputRestClient _dipInputRestClient;

        public GetDipInputSerializeAction(IDipInputRestClient dipInputRestClient)
            : base("GetDipInput")
        {
            _dipInputRestClient = dipInputRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _dipInputRestClient.GetDipInput();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var dipInputResponse = new DeSerializer().MapDipInput(data);
                    return new Mapper().MapDipInput(dipInputResponse);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
