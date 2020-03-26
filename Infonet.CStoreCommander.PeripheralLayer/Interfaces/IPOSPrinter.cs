using Infonet.CStoreCommander.EntityLayer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.PeripheralLayer.Interfaces
{
    public interface IPOSPrinter : IDisposable
    {
        /// <summary>
        /// Print a receipt
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        Task PrintAsync(PaymentReceipt receipt, int numberOfCopies);

        /// <summary>
        /// Print the array of strings
        /// </summary>
        /// <param name="lines">Array of lines</param>
        /// <returns></returns>
        Task PrintAsync(List<string> lines, int numberOfCopies, string signaturePath);

        bool IsAvailable();
    }
}
