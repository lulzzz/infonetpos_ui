using Microsoft.PointOfService;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using System.Threading;

namespace PeripheralsComponent
{

    public enum TextWeight : int
    {
        Normal = 0,
        Bold = 1
    }

    public enum TextAlignment : int
    {
        Left = 0,
        Center = 1,
        Right = 2
    }

    public enum TextSize : int
    {
        Normal = 0,
        Large = 1
    }

    [Guid("A6CF563F-F779-4828-B50B-F7080CAB81F3"), ComVisible(true)]
    public interface IOPOSPrinter : IDisposable
    {
        bool Print(String input, int textSize, int textWeight, int textAlignment, string signatureUrl);

        bool PrintFieldValue(String nameText, String valueText, int textSize, int textWeight);

        bool Cut(int percent);

        bool IsEnabled();

        void StartPrinting();

        void StopPrinting();
    }

    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D13D"), ComVisible(true)]
    public sealed class OPOSPrinterServer : IOPOSPrinter
    {
        private readonly string _deviceName;

        //Timeout to claim, the printer device
        private const int CLAIM_TIMEOUT_MS = 1000;

        //Expected character width of the receipt.
        //This is used because text cannot be aligned both left and right in a single line.. 
        //could be a more flexible implementation but needs R&D and is device specific.
        private const int RECEIPT_CHAR_WIDTH = 40;

        #region OPOS print command constants
        private const string NewLine = "\x1B|1lF";

