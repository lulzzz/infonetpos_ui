using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class FuelPriceBusinessLogic : IFuelPriceBusinessLogic
    {
        private readonly IFuelPriceSerializeManager _fuelPriceSerializeManager;
        private readonly IReportsBussinessLogic _reportsBussinessLogic;

        public FuelPriceBusinessLogic(IFuelPriceSerializeManager fuelPriceSerializeManager,
            IReportsBussinessLogic reportsBussinessLogic)
        {
            _fuelPriceSerializeManager = fuelPriceSerializeManager;
            _reportsBussinessLogic = reportsBussinessLogic;
        }

        public async Task<PriceIncrementDecrement> LoadPriceIncrementsAndDecrements(bool taxExempt)
        {
            return await _fuelPriceSerializeManager.LoadPriceIncrementsAndDecrements(taxExempt);
        }

        public async Task<PriceToDisplay> LoadPricesToDisplay()
        {
            return await _fuelPriceSerializeManager.LoadPricesToDisplay();
        }

        public async Task<bool> SavePricesToDisplay(List<string> grades,
            List<string> tiers, List<string> levels)
        {
            return await _fuelPriceSerializeManager.SavePricesToDisplay(grades, tiers, levels);
        }

        public async Task<SetPriceDecrement> SetPriceDecrement(PriceDecrement price, bool taxExempt)
        {
            var result = await _fuelPriceSerializeManager.SetPriceDecrement(price, taxExempt);
            if (result?.Report != null)
            {
                await _reportsBussinessLogic.SaveReport(result.Report);
            }
            return result;
        }

        public async Task<SetPriceIncrement> SetPriceIncrement(PriceIncrement price, bool taxExempt)
        {
            var result = await _fuelPriceSerializeManager.SetPriceIncrement(price, taxExempt);
            if (result?.Report != null)
            {
                await _reportsBussinessLogic.SaveReport(result.Report);
            }
            return result;
        }
    }
}
