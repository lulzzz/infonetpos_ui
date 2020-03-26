namespace Infonet.CStoreCommander.EntityLayer.Entities.Login
{
    public class LoginPolicy
    {
        public int PosID { get; set; }

        public bool WindowsLogin { get; set; }

        public bool UseShifts { get; set; }

        public bool ProvideTillFloat { get; set; }

        public bool UsePredefinedTillNumber { get; set; }

        public string PosLanguage { get; set; }

        public string Message { get; set; }

        public bool AutoShiftPick { get; set; }

        public string KeypadFormat { get; set; }
    }
}
