using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.FuelPump
{
    public class TierLevel
    {
        public string PageCaption { get; set; }
        public List<PumpTierLevel> PumpTierLevels { get; set; }
        public List<Tier> Tiers { get; set; }
        public List<Level> Levels { get; set; }
        public TierLevelMessage Message { get; set; }
    }
    public class PumpTierLevel
    {
        public int PumpId { get; set; }
        public int TierId { get; set; }
        public int LevelId { get; set; }
        public string TierName { get; set; }
        public string LevelName { get; set; }
    }

    public class Tier
    {
        public int TierId { get; set; }
        public string TierName { get; set; }
    }

    public class Level
    {
        public int LevelId { get; set; }
        public string LevelName { get; set; }
    }

    public class TierLevelMessage
    {
        public string Message { get; set; }
        public int MessageType { get; set; }
    }
}
