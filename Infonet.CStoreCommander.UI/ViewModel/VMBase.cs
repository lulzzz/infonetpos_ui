using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.PeripheralLayer;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Login;
using Infonet.CStoreCommander.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.ViewModel
{
    public abstract class VMBase : ViewModelBase
    {
        protected delegate TResult func<out TResult>();
        private string _trainerModeText = "          (( Trainer Mode ))           \r";
        public static DispatcherTimer Timer;
        public static DispatcherTimer FreezeTimer;
        public static DispatcherTimer DipInputTimer;
        public static OPOSPrinter OposPrinter;
        public static Printer Printer;
        public static bool AreFreezeEventsAttached;
        public static long UserActivityHappenedSinceInSeconds;
        public readonly InfonetLog _log;

        public static DateTime LoginStartTime;
        private static int _operationsCompletedInLogin;
        private object _lock = new object();

        public static bool IsSwitchUserStarted = false;
        public static DateTime SwitchUserStartTime;
        private static int _operationsCompletedInSwitchUser;

        protected static bool DisplayCustomerDisplayMessage = true;
        public static bool LoadFuelPrices = true;

        public delegate void PrintReceipt(string text, string ImagePath = null);
        public event PrintReceipt PrintReceiptEvent;

        public static int OperationsCompletedInLogin
        {
            get
            {
                return _operationsCompletedInLogin;
            }
            set
            {
                _operationsCompletedInLogin = value;
                if (_operationsCompletedInLogin == 4)
                {
                    InfonetLogManager.GetLogger(typeof(LoginScreenVM)).Info(string.Format("Time Taken In Login is {0}ms", (DateTime.Now -
                        LoginStartTime).TotalMilliseconds));
                }
            }
        }

        public static int OperationsCompletedInSwitchUser
        {
            get
            {
                return _operationsCompletedInSwitchUser;
            }
            set
            {
                _operationsCompletedInSwitchUser = value;
                if (_operationsCompletedInSwitchUser == 4)
                {
                    IsSwitchUserStarted = false;
                    InfonetLogManager.GetLogger(typeof(LoginScreenVM)).Info(string.Format("Time Taken In Switch User is {0}ms", (DateTime.Now -
                        SwitchUserStartTime).TotalMilliseconds));
                }
            }
        }

        #region Private Variables
        private double _width;
        private double _height;
        private string _title;
        private string _message;
        private string _continue;
        private string _cancel;
        private ICommand _shutDownCommand;
        private bool _isAppFetchingData;

        private delegate void YesConfirmation();
        private YesConfirmation _yesConfirmation;

        private delegate void NoConfirmation();
        private NoConfirmation _noConfirmation;

        private delegate void OkConfirmation();
        private OkConfirmation _okConfirmation;

        private delegate void CloseConfirmation();
        private CloseConfirmation _closeConfirmation;

        private delegate void ThirdButtonCommand();
        private ThirdButtonCommand _thirdButtonCommand;
        private object _appIdleTimer;
        #endregion

        #region Public Variable

        public int ErrorCode { get; set; }
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged(nameof(Width));
            }
        }
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                RaisePropertyChanged(nameof(Height));
            }
        }
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }
        public string Continue
        {
            get { return _continue; }
            set
            {
                _continue = value;
                RaisePropertyChanged(nameof(Continue));
            }
        }
        public string Cancel
        {
            get { return _cancel; }
            set
            {
                _cancel = value;
                RaisePropertyChanged(nameof(Cancel));
            }
        }
        public bool IsAppFetchingData
        {
            get { return _isAppFetchingData; }
            set
            {
                _isAppFetchingData = value;
                RaisePropertyChanged(nameof(IsAppFetchingData));
            }
        }

        protected InfonetLog Log { get; private set; }

        public PopupService PopupService { get; set; }
            = PopupService.PopupInstance;

        public LoadingService LoadingService { get; set; }
          = LoadingService.LoadingInstance;

        public ICacheBusinessLogic CacheBusinessLogic { get; set; }
        = SimpleIoc.Default.GetInstance<ICacheBusinessLogic>();
        #endregion

        #region Commands
        public ICommand ShutDownApplicationCommand
        {
            get
            {
                return _shutDownCommand;
            }

            set
            {
                _shutDownCommand = value;
                RaisePropertyChanged(nameof(ShutDownApplicationCommand));
            }
        }
        public RelayCommand FirstFrameBackNavigationCommand { get; set; }
        public RelayCommand SecondFrameBackNavigationCommand { get; set; }
        public RelayCommand InvokeKeyboardCommand { get; set; }
        #endregion

        public VMBase()
        {
            Width = Window.Current.Bounds.Width;
            Height = Window.Current.Bounds.Height;

            ShutDownApplicationCommand = new RelayCommand(ShutDownApplication);
            FirstFrameBackNavigationCommand = new RelayCommand(NavigateService.Instance.NavigateToHome);
            SecondFrameBackNavigationCommand = new RelayCommand(NavigateService.Instance.SecondFrameBackNavigation);
            InvokeKeyboardCommand = new RelayCommand(InvokeKeyBoardEvent);
            _log = Log = InfonetLogManager.GetLogger(GetType());
        }

        protected void ShutDownSystem()
        {
            ShutDownApplication();
            new WindowManager().ShutDownSystem();
        }

        protected void LogOffSystem()
        {
            ShutDownApplication();
            new WindowManager().LogOffSystem();
        }

        protected void ShutDownApplication()
        {
            CoreApplication.Exit();
        }

        public void ShowNotification(string message,
            Action func,
            Action close,
            SolidColorBrush okButtonColor = null,
            string okText = "",
            bool isOkButtonEnabled = true)
        {
            PopupService.CarwashCode = "";
            lock (_lock)
            {
                if (okText == "")
                {
                    okText = ApplicationConstants.Ok;
                }

                if (okButtonColor == null)
                {
                    okButtonColor = ApplicationConstants.ButtonWarningColor;
                }
                if (!PopupService.IsPopupOpen)
                {
                    _okConfirmation = null;
                    _closeConfirmation = null;
                    if (func != null)
                    {
                        _okConfirmation = new OkConfirmation(func);
                    }
                    if (close != null)
                    {
                        _closeConfirmation = new CloseConfirmation(close);
                    }

                    var messageStyler = new AlertMessageFormatter();
                    var messageStyle = messageStyler.CreateMessage(message);
                    PopupService.Message = messageStyle.Message;
                    PopupService.IsAlertPopupOpen = true;
                    PopupService.Title = messageStyle.Title;
                    PopupService.Continue = okText;
                    PopupService.OkButtonColor = okButtonColor;
                    PopupService.IsOkButtonEnabled = isOkButtonEnabled;
                    PopupService.IsPopupOpen = true;

                    PopupService.OkCommand = new RelayCommand(() =>
                    {
                        PopupService.IsAlertPopupOpen = false;
                        PopupService.IsPopupOpen = false;
                        if (_okConfirmation != null)
                        {
                            _okConfirmation();
                        }
                    });

                    PopupService.CloseCommand = new RelayCommand(() =>
                    {
                        PopupService.IsAlertPopupOpen = false;
                        PopupService.IsPopupOpen = false;
                        if (_closeConfirmation != null)
                        {
                            _closeConfirmation();
                        }
                    });

                }
            }
        }

        public void ShowConfirmationMessage(string message,
            Action yesConfirmation = null,
            Action noConfirmation = null,
            Action close = null,
            SolidColorBrush yesButtonColor = null,
            SolidColorBrush noButtonColor = null,
            string yesButtonText = "",
            string noButtonText = "",
            bool isYesButtonEnabled = true,
            bool isNoButtonEnabled = true,
            string thirdButtonText = "",
            bool isThirdButtonVisible = false,
            Action thirdButtonCommand = null,
            SolidColorBrush thirdButtonColor = default(SolidColorBrush))
        {
            if (yesButtonText == "")
            {
                yesButtonText = ApplicationConstants.Yes;
            }
            if (noButtonText == "")
            {
                noButtonText = ApplicationConstants.No;
            }
            if (yesButtonColor == null)
            {
                yesButtonColor = ApplicationConstants.ButtonConfirmationColor;
            }
            if (noButtonColor == null)
            {
                noButtonColor = ApplicationConstants.ButtonWarningColor;
            }

            if (!PopupService.IsPopupOpen)
            {
                _yesConfirmation = null;
                _noConfirmation = null;
                _closeConfirmation = null;

                if (yesConfirmation != null)
                {
                    _yesConfirmation = new YesConfirmation(yesConfirmation);
                }
                if (noConfirmation != null)
                {
                    _noConfirmation = new NoConfirmation(noConfirmation);
                }
                if (close != null)
                {
                    _closeConfirmation = new CloseConfirmation(close);
                }
                if (thirdButtonCommand != null)
                {
                    _thirdButtonCommand = new ThirdButtonCommand(thirdButtonCommand);
                }
                PopupService.IsPopupOpen = true;
                var messageStyler = new AlertMessageFormatter();
                var messageStyle = messageStyler.CreateMessage(message);
                PopupService.Message = messageStyle.Message;
                PopupService.Title = messageStyle.Title;
                PopupService.YesButtonText = yesButtonText;
                PopupService.NoButtonText = noButtonText;
                PopupService.YesButtonColor = yesButtonColor;
                PopupService.NoButtonColor = noButtonColor;
                if (thirdButtonColor == null)
                {
                    PopupService.ThirdButtonColor = ApplicationConstants.ButtonFooterColor;
                }
                else
                {
                    PopupService.ThirdButtonColor = thirdButtonColor;
                }
                PopupService.IsConfirmationPopupOpen = true;
                PopupService.IsYesbuttonEnabled = isYesButtonEnabled;
                PopupService.IsNoButtonEnabled = isNoButtonEnabled;
                PopupService.PopupInstance.IsThirdButtonVisible = isThirdButtonVisible;
                PopupService.PopupInstance.ThirdButtonText = thirdButtonText;

                PopupService.YesConfirmationCommand = new RelayCommand(() =>
                {
                    PopupService.IsConfirmationPopupOpen = false;
                    PopupService.IsPopupOpen = false;
                    if (_yesConfirmation != null)
                    {
                        _yesConfirmation();
                    }
                });

                PopupService.NoConfirmationCommand = new RelayCommand(() =>
                {
                    PopupService.IsConfirmationPopupOpen = false;
                    PopupService.IsPopupOpen = false;
                    if (_noConfirmation != null)
                    {
                        _noConfirmation();
                    }
                });

                PopupService.CloseCommand = new RelayCommand(() =>
                {
                    PopupService.IsConfirmationPopupOpen = false;
                    PopupService.IsPopupOpen = false;
                    if (_closeConfirmation != null)
                    {
                        _closeConfirmation();
                    }
                });

                PopupService.ThirdButtonCommand = new RelayCommand(() =>
                {
                    PopupService.IsConfirmationPopupOpen = false;
                    PopupService.IsPopupOpen = false;
                    if (_thirdButtonCommand != null)
                    {
                        _thirdButtonCommand();
                    }
                });
            }
        }

        protected void CloseReasonPopup()
        {
            PopupService.IsPopupOpen = false;
            PopupService.IsReasonPopupOpen = false;
        }

        /// <summary>
        /// Generic Method for Load data to/from API depending on function passed as parameter
        /// </summary>
        /// <param name="func"></param>
        protected async void PerformAction(func<Task> func, string elementNameToSetFocus = "")
        {
            LoadingService.ShowLoadingStatus(true);
            try
            {
                if (func != null)
                {
                    await func?.Invoke();
                }
            }
            catch (UserNotAuthorizedException ex)
            {
                Log.Warn(ex);
                NavigateService.Instance.NavigateToLogin();
            }
            catch (InternalServerException ex)
            {

                if (!string.IsNullOrEmpty(ex.Error?.Message))
                {
                    Log.Warn(ex);
                    ShowNotification(ex.Error.Message,
                        () => { SetFocusOnControl(elementNameToSetFocus); },
                        () => { SetFocusOnControl(elementNameToSetFocus); },
                        ApplicationConstants.ButtonWarningColor);
                }
            }
            catch (ApiDataException ex)
            {
                if (!string.IsNullOrEmpty(ex.Error?.Message))
                {
                    Log.Warn(ex);
                    ShowNotification(ex.Error.Message,
                        () => { SetFocusOnControl(elementNameToSetFocus); },
                        () => { SetFocusOnControl(elementNameToSetFocus); },
                        ApplicationConstants.ButtonWarningColor);
                }
            }
            catch (TaskCanceledException ex)
            {
                Log.Warn(ex);
                ShowNotification(ApplicationConstants.ApiTimeoutMessage,
                    () => { SetFocusOnControl(elementNameToSetFocus); },
                    () => { SetFocusOnControl(elementNameToSetFocus); },
                    ApplicationConstants.ButtonWarningColor);
            }
            catch (PrinterLayerException ex)
            {
                Log.Warn(ex);
                ShowNotification(ApplicationConstants.NoPrinterFound,
                    () => { SetFocusOnControl(elementNameToSetFocus); },
                    () => { SetFocusOnControl(elementNameToSetFocus); },
                    ApplicationConstants.ButtonWarningColor);
            }
            catch (Exception ex)
            {
                Log.Info(Message, ex);
                ShowNotification(ApplicationConstants.SomethingBadHappned,
                    () => { SetFocusOnControl(elementNameToSetFocus); },
                    () => { SetFocusOnControl(elementNameToSetFocus); },
                    ApplicationConstants.ButtonWarningColor);
            }
            finally
            {
                LoadingService.ShowLoadingStatus(false);
                SetFocusOnControl(elementNameToSetFocus);
            }
        }

        private void SetFocusOnControl(string elementNameToSetFocus)
        {
            if (!string.IsNullOrEmpty(elementNameToSetFocus))
            {
                MessengerInstance.Send(elementNameToSetFocus, "SetFocusOn");
            }
        }

        protected async Task PerformActionWithoutLoader(func<Task> func, bool showPopup = true)
        {
            try
            {
                await func?.Invoke();
            }
            catch (UserNotAuthorizedException ex)
            {
                Log.Warn(ex);

                if (showPopup && !string.IsNullOrEmpty(ex.Error?.Message))
                {
                    NavigateService.Instance.NavigateToLogin();
                }
            }
            catch (InternalServerException ex)
            {
                Log.Warn(ex);

                if (showPopup && !string.IsNullOrEmpty(ex.Error?.Message))
                {
                    ShowNotification(ex.Error.Message, null, null, ApplicationConstants.ButtonWarningColor);
                }
            }
            catch (ApiDataException ex)
            {
                Log.Warn(ex);
                if (showPopup && !string.IsNullOrEmpty(ex.Error?.Message))
                {
                    ShowNotification(ex.Error.Message, null, null, ApplicationConstants.ButtonWarningColor);
                }
            }
            catch (TaskCanceledException ex)
            {
                Log.Warn(ex);
            }
            catch (PrinterLayerException ex)
            {
                Log.Warn(ex);
                if (showPopup)
                {
                    ShowNotification(ApplicationConstants.NoPrinterFound, null, null, ApplicationConstants.ButtonWarningColor);
                }
            }
            catch (Exception ex)
            {
                Log.Info(Message, ex);
                if (showPopup)
                {
                    ShowNotification(ApplicationConstants.SomethingBadHappned, null, null, ApplicationConstants.ButtonWarningColor);
                }
            }
        }

        protected async Task PerformPrint(Report report, Uri signature = null)
        {
            if (report != null && report.ReportContent != null)
            {
                var reportContent = report.ReportContent.Split('\n').ToList();

                PerformPrint(report.ReportContent.Split('\n').ToList(), report.Copies, true, signature);
            }
        }

        protected async Task PerformPrint(List<string> data, int copies = 1, bool showPrinterError = true, Uri signature = null)
        {
            if (data == null || data.Count == 0 || copies < 1)
            {
                return;
            }

            if (CacheBusinessLogic.UseReceiptPrinter)
            {
                if (CacheBusinessLogic.UseOposReceiptPrinter)
                {
                    PrintUsingOPOSPrinter(data, copies, showPrinterError, signature);
                }
                else
                {
                    //PrintUsingNonOPOSPrinter(data, copies, showPrinterError, signature);
                    Win32Print(data, signature);
                }
            }
        }
        private void Win32Print(List<string> data, Uri signature = null)
        {
            string ReceiptContent = string.Join("\n", data.Select(x => x));
            string Sig=null;
            if (signature != null)
                Sig = signature.LocalPath;
            PrintReceiptEvent?.Invoke(ReceiptContent, Sig);
        }
        protected async Task PerformPrint(List<Report> reports, int copies = 1, Uri signature = null)
        {
            if (reports == null || reports.Count == 0 || copies < 1)
            {
                return;
            }

            if (CacheBusinessLogic.UseReceiptPrinter)
            {
                if (CacheBusinessLogic.UseOposReceiptPrinter)
                {
                    PrintUsingOPOSPrinter(reports, signature);
                }
                else
                {
                    PrintUsingNonOPOSPrinter(reports, signature);
                }
            }
        }

        private async Task PrintUsingOPOSPrinter(List<string> data, int copies, bool showPrinterError, Uri signature)
        {
            var result = await Task.Run(async () =>
            {
                try
                {
                    var listContent = new List<string>();
                    foreach (var s in data)
                    {
                        var t = s.Replace("\r", "");
                        listContent.Add(t);
                    }

                    if (CacheBusinessLogic.OperatorIsTrainer)
                    {
                        listContent.Insert(0, _trainerModeText);
                        listContent.Insert(1, "\r");
                    }

                    if (VMBase.OposPrinter != null)
                    {
                        
                        await VMBase.OposPrinter.PrintAsync(listContent, copies, signature?.LocalPath);
                        SoundService.Instance.PlaySoundFile(SoundTypes.PrintDone);
                        return 0;
                    }
                    return 1;
                }
                catch (PrinterLayerException ex)
                {
                    Log.Warn(ex);
                    return 1;
                }
                catch (Exception ex)
                {
                    Log.Info(Message, ex);
                    return 2;
                }
            });

            if (showPrinterError)
            {
                switch (result)
                {
                    case 1:
                        ShowNotification(ApplicationConstants.NoPrinterFound, null, null, ApplicationConstants.ButtonWarningColor);
                        break;
                    case 2:
                        ShowNotification(ApplicationConstants.SomethingBadHappned, null, null, ApplicationConstants.ButtonWarningColor);
                        break;
                }
            }
        }

        private async Task PrintUsingOPOSPrinter(List<Report> reports, Uri signature)
        {
            var result = await Task.Run(async () =>
            {
                try
                {
                    foreach (var report in reports)
                    {
                        var listContent = new List<string>();
                        report.ReportContent = report.ReportContent?.Replace("\r\n", "\n");
                        listContent = report.ReportContent.Split('\n').ToList();
                        if (CacheBusinessLogic.OperatorIsTrainer)
                        {
                            listContent.Insert(0, _trainerModeText);
                            listContent.Insert(1, "\r");
                        }

                        if (VMBase.OposPrinter == null)
                        {
                            return 1;
                        }
                        await VMBase.OposPrinter.PrintAsync(listContent, report.Copies, signature?.LocalPath);
                        SoundService.Instance.PlaySoundFile(SoundTypes.PrintDone);
                    }
                    return 0;
                }
                catch (PrinterLayerException ex)
                {
                    Log.Warn(ex);
                    return 1;
                }
                catch (Exception ex)
                {
                    Log.Info(Message, ex);
                    return 2;
                }
            });

            switch (result)
            {
                case 1:
                    ShowNotification(ApplicationConstants.NoPrinterFound, null, null, ApplicationConstants.ButtonWarningColor);
                    break;
                case 2:
                    ShowNotification(ApplicationConstants.SomethingBadHappned, null, null, ApplicationConstants.ButtonWarningColor);
                    break;
            }
        }

        private async Task PrintUsingNonOPOSPrinter(List<string> data, int copies, bool showPrinterError, Uri signature)
        {
            Log.Info("Prinitng has been started");
            Task.Run(async () =>
            {
                if (CacheBusinessLogic.OperatorIsTrainer)
                {
                    data.Insert(0, _trainerModeText);
                    data.Insert(1, "\r");
                }

                try
                {
                    await VMBase.Printer?.Print(data, copies, signature?.LocalPath);
                    SoundService.Instance.PlaySoundFile(SoundTypes.PrintDone);
                }
                catch (PrinterLayerException ex)
                {
                    Log.Warn(ex);
                    ShowNotification(ApplicationConstants.NoPrinterFound, null, null, ApplicationConstants.ButtonWarningColor);
                }
                catch (Exception ex)
                {
                    Log.Info(Message, ex);
                    ShowNotification(ApplicationConstants.SomethingBadHappned, null, null, ApplicationConstants.ButtonWarningColor);
                }
            });
        }

        private async Task PrintUsingNonOPOSPrinter(List<Report> reports, Uri signature)
        {
            Log.Info("Prinitng has been started");
            Task.Run(async () =>
            {
                try
                {
                    foreach (var report in reports)
                    {

                        var listContent = new List<string>();
                        report.ReportContent = report.ReportContent?.Replace("\r\n", "\n");
                        listContent = report.ReportContent.Split('\n').ToList();
                        if (CacheBusinessLogic.OperatorIsTrainer)
                        {
                            listContent.Insert(0, _trainerModeText);
                            listContent.Insert(1, "\r");
                        }

                        //await VMBase.Printer?.Print(listContent, report.Copies, signature?.LocalPath);
                        Win32Print(listContent, signature);
                        SoundService.Instance.PlaySoundFile(SoundTypes.PrintDone);
                    }
                }
                catch (PrinterLayerException ex)
                {
                    Log.Warn(ex);
                    ShowNotification(ApplicationConstants.NoPrinterFound, null, null, ApplicationConstants.ButtonWarningColor);
                }
                catch (Exception ex)
                {
                    Log.Info(Message, ex);
                    ShowNotification(ApplicationConstants.SomethingBadHappned, null, null, ApplicationConstants.ButtonWarningColor);
                }
            });
        }

        public void WriteToLineDisplay(LineDisplayModel data)
        {
            Task.Run(() =>
            {
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {

                    if (CacheBusinessLogic.UseCustomerDisplay)
                    {
                        if (CacheBusinessLogic.UseOposCustomerDisplay)
                        {
                            WriteToOPOSLineDisplay(data?.OposText1, data?.OposText2);
                        }
                        else
                        {
                            WriteToNonOPOSLineDisplay(data?.NonOposTexts);
                        }
                    }
                });
            });
        }

        private void WriteToNonOPOSLineDisplay(List<string> nonOposTexts)
        {
            if (nonOposTexts == null || nonOposTexts.Count == 0)
            {
                return;
            }

            try
            {
                using (var lineDisplay = new LineDisplay(CacheBusinessLogic.CustomerDisplayPort.ToString()))
                {
                    foreach (var text in nonOposTexts)
                    {
                        lineDisplay.DisplayText(text, false);
                    }
                }
            }
            catch (LineDisplayException ex)
            {
                Log.Warn(ex);
                if (DisplayCustomerDisplayMessage)
                {
                    DisplayCustomerDisplayMessage = false;
                    ShowNotification(ApplicationConstants.NoCustomerDisplayFound, null, null, ApplicationConstants.ButtonWarningColor);
                }
            }
            catch (Exception ex)
            {
                Log.Info(Message, ex);
                if (DisplayCustomerDisplayMessage)
                {
                    DisplayCustomerDisplayMessage = false;
                    ShowNotification(ApplicationConstants.SomethingBadHappned, null, null, ApplicationConstants.ButtonWarningColor);
                }
            }
        }

        private async Task WriteToOPOSLineDisplay(string oposText1, string oposText2)
        {
            if (oposText1 != null || oposText2 != null)
            {
                try
                {
                    using (var lineDisplay = new OPOSLineDisplay(CacheBusinessLogic.CustomerDisplayName))
                    {
                        lineDisplay.Clear();
                        lineDisplay.DisplayTextAt(0, 0, oposText1);
                        await Task.Delay(50);
                        lineDisplay.DisplayTextAt(1, 0, oposText2);
                    }
                }
                catch (LineDisplayException ex)
                {
                    Log.Warn(ex);
                    if (DisplayCustomerDisplayMessage)
                    {
                        DisplayCustomerDisplayMessage = false;
                        ShowNotification(ApplicationConstants.NoCustomerDisplayFound, null, null, ApplicationConstants.ButtonWarningColor);
                    }
                }
                catch (Exception ex)
                {
                    Log.Info(Message, ex);
                    if (DisplayCustomerDisplayMessage)
                    {
                        DisplayCustomerDisplayMessage = false;
                        ShowNotification(ApplicationConstants.SomethingBadHappned, null, null, ApplicationConstants.ButtonWarningColor);
                    }
                }
            }
        }

        public void OpenCashDrawer()
        {
            if (CacheBusinessLogic.UseCashDrawer)
            {
                if (CacheBusinessLogic.UseOposCashDrawer)
                {
                    try
                    {
                        using (var cashDrawer = new OPOSCashDrawer(CacheBusinessLogic.CashDrawerName))
                        {
                            cashDrawer.Open();
                            SoundService.Instance.PlaySoundFile(SoundTypes.drawerOpened);
                        }
                    }
                    catch (CashDrawerException ex)
                    {
                        ShowNotification(ApplicationConstants.NoCashDrawerFound, null, null
                                            , ApplicationConstants.ButtonWarningColor);
                    }
                }
                else
                {
                    try
                    {
                        VMBase.Printer?.OpenCashDrawer();
                        SoundService.Instance.PlaySoundFile(SoundTypes.drawerOpened);
                    }
                    catch (Exception ex)
                    {
                        ShowNotification(ApplicationConstants.NoCashDrawerFound, null, null
                                            , ApplicationConstants.ButtonWarningColor);
                    }
                }
            }
        }

        private void SetupTimer()
        {
            if (FreezeTimer == null)
            {
                FreezeTimer = new DispatcherTimer();
                FreezeTimer.Interval = new TimeSpan(0, 0, 1);
                FreezeTimer.Tick -= FreezeTimerTick;
                FreezeTimer.Tick += FreezeTimerTick;
            }
        }

        public void StartFreezeTimer()
        {
            SetupTimer();
            if (CacheBusinessLogic.FreezeTillAutomatically)
            {
                FreezeTimer.Start();
            }
            else
            {
                StopFreezeTimer();
            }
        }

        public void StopFreezeTimer()
        {
            UserActivityHappenedSinceInSeconds = 0;
            FreezeTimer.Stop();
        }

        private void FreezeTimerTick(object sender, object e)
        {
            if ((++UserActivityHappenedSinceInSeconds / 60) >= (int)CacheBusinessLogic.IdleIntervalAfterAppFreezes)
            {
                NavigateService.Instance.ChildOfMasterFrame =
                     NavigateService.Instance.MasterFrame.Content.GetType().Name;

                NavigateService.Instance.firstFrameOpenedPriorFreezeScreen
                    = NavigateService.Instance.FirstFrame;

                NavigateService.Instance.secondFrameOpenedPriorFreezeScreen
                    = NavigateService.Instance.SecondFrame;

                var internalFrame = NavigateService.Instance.GetInternalFrame();

                if (internalFrame != null)
                {
                    NavigateService.Instance.internalFrameOpenedPriorFreezeScreen = internalFrame;
                }

                if (PopupService.PopupInstance.IsPopupOpen == true)
                {
                    SavePopupStateDataForFreeze();
                }

                NavigateService.Instance.NavigateToFreeze();
                NavigateService.Instance.IsNavigatedFromFreezeScreen = true;
            }
        }

        internal void SavePopupStateDataForFreeze()
        {
            NavigateService.Instance.IsCheckoutOptionsOpen =
                PopupService.PopupInstance.IsCheckoutOptionsOpen;

            NavigateService.Instance.IsEnvelopeOpen =
                PopupService.PopupInstance.IsEnvelopeOpen;

            NavigateService.Instance.IsGstPstPopupOpen =
                PopupService.PopupInstance.IsGstPstPopupOpen;

            NavigateService.Instance.IsQitePopupOpen =
                PopupService.PopupInstance.IsQitePopupOpen;

            NavigateService.Instance.IsConfirmationPopupOpen =
                PopupService.PopupInstance.IsConfirmationPopupOpen;

            NavigateService.Instance.IsReturnsPopupOpen =
                PopupService.PopupInstance.IsReturnsPopupOpen;

            NavigateService.Instance.IsAlertPopupOpen =
                PopupService.PopupInstance.IsAlertPopupOpen;

            NavigateService.Instance.IsMessagePopupOpen =
                PopupService.PopupInstance.IsMessagePopupOpen;

            NavigateService.Instance.IsPurchaseOrderPopupOpen
                = PopupService.PopupInstance.IsPurchaseOrderPopupOpen;

            NavigateService.Instance.IsPumpOptionsPopupOpen =
                PopupService.PopupInstance.IsPumpOptionsPopupOpen;

            NavigateService.Instance.IsReasonPopupOpen =
            PopupService.PopupInstance.IsReasonPopupOpen;

            NavigateService.Instance.IsTaxExemptionPopupOpen =
            PopupService.PopupInstance.IsTaxExemptionPopupOpen;

            NavigateService.Instance.IsFngtrPopupOpen =
                PopupService.PopupInstance.IsFngtrPopupOpen;

            NavigateService.Instance.IsPopupWithTextBoxOpen =
              PopupService.PopupInstance.IsPopupWithTextBoxOpen;

            PopupService.PopupInstance.IsMessagePopupOpen =
            PopupService.PopupInstance.IsConfirmationPopupOpen =
            PopupService.PopupInstance.IsReturnsPopupOpen =
            PopupService.PopupInstance.IsAlertPopupOpen =
            PopupService.PopupInstance.IsPurchaseOrderPopupOpen =
            PopupService.PopupInstance.IsCheckoutOptionsOpen =
                PopupService.PopupInstance.IsEnvelopeOpen =
                PopupService.PopupInstance.IsGstPstPopupOpen =
                PopupService.PopupInstance.IsQitePopupOpen =
                PopupService.PopupInstance.IsPumpOptionsPopupOpen =
                PopupService.PopupInstance.IsReasonPopupOpen =
                PopupService.PopupInstance.IsTaxExemptionPopupOpen =
                 PopupService.PopupInstance.IsFngtrPopupOpen =
                PopupService.PopupInstance.IsPopupWithTextBoxOpen = false;
        }

        public static void ClaimOposPrinter()
        {
            new PeripheralsService().ClaimPrinter();
        }

        public static void ReleaseOposPrinter()
        {
            new PeripheralsService().ReleasePrinter();
        }

        protected Task<bool> VerifyPeripheralsConnected()
        {
            Action printerError = default(Action);
            Action cashDrawerError = default(Action);
            Action lineDisplayError = default(Action);

            printerError = new Action(async () =>
            {
                if (CacheBusinessLogic.UseReceiptPrinter && CacheBusinessLogic.UseOposReceiptPrinter)
                {
                    if (OposPrinter == null)
                    {
                        ClaimOposPrinter();
                    }
                    if (OposPrinter != null && OposPrinter.IsAvailable())
                    {
                        cashDrawerError();
                    }
                    else
                    {
                        ShowNotification(ApplicationConstants.NoPrinterFound, cashDrawerError, cashDrawerError, ApplicationConstants.ButtonWarningColor);
                    }
                }
                else
                {
                    ClaimOposPrinter();
                    cashDrawerError();
                }
            });

            cashDrawerError = new Action(async () =>
            {
                if (CacheBusinessLogic.UseCashDrawer && CacheBusinessLogic.UseOposCashDrawer)
                {
                    try
                    {
                        using (var cashDrawer = new OPOSCashDrawer(CacheBusinessLogic.CashDrawerName))
                        {
                        }
                        lineDisplayError();
                    }
                    catch (CashDrawerException ex)
                    {
                        ShowNotification(ApplicationConstants.NoCashDrawerFound, lineDisplayError, lineDisplayError
                                            , ApplicationConstants.ButtonWarningColor);
                    }
                }
                else
                {
                    lineDisplayError();
                }
            });

            lineDisplayError = new Action(async () =>
            {
                if (CacheBusinessLogic.UseCustomerDisplay && CacheBusinessLogic.UseOposCustomerDisplay)
                {
                    try
                    {
                        using (var lineDisplay = new OPOSLineDisplay(CacheBusinessLogic.CustomerDisplayName))
                        {

                        }
                    }
                    catch (LineDisplayException ex)
                    {
                        Log.Warn(ex);
                        DisplayCustomerDisplayMessage = false;
                        ShowNotification(ApplicationConstants.NoCustomerDisplayFound, null, null, ApplicationConstants.ButtonWarningColor);
                    }
                    catch (Exception ex)
                    {
                        Log.Info(Message, ex);
                        DisplayCustomerDisplayMessage = false;
                        ShowNotification(ApplicationConstants.SomethingBadHappned, null, null, ApplicationConstants.ButtonWarningColor);
                    }
                }
            });

            printerError();
            return Task.FromResult<bool>(true);
        }

        public void InvokeKeyBoardEvent()
        {
            new WindowManager().InvokeKeyBoard();
        }
    }
}
