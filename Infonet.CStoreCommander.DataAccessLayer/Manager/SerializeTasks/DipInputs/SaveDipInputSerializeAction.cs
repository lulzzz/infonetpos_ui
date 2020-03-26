using Infonet.CStoreCommander.DataAccessLayer.DataContracts.DipInput;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.DipInput;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.DipInputs
{
    public class SaveDipInputSerializeAction : SerializeAction
    {
        private readonly IDipInputRestClient _dipInputRestClient;
        private readonly List<DipInputContract> _dipInputs;

        public SaveDipInputSerializeAction(IDipInputRestClient dipInputRestClient,
            List<DipInput> dipInputs)
            : base("SaveDipInput")
        {
            _dipInputRestClient = dipInputRestClient;
            _dipInputs = (from d in dipInputs
                          select new DipInputContract
                          {
                              dipValue = d.DipValue,
                              grade = d.Grade,
                              gradeId = d.GradeId,
                              tankId = d.TankId
                          }).ToList();
        }

        protected async override Task<object> OnPerform()
        {
            var contract = JsonConvert.SerializeObject(_dipInputs);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _dipInputRestClient.SaveDipInput(content);
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
