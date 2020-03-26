using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Ackroo;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Core;

namespace Infonet.CStoreCommander.UI.ViewModel.Ackroo
{
    public class AckrooTenderVM : VMBase
    {
        public enum AckReqType
        {
            ACTIVATION,
            BALANCE,
            CARWASH,
            INCREASE,
            REDEEM

        }
        private readonly ICacheBusinessLogic _cacheBussinessLogic;
        private readonly IAckrooBusinessLogic _ackrooBusinessLogic;
        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;
        private CheckoutSummary _checkoutSummary;

        private AckrooBalanceInfo _AckBLInfo;
        private double _outStandingAmount;
        private double _loyaltyBalanceAmount;
        private double _giftBalanceAmount;
        public AckrooTenderVM(IAckrooBusinessLogic ackrooBusinessLogic,
            ICacheBusinessLogic cacheBussinessLogic,
            ICheckoutBusinessLogic checkoutBusinessLogic
            )
        {
            _cacheBussinessLogic = cacheBussinessLogic;
            _ackrooBusinessLogic = ackrooBusinessLogic;
            _checkoutBusinessLogic = checkoutBusinessLogic;
            MessengerInstance.Register<AckrooTenderMessage>(this, DeleySecondes);
            MessengerInstance.Register<AckrooAccoutBalanceMessage>(this, AccountBalance);
            MessengerInstance.Register<AckrooOutStandingAmtChangeMessage>(this, UpdateOutStanding);
            InitialCommands();
        }




        #region Methods
        private void UpdateOutStanding(AckrooOutStandingAmtChangeMessage obj)
        {
            _outStandingAmount = obj.NewOutStandingAmount;
            string sVal = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", _outStandingAmount);
            OutStandingBalance = string.Format(ApplicationConstants.AckrooOutStandingMessage, sVal);
        }
        private string TPSRequest(AckReqType AckType, string RedeemType = null, double? amount = null)
        {
            string sRequest = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            string sPrefix_Junk = "11";
            XDocument doc = null;
            switch (AckType)
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
                case AckReqType.REDEEM:
                    doc = new XDocument(
                           new XDeclaration("1.0", "UTF-8", "yes"),
                           new XElement(
                               "AckrooTPSRequest",
                               new XElement("Transaction",
                             new XAttribute("Type", "REDEEM"),
                             null
                             ),
                            new XElement("Source",
                            new XAttribute("Type", "POS"
                                ),
                            null
                            ),
                            new XElement("Lane", _cacheBussinessLogic.TillNumber),
                            new XElement("InvoiceNo", _cacheBussinessLogic.SaleNumber),
                            new XElement("RequestAmount", string.Format("{0:0.00}", amount)),
                            new XElement("Card",
                             new XElement("Type", RedeemType
                                 ),
                             new XElement("Track2", GetCardNum()),

                             null)
                               )
                        );
                    sRequest += doc.ToString();
                    break;


            }

