using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Login
{
    public class ActiveTills
    {
        public List<int> Tills { get; set; }

        public int ShiftNumber { get; set; }

        public decimal CashFloat { get; set; }

        public string Message { get; set; }

        public bool ShutDownPOS { get; set; }

        public bool ForceTill { get; set; }

        public string ShiftDate { get; set; }

        public bool IsTrainer { get; set; }
    }
}
