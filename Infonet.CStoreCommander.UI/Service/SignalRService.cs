using Infonet.CStoreCommander.UI.Utility;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Infonet.CStoreCommander.UI.Service
{
    public class SignalRService
    {
        public SignalRService(string baseUrl)
        {
            SignalR(baseUrl);
        }

        public void SignalR(string baseUrl)
        {
            Task.Run(async () =>
            {
                var conn = new HubConnection(baseUrl);
                var proxy = conn.CreateHubProxy("PumpStatusHub");

                try
                {
                    conn.Start().Wait();
                    proxy.Invoke("OpenPortReading").Wait();
                    proxy.On<string>("ReadUdpData", OnMessage);
                }
                catch (Exception ex)
                {
                    SignalR(baseUrl);
                }
            });
        }

        private object objLock = new object();

        private async void OnMessage(string obj)
        {
            
        }
    }
}
