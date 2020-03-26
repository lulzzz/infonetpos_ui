using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface ICarwashSerializeManager
    {

        Task<bool> ValidateCarwashCode(string code);

        Task<bool> GetCarwasServerStatus();
    }
}
