using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports
{
    public class GetKickBackBalanceReportSerializeAction : SerializeAction
    {
        private readonly IReportRestClient _restClient;
        private readonly double _kickBackPoints;

        public GetKickBackBalanceReportSerializeAction(IReportRestClient restClient, double kickBackPoints) 
            : base("GetKickBackBalanceReportSerializeAction")
        {
            _restClient = restClient;
            _kickBackPoints = kickBackPoints;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetKickBackReport(_kickBackPoints);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var ShiftContract = new DeSerializer().MapReport(data);
                    return new Mapper().MapReport(ShiftContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
