namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Settings.Maintenance
{
    public class ChangePasswordContract
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
