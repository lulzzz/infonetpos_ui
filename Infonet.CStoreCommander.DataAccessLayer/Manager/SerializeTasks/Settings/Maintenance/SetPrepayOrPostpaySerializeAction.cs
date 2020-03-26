using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Settings.Maintenance
{
    public class SetPrepayOrPostpaySerializeAction : SerializeAction
    {
        private readonly IMaintenanceRestClient _maintenanceRestClient;
        private readonly bool _isPrepay;
        private readonly bool _isOn;

        public SetPrepayOrPostpaySerializeAction(IMaintenanceRestClient maintenanceRestClient,
            bool isOn, bool isPrepay) : base("SetPrepayOrPostpay")
        {
            _maintenanceRestClient = maintenanceRestClient;
            _isOn = isOn;
            _isPrepay = isPrepay;
        }

        protected async override Task<object> OnPerform()
        {
            HttpResponseMessage response;

            if (_isPrepay)
            {
                response = await _maintenanceRestClient.SetPrepay(_isOn);
            }
            else
            {
                response = await _maintenanceRestClient.SetPostPay(_isOn);
            }

            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapSuccess(data);
                    return result.success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
