using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports
{
    public class GetAllDepartmentSerializeAction : SerializeAction
    {
        private readonly IReportRestClient _restClient;

        public GetAllDepartmentSerializeAction(IReportRestClient restClient)
            : base("GetAllDepartment")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetAllDepartment();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var departmentContract = new DeSerializer().MapDepartments(data);
                    return new Mapper().MapDepartments(departmentContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