            return sPrefix_Junk + sRequest;
        }
        private string GetCardNum()
        {
            string cn = CardNumber;
            cn = cn.Replace(";", "");
            cn = cn.Replace("?", "");
            cn = ";" + cn + "=?";
            return cn;
        }
        private async void DeleySecondes(AckrooTenderMessage obj)
        {
            _checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
            _outStandingAmount = double.Parse(_checkoutSummary.TenderSummary.OutstandingAmount, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
            IsEnabled = false;
            await Task.Delay(5000).ContinueWith(x => bar());
        }
        private void AccountBalance(AckrooAccoutBalanceMessage obj)
        {
            IsBalanceOKEnabled = false;
            RedeemAmountByGift = "";
            RedeemAmountByLoyalty = "";
            Task.Delay(5000).ContinueWith(x => bar1());


            string sVal = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", _outStandingAmount);
            OutStandingBalance = string.Format(ApplicationConstants.AckrooOutStandingMessage, sVal);
            List<AckrooItem> olist = new List<AckrooItem>();
            AckrooItem ai;
            ai = new AckrooItem();
            ai.Name = "Loyalty Balance";
            ai.Value = _AckBLInfo.LoyaltyBalace;
            olist.Add(ai);
            ai = new AckrooItem();
            ai.Name = "Gift Balance";
            ai.Value = _AckBLInfo.GiftBalance;
            olist.Add(ai);
            foreach (var c in _AckBLInfo.Categories)
            {
                olist.Add(c);
            }
            BalanceItems = olist;
            if (BalanceItems.Count > 0)
                SelectedBalanceItem = BalanceItems[0];
        }
        private async void bar1()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => { IsBalanceOKEnabled = true; });
        }
        private async void bar()
        {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => { IsEnabled = true; });
        }
        private void InitialCommands()
        {
            ClosePopupCommand = new RelayCommand(ClosePopup);
            CardNumberCommand = new RelayCommand<object>(CardNumberEntered);
            BalanceOKCommand = new RelayCommand(BalanceOK);

        }

        public void SelectedItemChaged()
        {
            if (SelectedBalanceItem == null)
                return;
            if (SelectedBalanceItem.Name != "Loyalty Balance" && SelectedBalanceItem.Name != "Gift Balance")
                return;

            double inputedamount = InputedAmount();
            double dVal;
            double dTemp;
            if (SelectedBalanceItem.Name == "Loyalty Balance")
            {
                if (_loyaltyBalanceAmount == 0)
                    RedeemAmountByLoyalty = "0";
                else
                {
                    if (_outStandingAmount > inputedamount)
                    {
                        dVal = _outStandingAmount - inputedamount;
                        if (_loyaltyBalanceAmount >= dVal)
                        {
                            if (!double.TryParse(RedeemAmountByLoyalty, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dTemp))
                                dTemp = 0;
                            if (dTemp == 0)
                                RedeemAmountByLoyalty = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", dVal);
                        }

                        else
                        {
                            if (!double.TryParse(RedeemAmountByLoyalty, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dTemp))
                                dTemp = 0;
                            if (dTemp == 0)
                                RedeemAmountByLoyalty = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", _loyaltyBalanceAmount);
                        }

                    }

                }
            }
            if (SelectedBalanceItem.Name == "Gift Balance")
            {
                if (_giftBalanceAmount == 0)
                    RedeemAmountByGift = "0";
                else
                {
                    if (_outStandingAmount > inputedamount)
                    {
                        dVal = _outStandingAmount - inputedamount;
                        if (_giftBalanceAmount >= dVal)
                        {
                            if (!double.TryParse(RedeemAmountByGift, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dTemp))
                                dTemp = 0;
                            if (dTemp == 0)
                                RedeemAmountByGift = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", dVal);

                        }
                        else
                        {
                            if (!double.TryParse(RedeemAmountByGift, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dTemp))
                                dTemp = 0;
                            if (dTemp == 0)
                                RedeemAmountByGift = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", _giftBalanceAmount);
                        }

                    }

                }
            }

        }
        private double InputedAmount()
        {
            double loyaltyamount = 0;
            double giftamount = 0;
            if (!double.TryParse(RedeemAmountByLoyalty, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out loyaltyamount))
            {
                loyaltyamount = 0;
                RedeemAmountByLoyalty = "0";
            }
            if (!double.TryParse(RedeemAmountByGift, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out giftamount))
            {
                giftamount = 0;
                RedeemAmountByGift = "0";
            }
            return loyaltyamount + giftamount;
        }
        private async void BalanceOK()
        {

            double dLoyaltyAmount;
            double dGiftAmount;
            AckrooBalanceInfo AckInfo;

            //Validate the redeem amount for loyalty
            if (!double.TryParse(RedeemAmountByLoyalty, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dLoyaltyAmount))
                dLoyaltyAmount = 0;
            if (dLoyaltyAmount > _loyaltyBalanceAmount)
            {

                ShowNotification("Loyalty redeem amount can't exceed the available loyalty balance!~Redeem Request",
                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );

                return;
            }
            //Validate the redeem amount for gift
            if (!double.TryParse(RedeemAmountByGift, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dGiftAmount))
                dGiftAmount = 0;
            if (dGiftAmount > _giftBalanceAmount)
            {

                ShowNotification("Gift redeem amount can't exceed the available gift balance!~Redeem Request",
                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );
                return;

            }

            if (InputedAmount() > _outStandingAmount)
            {

                ShowNotification("Redeem amount can't exceed the outstanding amount!~Redeem Request",
                       null,
                       null,
                       ApplicationConstants.ButtonWarningColor
                   );

                return;
            }
            XDocument doc;

            //Loyalty Redeem
            if (dLoyaltyAmount > 0)
                try
                {
                    NotifyInfo = "Requesting for Loyalty Redeem...";
                    doc = await Helper.SockeRequest(
                           _cacheBussinessLogic.REWARDS_TpsIp,
                           _cacheBussinessLogic.REWARDS_TpsPort.ToString(),
                           TPSRequest(AckReqType.REDEEM, "LOYALTY", dLoyaltyAmount)
                        );
                    NotifyInfo = "";
                    if (doc == null)
                    {

                        ShowNotification("There is no response.~Loyalty Redeem",
                              null,
                              null,
                              ApplicationConstants.ButtonWarningColor
                          );


                    }
                    else
                    {
                        AckInfo = GetBalaceInfo(doc, AckReqType.REDEEM);
                        if (AckInfo.TransactionStatus != "APPROVED")
                        {
                            ShowNotification(AckInfo.ResponseMessage + "~Loyalty Redeem",
                              null,
                              null,
                              ApplicationConstants.ButtonWarningColor
                          );
                        }
                        else
                        {
                            MessengerInstance.Send(new AckrooTenderPaymentMessage { TenderCode = "ACK", Amount = (decimal)dLoyaltyAmount });
                        }
                    }

                }
                catch (Exception ex)
                {

                    ShowNotification(ex.Message + "~Loyalty Redeem",
                          null,
                          null,
                          ApplicationConstants.ButtonWarningColor
                      );


                }
            //Gift Redeem
            if (dGiftAmount > 0)
                try
                {
                    NotifyInfo = "Requesting for Gift Redeem...";
                    doc = await Helper.SockeRequest(
                           _cacheBussinessLogic.REWARDS_TpsIp,
                           _cacheBussinessLogic.REWARDS_TpsPort.ToString(),
                           TPSRequest(AckReqType.REDEEM, "GIFT", dGiftAmount)
                        );
                    NotifyInfo = "";
                    if (doc == null)
                    {

                        ShowNotification("There is no response.~Gift Redeem",
                              null,
                              null,
                              ApplicationConstants.ButtonWarningColor
                          );


                    }
                    else
                    {
                        AckInfo = GetBalaceInfo(doc, AckReqType.REDEEM);
                        if (AckInfo.TransactionStatus != "APPROVED")
                        {
                            ShowNotification(AckInfo.ResponseMessage + "~Gift Redeem",
                              null,
                              null,
                              ApplicationConstants.ButtonWarningColor
                          );
                        }
                        else
                        {
                            MessengerInstance.Send(new AckrooTenderPaymentMessage { TenderCode = "ACKG", Amount = (decimal)dGiftAmount });
                        }
                    }

                }
                catch (Exception ex)
                {

                    ShowNotification(ex.Message + "~Gift Redeem",
                          null,
                          null,
                          ApplicationConstants.ButtonWarningColor
                      );

                    return;
                }
            CardNumber = "";
            PopupService.IsAckBalacePopOpen = false;
            PopupService.IsPopupOpen = false;
        }

        private void ClosePopup()
        {
            PopupService.IsAckTenderPopOpen = false;
            PopupService.IsPopupOpen = false;
        }
        private async void CardNumberEntered(object s)
        {
            if (!Helper.IsEnterKey(s))
            {
                return;
            }
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
            try
            {
                NotifyInfo = "Requesting Card Balance...";
                XDocument doc = await Helper.SockeRequest(
                       _cacheBussinessLogic.REWARDS_TpsIp,
                       _cacheBussinessLogic.REWARDS_TpsPort.ToString(),
                       TPSRequest(AckReqType.BALANCE)
                    );
                NotifyInfo = "";
                if (doc == null)
                {

                    ShowNotification("There is no response.~Balance Request",
                          null,
                          null,
                          ApplicationConstants.ButtonWarningColor
                      );

                    return;
                }

                _AckBLInfo = GetBalaceInfo(doc, AckReqType.BALANCE);

                if (_AckBLInfo.TransactionStatus != "APPROVED")
                {

                    ShowNotification(_AckBLInfo.ResponseMessage + "~Balance Request",
                           null,
                           null,
                           ApplicationConstants.ButtonWarningColor
                       );

                    return;
                }

                if (!string.IsNullOrEmpty(_AckBLInfo.LoyaltyBalace))
                    _loyaltyBalanceAmount = double.Parse(_AckBLInfo.LoyaltyBalace.Replace("$", ""));
                else
                    _loyaltyBalanceAmount = 0;
                if (!string.IsNullOrEmpty(_AckBLInfo.GiftBalance))
                    _giftBalanceAmount = double.Parse(_AckBLInfo.GiftBalance.Replace("$", ""));
                else
                    _giftBalanceAmount = 0;

                if (_loyaltyBalanceAmount == 0 && _giftBalanceAmount == 0)
                {
                    ShowNotification("The card is empty.~Balance Request",
                           null,
                           null,
                           ApplicationConstants.ButtonWarningColor
                       );

                    return;
                }



            }
            catch (Exception ex)
            {
                NotifyInfo = "";

                ShowNotification(ex.Message + "~Balance Request",
                           null,
                           null,
                           ApplicationConstants.ButtonWarningColor
                       );

                return;
            }

            PopupService.IsAckTenderPopOpen = false;
            PopupService.IsPopupOpen = false;
            PopupService.IsAckBalacePopOpen = true;
            IsEnabled = false;
            IsBalanceOKEnabled = false;

            MessengerInstance.Send(new AckrooAccoutBalanceMessage());
        }
        private AckrooBalanceInfo GetBalaceInfo(XDocument doc, AckReqType cType)
        {
            AckrooBalanceInfo ackbalance = new AckrooBalanceInfo();
            XElement xe;
            xe = GetElement(doc, "TransactionStatus");
            if (xe != null)
                ackbalance.TransactionStatus = xe.Value;

            xe = GetElement(doc, "ResponseMessage");
            if (xe != null)
                ackbalance.ResponseMessage = xe.Value;

            switch (cType)
            {
                case AckReqType.ACTIVATION:
                    xe = GetElement(doc, "AmountFunded");
                    if (xe != null)
                        if (!string.IsNullOrEmpty(xe.Value))
                            ackbalance.AmountFunded = xe.Value;
                    xe = GetElement(doc, "ElBalance");
                    if (xe != null && !string.IsNullOrEmpty(xe.Value))
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                    else
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", 0);
                    xe = GetElement(doc, "GcBalance");
                    if (xe != null && !string.IsNullOrEmpty(xe.Value))
                        ackbalance.GiftBalance = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
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
                case AckReqType.REDEEM:
                    xe = GetElement(doc, "ElBalance");
                    if (xe != null && !string.IsNullOrEmpty(xe.Value))
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                    else
                        ackbalance.LoyaltyBalace = string.Format("{0:C2}", 0);
                    xe = GetElement(doc, "GcBalance");
                    if (xe != null && !string.IsNullOrEmpty(xe.Value))
                        ackbalance.GiftBalance = string.Format("{0:C2}", double.Parse(xe.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                    else
                        ackbalance.GiftBalance = string.Format("{0:C2}", 0);
                    break;
            }



            return ackbalance;
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
        #endregion
        #region Properties
        public string REWARD_Message
        {
            get { return _cacheBussinessLogic.REWARDS_Message; }
        }
        private AckrooItem _selectedBalanceItem;

        public AckrooItem SelectedBalanceItem
        {
            get { return _selectedBalanceItem; }
            set
            {
                _selectedBalanceItem = value;
                RaisePropertyChanged(nameof(SelectedBalanceItem));
            }
        }



        private List<AckrooItem> _balanceItems;

        public List<AckrooItem> BalanceItems
        {
            get { return _balanceItems; }
            set
            {
                _balanceItems = value;
                RaisePropertyChanged(nameof(BalanceItems));
            }
        }

        private string _redeemAmountByGift;

        public string RedeemAmountByGift
        {
            get { return _redeemAmountByGift; }
            set
            {
                _redeemAmountByGift = value;
                RaisePropertyChanged(nameof(RedeemAmountByGift));
            }
        }

        private string _redeemAmountByLoyalty;

        public string RedeemAmountByLoyalty
        {
            get { return _redeemAmountByLoyalty; }
            set
            {
                _redeemAmountByLoyalty = value;
                RaisePropertyChanged(nameof(RedeemAmountByLoyalty));
            }
        }

        private string _outstandingBalance;

        public string OutStandingBalance
        {
            get { return _outstandingBalance; }
            set
            {
                _outstandingBalance = value;
                RaisePropertyChanged(nameof(OutStandingBalance));
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
        private bool _isBalanceOKEnabled;

        public bool IsBalanceOKEnabled
        {
            get { return _isBalanceOKEnabled; }
            set
            {
                _isBalanceOKEnabled = value;
                RaisePropertyChanged(nameof(IsBalanceOKEnabled));
            }
        }


        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }
        private string _cardnumber;

        public string CardNumber
        {
            get { return _cardnumber; }
            set
            {
                _cardnumber = value;
                RaisePropertyChanged(nameof(CardNumber));
            }
        }

        #endregion
        #region Commands
        public RelayCommand BalanceOKCommand { get; set; }
        public RelayCommand ClosePopupCommand { get; set; }
        public RelayCommand<object> CardNumberCommand { get; set; }

        #endregion
    }
}
