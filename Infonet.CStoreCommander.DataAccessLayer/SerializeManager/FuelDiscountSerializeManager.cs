using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Discount;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Discount;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Discount;


namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class FuelDiscountSerializeManager : SerializeManager, IFuelDiscountSerializeManager
    {
        private IFuelDiscountRestClient _fuelDiscountRestClient;
        public FuelDiscountSerializeManager(IFuelDiscountRestClient fuelDiscountRestClient)
        {
            _fuelDiscountRestClient = fuelDiscountRestClient;
        }
        public async Task<IEnumerable<ClientGroup>> GetDiscountItemsAsync()
        {
            var action = new GetAllClientGroupsSerializeAction(_fuelDiscountRestClient);
            await PerformTask(action);
            List<ClientGroupContract> olist = (List<ClientGroupContract>)action.ResponseValue;
            return new Mapper().MapClientGroups(olist);
        }

        public async Task<string> GetFuelCodesAsync()
        {
            var action = new GetFuelCodesSerializeAction(_fuelDiscountRestClient);
            await PerformTask(action);
            return (string)action.ResponseValue;

        }
    }
}
