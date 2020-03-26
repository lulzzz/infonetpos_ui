using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPrice
{
    public class PriceIncrementDecrementContract
    {
        public List<PriceIncrementContract> priceIncrements { get; set; }
        public List<PriceDecrementContract> priceDecrements { get; set; }
        public bool isCreditEnabled { get; set; }
    }

    public class PriceIncrementContract
    {
        public int row { get; set; }
        public int gradeId { get; set; }
        public string grade { get; set; }
        public string cash { get; set; }
        public string credit { get; set; }
    }

    public class SetPriceIncrementContract
    {
        public PriceIncrementContract price { get; set; }
        public ReportContract report { get; set; }
    }

    public class PriceDecrementContract
    {
        public int row { get; set; }
        public int tierId { get; set; }
        public int levelId { get; set; }
        public string tierLevel { get; set; }
        public string cash { get; set; }
        public string credit { get; set; }
    }

    public class SetPriceDecrementContract
    {
        public PriceDecrementContract price { get; set; }
        public ReportContract report { get; set; }
    }
}
