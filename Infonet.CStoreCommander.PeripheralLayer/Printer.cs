using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.EntityLayer.Model;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using PeripheralsComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.PeripheralLayer
{
    public class Printer : IPrinter
    {
        private IPrinterServer _printer;
        private PrintUtility _printUtility;
        public Printer(string deviceName)
        {
            try
            {
                _printer = new PrinterServer(deviceName);
                _printUtility = new PrintUtility(deviceName);
            }
            catch (Exception ex)
            {
                throw new PrinterLayerException(PrinterError.NotFound, ex);
            }
        }

        public void Dispose()
        {
            if (_printer != null)
            {
                _printer.Dispose();
            }
        }

        public bool OpenCashDrawer()
        {
            if (_printer != null)
            {
                try
                {
                    return _printer.OpenCashDrawer();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public async Task Print(List<string> lines, int numberOfCopies = 1, string signature = null)
        {
            var receipt = new PaymentReceipt();
            receipt.SignatureUrl = signature ?? string.Empty;
            foreach (var line in lines)
            {
                receipt.AddLine(line);
            }

            await PrintAsync(receipt, numberOfCopies);
        }

        public Task PrintAsync(PaymentReceipt receipt, int numberOfCopies = 1)
        {
            bool success = true;

            try
            {
                _printUtility.Print(string.Join("\n", receipt.Lines.Select(c => c.Text)), (short)numberOfCopies, string.Empty, receipt.SignatureUrl);
            }
            catch (Exception ex)
            {
                throw new PrinterLayerException(PrinterError.Unexpected, ex);
            }

            if (!success)
            {
                throw new PrinterLayerException(PrinterError.Unexpected, null);
            }

            return Task.FromResult<bool>(success); // to avoid warning

        }
    }
}
