using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.FuelPump
{
    public class PropaneModel : ViewModelBase
    {
        private int _id;
        private string _name;
        private int _positionId;

        public int PositionId
        {
            get { return _positionId; }
            set
            {
                _positionId = value;
                RaisePropertyChanged(nameof(PositionId));
            }
        }


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }


        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
    }
}
