using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPrice;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class FuelPriceSerializeManager : SerializeManager, IFuelPriceSerializeManager
    {
        private readonly IFuelPriceRestClient _fuelPriceRestClient;
        private readonly ICacheManager _cacheManager;

        public FuelPriceSerializeManager(IFuelPriceRestClient fuelPriceRestClient,
            ICacheManager cacheManager)
        {
            _fuelPriceRestClient = fuelPriceRestClient;
            _cacheManager = cacheManager;
        }

        public async Task<PriceToDisplay> LoadPricesToDisplay()
        {
            var action = new LoadPricesToDisplaySerializeAction(_fuelPriceRestClient);

            await PerformTask(action);

            return (PriceToDisplay)action.ResponseValue;
        }

        public async Task<bool> SavePricesToDisplay(List<string> grades, List<string> tiers, List<string> levels)
        {
            var action = new SavePricesToDisplaySerializeAction(_fuelPriceRestClient,
                grades, tiers, levels);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<PriceIncrementDecrement> LoadPriceIncrementsAndDecrements(bool taxExempt)
        {
            var action = new LoadPriceIncrementsAndDecrementsSerializeAction(_fuelPriceRestClient, taxExempt);

            await PerformTask(action);

            return (PriceIncrementDecrement)action.ResponseValue;
        }

        public async Task<SetPriceDecrement> SetPriceDecrement(PriceDecrement price, bool taxExempt)
        {
            var action = new SetPriceDecrementSerializeAction(_fuelPriceRestClient, price, taxExempt);

            await PerformTask(action);

            return (SetPriceDecrement)action.ResponseValue;
        }

        public async Task<SetPriceIncrement> SetPriceIncrement(PriceIncrement price, bool taxExempt)
        {
            var action = new SetPriceIncrementSerializeAction(_fuelPriceRestClient, price, taxExempt);

            await PerformTask(action);

            return (SetPriceIncrement)action.ResponseValue;
        }
    }
}
