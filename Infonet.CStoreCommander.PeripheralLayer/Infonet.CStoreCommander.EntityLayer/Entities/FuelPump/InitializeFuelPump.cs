using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.FuelPump
{
    public class InitializeFuelPump
    {
            public bool IsPrepayEnabled { get; set; }
            public bool IsFinishEnabled { get; set; }
            public bool IsManualEnabled { get; set; }
            public bool IsCurrentEnabled { get; set; }
            public bool IsFuelPriceEnabled { get; set; }
            public bool IsTierLevelEnabled { get; set; }
            public bool IsPropaneEnabled { get; set; }
            public bool IsStopButtonEnabled { get; set; }
            public bool IsResumeButtonEnabled { get; set; }
            public bool IsErrorEnabled { get; set; }
            public List<BigPumps> BigPumps { get; set; }
            public List<PumpStatus> Pumps { get; set; }       
    }

    public class BigPumps
    {
        public string PumpId { get; set; }
        public string IsPumpVisible { get; set; }
        public string PumpLabel { get; set; }
        public string PumpMessage { get; set; }
        public string Amount { get; set; }
    }

    public class PumpStatus
    {
        public int PumpId { get; set; }
        public string Status { get; set; }
        public string PumpButtonCaption { get; set; }
        public string BasketButtonCaption { get; set; }
        public int BasketButtonVisible { get; set; }
        public string BasketLabelCaption { get; set; }
        public bool PayPumporPrepay { get; set; }
        public string PrepayText { get; set; }
        public bool EnableBasketButton { get; set; }
        public bool EnableStackBasketButton { get; set; }
        public bool CanCashierAuthorize { get; set; }
    }
}
