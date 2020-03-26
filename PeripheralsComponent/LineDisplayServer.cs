using System;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace PeripheralsComponent
{
    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D14A"), ComVisible(true)]
    public interface ILineDisplayServer : IDisposable
    {
        bool DisplayText(string text, bool clear);

        bool Clear();
    }

    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D13F"), ComVisible(true)]
    public sealed class LineDisplayServer : ILineDisplayServer
    {
        private readonly SerialPort _serialPort;

        public LineDisplayServer(string portNumber)
        {
            try
            {
                _serialPort = new SerialPort(string.Format("COM{0}", portNumber));

                _serialPort.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("No Customer display found!");
            }
        }

        public void Dispose()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        public bool DisplayText(string text, bool clear)
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    return false;
                }

                if (clear)
                {
                    _serialPort.Write("\f");
                }
                _serialPort.Write(text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Clear()
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    return false;
                }
                _serialPort.Write("\f");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
