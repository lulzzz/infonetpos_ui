using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{

    /// <summary>
    /// Business methods for carwash
    /// </summary>
    public class CarwashBusinessLogic : ICarwashBusinessLogic
    {
        private readonly ICarwashSerializeManager _carwashSerializeManager;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serializeManger"></param>
        public CarwashBusinessLogic(ICarwashSerializeManager serializeManger)
        {
            _carwashSerializeManager = serializeManger;
        }

        /// <summary>
        /// method to return the carwash server status
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetCarwasServerStatus()
        {
           return  await _carwashSerializeManager.GetCarwasServerStatus();
        }

        /// <summary>
        /// method to validate the carwash code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> ValidateCarwashCode(string code)
        {
            return await _carwashSerializeManager.ValidateCarwashCode(code);
        }
    }
}
