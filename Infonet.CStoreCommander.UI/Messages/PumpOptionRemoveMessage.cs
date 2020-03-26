namespace Infonet.CStoreCommander.UI.Messages
{
    public class PumpOptionRemoveMessage
    {
        public bool RemoveFinishOption { get; set; }
        public bool RemoveManualOption { get; set; }
        public bool RemovePrepayOption { get; set; } = true;
    }
}
