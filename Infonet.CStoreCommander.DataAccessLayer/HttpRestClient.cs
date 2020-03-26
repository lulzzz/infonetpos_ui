using Infonet.CStoreCommander.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer
{
    public class HttpRestClient
    {
        private readonly InfonetLog _log;
        private readonly bool _addCompressionHandler;
        private readonly HttpClientHandler _clientHandler;
        private readonly TimeSpan _timeout;
        private Stopwatch _tracker;

        public HttpRestClient(bool addCompressionHandler = false)
        {
            _clientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            _log = InfonetLogManager.GetLogger<HttpRestClient>();
            _tracker = new Stopwatch();
        }

        public HttpRestClient(TimeSpan timeout, bool addCompressionHandler = false)
            : this(addCompressionHandler)
        {
            _timeout = timeout;
        }

        public async Task<HttpResponseMessage> GetAsync(string url, string authToken = null)
        {
            _log.Info(string.Format("Making HTTP GET Request to: {0}", url));
            _tracker.Restart();
            var client = GenerateClient();
            using (client)
            {
                SetAuthToken(client, authToken);
                var response = await client.GetAsync(url);
                _log.Info(string.Format("HTTP GET Response from URL: {0}", url));
                var data = await response.Content.ReadAsStringAsync();
                _log.Info(data);
                _tracker.Stop();
                _log.Info(string.Format("Duration Of API {0} is {1}ms", url, _tracker.Elapsed.TotalMilliseconds));
                return response;
            };
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string authToken = null)
        {
            _log.Info(string.Format("Making HTTP POST Request to: {0}", url));
            _tracker.Restart();
            var client = GenerateClient();
            using (client)
            {
                SetAuthToken(client, authToken);
                var response = await client.PostAsync(url, content);
                _log.Info(string.Format("HTTP POST Response from URL: {0}", url));
                var data = await response.Content.ReadAsStringAsync();
                _log.Info(data);
                _tracker.Stop();
                _log.Info(string.Format("Duration Of API {0} is {1}ms", url, _tracker.Elapsed.TotalMilliseconds));
                return response;
            };
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url, string authToken = null)
        {
            _log.Info(string.Format("Making HTTP DELETE Request to: {0}", url));
            _tracker.Restart();

            var client = GenerateClient();
            using (client)
            {
                SetAuthToken(client, authToken);
                var response = await client.DeleteAsync(url);
                _log.Info(string.Format("HTTP DELETE Response from URL: {0}", url));
                var data = await response.Content.ReadAsStringAsync();
                _log.Info(data);
                _tracker.Stop();
                _log.Info(string.Format("Duration Of API {0} is {1}ms", url, _tracker.Elapsed.TotalMilliseconds));
                return response;
            };
        }

        private HttpClient GenerateClient()
        {
            var client = _addCompressionHandler ? new HttpClient(_clientHandler) : new HttpClient();
            if (_timeout != null && _timeout != default(TimeSpan))
            {
                client.Timeout = _timeout;
            }

            return client;
        }

        private void SetAuthToken(HttpClient httpClient, string authToken)
        {
            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Add("authToken", authToken);
            }
        }
    }
}
