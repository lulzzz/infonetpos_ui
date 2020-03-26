using Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IFuelPriceSerializeManager
    {
        Task<PriceToDisplay> LoadPricesToDisplay();

        Task<bool> SavePricesToDisplay(List<string> grades, List<string> tiers, List<string> levels);

        Task<PriceIncrementDecrement> LoadPriceIncrementsAndDecrements(bool taxExempt);

        Task<SetPriceDecrement> SetPriceDecrement(PriceDecrement price, bool taxExempt);

        Task<SetPriceIncrement> SetPriceIncrement(PriceIncrement price, bool taxExempt);
    }
}
