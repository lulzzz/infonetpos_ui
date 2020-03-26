using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Management;
using System.Runtime.InteropServices;

namespace PeripheralsComponent
{
    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D15B"), ComVisible(true)]
    public interface IPrinterServer : IDisposable
    {
        bool Print(string data);

        bool OpenCashDrawer();
    }

    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D15C"), ComVisible(true)]
    public sealed class PrinterServer : IPrinterServer
    {
        private PrintDocument _printDocument;
        private string _data;
        private string _deviceName;

        public PrinterServer(string deviceName)
        {
            _deviceName = deviceName;
            _printDocument = new PrintDocument();

            _printDocument.PrinterSettings.PrinterName = _deviceName;
            _printDocument.PrintPage += OnPrint;

            //pd.Print();

            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", _deviceName);

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection coll = searcher.Get())
            {
                try
                {
                    foreach (ManagementObject printer in coll)
                    {
                        foreach (PropertyData property in printer.Properties)
                        {
                            Console.WriteLine(string.Format("{0}: {1}", property.Name, property.Value));
                        }
                    }
                }
                catch (ManagementException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void OnPrint(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Consolas", 9.5f, FontStyle.Regular, GraphicsUnit.Point);
            e.Graphics.DrawString(_data, font, Brushes.Black, new Point(0, 0));
        }

        public void Dispose()
        {
            _printDocument.Dispose();
        }

        public bool Print(string data)
        {
            try
            {
                _data = data;
                _printDocument.Print();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                _printDocument.Dispose();
            }
            return true;
        }

        public bool OpenCashDrawer()
        {
            return RawPrinterHelper.SendStringToPrinter(_deviceName,
                System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 48, 55, 121 }));
        }
    }
}
