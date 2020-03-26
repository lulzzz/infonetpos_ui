using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice
{
    public class PriceIncrementDecrement
    {
        public List<PriceIncrement> PriceIncrements { get; set; }
        public List<PriceDecrement> PriceDecrements { get; set; }
        public bool IsCreditEnabled { get; set; }
    }

    public class PriceIncrement
    {
        public int Row { get; set; }
        public int GradeId { get; set; }
        public string Grade { get; set; }
        public string Cash { get; set; }
        public string Credit { get; set; }
    }

    public class SetPriceIncrement
    {
        public PriceIncrement Price { get; set; }
        public Report Report { get; set; }
    }

    public class PriceDecrement
    {
        public int Row { get; set; }
        public int TierId { get; set; }
        public int LevelId { get; set; }
        public string TierLevel { get; set; }
        public string Cash { get; set; }
        public string Credit { get; set; }
    }

    public class SetPriceDecrement
    {
        public PriceDecrement Price { get; set; }
        public Report Report { get; set; }
    }
}
