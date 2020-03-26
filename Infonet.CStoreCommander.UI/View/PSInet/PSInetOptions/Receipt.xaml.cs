using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.ViewModel.PSInet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Infonet.CStoreCommander.UI.View.PSInet.PSInetOptions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Receipt : Page
    {
        public RichTextBlock TextContentBlock { get; set; }
        public PaymentSourceVM PYSVM { get; set; } = SimpleIoc.Default.GetInstance<PaymentSourceVM>();
        List<string> _pl;
        public Receipt()
        {
            this.InitializeComponent();
            //addtext();
            PYSVM.PDataEvent += PYSVM_PDataEvent;
            ScenarioImage.Source = null;
            TextBlock o = Footer.Children[0] as TextBlock;
            if (o != null)
                o.Text = "";
            TextContentBlock = TextContent;
        }

        private void PYSVM_PDataEvent(PSReceipt psrt)
        {
            TextContent.Blocks.Clear();
            ScenarioImage.Source = null;
            ScenarioImageLogo.Source = null;
            VenderName.Text = "";
            UPCCode.Text = "";
            string FontName = "/Assets/fangsong_GB2312.ttf#fangsong_GB2312";
            FontName = "Cambria";
            string sTemp="";
            
            string cr = "\r\n";
            Paragraph ph;
            Run run;
            //_pl = psrt.Lines.ToList();
            switch (psrt.StoreGUI)
            {
                case "TV":
                case "TF":
                    ph = new Paragraph();
                    run = new Run();

                    sTemp = psrt.ProdName + cr;
                    sTemp += string.Format("{0:C2}", psrt.Amount) + cr;
                    run.Text = sTemp;
                    ph.FontSize = 20;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Bold;
                    ph.TextAlignment = TextAlignment.Center;


                    ph.Inlines.Add(run);
                    AddContent(ph);

                    run = new Run();
                    sTemp = "Success" + cr+cr;
                    sTemp += string.Format("{0:C2}", psrt.Amount) + " has been added to" + cr;
                    sTemp += "mobile account " + psrt.Account + "." + cr+cr;


                    sTemp += psrt.Terms;
                    sTemp += cr + "Trans: " + psrt.TransID + cr
                            + "Term: " + psrt.TerminalId + cr
                            + "Rec #: " + psrt.ReferenceNum + cr
                            + "Clerk: " + cr
                            + "Date: " + psrt.TransDate + " Time: " + psrt.TransTime + cr;
                            
                            
                    run.Text = sTemp;
                    ph = new Paragraph();
                    ph.FontSize = 12;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Normal;
                    ph.TextAlignment = TextAlignment.Left;

                    ph.Inlines.Add(run);
                    AddContent(ph);

                    if (psrt.BarCodePath != null)
                    {
                        ScenarioImage.Source = new BitmapImage(new Uri(psrt.BarCodePath, UriKind.Absolute));
                        VenderName.FontSize = 10;
                        VenderName.Text = psrt.VenderName;
                        UPCCode.FontSize = 10;
                        UPCCode.Text = psrt.UPCCode;
                    }

                    if (psrt.LogoPath != null)
                        ScenarioImageLogo.Source = new BitmapImage(new Uri(psrt.LogoPath, UriKind.Absolute));

                    break;
                case "EP":
                case "VP":
                    ph = new Paragraph();
                    run = new Run();

                    
                    run.Text = psrt.ProdName;
                    ph.FontSize = 20;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Bold;
                    ph.TextAlignment = TextAlignment.Center;


                    ph.Inlines.Add(run);
                    AddContent(ph);

                    run = new Run();
                    sTemp = string.Format("{0:C2}", psrt.Amount);
                    
                    run.Text =  sTemp;
                    ph = new Paragraph();
                    ph.FontSize = 20;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Bold;
                    ph.TextAlignment = TextAlignment.Center;



                    ph.Inlines.Add(run);
                    AddContent(ph);

                    run = new Run();
                    sTemp = "This is your PIN:";
                    
                    run.Text = cr  + sTemp + cr;
                    ph = new Paragraph();
                    ph.FontSize = 12;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Normal;
                    ph.TextAlignment = TextAlignment.Center;

                    ph.Inlines.Add(run);
                    AddContent(ph);

                    run = new Run();

                    //run.Text = CenterSpacing(psrt.PIN.Length, 1) + psrt.PIN+cr;
                    run.Text =  psrt.PIN + cr;
                    ph = new Paragraph();
                    ph.FontSize = 20;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Bold;
                    ph.TextAlignment = TextAlignment.Center;

                    ph.Inlines.Add(run);
                    AddContent(ph);


                    run = new Run();
                    //sTemp = "This is your PIN:"+cr+ "Top-up your chatr";
                    
                    sTemp = psrt.Terms;
                    sTemp += cr + "Trans: " + psrt.TransID + cr
                            + "Serial: " + psrt.PinSerial + cr
                            + "Batch: " + psrt.PinBatch + cr
                            + "Date: " + psrt.TransDate + " Time: " + psrt.TransTime + cr
                            + "Term: " + psrt.TerminalId + cr
                            + "Rec #: " + psrt.ReferenceNum + cr;
                    run.Text = sTemp;
                    ph = new Paragraph();
                    ph.FontSize = 12;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Normal;
                    ph.TextAlignment = TextAlignment.Left;

                    ph.Inlines.Add(run);
                    AddContent(ph);

                    if (psrt.BarCodePath != null)
                    {
                        ScenarioImage.Source = new BitmapImage(new Uri(psrt.BarCodePath, UriKind.Absolute));
                        VenderName.FontSize = 10;
                        VenderName.Text = psrt.VenderName;
                        UPCCode.FontSize = 10;
                        UPCCode.Text = psrt.UPCCode;
                    }

                    if (psrt.LogoPath != null)
                        ScenarioImageLogo.Source = new BitmapImage(new Uri(psrt.LogoPath, UriKind.Absolute));
                    break;
                case "PS":
                case "PF":
                    //Receipt title
                    ph = new Paragraph();
                    run = new Run();
                    if(psrt.StatusDescription=="")
                    sTemp = "Activation" + cr + "Successfull" + cr + "Success" + cr;
                    else
                        sTemp = "Activation" + cr + "Successfull" + cr + psrt.StatusDescription + cr;

                    run.Text = sTemp;
                    ph.FontSize = 20;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Bold;
                    ph.TextAlignment = TextAlignment.Center;


                    ph.Inlines.Add(run);
                    AddContent(ph);


                    run = new Run();
                    sTemp = psrt.Terms;
                    sTemp += cr + "Ref # " + psrt.Custom1+cr
                            + "Trans: " + psrt.TransID + cr
                            + "Date: " + psrt.TransDate + " Time: " + psrt.TransTime + cr
                            + "Term: " + psrt.TerminalId + cr
                            + "Rec #: " + psrt.ReferenceNum + cr;
                    run.Text = sTemp;
                    ph = new Paragraph();
                    ph.FontSize = 12;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Normal;
                    ph.TextAlignment = TextAlignment.Left;

                    ph.Inlines.Add(run);
                    AddContent(ph);

                    if (psrt.BarCodePath != null)
                    {
                        ScenarioImage.Source = new BitmapImage(new Uri(psrt.BarCodePath, UriKind.Absolute));
                        VenderName.FontSize = 10;
                        VenderName.Text = psrt.VenderName;
                        UPCCode.FontSize = 10;
                        UPCCode.Text = psrt.UPCCode;
                    }
                    else
                    {
                        run = new Run();
                       
                        run.Text = psrt.VenderName;
                        ph = new Paragraph();
                        ph.FontSize = 12;
                        ph.FontFamily = new FontFamily(FontName);
                        ph.FontWeight = Windows.UI.Text.FontWeights.Normal;
                        ph.TextAlignment = TextAlignment.Center;

                        ph.Inlines.Add(run);
                        AddContent(ph);
                    }
                    

                    break;
                case "PV":
                    //Receipt title
                    ph = new Paragraph();
                    run = new Run();
                    sTemp = psrt.ProdName + cr;
                    sTemp += string.Format("{0:C2}", psrt.Amount)+cr;
                    if(psrt.Fee>0)
                    {
                        sTemp += "+ " + string.Format("{0:C2}", psrt.Fee) + " Fee" + cr;
                    }

                    if (psrt.StatusDescription == "")
                        sTemp += "Activation" + cr + "Successfull" + cr + "Success" + cr;
                    else
                        sTemp += "Activation" + cr + "Successfull" + cr + psrt.StatusDescription + cr;

                    run.Text = sTemp;
                    ph.FontSize = 20;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Bold;
                    ph.TextAlignment = TextAlignment.Center;


                    ph.Inlines.Add(run);
                    AddContent(ph);

                    run = new Run();

                    sTemp = psrt.Terms;
                    sTemp += cr + "Ref # " + psrt.Custom1 + cr
                            + "Trans: " + psrt.TransID + cr
                            + "Date: " + psrt.TransDate + " Time: " + psrt.TransTime + cr
                            + "Term: " + psrt.TerminalId + cr
                            + "Rec #: " + psrt.ReferenceNum + cr;
                    run.Text = sTemp;
                    
                    ph = new Paragraph();
                    ph.FontSize = 12;
                    ph.FontFamily = new FontFamily(FontName);
                    ph.FontWeight = Windows.UI.Text.FontWeights.Normal;
                    ph.TextAlignment = TextAlignment.Left;

                    ph.Inlines.Add(run);
                    AddContent(ph);


                    if (psrt.BarCodePath != null)
                    {
                        ScenarioImage.Source = new BitmapImage(new Uri(psrt.BarCodePath, UriKind.Absolute));
                        VenderName.FontSize = 10;
                        VenderName.Text = psrt.VenderName;
                        UPCCode.FontSize = 10;
                        UPCCode.Text = psrt.UPCCode;
                    }


                    break;
                
            }
        }
        
        
        
        internal void AddContent(Paragraph block)
        {
            TextContent.Blocks.Add(block);
        }
    }
}
