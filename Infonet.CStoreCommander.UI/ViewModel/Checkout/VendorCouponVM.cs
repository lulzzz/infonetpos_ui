using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.UI.Model.Checkout;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class VendorCouponVM : VMBase
    {
        private string _amount;
        private string _tenderCode;
        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;
        private ObservableCollection<VendorCouponModel> _vendorCoupons;
        private string _couponNumber;
        private bool _isCouponNumberEnabled;
        private string _serialNumber;
        private bool _isAddButtonEnable;
        private bool _isDoneButtonEnable;
        private bool _isDeleteButtonEnable;
        private VendorCouponModel _selectedVendorCoupon;

        public VendorCouponModel SelectedVendorCoupon
        {
            get { return _selectedVendorCoupon; }
            set
            {
                if (value != _selectedVendorCoupon)
                {
                    _selectedVendorCoupon = value;
                    IsDeleteButtonEnable = _selectedVendorCoupon != null;
                    RaisePropertyChanged(nameof(SelectedVendorCoupon));
                }
            }
        }
        public bool IsDeleteButtonEnable
        {
            get { return _isDeleteButtonEnable; }
            set
            {
                if (_isDeleteButtonEnable != value)
                {
                    _isDeleteButtonEnable = value;
                    RaisePropertyChanged(nameof(IsDeleteButtonEnable));
                }
            }
        }
        public bool IsDoneButtonEnable
        {
            get { return _isDoneButtonEnable; }
            set
            {
                if (_isAddButtonEnable != value)
                {
                    _isDoneButtonEnable = value;
                    RaisePropertyChanged(nameof(IsDoneButtonEnable));
                }
            }
        }
        public bool IsAddButtonEnable
        {
            get { return _isAddButtonEnable; }
            set
            {
                if (_isAddButtonEnable != value)
                {
                    _isAddButtonEnable = value;
                    RaisePropertyChanged(nameof(IsAddButtonEnable));
                }
            }
        }
        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (_serialNumber != value)
                {
                    _serialNumber = value;
                    RaisePropertyChanged(nameof(SerialNumber));
                }
            }
        }
        public bool IsCouponNumberEnabled
        {
            get { return _isCouponNumberEnabled; }
            set
            {
                if (_isCouponNumberEnabled != value)
                {
                    _isCouponNumberEnabled = value;
                    RaisePropertyChanged(nameof(IsCouponNumberEnabled));
                }
            }
        }
        public string CouponNumber
        {
            get { return _couponNumber; }
            set
            {
                if (_couponNumber != value)
                {
                    _couponNumber = value;
                    IsAddButtonEnable = !string.IsNullOrEmpty(_couponNumber);
                    RaisePropertyChanged(nameof(CouponNumber));
                }
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }
        public ObservableCollection<VendorCouponModel> VendorCoupons
        {
            get { return _vendorCoupons; }
            set
            {
                if (_vendorCoupons != value)
                {
                    _vendorCoupons = value;
                    RaisePropertyChanged(nameof(VendorCoupons));
                }
            }
        }
        public RelayCommand AddVendorCouponCommand { get; private set; }
        public RelayCommand DeleteVendorCouponCommand { get; set; }
        public RelayCommand CompleteVendorCouponCommand { get; set; }
        public RelayCommand<object> EnterPressedOnVendorSerialNumberCommand { get; set; }

        public VendorCouponVM(ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            InitializeCommands();
            UnregisterMessages();
            RegisterMessages();
        }

        private void UnregisterMessages()
        {
            MessengerInstance.Unregister<string>(this,
                 "SetSelectedTenderCode", SetSelectedTenderCode);
            MessengerInstance.Unregister<string>(this,
                "SetOutstandingAmount", SetOutstandingAmount);
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<string>(this,
                "SetSelectedTenderCode", SetSelectedTenderCode);
            MessengerInstance.Register<string>(this,
                "SetOutstandingAmount", SetOutstandingAmount);
        }

        private void SetOutstandingAmount(string amount)
        {
            Amount = amount;
        }

        private void SetSelectedTenderCode(string tenderCode)
        {
            _tenderCode = tenderCode;
            PerformAction(GetVendorCoupon);
        }

        private void InitializeCommands()
        {
            EnterPressedOnVendorSerialNumberCommand = new RelayCommand<object>(EnterPressedOnVendorSerialNumber);
            AddVendorCouponCommand = new RelayCommand(AddVendorCoupon);
            DeleteVendorCouponCommand = new RelayCommand(() => PerformAction(DeleteVendorCoupon));
            CompleteVendorCouponCommand = new RelayCommand(() => PerformAction(CompleteVendorCoupon));
        }

        private void EnterPressedOnVendorSerialNumber(dynamic args)
        {
            if (Helper.IsEnterKey(args) && IsAddButtonEnable)
            {
                AddVendorCoupon();
            }
        }

        private async Task CompleteVendorCoupon()
        {
            var response = await _checkoutBusinessLogic.PaymentByVendorCoupon(_tenderCode);
            NavigateService.Instance.NavigateToTenderScreen();
            MessengerInstance.Send(response, "UpdateTenderSummary");
        }

        private async Task DeleteVendorCoupon()
        {
            var response = await _checkoutBusinessLogic.RemoveVendorCoupon(_tenderCode,
                SelectedVendorCoupon.Coupon);
            UpdateVendorCoupon(response.VendorCoupons);
            SerialNumber = string.Empty;
            MessengerInstance.Send(response, "UpdateTenderSummary");
        }

        private void AddVendorCoupon()
        {
            PerformAction(async () =>
            {
                var response = await _checkoutBusinessLogic.AddVendorCoupon(_tenderCode,
                    CouponNumber, SerialNumber);


                UpdateVendorCoupon(response.VendorCoupons);
                SerialNumber = string.Empty;
                MessengerInstance.Send(response, "UpdateTenderSummary");
            });
        }

        private void UpdateVendorCoupon(List<SaleVendorCoupon> response)
        {
            var tempVendorCoupones = (from v in response
                                      select new VendorCouponModel
                                      {
                                          Coupon = v.Coupon,
                                          SerialNumber = v.SerialNumber
                                      }).ToList();

            VendorCoupons = new ObservableCollection<VendorCouponModel>(tempVendorCoupones);
        }

        private async Task GetVendorCoupon()
        {
            try
            {
                var response = await _checkoutBusinessLogic.GetVendorCoupon(_tenderCode);
                var tempVendors = (from v in response.SaleVendorCoupons
                                   select new VendorCouponModel
                                   {
                                       Coupon = v.Coupon,
                                       SerialNumber = v.SerialNumber
                                   }).ToList();

                if (!string.IsNullOrEmpty(response.DefaultCoupon))
                {
                    CouponNumber = response.DefaultCoupon;
                    IsCouponNumberEnabled = false;
                }

                VendorCoupons = new ObservableCollection<VendorCouponModel>(tempVendors);
            }
            catch (Exception)
            {
                NavigateService.Instance.NavigateToSaleSummary();
                throw;
            }
        }

        internal void ResetVM()
        {
            CouponNumber = string.Empty;
            SerialNumber = string.Empty;
            _tenderCode = string.Empty;
            SelectedVendorCoupon = null;
            IsCouponNumberEnabled = true;
            IsAddButtonEnable = false;
            VendorCoupons = null;
            IsDoneButtonEnable = true;
        }
    }
}

