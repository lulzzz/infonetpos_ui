using Infonet.CStoreCommander.Infonet.CStoreCommander.DataAccessLayer.Utilities;
using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Infonet.CStoreCommander.DataAccessLayer.Utility
{
    internal class Helper
    {
        internal static async Task<string> GetOfflineResponse(string dataFileName, IStorageFolder storageInstalledFolder)
        {
            var dataFilePath = await storageInstalledFolder.GetFolderAsync(DalConstants.DataAccessFolderName);
            dataFilePath = await dataFilePath.GetFolderAsync(DalConstants.OfflineDataFolderName);
            dataFilePath = await dataFilePath.GetFolderAsync(DalConstants.JsonContentFolderName);
            var dataFile = await dataFilePath.GetFileAsync(dataFileName);
            var response = string.Empty;
            if (dataFile != null)
            {
                var buffer = await FileIO.ReadBufferAsync(dataFile);
                using (var dataReader = DataReader.FromBuffer(buffer))
                {
                    var bytes = new Byte[buffer.Length];
                    dataReader.ReadBytes(bytes);
                    response = Encoding.GetEncoding("utf-8").GetString(bytes, 0, bytes.Length);
                }
            }
            return response;
        }


    }
}
