using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.PaymentSource;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.View.PSInet.PSInetOptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using ZXing;

namespace Infonet.CStoreCommander.UI.ViewModel.PSInet
{
    public class PaymentSourceVM: VMBase
    {
        private readonly IPaymentSourceBusinessLogic _paymentsourceBusinessLogic;
        private readonly ISaleBussinessLogic _saleBussinessLogic;
        private readonly ICacheBusinessLogic _cacheBussinessLogic;
        private List<PSProduct> _psproducts;
        private PSProfile _psprofile;
        private SaleModel _currentSale;
        private XDocument _doc;
        private string _saleRec;
        private int _remindTimes;
        private string _RefNo;
        private double _FeeAmount;
        private double _Amount;
        private string _StandInMessage;
        private string _LastReceiptFile = "LastPSReceipt.xml";
        


        private PSVoucherInfo _voucherinfo;
        private Windows.Storage.StorageFolder _PSImageFolder = null;
        public delegate void PrintAction();
        public event PrintAction PrintEvent;
        public delegate void PrintData(PSReceipt psrt);
        public event PrintData PDataEvent;
        #region Properties


        private PSTransaction _selectedPSTrans;

        public PSTransaction SelectedPSTransaction
        {
            get { return _selectedPSTrans; }
            set {
                _selectedPSTrans = value;
                RaisePropertyChanged(nameof(PSTransactions));
            }
        }

        private List<PSTransaction> _psTrans;

        public List<PSTransaction> PSTransactions
        {
            get { return _psTrans; }
            set {
                _psTrans = value;
                RaisePropertyChanged(nameof(PSTransactions));
            }
        }

        private string _refcode;

        public string RefCode
        {
            get { return _refcode; }
            set {
                _refcode = value;
                RaisePropertyChanged(nameof(RefCode));
            }
        }

        private string _transactionid;

        public string TransactionID
        {
            get { return _transactionid; }
            set {
                _transactionid = value;
                RaisePropertyChanged(nameof(TransactionID));
            }
        }

        private string _balance;

        public string Balance
        {
            get { return _balance; }
            set {
                _balance = value;
                RaisePropertyChanged(nameof(Balance));
            }
        }

        private bool _remindfiledownload;


        public bool RemindFileDownload
        {
            get { return _remindfiledownload; }
            set {
                _remindfiledownload = value;
                RaisePropertyChanged(nameof(RemindFileDownload));
            }
        }

        private string _prodinfo;

        public string ProdInfo
        {
            get { return _prodinfo; }
            set {
                _prodinfo = value;
                RaisePropertyChanged(nameof(ProdInfo));
            }
        }

        private string _amt;

        public string AMT
        {
            get { return _amt; }
            set {
                _amt = value;
                RaisePropertyChanged(nameof(AMT));
            }
        }

        private string _amountLabel;

        public string AmountLabel
        {
            get { return _amountLabel; }
            set {
                _amountLabel = value;
                RaisePropertyChanged(nameof(AmountLabel));
            }
        }

        private bool _displayAmount;

        public bool DisplayAmount
        {
            get { return _displayAmount; }
            set {
                _displayAmount = value;
                RaisePropertyChanged(nameof(DisplayAmount));
            }
        }

        private string _cardNumber;

        public string CardNumber
        {
            get { return _cardNumber; }
            set {
                _cardNumber = value;
                RaisePropertyChanged(nameof(CardNumber));
            }
        }
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set {
                _phoneNumber = value;
                RaisePropertyChanged(nameof(PhoneNumber));
            }
        }

        private string _phoneNumber2;

