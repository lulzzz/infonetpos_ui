namespace Infonet.CStoreCommander.EntityLayer.Entities.Login
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int TillNumber { get; set; }
        public decimal FloatAmount { get; set; }
        public int PosId { get; set; }
        public int ShiftNumber { get; set; }
        public string ShiftDate { get; set; }
        public bool UnauthorizedAccess { get; set; }
    }
}
