using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Common
{
    public class GetSoundsSerializeAction : SerializeAction
    {
        private readonly ISoundRestClient _restClient;

        public GetSoundsSerializeAction(ISoundRestClient restClient)
            : base("GetSounds")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetSounds();

            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var soundContract = new DeSerializer().MapSounds(data);
                    return  new Mapper().MapSounds(soundContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
