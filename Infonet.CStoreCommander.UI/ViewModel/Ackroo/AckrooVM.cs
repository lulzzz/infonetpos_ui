using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Login;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infonet.CStoreCommander.UI.Messages;
using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.View.Ackroo;
using Infonet.CStoreCommander.UI.Utility;
using System.Xml.Linq;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Windows.Storage.Streams;
using Infonet.CStoreCommander.EntityLayer.Entities.Ackroo;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;

namespace Infonet.CStoreCommander.UI.ViewModel.Ackroo
{
    public class AckrooVM : VMBase
    {
        public enum AckReqType
        {
            ACTIVATION,
            BALANCE,
            CARWASH,
            INCREASE

        }
        private readonly ICacheBusinessLogic _cacheBussinessLogic;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;
        private readonly ISaleBussinessLogic _saleBussinessLogic;
        private readonly IAckrooBusinessLogic _ackrooBusinessLogic;

        private List<string> _ReceiptHeader;
        private AckrooBalanceInfo _AckBLInfo;
        private AckReqType _CurrentType;
        private string _AckStockCode;
        private string _AckCWStockCode;
        private List<Carwash> _CarwashCategories;

        private ResourceLoader _resourceloader;

        public AckrooVM(ICacheBusinessLogic cacheBussinessLogic,
                       IReportsBussinessLogic reportsBusinessLogic,
                       IAckrooBusinessLogic ackrooBusinessLogic,
                       ISaleBussinessLogic saleBussinessLogic

            )
        {

            _cacheBussinessLogic = cacheBussinessLogic;
            _reportsBusinessLogic = reportsBusinessLogic;
            _ackrooBusinessLogic = ackrooBusinessLogic;
            _saleBussinessLogic = saleBussinessLogic;

            MessengerInstance.Register<AkrooMessage>(this, GetM);
            InitialCommands();
            EnablePrint = false;
            _CurrentType = AckReqType.BALANCE;
            BalanceInfo = null;
            _resourceloader = ResourceLoader.GetForCurrentView();





        }
        #region Methods
        private async void GetM(AkrooMessage message)
        {
            if (_ReceiptHeader == null)
            {
                _ReceiptHeader = await _reportsBusinessLogic.GetReceiptHeader();
            }
            if (_AckStockCode == null)
                _AckStockCode = await _ackrooBusinessLogic.GetAckrooStockCode();
        }
        private void InitialCommands()
        {
            AckCarwashSellCommand = new RelayCommand(CarwashSell);
            AckIncreasefundCommand = new RelayCommand(IncreaseFund);
            AckrMenuToIncreasefundCommand = new RelayCommand(ToIncreaseFund);
            AckrMenuToActivateCardCommand = new RelayCommand(ToActivateCard);
            AckrCardActivateCommand = new RelayCommand(ActivateCard);
            AckrMenuToCarwashCommand = new RelayCommand(ToCarwashSell);
            AckrCheckBalanceCommand = new RelayCommand(CheckBalance);
            CardNumberCommand = new RelayCommand<object>(CardNumberEntered);
            AmountCommand = new RelayCommand<object>(AmountEntered);
            AckrooBalanceCommand = new RelayCommand(AckrooCheckBalance);
            AckrooPrintCommand = new RelayCommand(AckrooPrint);
            BackToAckrooMainCommand = new RelayCommand(BackToAckrooMain);
        }

