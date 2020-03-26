using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System;
using System.Globalization;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Infonet.CStoreCommander.UI.ViewModel.Common
{
    public class MasterPageVM : VMBase
    {
        private readonly ICacheBusinessLogic _cacheBussinessLogic;
        private string _date;
        private string _time;
        private string _versionNumber;

        public string VersionNumber
        {
            get { return _versionNumber; }
            set
            {
                _versionNumber = value;
                RaisePropertyChanged(nameof(VersionNumber));
            }
        }


        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(nameof(Date));
            }
        }

        public ImageSource Background { get; set; }
        public ICommand SizeChangedCommand { get; set; }

        public MasterPageVM(ICacheBusinessLogic cacheBussinessLogic)
        {
            _cacheBussinessLogic = cacheBussinessLogic;
            StartDispatcher();
            SizeChangedCommand = new RelayCommand(SizeChanged);

            Background = new BitmapImage
            {
                UriSource = new Uri(App.Current.Resources["ApplicationBackground"].ToString(), UriKind.Absolute)
            };

            GetAppVersionNumber();
        }

        private void GetAppVersionNumber()
        {
            var version = Package.Current.Id.Version;
            VersionNumber = string.Format("C-STORE COMMANDER {0}.{1}.{2}   BY INFONET TECHNOLOGY",
               version.Major, version.Minor, version.Build);
            if(_cacheBussinessLogic.VERSION!=null)
            switch (_cacheBussinessLogic.VERSION)
            {
                case "MVCC":
                    VersionNumber = VersionNumber.Replace("BY INFONET TECHNOLOGY", "By MVCC powered by Infonet Technology");
                    break;
            }
        }

        private void SizeChanged()
        {
            Width = Window.Current.Bounds.Width;
            Height = Window.Current.Bounds.Height;
        }

        public void StartDispatcher()
        {
            if (Timer == null)
            {
                Timer = new DispatcherTimer();
                Timer.Interval = new TimeSpan(0, 0, 1);
                Timer.Tick -= AppTimerTick;
                Timer.Tick += AppTimerTick;
                Timer.Start();
            }
        }

        private void AppTimerTick(object sender, object e)
        {
            var dateTime = DateTime.Now;
            Date = dateTime.Date.ToString("MMM dd, yyyy", CultureInfo.CurrentCulture);
            Time = dateTime.ToString("hh:mm tt ", CultureInfo.CurrentCulture);
        }
    }
}
