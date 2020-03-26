namespace Infonet.CStoreCommander.EntityLayer.Entities.Customer
{
    public class Customer
    {
        private string _code;
        private string _name;
        private string _loyaltyNumber;
        private string _phoneNumber;
        private string _points;

        public string Code
        {
            get { return _code; }
            set { _code = value ?? string.Empty; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value ?? string.Empty; }
        }

        public string LoyaltyNumber
        {
            get { return _loyaltyNumber; }
            set { _loyaltyNumber = value ?? string.Empty; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value ?? string.Empty; }
        }

        public string Points
        {
            get { return _points; }
            set { _points = value ?? string.Empty; }
        }
    }
}
