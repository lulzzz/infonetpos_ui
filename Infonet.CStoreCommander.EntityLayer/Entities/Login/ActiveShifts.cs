using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Login
{
    public class ActiveShifts
    {
        public List<Shifts> Shifts { get; set; }
        public bool IsShiftUsedForDay { get; set; }
        public bool ForceShift { get; set; }
        public decimal CashFloat { get; set; }
    }
}
