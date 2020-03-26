using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.DipInput;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.DipInputs;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class DipInputSerializeManager : SerializeManager,
        IDipInputSerializeManager
    {
        private readonly IDipInputRestClient _restClient;

        public DipInputSerializeManager(IDipInputRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<List<DipInput>> GetDipInput()
        {
            var action = new GetDipInputSerializeAction(_restClient);

            await PerformTask(action);

            return (List<DipInput>)action.ResponseValue;
        }

        public async Task<Report> GetDipInputReport()
        {
            var action = new PrintDipInputSerializeAction(_restClient);
            await PerformTask(action);

            return (Report)action.ResponseValue;
        }

        public async Task<List<DipInput>> SaveDipInput(List<DipInput> dipInputs)
        {
            var action = new SaveDipInputSerializeAction(_restClient, dipInputs);

            await PerformTask(action);
            return (List<DipInput>)action.ResponseValue;
        }
    }
}
