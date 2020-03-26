using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Utility;

namespace Infonet.CStoreCommander.UI.Model.Stock
{
    public class PriceTypeModel : ViewModelBase
    {
        private string _column1;
        private string _column2;
        private string _column3;

        public int Id { get; set; }
        public string Column3
        {
            get { return _column3; }
            set
            {
                _column3 = Helper.SelectAllDecimalValue(value, _column3);
                RaisePropertyChanged(nameof(Column3));
            }
        }
        public string Column2
        {
            get { return _column2; }
            set
            {
                _column2 = Helper.SelectAllDecimalValue(value, _column2);
                RaisePropertyChanged(nameof(Column2));
            }
        }
        public string Column1
        {
            get { return _column1; }
            set
            {
                _column1 = Helper.SelectAllDecimalValue(value, _column1);
                RaisePropertyChanged(nameof(Column1));
            }
        }

    }
}
