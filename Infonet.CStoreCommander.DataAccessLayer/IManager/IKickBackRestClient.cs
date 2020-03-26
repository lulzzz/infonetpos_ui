using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IKickBackRestClient
    {
        Task<HttpResponseMessage> VerifyKickBack(string pointCardNumber, string phoneNumber);

        Task<HttpResponseMessage> CheckKickBackResponse(bool response);

        Task<HttpResponseMessage> CheckKickBackbalance(string cardNumber);

        Task<HttpResponseMessage> ValidateGasKing();
    }
}
