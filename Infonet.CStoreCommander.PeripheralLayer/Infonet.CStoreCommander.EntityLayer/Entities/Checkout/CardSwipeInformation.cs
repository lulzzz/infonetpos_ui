using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class CardSwipeInformation
    {
        public CardSwipeInformation()
        {
            CardType = CardType.None;
        }
        public bool IsArCustomer { get; set; }

        public CardType CardType { get; set; }

        public string TenderCode { get; set; }

        public string TenderClass { get; set; }

        public string Amount { get; set; }

        public string CardNumber { get; set; }

        public bool AskPin { get; set; }
        public bool IsGasKing { get; set; }
        public string KickBackValue { get; set; }
        public double KickbackPoints { get; set; }
        public bool IsKickBackLinked { get; set; }
        public bool IsFleet { get; set; }
        public string Pin { get; set; }

        public string Caption { get; set; }
        public List<string> PromptMessages { get; set; }
        public string ProfileId { get; set; }

        public string PoMessage { get; set; }
        public List<ProfileValidations> ProfileValidations { get; set; }
        public string PoNumber { get; set; }

        public bool IsInvalidLoyaltyCard { get; set; }
    }


    public class ProfileValidations
    {
        public string Message { get; set; }
        public int MessageType { get; set; }
    }
}