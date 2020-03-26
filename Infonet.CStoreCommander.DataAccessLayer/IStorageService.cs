using Windows.Storage;

namespace Infonet.CStoreCommander.DataAccessLayer
{
    public interface IStorageService
    {
        IStorageFolder StorageFolder { get; }

        IStorageFolder LocalFolder { get; }

        IStorageFolder LocalCacheFolder { get; }
    }
}
