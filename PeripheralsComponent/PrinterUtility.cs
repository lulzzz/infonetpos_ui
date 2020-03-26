using Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PeripheralsComponent
{
    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D17E"), ComVisible(true)]
    public sealed class PrintUtility
    {
        public PrintUtility(string deviceName)
        {
            _deviceName = deviceName;

            try
            {
                if (_printer == null || _printer.DeviceName != _deviceName)
                {
                    _printer = FindPrinter(_deviceName);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string _deviceName;
        private PrinterCollection Printers = new PrinterCollection();
        private Printer _printer;

        [DllImport("LPTtest.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern byte TestPrinterStatus();

        [DllImport("user32", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(int hWnd, int wMsg, int wParam, ref object lParam);
        [DllImport("kernel32", EntryPoint = "GetWindowsDirectoryA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int GetWindowsDirectory(StringBuilder lpBuffer, int nSize);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringA", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetPrivateProfileString(string lpApplicationName, object lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);


        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int WritePrivateProfileString(string lpApplicationName, object lpKeyName, object lpString, string lpFileName);
        //Jan21, 2010- usb printer + cash drawer
        [DllImport("winspool.drv", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int ClosePrinter(int hPrinter);

        [DllImport("winspool.drv", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int EndDocPrinter(int hPrinter);

        [DllImport("winspool.drv", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int EndPagePrinter(int hPrinter);

        [DllImport("winspool.drv", EntryPoint = "OpenPrinterA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int OpenPrinter(string pPrinterName, ref int phPrinter, int pDefault);


        [DllImport("winspool.drv", EntryPoint = "StartDocPrinterA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int StartDocPrinter(int hPrinter, int Level, ref DOCINFO pDocInfo);

        [DllImport("winspool.drv", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int StartPagePrinter(int hPrinter);


        [DllImport("winspool.drv", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int WritePrinter(int hPrinter, ref object pBuf, int cdBuf, ref int pcWritten);

        private struct DOCINFO
        {
            public string pDocName;
            public string pOutputFile;
            public string pDatatype;
        }

        const int HWND_BROADCAST = 0xFFFF;
        const int WM_WININICHANGE = 0x1A;

        object[] msgvalue = new object[3];

        public void Print(string data, short copies, string mask, string signatureFile)
        {
            short n = 0;
            short i = 0;
            short j = 0;
            string defaultFontName = "";
            short defaultFontSize = 0;
            Array.Clear(msgvalue, 0, msgvalue.Length);
            try
            {
                if (_printer == null || _printer.DeviceName != _deviceName)
                {
                    _printer = FindPrinter(_deviceName);
                }
                if (_printer == null)
                {
                    msgvalue[1] = _deviceName;
                    msgvalue[2] = _deviceName;
                    return;
                }
                string[] maskArray = null;
                for (n = 1; n <= copies; n++)
                {
                    if (_deviceName.Trim().Substring(0, 14) == "EPSON TM-T88II") // only Epson-II ( even if there is no 15 character it will return true and allothers  EPSON-III, IIV etc,it will return false
                        //if (_deviceName.Trim().Substring(0, 5) == "EPSON") // only Epson-II ( even if there is no 15 character it will return true and allothers  EPSON-III, IIV etc,it will return false
                    {
                        for (i = 0; i <= _printer.FontCount - 1; i++) // Determine number of fonts.
                        {
                            ///'if in this printer, we have "15 CPI" font, set it as default
                            if (_printer.get_Fonts(i) == "15 cpi")
                            {
                                _printer.FontName = _printer.get_Fonts(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (i = 0; i <= _printer.FontCount - 1; i++)
                        {
                            if (_printer.get_Fonts(i) == "Letter Gothic")
                            {
                                _printer.Font = new Font("Letter Gothic", 10f, FontStyle.Bold, GraphicsUnit.Point);
                                break;
                            }
                        }
                    }
                    defaultFontName = System.Convert.ToString(_printer.FontName);
                    defaultFontSize = System.Convert.ToInt16(_printer.FontSize);
                    //defaultFontName = System.Convert.ToString("Courier New");
                    //defaultFontSize = System.Convert.ToInt16(10);
                    if (mask.Length > 0)
                    {
                        maskArray = mask.Split(',');
                    }

                    foreach (var line in data.Split('\n'))
                    {
                        var text = line.Clone().ToString().Replace('\r', ' ');
                        if ((text.ToUpper()).IndexOf((Char.ConvertFromUtf32(169)).ToString()) + 1 > 0)
                        {
                            if (_deviceName.Trim().Substring(0, 14) == "EPSON TM-T88II")
                            //if (_deviceName.Trim().Substring(0, 5) == "EPSON")
                            {
                                _printer.NewPage();
                            }
                            else
                            {
                                _printer.EndDoc();
                            }
                        }
                        else
                        {
                            var eSignature = text.Contains("Signature");
                            if (eSignature)
                            {
                                text = text.Replace("_", "");
                            }
                            _printer.Print(text);
                            _printer.CurrentY -= 52;

                            if (eSignature)
                            {
                                try
                                {
                                    signatureFile = signatureFile?.Replace("Sign.", "SignL.");
                                    using (Image image = Image.FromFile(signatureFile))
                                    {
                                        _printer.PaintPicture(image, 0, _printer.CurrentY + 100, 4000, 1400);
                                        _printer.CurrentY += 1430;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                    }
                    _printer.Print("\n");
                    _printer.EndDoc();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private Printer FindPrinter(string printerName)
        {
            Printer returnValue = null;
            Printer Pr = null;

            returnValue = null;
            foreach (Printer tempLoopVar_Pr in Printers)
            {
                Pr = tempLoopVar_Pr;
                if (Pr.DeviceName == printerName)
                {
                    returnValue = Pr;
                    break;
                }
            }
            return returnValue;
        }
    }
}
