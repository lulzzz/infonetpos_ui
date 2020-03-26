using Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IFuelPriceBusinessLogic
    {
        Task<PriceToDisplay> LoadPricesToDisplay();

        Task<bool> SavePricesToDisplay(List<string> grades, List<string> tiers, List<string> levels);

        Task<PriceIncrementDecrement> LoadPriceIncrementsAndDecrements(bool taxExempt);

        Task<SetPriceIncrement> SetPriceIncrement(PriceIncrement price, bool taxExempt);

        Task<SetPriceDecrement> SetPriceDecrement(PriceDecrement price, bool taxExempt);
    }
}
