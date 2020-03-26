using Infonet.CStoreCommander.DataAccessLayer;
using Windows.Storage;

namespace Infonet.CStoreCommander.UI.Utility
{
    public class StorageService : IStorageService
    {
        public IStorageFolder StorageFolder => Windows.ApplicationModel.Package.Current.InstalledLocation;

        public IStorageFolder LocalFolder => ApplicationData.Current.LocalFolder;

        public IStorageFolder LocalCacheFolder => ApplicationData.Current.LocalCacheFolder;
    }
}
