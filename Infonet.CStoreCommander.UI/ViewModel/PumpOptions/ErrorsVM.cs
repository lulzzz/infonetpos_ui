using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.PumpOptions
{
    public class ErrorsVM : VMBase
    {
        private IFuelPumpBusinessLogic _fuelBusinessLogic;
        private string _errorString;
        private bool _isClearButtonEnable;

        public bool IsClearButtonEnable
        {
            get { return _isClearButtonEnable; }
            set
            {
                if (_isClearButtonEnable != value)
                {
                    _isClearButtonEnable = value;
                    RaisePropertyChanged(nameof(IsClearButtonEnable));
                }
            }
        }


        public string ErrorString
        {
            get { return _errorString; }
            set
            {
                if (_errorString != value)
                {
                    _errorString = value;
                    IsClearButtonEnable = !string.IsNullOrEmpty(_errorString);
                    RaisePropertyChanged(nameof(ErrorString));
                }
            }
        }


        public RelayCommand ClearErrorCommand { get; set; }
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand BackPageCommand { get; set; }

        public ErrorsVM(IFuelPumpBusinessLogic fuelBusinessLogic)
        {
            _fuelBusinessLogic = fuelBusinessLogic;
            InitializeCommands();
        }

        internal void ResetVM()
        {
            ErrorString = string.Empty;
            PerformAction(GetError);
        }

        private async Task GetError()
        {
            ErrorString = await _fuelBusinessLogic.GetError();
        }

        private void InitializeCommands()
        {
            ClearErrorCommand = new RelayCommand(ClearError);
            PrintCommand = new RelayCommand(PrintReport);
            BackPageCommand = new RelayCommand(() =>
            {
                if (CacheBusinessLogic.IsFuelOnlySystem)
                {
                    MessengerInstance.Send<FuelOnlySystemMessage>(new FuelOnlySystemMessage());
                }
                NavigateService.Instance.NavigateToHome();
            });
        }

        private async void PrintReport()
        {
            await PerformPrint(ErrorString?.Split('\n')?.ToList());
        }

        private void ClearError()
        {
            ShowConfirmationMessage(ApplicationConstants.ClearErrorFile,
            async () =>
            {
                var response = await _fuelBusinessLogic.ClearError();
                if (response)
                {
                    ErrorString = string.Empty;
                }
                MessengerInstance.Send<bool>(true, "ErrorsCleared");
            });
        }
    }
}
