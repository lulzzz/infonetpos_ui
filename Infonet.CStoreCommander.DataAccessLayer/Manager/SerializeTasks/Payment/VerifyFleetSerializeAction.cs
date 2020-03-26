using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payment
{
    public class VerifyFleetSerializeAction : SerializeAction
    {
        private readonly IPaymentRestClient _paymentRestClient;

        public VerifyFleetSerializeAction(IPaymentRestClient paymentRestClient) 
            : base("VerifyFleet")
        {
            _paymentRestClient = paymentRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _paymentRestClient.ValidateFleet();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var paymentByFleetContract = new DeSerializer().MapPaymentByFleet(data);
                    return new Mapper().MapPaymentByFleet(paymentByFleetContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
