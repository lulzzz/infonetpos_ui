using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Model.Reports;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions
{
    public class SalesCountVM : VMBase
    {
        #region Properties
        private IReportsBussinessLogic _reportsBussinessLogic;
        private Dictionary<string, string> _departments;
        private Dictionary<int, string> _tills;
        private Dictionary<int, string> _shifts;
        private int _selectedDepartmentIndex;
        private int _selectedTillIndex;
        private int _selectedShiftIndex;

        public int SelectedShiftIndex
        {
            get { return _selectedShiftIndex; }
            set
            {
                _selectedShiftIndex = value;
                RaisePropertyChanged(nameof(SelectedShiftIndex));
            }
        }
        public int SelectedTillIndex
        {
            get { return _selectedTillIndex; }
            set
            {
                _selectedTillIndex = value;
                RaisePropertyChanged(nameof(SelectedTillIndex));
            }
        }
        public int SelectedDepartmentIndex
        {
            get { return _selectedDepartmentIndex; }
            set
            {
                _selectedDepartmentIndex = value;
                RaisePropertyChanged(nameof(SelectedDepartmentIndex));
            }
        }
        public Dictionary<int, string> Shifts
        {
            get { return _shifts; }
            set
            {
                _shifts = value;
                RaisePropertyChanged(nameof(Shifts));
            }
        }
        public Dictionary<int, string> Tills
        {
            get { return _tills; }
            set
            {
                _tills = value;
                RaisePropertyChanged(nameof(Tills));
            }
        }

        public Dictionary<string, string> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                RaisePropertyChanged(nameof(Departments));
            }
        }
        #endregion

        public RelayCommand<object> DepartmentSelectedCommand;
        public RelayCommand LoadAllDataCommand { get; set; }
        public RelayCommand RunReportCommand { get; set; }

        public SalesCountVM(IReportsBussinessLogic reportsBussinessLogic)
        {
            _reportsBussinessLogic = reportsBussinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            LoadAllDataCommand = new RelayCommand(LoadAllData);
            RunReportCommand = new RelayCommand(RunReport);
        }

        private void RunReport()
        {
            var saleCountModel = new SaleCountModel
            {
                departmentID = Departments.ElementAt(SelectedDepartmentIndex).Key,
                ShiftNumber = Shifts.ElementAt(SelectedShiftIndex).Key,
                TillNumber = Tills.ElementAt(SelectedTillIndex).Key
            };

            MessengerInstance.Send<SaleCountModel>(saleCountModel, "RunReport");
        }

        private void LoadAllData()
        {
            PerformAction(GetAllDepartment);
            PerformAction(GetAllTill);
            PerformAction(GetShifts);
        }


        private async Task GetAllDepartment()
        {
            var departments = await _reportsBussinessLogic.GetAllDepartment();
            var tempDepartments = new Dictionary<string, string>();

            foreach (var deparment in departments)
            {
                tempDepartments.Add(deparment.ID, deparment.Name);
            }

            Departments = tempDepartments;

            if (Departments.Count > 0)
            {
                SelectedDepartmentIndex = 0;
            }
        }

        private async Task GetAllTill()
        {
            var tills = await _reportsBussinessLogic.GetAllTill();
            var tempTills = new Dictionary<int, string>();

            foreach (var till in tills)
            {
                tempTills.Add(till.ID, till.Number);
            }

            Tills = tempTills;

            if (Tills.Count > 0)
            {
                SelectedTillIndex = 0;
            }
        }

        private async Task GetShifts()
        {
            var shifts = await _reportsBussinessLogic.GetAllShift();
            var tempShifts = new Dictionary<int, string>();

            foreach (var till in shifts)
            {
                tempShifts.Add(till.ID, till.Number);
            }

            Shifts = tempShifts;

            if (Shifts.Count > 0)
            {
                SelectedShiftIndex = 0;
            }
        }

        internal void ResetVM()
        {
            SelectedTillIndex = -1;
            SelectedShiftIndex = -1;
            SelectedDepartmentIndex = -1;
        }
    }
}
