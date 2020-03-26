using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Discount;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;


namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class FuelDiscountBusinessLogic : IFuelDiscountBusinessLogic
    {
        IFuelDiscountSerializeManager _fuelDiscountSerialManager;
        public FuelDiscountBusinessLogic(IFuelDiscountSerializeManager fuelDiscountSerialManager)
        {
            _fuelDiscountSerialManager = fuelDiscountSerialManager;
        }
        public async Task<IEnumerable<ClientGroup>> GetDiscountItemsAsync()
        {
            return await _fuelDiscountSerialManager.GetDiscountItemsAsync();
        }

        public async Task<string> GetFuelCodesAsync()
        {
            return await _fuelDiscountSerialManager.GetFuelCodesAsync();
        }
    }
}