        private async void CarwashSell()
        {
            EnableCommand = false;
            //var socket = new Windows.Networking.Sockets.StreamSocket();
            //var serverHost = new Windows.Networking.HostName(_cacheBussinessLogic.REWARDS_TpsIp);
            try
            {
                _AckCWStockCode = await _ackrooBusinessLogic.GetAckrooCarwashStockCode(SelectedCarwashCategory);
                if (string.IsNullOrEmpty(_AckCWStockCode))
                {
                    ShowNotification("The stock code for Ackroo Carwash is not available.~Ackroo Carwash",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                    return;
                }
                if (string.IsNullOrEmpty(CarwashUnits))
                {
                    ShowNotification("Please enter the number of units for the carwash.~Ackroo Carwash",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                    return;
                }
                int iUnit;
                if (!int.TryParse(CarwashUnits, out iUnit))
                {
                    ShowNotification("Please enter a number for Carwash unit.~Ackroo Carwash",

                        null,
                        null,
                        ApplicationConstants.ButtonWarningColor
                    );
                    return;
                }

                NotifyInfo = "Processing Ackroo Carwash...";
                XDocument doc = await Helper.SockeRequest(
                     _cacheBussinessLogic.REWARDS_TpsIp,
                     _cacheBussinessLogic.REWARDS_TpsPort.ToString(),
                     TPSRequest()

                    );





                if (doc == null)
                {
                    ShowNotification("There is no response.~Ackroo Carwash",
                          null,
                          null,
                          ApplicationConstants.ButtonWarningColor
                      );
                    NotifyInfo = "";
                    return;
                }
                _AckBLInfo = GetBalaceInfo(doc);
                if (_AckBLInfo.TransactionStatus != "APPROVED")
                {
                    NotifyInfo = _AckBLInfo.ResponseMessage;
                    return;
                }

                //add product to current sale



                var result = await _saleBussinessLogic.AddStockToSale(_AckCWStockCode,
                        int.Parse(CarwashUnits),
                        false,
                        null,
                        false
                        );
                SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);
                var sale = result.ToModel();
                //update sale screen
                MessengerInstance.Send(sale, "UpdateSale");
                NotifyInfo = "The Carwash service is activated.";
                EnablePrint = true;
            }
            catch (Exception ex)
            {
                ShowNotification(ex.Message + "~Ackroo Carwash",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                NotifyInfo = "";
            }

        }

        private async void ToCarwashSell()
        {


            if (_CarwashCategories == null)
            {
                _CarwashCategories = await _ackrooBusinessLogic.GetCarwashCategories();

            }


            if (_CarwashCategories == null || _CarwashCategories.Count <= 0)
            {
                ShowNotification("Ackroo Carwash categories are not available.~Ackroo Carwash",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                return;
            }
            if (CarwashCategories == null)
            {
                CarwashCategories = new List<string>();
                foreach (var c in _CarwashCategories)
                {
                    CarwashCategories.Add(c.Category);
                }
            }
            SelectedCarwashCategory = CarwashCategories[0];
            _CurrentType = AckReqType.CARWASH;
            CarwashUnits = "1";
            NotifyInfo = null;
            CardNumber = null;
            AckrooAmount = null;
            EnablePrint = false;
            EnableCommand = true;
            NavigateService.Instance.NavigateAckrooFrame(typeof(AckrooSellCarwashUnits));
        }

        private void AmountEntered(object s)
        {
            if (!Helper.IsEnterKey(s))
            {
                return;
            }
            switch(_CurrentType)
            {
                case AckReqType.INCREASE:
                    IncreaseFund();
                    break;
                case AckReqType.CARWASH:
                    CarwashSell();
                    break;
                case AckReqType.ACTIVATION:
                    ActivateCard();
                    break;
            }
            

        }

        private async void IncreaseFund()
        {
           
            NotifyInfo = "";

            double dAmount = 0;

            try
            {

                if (string.IsNullOrEmpty(CardNumber))
                {

                    //ShowNotification("Card number is not entered.~Increase Fund",

                    //       null,
                    //       null,
                    //       ApplicationConstants.ButtonWarningColor
                    //   );
                    return;
                }

                if (!double.TryParse(AckrooAmount, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dAmount))
                {
                    //ShowNotification("The amount format is invalid.~Increase Fund",

                    //       null,
                    //       null,
                    //       ApplicationConstants.ButtonWarningColor
                    //   );
                    return;
                }
                EnableCommand = false;
                NotifyInfo = "Processing...";
                XDocument doc = await Helper.SockeRequest(
                    _cacheBussinessLogic.REWARDS_TpsIp,
                    _cacheBussinessLogic.REWARDS_TpsPort.ToString(),
                    TPSRequest()
                    );

                if (doc == null)
                {
                    ShowNotification("There is no response.~Increase Fund",
                          null,
                          null,
                          ApplicationConstants.ButtonWarningColor
                      );
                    NotifyInfo = "";
                    return;
                }
                _AckBLInfo = GetBalaceInfo(doc);
                if (_AckBLInfo.TransactionStatus != "APPROVED")
                {
                    NotifyInfo = _AckBLInfo.ResponseMessage;
                    return;
                }

                //add product to current sale



                var result = await _saleBussinessLogic.AddStockToSale(_AckStockCode,
                        1,
                        false,
                        null,
                        false
                        );



                SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);
                var sale = result.ToModel();
                SaleLineModel sl = (SaleLineModel)sale.SaleLines[sale.SaleLines.Count - 1];
                var saleupdate = await _saleBussinessLogic.UpdateSale(
                         sl.LineNumber,
                         "",
                         "",
                         "1",
                         string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", double.Parse(AckrooAmount, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture)),
                         "",
                         ""
                        );

                //update sale screen
                MessengerInstance.Send(saleupdate.ToModel(), "UpdateSale");

                NotifyInfo = "The action is successfully completed.";
                EnablePrint = true;
            }
            catch (Exception ex)
            {
                ShowNotification(ex.Message + "~Increase Fund",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                NotifyInfo = "";
            }



        }

        private async void ActivateCard()
        {
            
            NotifyInfo = "";

            double dAmount = 0;

            try
            {

                if (string.IsNullOrEmpty(CardNumber))
                {

                    ShowNotification("Card number is not entered.~Activate Card",

                           null,
                           null,
                           ApplicationConstants.ButtonWarningColor
                       );
                    return;
                }

                if (!double.TryParse(AckrooAmount, out dAmount))
                {
                    ShowNotification("The amount format is invalid.~Activate Card",

                           null,
                           null,
                           ApplicationConstants.ButtonWarningColor
                       );
                    return;
                }
                EnableCommand = false;
                NotifyInfo = "Processing...";

                XDocument doc = await Helper.SockeRequest(
                    _cacheBussinessLogic.REWARDS_TpsIp,
                    _cacheBussinessLogic.REWARDS_TpsPort.ToString(),
                    TPSRequest()
                    );





                if (doc == null)
                {
                    ShowNotification("There is no response.~Activate Card",
                          null,
                          null,
                          ApplicationConstants.ButtonWarningColor
                      );
                    NotifyInfo = "";
                    return;
                }
                _AckBLInfo = GetBalaceInfo(doc);
                if (_AckBLInfo.TransactionStatus != "APPROVED")
                {
                    NotifyInfo = _AckBLInfo.ResponseMessage;

                    return;
                }

                //add product to current sale

                if (dAmount > 0)
                {
                    var result = await _saleBussinessLogic.AddStockToSale(_AckStockCode,
                        1,
                        false,
                        null,
                        false
                        );



                    SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);
                    var sale = result.ToModel();
                    SaleLineModel sl = (SaleLineModel)sale.SaleLines[sale.SaleLines.Count - 1];
                    var saleupdate = await _saleBussinessLogic.UpdateSale(
                             sl.LineNumber,
                             "",
                             "",
                             "1",
                             string.Format("{0:0.00}", double.Parse(AckrooAmount)),
                             "",
                             ""
                            );

                    //update sale screen
                    MessengerInstance.Send(saleupdate.ToModel(), "UpdateSale");


                }

                NotifyInfo = "The card is successfully activated.";
                EnablePrint = true;
            }
            catch (Exception ex)
            {
                ShowNotification(ex.Message + "~Activate Card",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                NotifyInfo = "";
            }



        }
        private async void ToActivateCard()
        {
            if (_AckStockCode == null)
                _AckStockCode = await _ackrooBusinessLogic.GetAckrooStockCode();
            if (string.IsNullOrEmpty(_AckStockCode))
            {
                ShowNotification("Ackroo Stock is not found.~Ackroo Stock",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                return;
            }
            _CurrentType = AckReqType.ACTIVATION;
            NotifyInfo = null;
            CardNumber = null;
            AckrooAmount = "0.00";
            EnablePrint = false;
            EnableCommand = true;

            NavigateService.Instance.NavigateAckrooFrame(typeof(AckrooCardActivation));
        }
        private async void ToIncreaseFund()
        {
            if (_AckStockCode == null)
                _AckStockCode = await _ackrooBusinessLogic.GetAckrooStockCode();
            if (string.IsNullOrEmpty(_AckStockCode))
            {
                ShowNotification("Ackroo Stock is not found.~Ackroo Stock",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                return;
            }
            _CurrentType = AckReqType.INCREASE;
            NotifyInfo = null;
            CardNumber = null;
            AckrooAmount = null;
            EnablePrint = false;
            EnableCommand = true;
            Ackr_Label1 = _resourceloader.GetString("Ackr_Label1_IncreaseFund");
            NavigateService.Instance.NavigateAckrooFrame(typeof(AckrooIncreaseFund));
        }

        private void BackToAckrooMain()
        {

            MessengerInstance.Send(new CloseKeyboardMessage());
            NavigateService.Instance.NavigateAckrooFrame(typeof(AckrooMenu));

        }
        internal void ResetVM()
        {
            NavigateService.Instance.NavigateAckrooFrame(typeof(AckrooMenu));
        }
        private async void AckrooPrint()
        {
            string sLine = string.Empty;
            string param1 = string.Empty;
            string param2 = string.Empty;
            try
            {
                if (_ReceiptHeader == null)
                {
                    _ReceiptHeader = await _reportsBusinessLogic.GetReceiptHeader();
                }
                List<string> olist = new List<string>();
                foreach (var c in _ReceiptHeader)
                {
                    olist.Add(c);
                }

                olist.Add("");
                olist.Add("");
                param1 = "Date: " + string.Format("{0:MM/dd/yyyy}", DateTime.Now);
                param2 = "Time:" + string.Format("{0:HH:mm:ss}", DateTime.Now);
                sLine = string.Format("{0,-18}  {1,-18}", param1, param2);
                olist.Add(sLine);
                if (("CASHIER-" + _cacheBussinessLogic.UserName).Length > 9)
                    param1 = "UserID: " + _cacheBussinessLogic.UserName;
                else
                    param1 = "UserID: CASHIER-" + _cacheBussinessLogic.UserName;
                param2 = "Line: " + _cacheBussinessLogic.TillNumber + "/" + _cacheBussinessLogic.ShiftNumber;
                sLine = string.Format("{0,-18}  {1,-18}", param1, param2);
                olist.Add(sLine);
                olist.Add("");
                switch (_CurrentType)
                {
                    case AckReqType.ACTIVATION:
                        olist.Add("Ackroo Trans Type: Activation");
                        olist.Add("");
                        olist.Add("Trans No: " + _cacheBussinessLogic.SaleNumber);
                        olist.Add("");
                        olist.Add("Ackroo Card #: " + GetGiveXMaskNum(CardNumber));
                        olist.Add("Expiry Date: ");
                        olist.Add("");
                        param1 = "Gift Balace:";
                        param2 = _AckBLInfo.GiftBalance;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);
                        param1 = "Loyalty Balance:";
                        param2 = _AckBLInfo.LoyaltyBalace;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);

                        break;
                    case AckReqType.BALANCE:

                        olist.Add("Ackroo Trans Type: Check Balance");
                        olist.Add("");
                        olist.Add("Trans No: " + _cacheBussinessLogic.SaleNumber);
                        olist.Add("");
                        olist.Add("Ackroo Card #: " + GetGiveXMaskNum(CardNumber));
                        olist.Add("Expiry Date: ");
                        olist.Add("");
                        param1 = "Gift Balace:";
                        param2 = _AckBLInfo.GiftBalance;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);
                        param1 = "Loyalty Balance:";
                        param2 = _AckBLInfo.LoyaltyBalace;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);
                        foreach (var c in _AckBLInfo.Categories)
                        {
                            sLine = string.Format("{0,20} {1,10}", c.Name + ":", c.Value);
                            olist.Add(sLine);
                        }


                        break;
                    case AckReqType.INCREASE:
                        olist.Add("Ackroo Trans Type: Increment");
                        olist.Add("");
                        olist.Add("Trans No: " + _cacheBussinessLogic.SaleNumber);
                        olist.Add("");
                        olist.Add("Ackroo Card #: " + GetGiveXMaskNum(CardNumber));
                        olist.Add("Expiry Date: ");
                        olist.Add("");
                        param1 = "Gift Balace:";
                        param2 = _AckBLInfo.GiftBalance;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);
                        param1 = "Loyalty Balance:";
                        param2 = _AckBLInfo.LoyaltyBalace;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);
                        param1 = "Increment Amount:";
                        param2 = _AckBLInfo.Increment;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);

                        break;
                    case AckReqType.CARWASH:
                        olist.Add("Ackroo Trans Type: Activation");
                        olist.Add("");
                        olist.Add("Trans No: " + _cacheBussinessLogic.SaleNumber);
                        olist.Add("");
                        olist.Add("Ackroo Card #: " + GetGiveXMaskNum(CardNumber));
                        olist.Add("Expiry Date: ");
                        olist.Add("");
                        param1 = "Gift Balace:";
                        param2 = _AckBLInfo.GiftBalance;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);
                        param1 = "Loyalty Balance:";
                        param2 = _AckBLInfo.LoyaltyBalace;
                        sLine = string.Format("{0,20} {1,10}", param1, param2);
                        olist.Add(sLine);
                        break;
                }
                olist.Add("");
                olist.Add("");
                param1 = "APPROVED";
                sLine = string.Format("              {0,4}", param1);
                olist.Add(sLine);
                olist.Add("");
                olist.Add("");
                param1 = "Thank You!";
                sLine = string.Format("              {0,4}", param1);
                olist.Add(sLine);
                await PerformPrint(olist, 1, true, null);
            }
            catch (Exception ex)
            {
                EnablePrint = false;
                ShowNotification(ex.Message + "~Print Receipt",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );

            }
        }

        private async void AckrooCheckBalance()
        {
            BalanceInfo = "";
            NotifyInfo = "";
            if (string.IsNullOrEmpty(CardNumber))
            {
                ShowNotification("Card number is not entered.~Balance Request",

                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                return;
            }
            EnableCommand = false;
            try
            {

                BalanceInfo = "Requesting Card Balance...";
                XDocument doc = await Helper.SockeRequest(
                       _cacheBussinessLogic.REWARDS_TpsIp,
                       _cacheBussinessLogic.REWARDS_TpsPort.ToString(),
                       TPSRequest()
                    );







                if (doc == null)
                {
                    ShowNotification("There is no response.~Balance Request",
                          null,
                          null,
                          ApplicationConstants.ButtonWarningColor
                      );
                    BalanceInfo = "";
                    return;
                }
                _AckBLInfo = GetBalaceInfo(doc);

                if (_AckBLInfo.TransactionStatus != "APPROVED")
                {
                    BalanceInfo = _AckBLInfo.ResponseMessage;
                    return;
                }
                EnablePrint = true;
                StringBuilder sb = new StringBuilder();
                string sVal = "Ackroo Card Status:".ToUpper();
                string sTemp = string.Format("{0,25}  {1,-15}", sVal, _AckBLInfo.Active);

                sb.Append(sTemp + Environment.NewLine);
                sVal = "Loyalty Balance:".ToUpper();
                sTemp = string.Format("{0,30}  {1,-15}", sVal, _AckBLInfo.LoyaltyBalace);

                sb.Append(sTemp + Environment.NewLine);
                sVal = "Gift Balance:".ToUpper();
                sTemp = string.Format("{0,34}  {1,-15}", sVal, _AckBLInfo.GiftBalance);

                sb.Append(sTemp + Environment.NewLine);

                foreach (var c in _AckBLInfo.Categories)
                {
                    sTemp = string.Format("{0,27}  {1,-15}", (c.Name.ToUpper() + ":"), c.Value);

                    sb.Append(sTemp + Environment.NewLine);
                }
                BalanceInfo = sb.ToString();
            }
            catch (Exception ex)
            {
                BalanceInfo = "";

                ShowNotification(ex.Message + "~Balance Request",
                           null,
                           null,
                           ApplicationConstants.ButtonWarningColor
                       );

            }

        }
        private AckrooBalanceInfo GetBalaceInfo(XDocument doc)
        {
            AckrooBalanceInfo ackbalance = new AckrooBalanceInfo();
            XElement xe;
            xe = GetElement(doc, "TransactionStatus");
            if (xe != null)
                ackbalance.TransactionStatus = xe.Value;

            xe = GetElement(doc, "ResponseMessage");
            if (xe != null)
                ackbalance.ResponseMessage = xe.Value;

            switch (_CurrentType)
            {
                case AckReqType.ACTIVATION:
                    xe = GetElement(doc, "AmountFunded");
                    if (xe != null)
                        if (!string.IsNullOrEmpty(xe.Value))
                            ackbalance.AmountFunded = xe.Value;
                    xe = GetElement(doc, "ElBalance");
                    if (xe != null && !string.IsNullOrEmpty(xe.Value))
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", double.Parse(xe.Value));
                    else
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", 0);
                    xe = GetElement(doc, "GcBalance");
                    if (xe != null && !string.IsNullOrEmpty(xe.Value))
                        ackbalance.GiftBalance = string.Format("{0:C2}", double.Parse(xe.Value));
                    else
                        ackbalance.GiftBalance = string.Format("{0:C2}", 0);

                    break;
                case AckReqType.BALANCE:
                    xe = GetElement(doc, "Active");
                    if (xe != null)
                        if (Boolean.Parse(xe.Value) == true)
                            ackbalance.Active = "Active";
                        else
                            ackbalance.Active = "InActive";

                    xe = GetElement(doc, "Gift");
                    if (xe != null)
                        ackbalance.GiftBalance = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));

                    xe = GetElement(doc, "Loyalty");
                    if (xe != null)
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));

                    XNamespace ns = doc.Root.GetDefaultNamespace();
                    IEnumerable<XElement> clist = from c in doc.Descendants(ns + "Carwash")
                                                  select c;
                    AckrooItem ackitem = null;
                    int iCount;
                    if (clist != null)
                    {
                        ackbalance.Categories = new List<AckrooItem>();
                        foreach (XElement item in clist)
                        {
                            iCount = 0;
                            foreach (XElement xxe in item.Elements())
                            {
                                if (iCount == 0 && ackitem == null)
                                {
                                    ackitem = new AckrooItem();
                                }
                                if (xxe.Name.LocalName == "Category")
                                {
                                    ackitem.Name = xxe.Value;

                                    iCount++;
                                }
                                if (xxe.Name.LocalName == "Balance")
                                {
                                    ackitem.Value = xxe.Value;
                                    iCount++;
                                }
                                if (iCount == 2)
                                {
                                    iCount = 0;
                                    ackbalance.Categories.Add(ackitem);
                                    ackitem = null;
                                }

                            }

                        }
                    }
                    break;
                case AckReqType.INCREASE:
                    xe = GetElement(doc, "AmountFunded");
                    if (xe != null)
                        ackbalance.Increment = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                    xe = GetElement(doc, "ElBalance");
                    if (xe != null)
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                    xe = GetElement(doc, "GcBalance");
                    if (xe != null)
                        ackbalance.GiftBalance = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                    break;
                case AckReqType.CARWASH:
                    xe = GetElement(doc, "ElBalance");
                    if (xe != null)
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                    xe = GetElement(doc, "GcBalance");
                    if (xe != null)
                        ackbalance.GiftBalance = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                    break;
            }



            return ackbalance;
        }
        private string TPSRequest()
        {
            string sRequest = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            string sPrefix_Junk = "11";
            XDocument doc = null;
            double amt = 0;
            switch (_CurrentType)
            {
                case AckReqType.BALANCE:
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                           new XElement(
                               "AckrooTPSRequest",
                               new XElement("Transaction",
                             new XAttribute("Type", "PreAuth"),
                             null
                             ),
                            new XElement("Source",
                            new XAttribute("Type", "POS"
                                ),
                            null
                            ),
                            new XElement("Lane", _cacheBussinessLogic.TillNumber),
                            new XElement("InvoiceNo", _cacheBussinessLogic.SaleNumber),
                            new XElement("Card",
                             new XElement("Type", "Loyalty"
                                 ),
                             new XElement("Track2", GetCardNum()),

                             null)
                               )
                        );
                    sRequest += doc.ToString();

                    break;
                case AckReqType.ACTIVATION:
                    amt = double.Parse(AckrooAmount, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                           new XElement(
                               "AckrooTPSRequest",
                               new XElement("Transaction",
                             new XAttribute("Type", "Activate"),
                             null
                             ),
                            new XElement("Source",
                            new XAttribute("Type", "POS"
                                ),
                            null
                            ),
                            new XElement("Lane", _cacheBussinessLogic.TillNumber),
                            new XElement("InvoiceNo", _cacheBussinessLogic.SaleNumber),
                            new XElement("RequestAmount", string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", amt)),
                            new XElement("Card",
                             new XElement("Type", "GIFT"
                                 ),
                             new XElement("Track2", GetCardNum()),

                             null)
                               )
                        );
                    sRequest += doc.ToString();

                    break;
                case AckReqType.INCREASE:
                    amt = double.Parse(AckrooAmount, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                           new XElement(
                               "AckrooTPSRequest",
                               new XElement("Transaction",
                             new XAttribute("Type", "CREDIT"),
                             null
                             ),
                            new XElement("Source",
                            new XAttribute("Type", "POS"
                                ),
                            null
                            ),
                            new XElement("Lane", _cacheBussinessLogic.TillNumber),
                            new XElement("InvoiceNo", _cacheBussinessLogic.SaleNumber),
                            new XElement("RequestAmount", string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", amt)),
                            new XElement("Card",
                             new XElement("Type", "GIFT"
                                 ),
                             new XElement("Track2", GetCardNum()),

                             null)
                               )
                        );
                    sRequest += doc.ToString();
                    break;

                case AckReqType.CARWASH:
                    var uid = (from c in _CarwashCategories
                               where c.Category == SelectedCarwashCategory
                               select c.UnitId).Single();
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                           new XElement(
                               "AckrooTPSRequest",
                               new XElement("Transaction",
                             new XAttribute("Type", "CREDIT"),
                             null
                             ),
                            new XElement("Source",
                            new XAttribute("Type", "POS"
                                ),
                            null
                            ),
                            new XElement("Lane", _cacheBussinessLogic.TillNumber),
                            new XElement("InvoiceNo", _cacheBussinessLogic.SaleNumber),
                             new XElement("Card",
                             new XElement("Type", "Carwash"
                                 ),
                             new XElement("Track2", GetCardNum()),

                             null),
                             new XElement("NoOfUnits", CarwashUnits),
                             new XElement("CarWash",
                                new XElement("UnitID", uid)
                             )
                               )
                        );
                    sRequest += doc.ToString();
                    break;

            }

            return sPrefix_Junk + sRequest;
        }
        private string GetGiveXMaskNum(string CardNumber)
        {
            string cc = CardNumber;
            cc = cc.Replace(";", "");
            cc = cc.Replace("?", "");
            if (cc.Length <= 6)
                return cc;
            if (cc.Length <= 12)
                return cc.Substring(0, 6) + "*".PadLeft(cc.Length - 6, '*');
            else
            {
                string sval = cc.Substring(0, 6) + "*".PadLeft(5, '*') + cc.Substring(11, cc.Length - 12) + "*";
                return sval;
            }
        }
        private string GetCardNum()
        {
            string cn = CardNumber;
            cn = cn.Replace(";", "");
            cn = cn.Replace("?", "");
            cn = ";" + cn + "=?";
            return cn;
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
        private void CardNumberEntered(object s)
        {
            if (!Helper.IsEnterKey(s))
            {
                return;
            }
            // Eliminating track1 over here
            //if (Helper.IsEnterKey(s) && CardNumber.IndexOf('?') != -1 &&
            //    CardNumber.IndexOf('?') == CardNumber.LastIndexOf('?'))
            //{
            //    return;
            //}

            //ShowNotification(CardNumber + "~CardNumber", null, null, ApplicationConstants.ButtonWarningColor);
            switch (_CurrentType)
            {
                case AckReqType.BALANCE:
                    AckrooCheckBalance();
                    break;
            }
        }

        private void CheckBalance()
        {
            _CurrentType = AckReqType.BALANCE;
            BalanceInfo = null;
            CardNumber = null;
            EnablePrint = false;
            EnableCommand = true;
            NavigateService.Instance.NavigateAckrooFrame(typeof(AckrooBalance));
            //string sTemp = "";
            //string sLine = "";
            //string param1 = "";
            //string param2 = "";
            //if(_ReceiptHeader!=null)
            //{
            //    List<string> olist = new List<string>();
            //    foreach(var c in _ReceiptHeader)
            //    {
            //        olist.Add(c);
            //    }

            //    olist.Add("");
            //    olist.Add("");
            //    param1 ="Date: "+ string.Format("{0:MM/dd/yyyy}", DateTime.Now);
            //    param2 = "Time:" + string.Format("{0:HH:mm:ss}", DateTime.Now);
            //    sLine = string.Format("{0,-18}  {1,-18}", param1, param2);
            //    //sTemp = "Date:" + string.Format("{0:MM/dd/yyyy}", DateTime.Now) + "  Time:" + string.Format("{0:HH:mm:ss}", DateTime.Now);
            //    olist.Add(sLine);
            //    if (("CASHIER-" + _cacheBussinessLogic.UserName).Length > 9)
            //        param1 = "UserID: " + _cacheBussinessLogic.UserName;
            //    else
            //        param1 = "UserID: CASHIER-" + _cacheBussinessLogic.UserName;
            //    param2 = "Line: " + _cacheBussinessLogic.TillNumber + "/" + _cacheBussinessLogic.ShiftNumber;
            //    sLine = string.Format("{0,-18}  {1,-18}", param1, param2);
            //    olist.Add(sLine);
            //    olist.Add("");
            //    olist.Add("Ackroo Trans Type: Check Balance");
            //    olist.Add("");
            //    olist.Add("Trans No: " + _cacheBussinessLogic.SaleNumber);
            //    olist.Add("");
            //    olist.Add("Ackroo Card #: 612573*****255765*");
            //    olist.Add("Expiry Date: ");
            //    olist.Add("");
            //    param1 = "Gift Balace:";
            //    param2 = "$0.00";
            //    sLine = string.Format("{0,20} {1,10}", param1, param2);
            //    olist.Add(sLine);
            //    param1 = "Loyalty Balance:";
            //    param2 = "$21,045.06";
            //    sLine = string.Format("{0,20} {1,10}", param1, param2);
            //    olist.Add(sLine);
            //    param1 = "CAR WASH - BASIC:";
            //    param2 = "134";
            //    sLine = string.Format("{0,20} {1,10}", param1, param2);
            //    olist.Add(sLine);
            //    param1 = "CAR WASH - PREMIUM:";
            //    param2 = "307";
            //    sLine = string.Format("{0,20} {1,10}", param1, param2);
            //    olist.Add(sLine);
            //    olist.Add("");
            //    olist.Add("");
            //    param1 = "APPROVED";
            //    sLine = string.Format("                  {0,4}", param1);
            //    olist.Add(sLine);
            //    olist.Add("");
            //    olist.Add("");
            //    param1 = "Thank You!";
            //    sLine = string.Format("                  {0,4}", param1);
            //    olist.Add(sLine);
            //    PerformPrint(olist, 1, true, null);

            //}
        }
        #endregion
        #region Commands
        public RelayCommand AckCarwashSellCommand { get; set; }
        public RelayCommand AckIncreasefundCommand { get; set; }
        public RelayCommand AckrMenuToCarwashCommand { get; set; }
        public RelayCommand AckrMenuToIncreasefundCommand { get; set; }
        public RelayCommand AckrMenuToActivateCardCommand { get; set; }
        public RelayCommand AckrCardActivateCommand { get; set; }
        public RelayCommand AckrCheckBalanceCommand { get; set; }
        public RelayCommand<object> AmountCommand { get; set; }
        public RelayCommand<object> CardNumberCommand { get; set; }
        public RelayCommand AckrooBalanceCommand { get; set; }
        public RelayCommand AckrooPrintCommand { get; set; }
        public RelayCommand BackToAckrooMainCommand { get; set; }
        #endregion
        #region Properties
        private string _ackr_label1;

        public string Ackr_Label1
        {
            get { return _ackr_label1; }
            set
            {
                _ackr_label1 = value;
                RaisePropertyChanged(nameof(Ackr_Label1));
            }
        }

        private string _carwashunits;

        public string CarwashUnits
        {
            get { return _carwashunits; }
            set
            {
                _carwashunits = value;
                RaisePropertyChanged(nameof(CarwashUnits));
            }
        }

        private string _selectedCarwashCategory;

        public string SelectedCarwashCategory
        {
            get { return _selectedCarwashCategory; }
            set
            {
                _selectedCarwashCategory = value;
                RaisePropertyChanged(nameof(SelectedCarwashCategory));
            }
        }

        private List<string> _carwashCategories;

        public List<string> CarwashCategories
        {
            get { return _carwashCategories; }
            set
            {
                _carwashCategories = value;
                RaisePropertyChanged(nameof(CarwashCategories));
            }
        }

        private string _ackrooamount;

        public string AckrooAmount
        {
            get { return _ackrooamount; }
            set
            {
                _ackrooamount = value;
                RaisePropertyChanged(nameof(AckrooAmount));
            }
        }


        private string _cardNumber;

        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                RaisePropertyChanged(nameof(CardNumber));
            }
        }
        private string _notify;

        public string NotifyInfo
        {
            get { return _notify; }
            set
            {
                _notify = value;
                RaisePropertyChanged(nameof(NotifyInfo));
            }
        }
        private string _balanceinfo;

        public string BalanceInfo
        {
            get { return _balanceinfo; }
            set
            {
                _balanceinfo = value;
                RaisePropertyChanged(nameof(BalanceInfo));
            }
        }
        private bool _disablePrinter;

        private bool _enableCommand;

        public bool EnableCommand
        {
            get { return _enableCommand; }
            set
            {
                _enableCommand = value;
                RaisePropertyChanged(nameof(EnableCommand));
            }
        }


        private bool _enableprint;

        public bool EnablePrint
        {
            get { return _enableprint; }
            set
            {
                _enableprint = value;
                RaisePropertyChanged(nameof(EnablePrint));
            }
        }

        #endregion

    }
}
