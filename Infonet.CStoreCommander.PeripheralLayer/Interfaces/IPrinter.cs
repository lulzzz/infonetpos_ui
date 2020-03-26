using Infonet.CStoreCommander.EntityLayer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.PeripheralLayer.Interfaces
{
    public interface IPrinter : IDisposable
    {
        Task PrintAsync(PaymentReceipt receipt, int numberOfCopies);

        Task Print(List<string> lines, int numberOfCopies, string signature);

        bool OpenCashDrawer();
    }
}
