using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Payout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IPayoutSerializeManager
    {
        Task<VendorPayout> GetVendorPayout();

        Task<CommonPaymentComplete> CompletePayout(List<Tax> taxes, 
            string vendorCode, string amount,
            string reasonCode);
    }
}
