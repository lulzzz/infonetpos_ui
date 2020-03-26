using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.System
{
    public class GetRegisterSettingsSerializeAction : SerializeAction
    {
        private readonly ISystemRestClient _restClient;
        private readonly byte _registerNumber;

        public GetRegisterSettingsSerializeAction(
            ISystemRestClient restClient,
            byte registerNumber)
            : base("GetRegisterSettings")
        {
            _restClient = restClient;
            _registerNumber = registerNumber;
        }

        /// <summary>
        /// Method to add stock
        /// </summary>
        /// <returns></returns>
        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetRegisterSettings(_registerNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var parsedData = new DeSerializer().MapRegister(data);
                    return new Mapper().MapRegister(parsedData);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
