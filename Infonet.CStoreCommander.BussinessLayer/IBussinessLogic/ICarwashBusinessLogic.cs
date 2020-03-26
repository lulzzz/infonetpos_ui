using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface ICarwashBusinessLogic
    {
        Task<bool> ValidateCarwashCode(string code);

        Task<bool> GetCarwasServerStatus();
    }
}
