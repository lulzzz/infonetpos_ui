using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Infonet.CStoreCommander.UI.Utility
{
    public class MSRService
    {
        private bool _isShift;
        private string _msrData = string.Empty;
        private bool _readMSRComplete;
        private bool _isSemiColonEntered;
        private DispatcherTimer _timer;
        private Nullable<CancellationToken> _cancelToken;

        private DateTime _lastReadTime;

        public delegate void ReadCompleteHandler(string data);

        public event ReadCompleteHandler ReadCompleted;

        public void Start(Nullable<CancellationToken> cancelToken)
        {
            _cancelToken = cancelToken;
            _isSemiColonEntered = false;
            _readMSRComplete = false;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick -= TimerTick;
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        public void ReadKey(VirtualKey key, CorePhysicalKeyStatus keyStatus)
        {
            _lastReadTime = DateTime.Now;

            if (key.ToString() == "186")
            {
                _isSemiColonEntered = true;
            }

            if (key == VirtualKey.Shift)
            {
                _isShift = true;
            }
            else
            {
                char? character = Helper.ToChar(key, _isShift);
                if (character.HasValue)
                {
                    _msrData += character;
                }
                _isShift = false;
            }

            if (key.ToString() == "191" && _isSemiColonEntered)
            {
                _readMSRComplete = true;
                _isSemiColonEntered = false;
            }
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Tick -= TimerTick;
            }
            _timer?.Stop();
        }

        private void TimerTick(object sender, object e)
        {
            try
            {
                //check for user cancellation
                if (_cancelToken.HasValue && _cancelToken.Value.IsCancellationRequested)
                {
                    //process canceled
                    if (_timer != null)
                        _timer.Stop();
                }
                else if (_readMSRComplete)
                {
                    //collect MSR data if completion is flagged
                    ReadCompleted(_msrData);
                    _msrData = string.Empty;
                    _readMSRComplete = false;
                }

                // If no key is read from 2 seconds then no card is swiped
                if ((DateTime.Now - _lastReadTime).TotalMilliseconds > 2000)
                {
                    _msrData = string.Empty;
                }
            }
            catch (Exception)
            {
                //unexpected
                _readMSRComplete = true;
            }
        }
    }
}
