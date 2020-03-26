using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Utility
{
    /// <summary>
    /// Service for Displaying Loader
    /// </summary>
    public class LoadingService : ViewModelBase
    {
        private bool _isApplicationFetchingData;
        private int _numberOfApiCallsCounter { get; set; } = 0;

        public static LoadingService LoadingInstance { get; } = new LoadingService();

        public bool IsApplicationFetchingData
        {
            get { return _isApplicationFetchingData; }
            set
            {
                _isApplicationFetchingData = value;
                RaisePropertyChanged(nameof(IsApplicationFetchingData));
            }
        }

        /// <summary>
        /// Shows or hides the Loader
        /// </summary>
        /// <param name="show">True or False</param>
        public void ShowLoadingStatus(bool show)
        {
            _numberOfApiCallsCounter += show ? 1 : -1;

            IsApplicationFetchingData = _numberOfApiCallsCounter > 0;
        }
    }
}
