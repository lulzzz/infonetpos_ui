using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class TierLevelContract
    {
        public string pageCaption { get; set; }
        public List<PumpTierLevelContract> pumpTierLevels { get; set; }
        public List<TierContract> tiers { get; set; }
        public List<LevelContract> levels { get; set; }
        public TierLevelMessageContract message { get; set; }
    }

    public class PumpTierLevelContract
    {
        public int pumpId { get; set; }
        public int tierId { get; set; }
        public int levelId { get; set; }
        public string tierName { get; set; }
        public string levelName { get; set; }
    }

    public class TierContract
    {
        public int tierId { get; set; }
        public string tierName { get; set; }
    }

    public class LevelContract
    {
        public int levelId { get; set; }
        public string levelName { get; set; }
    }

    public class TierLevelMessageContract
    {
        public string message { get; set; }
        public int messageType { get; set; }
    }


    public class UpdateTierLevelContract
    {
        public List<int> pumpIds { get; set; }
        public int levelId { get; set; }
        public int tierId { get; set; }
    }
}