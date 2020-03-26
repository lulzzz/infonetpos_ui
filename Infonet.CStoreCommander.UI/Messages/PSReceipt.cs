using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.Messages
{
    public class PSReceipt
    {
        public string StoreGUI { get; set; }
        public string ProdName { get; set; }
        public string BarCodePath { get; set; }
        public string LogoPath { get; set; }
        public string VenderName { get; set; }
        public string UPCCode { get; set; }
        public double Amount { get; set; }
        public double Fee { get; set; }
        public string PIN { get; set; }
        public string PinSerial { get; set; }
        public string PinBatch { get; set; }
        public string TransID { get; set; }
        public string TransDate { get; set; }
        public string TransTime { get; set; }
        public string[] Lines { get; set; }
        public string TerminalId { get; set; }
        public string ReferenceNum { get; set; }
        public string StatusDescription { get; set; }
        public string Custom1 { get; set; }
        public string Terms { get; set; }
        public string Account { get; set; }
    }
}
