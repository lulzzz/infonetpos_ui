using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.EntityLayer.Model;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using PeripheralsComponent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.PeripheralLayer
{
    public class OPOSPrinter : IPOSPrinter
    {
        private readonly string _deviceName;
        private IOPOSPrinter _printer;

        public OPOSPrinter(string deviceName)
        {
            _deviceName = deviceName;
            InitializeAsync();
        }

        private Task InitializeAsync()
        {
            try
            {
                //if this fails, it is likely that the Proxy stub COM component has not been registered using regsvr32
                //or, the proxy stub and winmd file is not in the expected directory, or permissions on that directory hav not been setup
                _printer = new OPOSPrinterServer(_deviceName);
            }
            catch (Exception ex)
            {
                throw new PrinterLayerException(PrinterError.NotFound, ex);
            }

            return Task.FromResult<bool>(true); //to avoid warning
        }

        public Task PrintAsync(PaymentReceipt receipt, int numberOfCopies = 1)
        {
            if (!_printer.IsEnabled())
            {
                InitializeAsync();
            }

            bool success = true;

            try
            {
                _printer.StartPrinting();
                for (int i = 0; i < numberOfCopies; i++)
                {
                    //send receipt to the printer, line by line
                    foreach (var line in receipt.Lines)
                    {
                        if (string.IsNullOrEmpty(line.TextRight))
                        {
                            if (!_printer.Print(line.Text, (int)line.Size, (int)line.Weight, (int)line.Alignment, receipt.SignatureUrl))
                                success = false;
                        }
                        else
                        {
                            //special case field and value alignment,
                            if (!_printer.PrintFieldValue(line.Text, line.TextRight, (int)line.Size, (int)line.Weight))
                                success = false;
                        }
                    }

                    //cut the receipt - can be a partial cut
                    if (receipt.CutPercentage > 0)
                    {
                        if (!_printer.Cut(receipt.CutPercentage))
                            success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Eating exception so that message does not needs to be shown
                //throw new PrinterLayerException(PrinterError.Unexpected, ex);
            }
            finally
            {
                _printer.StopPrinting();
            }

            return Task.FromResult<bool>(success); // to avoid warning
        }

        public void Dispose()
        {
            //make sure printer is cleaned up
            if (_printer != null)
            {
                _printer.Dispose();
                _printer = null;
            }
        }

        public async Task PrintAsync(List<string> lines, int numberOfCopies = 1, string signaturePath = null)
        {
            var receipt = new PaymentReceipt();
            receipt.SignatureUrl = signaturePath ?? string.Empty;
            foreach (var line in lines)
            {
                receipt.AddLine(line);
            }

            await PrintAsync(receipt, numberOfCopies);
        }

        public bool IsAvailable()
        {
            return _printer != null && _printer.IsEnabled();
        }
    }
}
