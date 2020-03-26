using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.DipInputs
{
    public class PrintDipInputSerializeAction : SerializeAction
    {
        private readonly IDipInputRestClient _dipInputRestClient;

        public PrintDipInputSerializeAction(IDipInputRestClient dipInputRestClient)
            : base("PrintDipInput")
        {
            _dipInputRestClient = dipInputRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _dipInputRestClient.GetDipInputPrint();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var reportResponse = new DeSerializer().MapReport(data);
                    return new Mapper().MapReport(reportResponse);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
