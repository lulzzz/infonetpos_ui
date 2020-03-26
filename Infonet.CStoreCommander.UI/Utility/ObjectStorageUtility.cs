using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Infonet.CStoreCommander.UI.Utility
{
    public class ObjectStorageUtility
    {
        public static async Task<object> LoadObject(Type T, string FileName)
        {
            StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (await localFolder.TryGetItemAsync(FileName) == null)
                return null;



            var readStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(FileName);
           
            DataContractSerializer objSerializer = new DataContractSerializer(T);
            var obj = objSerializer.ReadObject(readStream);
            return obj;
        }
        public static async Task<bool> SaveObject<T>(object o,string FileName)
        {
            try
            {
                StorageFile savedFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
                using(Stream writeStream = await savedFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer objSerializer = new DataContractSerializer(typeof(T));
                    objSerializer.WriteObject(writeStream, o);
                    await writeStream.FlushAsync();
                    writeStream.Dispose();
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

        }
    }
}
