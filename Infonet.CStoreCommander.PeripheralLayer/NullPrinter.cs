using Infonet.CStoreCommander.EntityLayer.Model;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.PeripheralLayer
{

    /// <summary>
    /// Useful for unit test purposes - printer that does nothing
    /// </summary>
    public class NullPrinter : IPOSPrinter
    {

        public async Task InitializeAsync()
        {
            await Task.Yield();
        }

        public async Task PrintAsync(PaymentReceipt receipt, int numberOfCopies = 1)
        {
            await Task.Yield();
        }

        public void Dispose()
        {

        }

        public Task PrintAsync(List<string> lines, int numberOfCopies = 1, string signature = null)
        {
            throw new NotImplementedException();
        }

        public bool IsAvailable()
        {
            throw new NotImplementedException();
        }
    }
}