        private readonly String Bold = Encoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'b', (byte)'C' });

        private readonly String Underline = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'u', (byte)'C' });
        private readonly String Italic = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'i', (byte)'C' });
        private readonly String CenterAlign = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' });
        private readonly String RightAlign = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'r', (byte)'A' });
        private readonly String DoubleWideCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'C' });
        private readonly String DoubleHightCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'3', (byte)'C' });
        private readonly String DoubleWideAndHightCharacters = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'4', (byte)'C' });

        #endregion

        private PosPrinter _printer;

        const int CUT_PERCENT = 100;

        public OPOSPrinterServer(string deviceName)
        {
            _deviceName = deviceName;

            _printer = FindPrinter(_deviceName);
            if (_printer == null)
            {
                throw new Exception("No Receipt Printer Available!");
            }

            _printer.MapMode = MapMode.Metric;
            _printer.RecLetterQuality = true;
            _printer.AsyncMode = true;
        }

        public void StartPrinting()
        {
            char ESC = '\u001B';
            _printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);
            _printer.PrintNormal(PrinterStation.Receipt, ESC + "|N");
        }

        public void StopPrinting()
        {
            _printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
        }

        public bool Cut(int percent)
        {
            if (_printer == null)
            {
                _printer = FindPrinter(_deviceName);
            }

            if (_printer != null)
            {
                if (_printer.CapRecPaperCut == true)
                {
                    _printer.CutPaper(CUT_PERCENT);
                }

                return true;
            }

            return false;
        }

        public bool PrintFieldValue(String nameText, String valueText, int textSize, int textWeight)
        {
            TextSize size = TextSize.Normal;
            TextWeight weight = TextWeight.Normal;

            if (Enum.IsDefined(typeof(TextSize), textSize))
                size = (TextSize)textSize;
            if (Enum.IsDefined(typeof(TextWeight), textWeight))
                weight = (TextWeight)textWeight;

            if (_printer == null)
            {
                _printer = FindPrinter(_deviceName);
            }

            if (_printer != null)
            {
                string textField = nameText.Replace("ESC", ((char)27).ToString());
                string textValue = valueText.Replace("ESC", ((char)27).ToString());

                string mods = "";
                switch (size)
                {
                    case TextSize.Large:
                        mods += DoubleWideAndHightCharacters;
                        break;
                }

                switch (weight)
                {
                    case TextWeight.Bold:
                        mods += Bold;
                        break;
                }

                //add padding to fill expected receipt width
                while (textField.Length + textValue.Length < RECEIPT_CHAR_WIDTH)
                {
                    textValue = " " + textValue;
                }

                _printer.PrintNormal(PrinterStation.Receipt, mods + textField + textValue + NewLine);
                return true;
            }
            return false;
        }

        public bool Print(String input, int textSize, int textWeight, int textAlignment, string signatureUrl)
        {
            TextSize size = TextSize.Normal;
            TextWeight weight = TextWeight.Normal;
            TextAlignment align = TextAlignment.Left;

            if (Enum.IsDefined(typeof(TextSize), textSize))
                size = (TextSize)textSize;
            if (Enum.IsDefined(typeof(TextWeight), textWeight))
                weight = (TextWeight)textWeight;
            if (Enum.IsDefined(typeof(TextAlignment), textAlignment))
                align = (TextAlignment)textAlignment;

            if (_printer == null)
            {
                _printer = FindPrinter(_deviceName);
            }

            if (_printer != null)
            {
                string textToPrint = input;

                string text = textToPrint.Replace("ESC", ((char)27).ToString());

                string mods = "";
                switch (size)
                {
                    case TextSize.Large:
                        mods += DoubleWideAndHightCharacters;
                        break;
                }

                switch (align)
                {
                    case TextAlignment.Center:
                        mods += CenterAlign;
                        break;
                    case TextAlignment.Right:
                        mods += RightAlign;
                        break;
                }

                switch (weight)
                {
                    case TextWeight.Bold:
                        mods += Bold;
                        break;
                }

                if (text.Contains("©"))
                {
                    var index = text.IndexOf("©");

                    if (index == 0)
                    {
                        PrintCopyrightSymbol();

                        if (index < text.Length - 1)
                        {
                            DumpToPrinter(mods + text[index + 1] + NewLine, false);
                        }
                        else
                        {
                            DumpToPrinter(mods + NewLine, false);
                        }
                    }
                    else if (index == text.Length - 1)
                    {
                        DumpToPrinter(mods + text[index - 1], false);
                        PrintCopyrightSymbol();
                        DumpToPrinter(mods + NewLine, false);

                    }
                    else
                    {
                        DumpToPrinter(mods + text[index - 1], false);
                        PrintCopyrightSymbol();
                        DumpToPrinter(mods + text[index + 1] + NewLine, false);
                    }
                }
                else if (text.Contains("Signature"))
                {
                    var eSignature = !string.IsNullOrEmpty(signatureUrl);
                    if (eSignature)
                    {
                        text = text.Replace("_", "");
                    }
                    DumpToPrinter(mods + text + NewLine, false);

                    if (eSignature)
                    {
                        PrintSignature(signatureUrl);
                    }
                }
                else
                {
                    DumpToPrinter(mods + text + NewLine, false);
                }

                return true;
            }
            return false;
        }

        private PosPrinter FindPrinter(string objectName)
        {
            PosExplorer myPosExplorer = new PosExplorer();
            DeviceCollection myDevices = myPosExplorer.GetDevices(DeviceType.PosPrinter, DeviceCompatibilities.Opos);
            //find the printer by expected service object name
            foreach (DeviceInfo devInfo in myDevices)
            {
                if (devInfo.ServiceObjectName == objectName)
                {
                    try
                    {
                        var p = myPosExplorer.CreateInstance(devInfo) as PosPrinter;

                        //open
                        p.Open();

                        //claim the printer for use
                        p.Claim(CLAIM_TIMEOUT_MS);

                        //make sure it is enabled
                        p.DeviceEnabled = true;

                        return p;
                    }
                    catch (Exception)
                    {
                        // Eating exception for other printers may be connected
                    }
                }
            }
            return null;
        }

        public void Dispose()
        {
            if (_printer != null)
            {
                _printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
                _printer.AsyncMode = false;

                _printer.Release();
                _printer.Close();
                _printer = null;
            }
        }

        private void DumpToPrinter(string line, bool Cut_It)
        {
            var voucherNum = 0;

            if (line.IndexOf("<TabB>") != -1)
            {
                //tab and bold  used in printing the last receipt
                line = line.Trim();
                var i = (short)(line.IndexOf("</TabB>") + 1);
                if (line.Substring(i + 7 - 1) == "?*?*?*?*?*?*?")
                {
                    voucherNum++;
                }
            }
            else if (line.IndexOf("<Tab>") != -1)
            {
                //just tab, used in printing the last receipt
                line = line.Trim();
                var i = (short)(line.IndexOf("</Tab>") + 1);
            }
            _printer.PrintNormal(PrinterStation.Receipt, line);

        }

        private void PrintCopyrightSymbol()
        {
            _printer.PrintBitmap(PrinterStation.Receipt, @"C:\Program Files (x86)\infonet-pos\copyright.bmp",
               PosPrinter.PrinterBitmapAsIs,
               PosPrinter.PrinterBitmapLeft);
        }

        private void PrintSignature(string signatureUrl)
        {
            try
            {
                _printer.PrintBitmap(PrinterStation.Receipt, signatureUrl,
                    4200, PosPrinter.PrinterBitmapLeft);
            }
            catch (Exception ex)
            {
                _printer.Close();
            }
        }

        public bool IsEnabled()
        {
            return _printer != null && _printer.Claimed && _printer.DeviceEnabled;
        }
    }
}