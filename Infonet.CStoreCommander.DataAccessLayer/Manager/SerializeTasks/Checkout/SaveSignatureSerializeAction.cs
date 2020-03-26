using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class SaveSignatureSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly Uri _image;
        private readonly SaveSignature _saveSignature;

        public SaveSignatureSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager, Uri image) : base("SaveSignature")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _image = image;
            _saveSignature = new SaveSignature
            {
                encodedImage = image.AbsoluteUri
            };
        }

        protected async override Task<object> OnPerform()
        {
            var file = await StorageFile.GetFileFromPathAsync(_image.LocalPath);
            
            var stream = await file.OpenStreamForReadAsync();
            var content = new StreamContent(stream);
            var response = await _restClient.SaveSignature(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapSuccess(data);
                    return result.success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
