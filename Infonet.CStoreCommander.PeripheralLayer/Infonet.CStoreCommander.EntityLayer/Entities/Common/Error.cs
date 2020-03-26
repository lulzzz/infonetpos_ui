namespace Infonet.CStoreCommander.EntityLayer.Entities.Common
{
    public class Error
    {
        public bool ShutDownPos { get; set; }

        public string Message { get; set; }

        public MessageType MessageType { get; set; }
    }

    public enum MessageType
    {
        OkOnly = 0,
        OkCancel = 1,
        AbortRetryIgnore = 2,
        YesNoCancel = 3,
        YesNo = 4,
        RetryCancel = 5,
        Critical = 16,
        Question = 32,
        Exclamation = 48,
        Information = 64,
        DefaultButton2 = 256,
        DefaultButton3 = 512,
        SystemModal = 4096,
        MsgBoxHelp = 16384,
        MsgBoxSetForeground = 65536,
        MsgBoxRight = 524288,
        MsgBoxRtlReading = 1048576
    }
}
