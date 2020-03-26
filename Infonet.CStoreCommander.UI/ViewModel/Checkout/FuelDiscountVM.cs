using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Discount;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Infonet.CStoreCommander.UI.Model.Sale;


namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class FuelDiscountVM: VMBase
    {
        private double _quantity;
        private double _saleamount;
        
        private readonly IFuelDiscountBusinessLogic _fuelDiscountBusinessLogic;
        
        private ObservableCollection<ClientDiscoutGrp> _discountGroups;
        private List<SaleLineModel> _fuelLines;

        public IEnumerable<ClientDiscoutGrp> DiscountGroups
        {
            get { return _discountGroups; }
           
        }
        private ClientDiscoutGrp _selectedGroup;

        public ClientDiscoutGrp SelectedGroup
        {
            get { return _selectedGroup; }
            set { Set(nameof(SelectedGroup), ref _selectedGroup, value); }
        }

        public FuelDiscountVM(IFuelDiscountBusinessLogic fuelDiscountBusinessLogic)
        {
            _fuelDiscountBusinessLogic = fuelDiscountBusinessLogic;
            
            _discountGroups = new ObservableCollection<ClientDiscoutGrp>();
            InitializeCommands();
            MessengerInstance.Register<FuelLineInfo>(this, GetFuelLineInfo);
        }
        private async void GetFuelLineInfo(FuelLineInfo message)
        {
            _fuelLines = message.FuelLines;
            _quantity = 0;
            _saleamount = 0;
            foreach(var c in message.FuelLines)
            {
                _quantity += double.Parse(c.Quantity,System.Globalization.NumberStyles.Any,System.Globalization.CultureInfo.InvariantCulture);
                _saleamount += double.Parse(c.Quantity, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture) *double.Parse(c.Price, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
            }
           
            if (_discountGroups.Count == 0)
            {

                IEnumerable<ClientGroup> disgrps = await _fuelDiscountBusinessLogic.GetDiscountItemsAsync();
                ClientDiscoutGrp cdg;
                foreach (var c in disgrps)
                {
                   
                    if (c.DiscountType == "$" || c.DiscountType == "%")
                    {
                        cdg = new ClientDiscoutGrp();
                        cdg.GroupID = c.GroupId;
                        cdg.GroupName = c.GroupName;
                        cdg.DiscountRate = c.DiscountRate;
                        cdg.DiscountType = c.DiscountType;
                        if (c.DiscountType == "$")
                        {
                            cdg.DiscountRate = c.DiscountRate;
                            cdg.DiscountRateDisplay = string.Format("{0:0.0000} $", c.DiscountRate);
                            cdg.Discount = _quantity * cdg.DiscountRate;
                            cdg.DiscountDisplay = string.Format("{0:C2}", cdg.Discount);
                        }
                        else
                        {
                            cdg.DiscountRate = c.DiscountRate;
                            cdg.DiscountRateDisplay = string.Format("{0:0.0000} %", c.DiscountRate);
                            cdg.Discount = _saleamount * cdg.DiscountRate / 100;
                            cdg.DiscountDisplay = string.Format("{0:C2}", cdg.Discount);
                        }
                        _discountGroups.Add(cdg);
                    }
                }
            }
            else
            {
                foreach(var c in _discountGroups)
                {
                    if (c.DiscountType == "$")
                    {
                        c.Discount = _quantity * c.DiscountRate;
                        c.DiscountDisplay=string.Format("{0:C2}", c.Discount);
                    }
                    else
                    {
                        c.Discount = _saleamount * c.DiscountRate / 100;
                        c.DiscountDisplay = string.Format("{0:C2}", c.Discount);
                    }
                }
            }

            
             if (_discountGroups.Count>0)
             SelectedGroup = _discountGroups[0];
        }
      
        private void InitializeCommands()
        {
            CloseFuelDiscountPopupCommand = new RelayCommand(CloseFuelDiscountPopup);
            ApplyFuelDiscountCommand = new RelayCommand(ApplyFuelDiscount);
        }
        public string FuelDiscountTitle { get { return "Do you want to apply a fuel discount?"; } }
        public RelayCommand CloseFuelDiscountPopupCommand { get; set; }
        public RelayCommand ApplyFuelDiscountCommand { get; set; }
        private  void CloseFuelDiscountPopup()
        {
            Messenger.Default.Send<FuelDiscount>(new FuelDiscount()
            {
                DiscoutType = _selectedGroup.DiscountType,
                DiscountName = _selectedGroup.GroupName,
                Reason = "NO DISCOUNT",
                DiscountRate = 0,
                FuelLines = _fuelLines
            });
            
            PopupService.IsFuelDiscountPopupOpen = false;
            PopupService.IsPopupOpen = false;
        }
        private  void ApplyFuelDiscount()
        {
            
            
            Messenger.Default.Send<FuelDiscount>(new FuelDiscount() { 
                                                                      DiscoutType = _selectedGroup.DiscountType,
                                                                      DiscountName = _selectedGroup.GroupName,
                                                                      CustGrpID = _selectedGroup.GroupID,
                                                                      Reason = "ISSUE_FUELDISCOUNT",
                                                                      DiscountRate =  _selectedGroup.DiscountRate,
                                                                      FuelLines = _fuelLines
            });

            

            
            PopupService.IsFuelDiscountPopupOpen = false;
            PopupService.IsPopupOpen = false;
            
        }

    }
    
    public class ClientDiscoutGrp: VMBase
    {
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string DiscountType { get; set; }
        public double DiscountRate { get; set; }
        
        public string DiscountRateDisplay { get; set; }
        public double Discount { get; set; }
        private string _discountDisplay;
        public string DiscountDisplay
        {
            get { return _discountDisplay; }
            set { Set(nameof(DiscountDisplay), ref _discountDisplay, value); }
        }
    }
}
