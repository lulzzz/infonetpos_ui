using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class InitalizeFuelPumpContract
    {
        public bool isPrepayEnabled { get; set; }
        public bool isFinishEnabled { get; set; }
        public bool isManualEnabled { get; set; }
        public bool isCurrentEnabled { get; set; }
        public bool isFuelPriceEnabled { get; set; }
        public bool isTierLevelEnabled { get; set; }
        public bool isPropaneEnabled { get; set; }
        public bool isStopButtonEnabled { get; set; }
        public bool isResumeButtonEnabled { get; set; }
        public bool isErrorEnabled { get; set; }
        public List<BigPumpsContract> bigPumps { get; set; }
        public List<PumpStatusContract> pumps { get; set; }

    }

    public class BigPumpsContract
    {
        public string pumpId { get; set; }
        public string isPumpVisible { get; set; }
        public string pumpLabel { get; set; }
        public string pumpMessage { get; set; }
        public string amount { get; set; }
    }

    public class PumpStatusContract
    {
        public int pumpId { get; set; }
        public string status { get; set; }
        public string pumpButtonCaption { get; set; }
        public string basketButtonCaption { get; set; }
        public int basketButtonVisible { get; set; }
        public string basketLabelCaption { get; set; }
        public bool payPumporPrepay { get; set; }
        public string prepayText { get; set; }
        public bool enableBasketButton { get; set; }
        public bool enableStackBasketButton { get; set; }
        public bool canCashierAuthorize { get; set; }
    }
}
