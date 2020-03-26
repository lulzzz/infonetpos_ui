using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class TenderInformationContract
    {
        public string cardType { get; set; }
        public string tenderCode { get; set; }
        public string tenderClass { get; set; }
        public string amount { get; set; }
        public string cardNumber { get; set; }
        public bool askPin { get; set; }
        public string pin { get; set; }
        public string caption { get; set; }
        public string poMessage { get; set; }
        public List<string> promptMessages { get; set; }
        public string profileId { get; set; }
        public bool isArCustomer { get; set; }
        public bool isGasKing { get; set; }
        public string kickBackValue { get; set; }
        public double kickbackPoints { get; set; }
        public bool isKickBackLinked { get; set; }
        public bool isFleet { get; set; }

        public bool isInvalidLoyaltyCard { get; set; }
        public List<ProfileValidationsContract> profileValidations { get; set; }
    }

    public class ProfileValidationsContract
    {
        public string message { get; set; }
        public int messageType { get; set; }
    }
}