using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.PeripheralLayer;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class SignatureVM : VMBase
    {
        public ISignaturePad SignaturePad;

        private Uri _image;
        private Uri _signatureImage;

        public Uri SignatureImage
        {
            get { return _signatureImage; }
            set
            {
                _signatureImage = value;
                RaisePropertyChanged(nameof(SignatureImage));
            }
        }

        public RelayCommand ConnectToDeviceCommand { get; set; }
        public RelayCommand AcceptCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        private short _attemptsForConnectingDevice;
        private const short _maxAttemptsForConnectingDevice = 2;

        public SignatureVM(ICheckoutBusinessLogic checkoutBusinessLogic)
        {
            _checkoutBusinessLogic = checkoutBusinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ConnectToDeviceCommand = new RelayCommand(() => { ConnectToDevice(); });

            AcceptCommand = new RelayCommand(ConfirmSignatureAccept);

            ClearCommand = new RelayCommand(() => SignaturePad.Clear());
            BackCommand = new RelayCommand(ConfirmForCompletePayment);
        }

        private void ConfirmSignatureAccept()
        {
            if (!SignaturePad.IsSignatureEmpty())
            {
                ShowConfirmationMessage(ApplicationConstants.AcceptTheSignature, AcceptSignature);
            }
        }

        private void AcceptSignature()
        {
            PerformAction(async () =>
            {
                var signature = SignaturePad.FinalizeSignature();
                if (!string.IsNullOrEmpty(signature))
                {
                    _image = new Uri(signature);
                    await _checkoutBusinessLogic.SaveSignature(_image);
                    CompletePayment(true);
                }
            });
        }

        private void ConfirmForCompletePayment()
        {
            ShowConfirmationMessage(ApplicationConstants.NoSignatureConfirmation,
                () => CompletePayment(false),
                null,
                null);
        }

        private async Task ConnectToDevice()
        {
            try
            {
                SignaturePad = new SignaturePad();
                var localFolder = await Helper.GetLocalFolder();
                SignaturePad.Initialize(localFolder.Path);
            }
            catch (Exception ex)
            {
                _attemptsForConnectingDevice++;

                _log.Info(ex.ToString(), ex);
                // Adding delay to show that we are re-trying
                await Task.Delay(300);
                ShowNotification(ApplicationConstants.SignaturePadNotConnected,
                    () =>
                    {
                        if (_attemptsForConnectingDevice >= _maxAttemptsForConnectingDevice)
                        {
                            CompletePayment(false);
                        }
                        else
                        {
                            ConnectToDevice();
                        }
                    },
                    () =>
                    {
                        if (_attemptsForConnectingDevice >= _maxAttemptsForConnectingDevice)
                        {
                            CompletePayment(false);
                        }
                        else
                        {
                            ConnectToDevice();
                        }
                    }, ApplicationConstants.ButtonWarningColor);
            }
        }

        private void CompletePayment(bool printSignature)
        {
            PerformAction(async () =>
            {
                MessengerInstance.Send(printSignature ? _image : null, "CompletePaymentAfterSignature");
                SignaturePad?.Clear();
                SignaturePad?.Dispose();
            });
        }

        public void ReInitialize()
        {
            _attemptsForConnectingDevice = 0;
        }
    }
}
