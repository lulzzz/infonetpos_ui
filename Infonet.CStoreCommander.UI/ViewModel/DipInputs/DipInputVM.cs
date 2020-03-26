using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Model.DipInputs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.DipInput;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Messages;

namespace Infonet.CStoreCommander.UI.ViewModel.DipInputs
{
    public class DipInputVM : VMBase
    {
        private readonly IReportsBussinessLogic _reportsBusinessLogic;
        private ObservableCollection<DipInputModel> _dipInputList;
        private DipInputModel _selectedDipModel;

        public DipInputModel SelectedDipModel
        {
            get { return _selectedDipModel; }
            set
            {
                _selectedDipModel = value;
                RaisePropertyChanged(nameof(SelectedDipModel));
            }
        }


        public ObservableCollection<DipInputModel> DipInputList
        {
            get { return _dipInputList; }
            set
            {
                _dipInputList = value;
                RaisePropertyChanged(nameof(DipInputList));
            }
        }

        public RelayCommand GetDipInputCommand { get; set; }
        public RelayCommand SaveDipInputCommand { get; set; }
        public RelayCommand PrintDipInputCommand { get; set; }
        public RelayCommand DipValueUpdatedCommand { get; set; }
        public RelayCommand BackDipInputCommand { get; set; }
        public RelayCommand EnterPressedOnValueCommand { get; set; }

        public IDipInputBusinessLogic _dipInputBusinessLogic;


        public DipInputVM(IDipInputBusinessLogic dipInputBusinessLogic,
            IReportsBussinessLogic reportsBussinessLogic)
        {
            _dipInputBusinessLogic = dipInputBusinessLogic;
            _reportsBusinessLogic = reportsBussinessLogic;
            InitializeCommands();
        }


        private void InitializeCommands()
        {
            GetDipInputCommand = new RelayCommand(() => PerformAction(GetDipInput));
            SaveDipInputCommand = new RelayCommand(() => PerformAction(SaveDipInput));
            PrintDipInputCommand = new RelayCommand(() => PerformAction(PrintDipInput));
            BackDipInputCommand = new RelayCommand(() =>
            {
                NavigateService.Instance.NavigateToHome();
                DipInputTimer.Start();
                MessengerInstance.Send<EnableFuelOptionButtonMessage>(new EnableFuelOptionButtonMessage
                {
                    EnableFuelOptionButton = true
                });
            });

            EnterPressedOnValueCommand = new RelayCommand(() =>
            {
                SelectedDipModel = null;
            });
        }


        private async Task SaveDipInput()
        {
            var dipInputs = new List<DipInput>(
                (from d in DipInputList
                 select new DipInput
                 {
                     DipValue = d.DipValue,
                     Grade = d.Grade,
                     GradeId = d.GradeId,
                     TankId = d.TankId
                 }).ToList());

            var response = await _dipInputBusinessLogic.SaveDipInput(dipInputs);

            DipInputTimer.Interval = new TimeSpan(0, 0, (int)(CacheBusinessLogic
                .DipInputTime.AddDays(1) - DateTime.Now).TotalSeconds);
            DipInputTimer.Start();
        }

        private async Task PrintDipInput()
        {
            var response = await _dipInputBusinessLogic.GetDipInputReport();

            await PerformPrint(response);
        }

        private async Task GetDipInput()
        {
            var response = await _dipInputBusinessLogic.GetDipInput();

            var tempDipInputList = new ObservableCollection<DipInputModel>(
                (from r in response
                 select new DipInputModel
                 {
                     DipValue = r.DipValue,
                     Grade = r.Grade,
                     GradeId = r.GradeId,
                     TankId = r.TankId
                 }).ToList()
            );

            DipInputList = new ObservableCollection<DipInputModel>(tempDipInputList);
        }

        internal void ReInitialize()
        {
            PerformAction(GetDipInput);
        }
    }
}