        public string PhoneNumber2
        {
            get { return _phoneNumber2; }
            set {
                _phoneNumber2 = value;
                RaisePropertyChanged(nameof(PhoneNumber2));
            }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));
            }
        }

        

        private string _selectedTab;

        public string SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                RaisePropertyChanged(nameof(SelectedTab));

            }
        }
        private string _selectedCategory;

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set {
                  _selectedCategory = value;
                   RaisePropertyChanged(nameof(SelectedCategory));
                   SelCategory(_selectedCategory);
            }
        }

        private List<string> _categories;

        public List<string> Categories
        {
            get { return _categories; }
            set {
                 _categories = value;
                 RaisePropertyChanged(nameof(Categories));
                 
            }
        }

        private List<PSProduct> _products;

        public List<PSProduct> Products
        {
            get { return _products; }
            set {
                _products = value;
                RaisePropertyChanged(nameof(Products));
            }
        }
        private PSProduct _selectedProduct;

        public PSProduct SelectedProduct
        {
            get { return _selectedProduct; }
            set {
                _selectedProduct = value;
                RaisePropertyChanged(nameof(SelectedProduct));
            }
        }

        #endregion

        public  PaymentSourceVM(IPaymentSourceBusinessLogic paymentsourceBusinessLogic,
            ISaleBussinessLogic saleBussinessLogic,
            ICacheBusinessLogic cacheBussinessLogic
            )
        {

            InitializeCommands();
            _paymentsourceBusinessLogic = paymentsourceBusinessLogic;
            _saleBussinessLogic = saleBussinessLogic;
            _cacheBussinessLogic = cacheBussinessLogic;
            MessengerInstance.Register<PSMessage>(this, HotButtonProcess);
            MessengerInstance.Register<PSButtonMessage>(this, PSInetButtonPressed);
            PerformAction(async()=>{
                if(await Helper.IfStorageItemExist(Windows.Storage.ApplicationData.Current.LocalFolder, "PSImages"))
                {
                    _PSImageFolder = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFolderAsync("PSImages");
                }
                else
                {
                    _PSImageFolder = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync("PSImages");
                }
            });
            PerformAction(async () => { Balance = await GetBalanceSt(); });
            _remindTimes = 1;
        }
        #region Methods
        private string BuildXMLRequest1(string RefNo)
        {
            XDocument doc=null;
            switch (SelectedProduct.StoreGUI)
            {
                case "PS":
                case "PV":
                case "PF":
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement(
                         "RequestXml",
                         new XElement("Type", "PinlessRequest2ndStep"),
                         new XElement("TerminalId", _psprofile.TerminalId),
                         new XElement("Password", _psprofile.PSpwd),
                         new XElement("Language", "eng"),
                         new XElement("AccountNo", CardNumber),
                         new XElement("Amount", string.Format("{0:0.00}", _Amount)),
                         new XElement("RefNo", RefNo),
                         new XElement("ProdCode", SelectedProduct.ProductCode)

                        ));
                    break;
                case "TV":
                case "TF":
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement(
                         "RequestXml",
                         new XElement("Type", "PinlessRequest2ndStep"),
                         new XElement("TerminalId", _psprofile.TerminalId),
                         new XElement("Password", _psprofile.PSpwd),
                         new XElement("Language", "eng"),
                         new XElement("AccountNo", PhoneNumber),
                         new XElement("Amount", string.Format("{0:0.00}", _Amount)),
                         new XElement("RefNo", RefNo),
                         new XElement("ProdCode", SelectedProduct.ProductCode)

                        ));
                    break;
            }
             
            return doc.ToString();
        }
        private async Task<string> GetBalanceSt()
        {
            if (_psprofile == null)
                _psprofile = await _paymentsourceBusinessLogic.GetPSProfileAsync();

            XDocument doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement(
                         "RequestXml",
                         new XElement("Type", "GetBalanceSt"),
                         new XElement("TerminalId", _psprofile.TerminalId),
                         new XElement("Password", _psprofile.PSpwd)
                         

                        ));
            HttpWebRequest req;
            WebResponse resp;

            try
            {
                req = (HttpWebRequest)WebRequest.Create(_psprofile.URL + doc.ToString());
                req.ContentType = "application/xml";
                req.Method = "GET";
                req.ContinueTimeout = 20000;
                resp = await req.GetResponseAsync();
                Stream responseStream = resp.GetResponseStream();
                doc = XDocument.Load(responseStream);
                XElement xe = GetElement(doc, "string");
                string prods = xe.Value.Replace("&amp;lt;", "<");
                prods = prods.Replace("&amp;gt;", ">");
                prods = prods.Replace("&lt;", "<");
                prods = prods.Replace("&gt;", ">");
                doc = XDocument.Parse(prods);
                xe = GetElement(doc, "StatusCode");
                if(xe.Value!="0")
                {
                    xe = GetElement(doc, "StatusDescription");
                    if (xe != null)
                        return xe.Value;
                    else
                        return "The Payment Source Connection is broken.";
                }
                    
                xe = GetElement(doc, "Balance");
                double bl;
                if (double.TryParse(xe.Value, out bl))
                    return "Balance: " + string.Format("{0:C2}", bl);
                else
                    return "The Payment Source Connection is broken.";
                
            }
            catch(Exception ex)
            {
                return ex.Message;
                //return "The Payment Source Connection is broken.";
            }
            
        }
        private async Task<string> BuildXMLRequest()
        {
            string sVal = string.Empty;
            string ID = await _paymentsourceBusinessLogic.GetPSTransactionIDAsync();
            _saleRec = _currentSale.SaleNumber + "-" + ID;

            XDocument doc=null;
            if (_psprofile == null)
                _psprofile = await _paymentsourceBusinessLogic.GetPSProfileAsync();
            switch (SelectedProduct.StoreGUI)
            {
                case "EP":
                case "VP":
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement(
                         "RequestXml",
                         new XElement("Type", "PinRequest"),
                         new XElement("TerminalId", _psprofile.TerminalId),
                         new XElement("Password", _psprofile.PSpwd),
                         new XElement("Language", "eng"),
                         new XElement("ValueCode", string.Format("{0:0.00}",_Amount)),
                         new XElement("RefNo", _saleRec),
                         new XElement("ProdCode", SelectedProduct.ProductCode)

                        ));
                    sVal = doc.ToString();
                    break;
                case "PS":
                case "PF":
                case "PV":
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement(
                         "RequestXml",
                         new XElement("Type", "PinlessRequest"),
                         new XElement("TerminalId", _psprofile.TerminalId),
                         new XElement("Password", _psprofile.PSpwd),
                         new XElement("Language", "eng"),
                         new XElement("AccountNo", CardNumber),
                         new XElement("Amount", string.Format("{0:0.00}", _Amount)),
                         new XElement("RefNo", _saleRec),
                         new XElement("ProdCode", SelectedProduct.ProductCode)

                        ));

                    sVal = doc.ToString();
                    break;
                case "TV":
                case "TF":
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement(
                         "RequestXml",
                         new XElement("Type", "PinlessRequest"),
                         new XElement("TerminalId", _psprofile.TerminalId),
                         new XElement("Password", _psprofile.PSpwd),
                         new XElement("Language", "eng"),
                         new XElement("AccountNo", PhoneNumber),
                         new XElement("Amount", string.Format("{0:0.00}", _Amount)),
                         new XElement("RefNo", _saleRec),
                         new XElement("ProdCode", SelectedProduct.ProductCode)

                        ));

                    sVal = doc.ToString();
                    break;
            }
            return sVal;
        }
        private XElement GetElement(XDocument doc, string elementName)
        {
            foreach (XNode node in doc.DescendantNodes())
            {
                if (node is XElement)
                {
                    XElement element = (XElement)node;
                    if (element.Name.LocalName.Equals(elementName))
                        return element;
                }
            }
            return null;
        }
        public  void ActivateCard()
        {
            

            PerformAction(async () => {

                double amt;
                switch(SelectedProduct.StoreGUI)
                {
                    case "PV":
                        if (!double.TryParse(AMT, out amt))
                        {
                            ShowNotification("The inputed amount is invalid.~Amount Error", null, null, ApplicationConstants.ButtonWarningColor);
                            return;
                        }
                        else
                            _Amount = amt;
                        if (CardNumber == "" || CardNumber == null)
                        {
                            ShowNotification("Please enter the card number.~Card Number", null, null, ApplicationConstants.ButtonWarningColor);
                            return;
                        }
                        break;
                    case "TV":
                    case "TF":
                        if (PhoneNumber == "" || PhoneNumber == null)
                        {
                            ShowNotification("Please enter phone number.~Phone Number", null, null, ApplicationConstants.ButtonWarningColor);
                            return;
                        }
                        else
                        {
                            if (PhoneNumber2 == "" || PhoneNumber2 == null)
                            {
                                ShowNotification("Please re-enter phone number.~Phone Number", null, null, ApplicationConstants.ButtonWarningColor);
                                return;
                            }
                            else
                            {
                                if (PhoneNumber != PhoneNumber2)
                                {
                                    ShowNotification("The re-enter does not match.~Phone Number", null, null, ApplicationConstants.ButtonWarningColor);
                                    return;
                                }
                                else
                                {
                                    if (SelectedProduct.StoreGUI != "TV")
                                    {
                                        _Amount = double.Parse(SelectedProduct.Amount);
                                    }
                                    else
                                    {
                                        if (!double.TryParse(AMT, out amt))
                                        {
                                            ShowNotification("The inputed topup amount is invalid.~Topup Amount Error", null, null, ApplicationConstants.ButtonWarningColor);
                                            return;
                                        }
                                        else
                                            _Amount = amt;
                                    }
                                }
                            }
                        }
                        break;
                    case "VP":
                        if (!double.TryParse(AMT, out amt))
                        {
                            ShowNotification("The inputed amount is invalid.~Amount Error", null, null, ApplicationConstants.ButtonWarningColor);
                            return;
                        }
                        else
                            _Amount = amt;
                        break;
                    default:
                        _Amount = double.Parse(SelectedProduct.Amount);
                        break;
                }
                
                    

                _FeeAmount = ProductFee();




                PSReceipt psrt = new PSReceipt();
                string myrequest = await BuildXMLRequest();
                XElement xe;
                switch(SelectedProduct.StoreGUI)
                {
                    case "EP":
                    case "VP":
                        try
                        {


                            _doc = await PSRequest(myrequest, true);

                            xe = GetElement(_doc, "StatusCode");
                            if (xe.Value != "0")
                            {
                                xe = GetElement(_doc, "StatusDescription");
                                ShowNotification(xe.Value + "~Activation failed", null, null, ApplicationConstants.ButtonWarningColor);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            ShowNotification(ex.Message + "~Activation failed", null, null, ApplicationConstants.ButtonWarningColor);
                            return;
                        }
                        xe = GetElement(_doc, "TransNo");
                        psrt.TransID = xe.Value;
                        break;
                    case "PS":
                    case "PV":
                    case "PF":
                    case "TV":
                    case "TF":
                        try
                        {
                            var rootElement = XElement.Parse(myrequest);
                            _RefNo = rootElement.Element("RefNo").Value;
                            _doc = await PSRequest(myrequest, true);
                            xe = GetElement(_doc, "StatusCode");
                            string StatusC = xe.Value;
                            
                            if (StatusC != "0" && StatusC!= "Unknown")
                            {
                                xe = GetElement(_doc, "StatusDescription");
                                ShowNotification(xe.Value + "~Activation failed", null, null, ApplicationConstants.ButtonWarningColor);
                                return;
                            }

                            xe = GetElement(_doc, "TransNo");
                            psrt.TransID = xe.Value;

                            xe = GetElement(_doc, "StatusDescription");
                            _StandInMessage = xe.Value;

                            xe = GetElement(_doc, "AuthorizationCode");
                            string AuthCode = xe.Value;

                            if (StatusC == "0" && (AuthCode == "" || AuthCode == null))
                            {
                                myrequest = BuildXMLRequest1(_RefNo);
                                _doc = await PSRequest(myrequest, false);
                                xe = GetElement(_doc, "StatusCode");
                                StatusC = xe.Value;
                                if (StatusC == "1")
                                {
                                    xe = GetElement(_doc, "StatusDescription");
                                    ShowNotification(xe.Value + "~Activation failed", null, null, ApplicationConstants.ButtonWarningColor);
                                    return;
                                }
                                if (StatusC == "0")
                                    _StandInMessage = "";

                            }


                        }
                        catch (Exception ex)
                        {
                            ShowNotification(ex.Message + "~Error_TwoStep", null, null, ApplicationConstants.ButtonWarningColor);
                            return;
                        }
                        break;
                    
                }
                






                //PrintVoucher();
                
                string sFileName = null;
                StorageFile barcodefile = null;
                
                string sVender = string.Empty;
                bool bSaved;

                _voucherinfo = await _paymentsourceBusinessLogic.GetPSVoucherInfoAsync(SelectedProduct.Name);
                barcodefile = await SaveBarCodeAsFile(SelectedProduct.UpcNumber);
                
                if (_voucherinfo.Logos != null)
                    foreach (var c in _voucherinfo.Logos)
                    {
                        if (await CheckLogoFile(c.ImageFileName))
                        {
                            sFileName = c.ImageFileName;
                            break;
                        }

                    }
                //psrt.Terms = TermsAndConditions(_voucherinfo.Voucher.Voucher.Split('|'));
                ParseTerms(psrt);
                psrt.ReferenceNum = _saleRec;
                psrt.TerminalId = _psprofile.TerminalId;
                psrt.UPCCode = SelectedProduct.UpcNumber;
                psrt.Amount = double.Parse(SelectedProduct.Amount);
                psrt.Fee = _FeeAmount;
                psrt.ProdName = SelectedProduct.Name;
                
                xe = GetElement(_doc, "TransDate");
                psrt.TransDate = xe.Value;


                xe = GetElement(_doc, "TransTime");
                psrt.TransTime = xe.Value;
                psrt.StoreGUI = SelectedProduct.StoreGUI;

                switch (SelectedProduct.StoreGUI)
                {
                    case "EP":
                    case "VP":

                        xe = GetElement(_doc, "Pin");
                        psrt.PIN = xe.Value;
                        string sPIN = xe.Value;

                        xe = GetElement(_doc, "PinSerial");
                        psrt.PinSerial = xe.Value;


                        xe = GetElement(_doc, "PinBatch");
                        psrt.PinBatch = xe.Value;

                        psrt.BarCodePath = barcodefile.Path;
                        psrt.LogoPath = _PSImageFolder.Path + "\\" + sFileName;


                        

                        
                        break;
                    case "PS":
                    case "PF":
                    case "PV":

                        psrt.BarCodePath = barcodefile.Path;
                        psrt.LogoPath = _PSImageFolder.Path + "\\" + sFileName;



                        
                        if(_StandInMessage!="")
                        psrt.StatusDescription = _StandInMessage;
                        else
                        {
                            xe = GetElement(_doc, "StatusDescription");
                            psrt.StatusDescription = xe.Value;
                        }

                        xe = GetElement(_doc, "Custom1");
                        psrt.Custom1 = xe.Value;



                        psrt.Amount = _Amount;

                        

                        

                        break;
                    case "TF":
                    case "TV":
                        
                        psrt.BarCodePath = barcodefile.Path;
                        psrt.LogoPath = _PSImageFolder.Path + "\\" + sFileName;
                        psrt.Amount = _Amount;
                        psrt.Account = PhoneNumber;

                        break;
                        
                }

                bSaved = await ObjectStorageUtility.SaveObject<PSReceipt>(psrt, _LastReceiptFile);

                PDataEvent?.Invoke(psrt);

                PrintEvent?.Invoke();

                //Update Balance
                Balance = await GetBalanceSt();
                //add product to current sale


                var result = await _saleBussinessLogic.AddStockToSale(SelectedProduct.UpcNumber,
                    1,
                    true,
                    null,
                    false
                    );
                SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);
                var sale = result.ToModel();
                SaleLineModel sl = (SaleLineModel)sale.SaleLines[sale.SaleLines.Count - 1];
                
                sl.Price = string.Format("{0:0.00}",_Amount+_FeeAmount);
                
                var saleupdate = await _saleBussinessLogic.UpdateSale(
                     sl.LineNumber,
                     "",
                     "",
                     "1",
                     sl.Price,
                     "",
                     ""
                    );

                //Save transaction number to saleline
                
                bSaved = await _paymentsourceBusinessLogic.SavePSTransactionIDAsync(sale.TillNumber, sale.SaleNumber, sl.LineNumber, psrt.TransID);
                
                
                
                //update sale screen
                MessengerInstance.Send(saleupdate.ToModel(), "UpdateSale");
                SelectedTab = PaymentSourceTabs.EP.ToString();
                NavigateService.Instance.NavigatePaymentSourceFrame(typeof(EP));
            });
            
            


            
        }
        private void ParseTerms(PSReceipt psrt)
        {
            string[] Lines = _voucherinfo.Voucher.Voucher.Split('|');
            List<string> pl = Lines.ToList();
            string terms = "";

            int iStart = pl.FindIndex(x => !x.Contains("{") && x.Trim() != "");
            int iVal = 0;
            for (int i = iStart; i < Lines.Length; i++)
            {
                if (Lines[i].Contains("{"))
                {
                    iVal = i;
                    break;
                }
                else
                    terms += Lines[i] + "\r\n";
            }
            psrt.Terms = terms;
            //vendor info
            psrt.VenderName = "";
            iStart = pl.FindIndex(x => x.Contains("{c}Powered by"));
            

            
            if(iStart>0)
              psrt.VenderName = Lines[iStart].Replace("{c}", "");
            

        }
        private string TermsAndConditions(string[] Lines)
        {
            List<string> pl = Lines.ToList();
            string terms = "";
            
            int iStart = pl.FindIndex(x => !x.Contains("{") && x.Trim() != "");
            for(int i=iStart;i<Lines.Length;i++)
            {
                if (Lines[i].Contains("{"))
                    break;
                else
                    terms += pl[i].ToString() + "\r\n";
            }
            
            
            return terms;
        }
        private void GetLimit(out double h, out double l)
        {
            h = 0;
            l = 0;
            try
            {
                if (SelectedProduct.AmtDisplay.Contains("to"))
                {
                    string amtd = SelectedProduct.AmtDisplay.Replace("to", "|");
                    string[] limits = amtd.Split('|');
                    l = double.Parse(limits[0].Replace("$", ""));
                    h = double.Parse(limits[1].Replace("$", ""));
                }
                else
                {
                    h = double.Parse(SelectedProduct.AmountLimit);
                }
            }
            catch(Exception )
            {

            }
        }
        private double ProductFee()
        {
            double fee = 0;
            try
            {
              if(SelectedProduct.AmtDisplay.Contains("+"))
                {
                    string[] load = SelectedProduct.AmtDisplay.Split('+');
                    string feeval = load[1].Replace("Fee", "");
                    feeval = feeval.Replace("$", "");
                    fee = double.Parse(feeval);
                }
            }
            catch(Exception)
            {
                fee = 0;
            }
            return fee;
        }
        private async void PrintVoucher()
        {
            

            List<string> PrintLines = new List<string>();
            
            XElement xe;
            string sFileName = null;
            StorageFile barcodefile = null;
            PSReceipt psrt = new PSReceipt();
            string sVender = string.Empty;
            bool bSaved;

            _voucherinfo = await _paymentsourceBusinessLogic.GetPSVoucherInfoAsync(SelectedProduct.Name);
            barcodefile = await SaveBarCodeAsFile(SelectedProduct.UpcNumber);
            
            if (_voucherinfo.Logos != null)
                foreach (var c in _voucherinfo.Logos)
                {
                    if (await CheckLogoFile(c.ImageFileName))
                    {
                        sFileName = c.ImageFileName;
                        break;
                    }

                }
            psrt.Lines = _voucherinfo.Voucher.Voucher.Split('|');
            psrt.ReferenceNum = _saleRec;
            psrt.TerminalId = _psprofile.TerminalId;
            psrt.UPCCode = SelectedProduct.UpcNumber;
            psrt.Amount = double.Parse(SelectedProduct.Amount);
            psrt.Fee = _FeeAmount;
            psrt.ProdName = SelectedProduct.Name;
            xe = GetElement(_doc, "TransNo");
            psrt.TransID = xe.Value;


            xe = GetElement(_doc, "TransDate");
            psrt.TransDate = xe.Value;


            xe = GetElement(_doc, "TransTime");
            psrt.TransTime = xe.Value;
            psrt.StoreGUI = SelectedProduct.StoreGUI;

            switch (SelectedProduct.StoreGUI)
            {
                case "EP":
                    
                    
                    
                    
                    
                   
                    
                   
                    xe = GetElement(_doc, "Pin");
                    psrt.PIN = xe.Value;
                    string sPIN = xe.Value;

                    xe = GetElement(_doc, "PinSerial");
                    psrt.PinSerial = xe.Value;
                    

                    xe = GetElement(_doc, "PinBatch");
                    psrt.PinBatch = xe.Value;
                    

                    
                    

                    

                    //lines = _voucherinfo.Voucher.Voucher.Split('|');
                    
                    
                    
                    
                    //await PerformPrint(PrintLines, 1, true, new Uri(barcodefile.Path));
                    //psrt.StoreGUI = "EP";
                    
                    psrt.BarCodePath = barcodefile.Path;
                    psrt.LogoPath = _PSImageFolder.Path + "\\"+ sFileName;
                   
                    
                    bSaved = await ObjectStorageUtility.SaveObject<PSReceipt>(psrt, _LastReceiptFile);

                    PDataEvent?.Invoke(psrt);
                    
                    PrintEvent?.Invoke();
                    break;
                case "PS":
                case "PF":
                case "PV":

                    psrt.BarCodePath = barcodefile.Path;
                    psrt.LogoPath = _PSImageFolder.Path + "\\" + sFileName;

                    

                    xe = GetElement(_doc, "StatusDescription");
                    psrt.StatusDescription = xe.Value;

                    xe = GetElement(_doc, "Custom1");
                    psrt.Custom1 = xe.Value;



                    psrt.Amount = _Amount;

                    bSaved = await ObjectStorageUtility.SaveObject<PSReceipt>(psrt, _LastReceiptFile);

                    PDataEvent?.Invoke(psrt);
                    PrintEvent?.Invoke();
                    
                    break;
                //case "PF":
                    
                //case "PV":
                //    //psrt.StoreGUI = "PV";

                //    psrt.BarCodePath = barcodefile.Path;
                //    psrt.LogoPath = _PSImageFolder.Path + "\\" + sFileName;

                //    xe = GetElement(_doc, "StatusDescription");
                //    psrt.StatusDescription = xe.Value;

                //    xe = GetElement(_doc, "Custom1");
                //    psrt.Custom1 = xe.Value;
                //    psrt.Amount = _Amount;


                //    bSaved = await ObjectStorageUtility.SaveObject<PSReceipt>(psrt, _LastReceiptFile);

                //    PDataEvent?.Invoke(psrt);
                //    PrintEvent?.Invoke();




                //    break;
            }


        }
        private async Task<StorageFile> SaveBarCodeAsFile(string code)
        {
            string FileName = "BarCode.bmp";
            Guid BitmapEncoderGuid = BitmapEncoder.BmpEncoderId;
            var file = await _PSImageFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoderGuid, stream);
                WriteableBitmap WB = BarcodeImg(code);
                Stream pixelStream = WB.PixelBuffer.AsStream();
                byte[] pixels = new byte[pixelStream.Length];
                await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                            (uint)WB.PixelWidth,
                            (uint)WB.PixelHeight,
                            96.0,
                            96.0,
                            pixels);
                await encoder.FlushAsync();
            }
            return file;
        }
        private WriteableBitmap BarcodeImg(string code)
        {
            IBarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 25,
                    Width = 160,
                    PureBarcode=true,
                    Margin = 0
                    
                }
            };

            return writer.Write(code);
            
        }
        private async Task<bool> CheckLogoFile(string sFileName)
        {


            if (await Helper.IfStorageItemExist(_PSImageFolder, sFileName))
                return true;
            //download all logos
            var olist = await _paymentsourceBusinessLogic.GetPSLogosAsync();
            foreach(var c in olist)
            {
                SaveImage(c.ImageString, c.ImageFileName);
            }
            if (await Helper.IfStorageItemExist(_PSImageFolder, sFileName))
                return true;
            else
                return false;
        }
        private async void SaveImage(string imgstr, string sfilename)
        {
            var file = await _PSImageFolder.CreateFileAsync(sfilename, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await stream.WriteAsync(Convert.FromBase64String(imgstr).AsBuffer());
                
            }
        }
        private async void EPProcess(string payload)
        {
            try
            {
                HttpWebRequest req;
                req = (HttpWebRequest)WebRequest.Create(_psprofile.URL + payload);
                req.ContentType = "application/xml";
                req.Method = "GET";

                WebResponse resp;

                resp = await req.GetResponseAsync();
                Stream responseStream = resp.GetResponseStream();
                _doc = XDocument.Load(responseStream);

                XElement xe = GetElement(_doc, "string");
                string prods = xe.Value.Replace("&amp;lt;", "<");
                prods = prods.Replace("&amp;gt;", ">");
                prods = prods.Replace("&lt;", "<");
                prods = prods.Replace("&gt;", ">");
                _doc = XDocument.Parse(prods);

            }
            
            catch (Exception ex)
            {
                ShowNotification(ex.Message + "~EP Process", null, null, ApplicationConstants.ButtonWarningColor);
            }
        }
        
        private async Task<XDocument> PSRequest(string payload, bool bFirstStep)
        {
            XDocument doc;
            


                using (HttpClient client = new HttpClient())
                {
                    TimeSpan tsp = new TimeSpan(0, 0, 20);
                    client.Timeout = tsp;
                    
                    try
                    {
                        using (var response = await client.GetAsync(_psprofile.URL + payload))
                        {
                            var content = response.Content;
                            var responseStream = await content.ReadAsStreamAsync();
                            doc = XDocument.Load(responseStream);
                            XElement xe = GetElement(doc, "string");
                            string prods = xe.Value.Replace("&amp;lt;", "<");
                            prods = prods.Replace("&amp;gt;", ">");
                            prods = prods.Replace("&lt;", "<");
                            prods = prods.Replace("&gt;", ">");
                            doc = XDocument.Parse(prods);
                            xe = GetElement(doc, "ProductVer");
                            RemindFileDownload = false;
                            if (_psprofile.GroupNumber + _psprofile.ProductVersion != xe.Value && _remindTimes > 0)
                            {
                                _remindTimes -= 1;
                                prods = "A new version of product file is available to download!";
                                ShowNotification(prods + "~New product reminder", null, null, ApplicationConstants.ButtonWarningColor);
                            }
                        }
                    }
                    catch(WebException wex)
                    {
                        if (wex.Status == WebExceptionStatus.Timeout && !bFirstStep)
                        {
                            doc = new XDocument(
                            new XDeclaration("1.0", "UTF-8", "yes"),
                            new XElement(
                                 "ResponseXml",
                                 new XElement("StatusCode", "Unknown"
                                     ),
                                new XElement("StatusDescription",
                                       wex.Message
                                    )
                                    )
                            );
                        }
                        else
                        {
                            doc = new XDocument(
                            new XDeclaration("1.0", "UTF-8", "yes"),
                            new XElement(
                                 "ResponseXml",
                                 new XElement("StatusCode", "1"
                                     ),
                                new XElement("StatusDescription",
                                       wex.Message
                                    )
                                    )
                            );
                        }
                    }
                    catch(Exception ex)
                    {
                       if(!bFirstStep)
                        doc = new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement(
                             "ResponseXml",
                             new XElement("StatusCode", "Unknown"
                                 ),
                            new XElement("StatusDescription",
                                   ex.Message
                                )
                                )
                        );
                       else
                        doc = new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement(
                             "ResponseXml",
                             new XElement("StatusCode", "1"
                                 ),
                            new XElement("StatusDescription",
                                   ex.Message
                                )
                                )
                        );
                }
                }
                  


            return doc; 
            
            
        }
       
        private bool ValidateAmtInput(out double dVal, out string Erro)
        {
            bool bRet = false;
            dVal = 0;
            Erro = "";
            string amtrange = "";
            try
            {
                amtrange = SelectedProduct.AmtDisplay.Replace("$", "");
                string[] amt = amtrange.Split('-');
                double lowR = double.Parse(amt[0]);
                double highR = double.Parse(amt[1]);
                dVal = double.Parse(AMT.Replace("$",""));
                if(dVal >= lowR && dVal <= highR)
                {
                    bRet = true;
                }
                else
                {
                    Erro = "The entered amount is out of range.";
                }
            }
            catch(Exception ex)
            {
                Erro = ex.Message;
            }
            return bRet;
        }
        private void AddToSale()
        {
            if (SelectedProduct == null)
                return;
            switch(SelectedProduct.StoreGUI)
            {
                case "EP":
                    ActivateCard();
                    break;
                case "VP":
                    AMT = "";
                    AmountLabel = "Enter amount: " + SelectedProduct.AmtDisplay;
                    ProdInfo = ProductInformation();
                    NavigateService.Instance.NavigatePaymentSourceFrame(typeof(VP));
                    break;
                case "PF":
                
                case "PS":
                    CardNumber = "";
                    AMT = "";
                    DisplayAmount = false;
                    AmountLabel = "Amount: " + SelectedProduct.AmtDisplay;
                    ProdInfo = ProductInformation();
                    NavigateService.Instance.NavigatePaymentSourceFrame(typeof(PS));
                    break;
                case "PV":
                    CardNumber = "";
                    AMT = "";
                    AmountLabel = "Enter amount: " + SelectedProduct.AmtDisplay;
                    ProdInfo = ProductInformation();
                    DisplayAmount = true;
                    NavigateService.Instance.NavigatePaymentSourceFrame(typeof(PS));
                    break;
                case "TV":
                    PhoneNumber = "";
                    PhoneNumber2 = "";
                    AMT = "";
                    ProdInfo = ProductInformation();
                    AmountLabel = "Enter amount: "+SelectedProduct.AmtDisplay;
                    DisplayAmount = true;
                    NavigateService.Instance.NavigatePaymentSourceFrame(typeof(TX));
                    break;
                case "TF":
                    PhoneNumber = "";
                    PhoneNumber2 = "";
                    AMT = "";
                    ProdInfo = ProductInformation();
                    AmountLabel = "Amount: " + SelectedProduct.AmtDisplay;
                    DisplayAmount = false;
                    NavigateService.Instance.NavigatePaymentSourceFrame(typeof(TX));
                    break;

            }
        }
        private string ProductInformation()
        {
            if (SelectedProduct.Description == "")
            {
                return "Product: " + SelectedProduct.Name
                  + "\r\nCategory: " + SelectedProduct.CategoryName
                  + "\r\nUPC Number: " + SelectedProduct.UpcNumber;
            }
            else
                return "Product: " + SelectedProduct.Name
                      + "\r\nCategory: " + SelectedProduct.CategoryName
                      + "\r\nUPC Number: " + SelectedProduct.UpcNumber
                      
                      + "\r\nDescription: " + SelectedProduct.Description;
            
        }
        private void PSInetButtonPressed(PSButtonMessage ps)
        {
            _currentSale = ps.CurrentSale;
        }
        private async void HotButtonProcess(PSMessage ps)
        {
            if(_psproducts==null)
            {
                _psproducts = await _paymentsourceBusinessLogic.GetPSProductsAsync();
                if (_psproducts == null)
                    return;

            }
            
            
            
            var prod = (from c in _psproducts
                        where c.UpcNumber == ps.UPCNumber
                        select c).FirstOrDefault();
            if(prod==null)
            {
                ShowNotification("The product does not exist.~PSInet Product",
                    null,
                    null,
                    ApplicationConstants.ButtonWarningColor
                    );
                return;
            }
            SelectedProduct = prod;
            _currentSale = ps.CurrentSale;
            //_saleAmount = ps.SaleAmount;
            AddToSale();
            
        }
        private void SelCategory(string catname)
        {
            if (catname == "All Products")
                Products = _psproducts.OrderBy(x=>x.Name).ToList();
            else
                Products = (from c in _psproducts
                            where c.CategoryName == catname
                            select c).OrderBy(x => x.Name).ToList();
            if (Products.Count > 0)
                SelectedProduct = Products[0];
        }
        internal void ResetVM()
        {
            EPTabPressed();
            SearchText = "";
            

        }
        private void InitializeCommands()
        {
            EPTabPressedCommand = new RelayCommand(EPTabPressed);
            QueryCommand = new RelayCommand(PSQuery);
            FileDownloadTabPressedCommand = new RelayCommand(FileDownloadTabPressed);
            FileDownloadCommand = new RelayCommand(DownloadFile);
            RefundTabPressedCommand = new RelayCommand(RefundTabPressed);
            RefundCommand = new RelayCommand(RefundRequest);
            SearchCommand = new RelayCommand<object>(DoSearch);
            SearchCommand1 = new RelayCommand(SearchProduct);
            AddToSaleCommand = new RelayCommand(AddToSale);
            CardNumberCommand = new RelayCommand<object>(CardNumberEntered);
            CardActivationCommand = new RelayCommand(ActivateCard);
            PrintLastReceiptCommand = new RelayCommand(PrintLastReceipt);
            
        }

        

        private void PSQuery()
        {
            PerformAction(async () => {
                if (string.IsNullOrEmpty(RefCode))
                {
                    ShowNotification("Please enter reference code.~Query Request",
                            null,
                            null,
                            ApplicationConstants.ButtonWarningColor
                        );
                    return;
                }


                XDocument doc = new XDocument(
                              new XDeclaration("1.0", "UTF-8", "yes"),
                          new XElement(
                            "RequestXml",
                            new XElement("Type", "Query"),
                            new XElement("TerminalId", _psprofile.TerminalId),
                            new XElement("Password", _psprofile.PSpwd),
                            new XElement("OriginalRefNo", RefCode)



                           ));

                HttpWebRequest req;
                WebResponse resp;
                string prods = string.Empty;
                XElement xe;
                
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(_psprofile.URL + doc.ToString());
                    req.ContentType = "application/xml";
                    req.Method = "GET";
                    req.ContinueTimeout = 20000;
                    resp = await req.GetResponseAsync();
                    Stream responseStream = resp.GetResponseStream();
                    doc = XDocument.Load(responseStream);

                    xe = GetElement(doc, "string");
                    prods = xe.Value.Replace("&amp;lt;", "<");
                    prods = prods.Replace("&amp;gt;", ">");
                    prods = prods.Replace("&lt;", "<");
                    prods = prods.Replace("&gt;", ">");
                    doc = XDocument.Parse(prods);
                    
                    xe = GetElement(doc, "TransNo");
                    TransactionID = xe.Value;
                }
                catch (Exception ex)
                {
                    ShowNotification(ex.Message + "~Query",
                               null,
                               null,
                               ApplicationConstants.ButtonWarningColor
                           );
                }
            });

        }

        private  void RefundRequest()
        {
            PerformAction(async () =>
            {
                if (string.IsNullOrEmpty(TransactionID))
                {
                    ShowNotification("Please enter transaction number.~Refund Request",
                            null,
                            null,
                            ApplicationConstants.ButtonWarningColor
                        );
                    return;
                }
                string RefNo = string.Empty;

               
                


                RefNo = await _paymentsourceBusinessLogic.GetPSTransactionIDAsync();
                RefNo = _currentSale.SaleNumber + "-" + RefNo;


                XDocument doc = new XDocument(
                              new XDeclaration("1.0", "UTF-8", "yes"),
                          new XElement(
                            "RequestXml",
                            new XElement("Type", "PinlessRequest"),
                            new XElement("TerminalId", _psprofile.TerminalId),
                            new XElement("Password", _psprofile.PSpwd),
                            new XElement("CardData", TransactionID),
                            new XElement("Amount", "0"),
                            new XElement("RefNo", RefNo),
                            new XElement("ProdCode", "RETURN")


                           ));

                
                string prods = string.Empty;
                XElement xe;
                string new_transid;
                try
                {
                    doc = await PSRequest(doc.ToString(), true);
                    
                    xe = GetElement(doc, "StatusCode");
                    if (xe.Value != "0")
                    {
                        xe = GetElement(doc, "StatusDescription");
                        
                        ShowNotification(xe.Value + "~Refund Request",
                               null,
                               null,
                               ApplicationConstants.ButtonWarningColor
                           );

                       if (xe.Value.Contains("Transaction prevously refunded"))
                            Balance = await GetBalanceSt();

                        return;
                    }

                    xe = GetElement(doc, "TransNo");
                    new_transid = xe.Value;

                    //Update Balance
                    Balance = await GetBalanceSt();



                    PSRefund psrf = await _paymentsourceBusinessLogic.GetPSRefundInfoAsync(TransactionID, _currentSale.SaleNumber, _currentSale.TillNumber);

                    //if psrf is null, it means that there is no sale line for the transactionid
                    //so just exit
                    if (psrf == null)
                        return;



                    //if psrf is not null, it means that there is a sale for the transactionid
                    //so we have to return (remove) the sale


                    var result = await _saleBussinessLogic.AddStockToSale(psrf.UpcNumber,
                        1,
                        true,
                        null,
                        false
                        );
                    SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);

                    var sale = result.ToModel();
                    SaleLineModel sl = (SaleLineModel)sale.SaleLines[sale.SaleLines.Count - 1];

                    //sl.Price = string.Format("{0:0.00}", -1 * double.Parse(psrf.Amount));

                    var saleupdate = await _saleBussinessLogic.UpdateSale(
                         sl.LineNumber,
                         "",
                         "",
                         "-1",
                         psrf.Amount,
                         "",
                         ""
                        );

                    //Save transaction number to saleline

                    bool bSaved = await _paymentsourceBusinessLogic.SavePSTransactionIDAsync(sale.TillNumber, sale.SaleNumber, sl.LineNumber, new_transid);



                    //update sale screen
                    MessengerInstance.Send(saleupdate.ToModel(), "UpdateSale");
                    ShowNotification("Successful refund~Refund Request",
                           null,
                           null,
                           ApplicationConstants.ButtonWarningColor
                       );
                }
                catch (Exception ex)
                {
                    ShowNotification(ex.Message + "~Refund Request",
                           null,
                           null,
                           ApplicationConstants.ButtonWarningColor
                       );
                    return;
                }

            });
            SelectedTab = PaymentSourceTabs.EP.ToString();
            NavigateService.Instance.NavigatePaymentSourceFrame(typeof(EP));
        }

        private void PrintLastReceipt()
        {
            object psrt=null;
            var task = Task.Run(async () => { psrt = await ObjectStorageUtility.LoadObject(typeof(PSReceipt), _LastReceiptFile); });
            task.Wait();
            if (psrt == null)
            {
                ShowNotification("There is no receipt.~Print Last Receipt",
                        null,
                        null,
                        ApplicationConstants.ButtonWarningColor
                    );
                return;
            }
                
            PDataEvent?.Invoke((PSReceipt)psrt);
            PrintEvent?.Invoke();

        }
        private void CardNumberEntered(object s)
        {
            if (!Helper.IsEnterKey(s))
            {
                return;
            }
            // Eliminating track1 over here
            if (Helper.IsEnterKey(s) && CardNumber.IndexOf('?') != -1 &&
                CardNumber.IndexOf('?') == CardNumber.LastIndexOf('?'))
            {
                return;
            }

            //ShowNotification(CardNumber + "~CardNumber", null, null, ApplicationConstants.ButtonWarningColor);
        }
        private void SearchProduct()
        {
            if (_psproducts.Count == 0)
                return;
            if (string.IsNullOrEmpty(SearchText))
                return;
            var olist = (from c in _psproducts
                         where c.Name.ToUpper().StartsWith(SearchText.ToUpper())
                         select c).ToList();

            if (olist.Count == 0)
            {
                SelectedCategory = Categories[0];
                Products = null;
            }

            else
            {
                SelectedCategory = (from c in Categories
                                    where c == olist[0].CategoryName
                                    select c).SingleOrDefault();
                Products = olist;
            }
            if (Products != null)
                SelectedProduct = Products[0];
        }
        private void DoSearch(object s)
        {
            if (!Helper.IsEnterKey(s))
            {
                return;
            }
            SearchProduct();



        }

        private  void DownloadFile()
        {

            PerformAction(async () => {
                var files = await _paymentsourceBusinessLogic.DownloadFilesAsync();
                if (files)
                {
                    ShowNotification("Download is successfull!~Download File",
                        null,
                        null,
                        ApplicationConstants.ButtonWarningColor
                    );
                    _psprofile = await _paymentsourceBusinessLogic.GetPSProfileAsync();
                    _psproducts = await _paymentsourceBusinessLogic.GetPSProductsAsync();
                    RemindFileDownload = false;

                }
                else
                {
                    ShowNotification("Download failed.~Download File",
                        null,
                        null,
                        ApplicationConstants.ButtonWarningColor
                    );
                }
            });
            
        }
        private async void EPTabPressed()
        {
            SelectedTab = PaymentSourceTabs.EP.ToString();
            
            NavigateService.Instance.NavigatePaymentSourceFrame(typeof(EP));
            if(_psproducts==null)
            {
                _psproducts = await  _paymentsourceBusinessLogic.GetPSProductsAsync();
                if(_psproducts==null)
                {
                    return;
                }
            }
            var olist = (from c in _psproducts
                         //where c.StoreGUI == "EP"
                         group c by c.CategoryName into g
                         select g.Key).ToList();
            
            olist.Insert(0, "All Products");
            Categories = olist;
            SelectedCategory = olist[0];
            var olist1 = (from c in _psproducts
                          //where c.StoreGUI == "EP"
                          select c).ToList();
            if (olist1.Count > 0)
            {
                Products = olist1.OrderBy(x => x.Name).ToList();
                SelectedProduct = Products[0];
            }
                
        }
        private async void RefundTabPressed()
        {
            SelectedTab = PaymentSourceTabs.REFUND.ToString();
            TransactionID = "";
            if (!string.IsNullOrEmpty(_RefNo))
                RefCode = _RefNo;
            else
                RefCode = _saleRec;
            var olist = await _paymentsourceBusinessLogic.GetPSTransactionsAsync(_cacheBussinessLogic.TillNumber, _cacheBussinessLogic.SaleNumber, 30);
            if(olist!=null && olist.Count>0)
            {
                olist = (from c in olist
                         orderby DateTime.Parse(c.SALE_DATE) descending
                         select c).ToList();
                PSTransactions = olist;
                SelectedPSTransaction = olist[0];
            }
            else
            {
                PSTransactions = null;
            }
            NavigateService.Instance.NavigatePaymentSourceFrame(typeof(Refund));
        }
        private void FileDownloadTabPressed()
        {
            SelectedTab = PaymentSourceTabs.DOWNLOAD.ToString();
            NavigateService.Instance.NavigatePaymentSourceFrame(typeof(Download));
        }
        #endregion
        #region Commands
        
        public RelayCommand EPTabPressedCommand { get; private set; }
        
        public RelayCommand FileDownloadTabPressedCommand { get; private set; }
        public RelayCommand FileDownloadCommand { get; private set; }
        public RelayCommand RefundTabPressedCommand { get; private set; }
        public RelayCommand RefundCommand { get; private set; }
        public RelayCommand QueryCommand { get; private set; }
        public RelayCommand<object> SearchCommand { get; set; }
        public RelayCommand SearchCommand1 { get; set; }
        public RelayCommand AddToSaleCommand { get; set; }
        public RelayCommand<object> CardNumberCommand { get; set; }
        public RelayCommand CardActivationCommand { get; set; }
        public RelayCommand PrintLastReceiptCommand { get; set; }
        #endregion
    }
}
