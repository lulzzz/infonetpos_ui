using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Sale
{
    public class GiftCardModel : ViewModelBase
    {
        private string _cardNumber;
        private int _giftNumber;
        private string _price;
        private int _quantity;

        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                RaisePropertyChanged(nameof(CardNumber));
            }
        }

        public int GiftNumber
        {
            get { return _giftNumber; }
            set
            {
                _giftNumber = value;
                RaisePropertyChanged(nameof(GiftNumber));
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }
    }
}
