using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Reflection;
//using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml.Media.Imaging;

namespace Infonet.CStoreCommander.UI.Utility
{
    public class Helper { 

        //Added by Tony 07/29/2019
        public static List<Report> ReceiptLabelMapping(List<Report> rcs, string ReceiptType)
    {
        //"default";
        if (rcs == null || rcs.Count == 0)
            return rcs;

        if (ReceiptType.ToLower() == "default")
            return rcs;
        switch (ReceiptType.ToLower())
        {
            case "en-ar":
                rcs = English_Arabic(rcs);
                break;

        }
        return rcs;
    }
    private static List<Report> English_Arabic(List<Report> rcs)
    {
        foreach (Report r in rcs)
        {
            r.ReportContent = En_Ar(r.ReportContent);
        }
        return rcs;
    }
    public static List<string> En_Ar_List(List<string> reclines)
    {
        int i;
        string sTemp;

        int k = 0;


        //SALE RECEIPT
        i = reclines.FindIndex(x => x.Contains("SALE RECEIPT"));

        //Don't do anything if the receipt is not in English and it is not a SALE RECEIPT.
        if (i < 0)
            return reclines;
        if (k < i)
        {
            k = i;
        }
        reclines.Insert(i + 1, "          إيصال المبيعات                ");
        //sTemp = reclines[i + 1].Replace("SALE RECEIPT", "");
        //sTemp = sTemp.Trim();
        //reclines[i + 1] = reclines[i + 1].Replace(sTemp, "");
        //reclines[i + 1] = reclines[i + 1].Replace("SALE RECEIPT", "إيصال المبيعات");

        //Total keyword
        //i = 0;
        //for (int j = 0; j < reclines.Count; j++)
        //{
        //    if (reclines[j].Contains("Total"))
        //    {
        //        reclines[j] = reclines[j].Replace("Total", "Total" + i + ":");
        //        i += 1;
        //    }
        //}

        //Customer
        i = reclines.FindIndex(x => x.Contains("Customer:"));
        if (i >= 0)
            reclines.Insert(i + 1, "          طريقة ا                       ");
        if (k < i)
        {
            k = i+1;
                k += 1;
        }
        //sTemp = reclines[i + 1].Replace("Customer:", "");
        //sTemp = sTemp.Trim();
        //reclines[i + 1] = reclines[i + 1].Replace(sTemp, "");
        //reclines[i + 1] = reclines[i + 1].Replace("Customer:", "رقم المحاسب");
        //Cashier:
        i = reclines.FindIndex(k, x => x.Contains("Cashier:"));

        if (i >= 0)
            reclines.Insert(i + 1, "      أمين الصندوق                      ");
        if (k < i)
        {
                k = i + 1;
                k += 1;
            }
        //sTemp = reclines[i + 1].Replace("Cashier:", "");
        //sTemp = sTemp.Trim();
        //reclines[i + 1] = reclines[i + 1].Replace(sTemp, "");
        //reclines[i + 1] = reclines[i + 1].Replace("Cashier:", "أمين الصندوق");
        //Quantity  Reg Price
        i = reclines.FindIndex(k, x => x.Contains("Quantity  Reg Price"));
        if (i >= 0)
        {
            sTemp = " " + "الكمية" + "    " + "سعر الوحدة" + "      " + "السعر" + "    " + "القيمة";
            reclines.Insert(i + 1, sTemp);
        }
        if (k < i)
        {
                k = i + 1;
                k += 1;
          }
        //sTemp = reclines[i + 1];
        //sTemp = sTemp.Replace("Quantity", "الكمية");
        //sTemp = sTemp.Replace("Reg Price", "سعر الوحدة");
        //sTemp = sTemp.Replace("Price", "السعر");
        //sTemp = sTemp.Replace("Amount", "القيمة");
        //reclines[i + 1] = sTemp;

        //Sub Total
        i = reclines.FindIndex(k, x => x.Contains("Sub Total"));
        if (i >= 0)
        {
            reclines.Insert(i + 1, "المجموع");
            //sTemp = reclines[i + 1].Replace("Sub Total0:", "");
            //sTemp = sTemp.Trim();
            //reclines[i + 1] = reclines[i + 1].Replace(sTemp, "");
            //reclines[i + 1] = reclines[i + 1].Replace("Sub Total0:", "المجموع");
            //reclines[i] = reclines[i].Replace("Sub Total0:", "Sub Total");
        }
        if (k < i)
        {
                k = i + 1;
                k += 1;
         }
        //Total
        i = reclines.FindIndex(k, x => x.Contains("Total"));
        if (i >= 0)
        {
            reclines.Insert(i + 1, "المجموع النهائي");
            //sTemp = reclines[i + 1].Replace("Total1:", "");
            //sTemp = sTemp.Trim();
            //reclines[i + 1] = reclines[i + 1].Replace(sTemp, "");
            //reclines[i + 1] = reclines[i + 1].Replace("Total1:", "المجموع النهائي");
            //reclines[i] = reclines[i].Replace("Total1:", "Total");
        }
        if (k < i)
        {
                k = i + 1;
                k += 1;
            }
        //GST

        i = reclines.FindIndex(k, x => x.Contains("GST"));
        if (i >= 0)
            reclines.Insert(i + 1, "الضريبة");
            //sTemp = reclines[i + 1].Replace("GST", "");
            //sTemp = sTemp.Trim();
            //reclines[i + 1] = reclines[i + 1].Replace(sTemp, "");
            //reclines[i + 1] = reclines[i + 1].Replace("GST", "الضريبة");

            //Cash , Card, Credit Card
            if (k < i)
            {
                k = i + 1;
                k += 1;
            }
            i = reclines.FindIndex(k, x => x.Contains("Cash"));
        if (i >= 0)
        {
            reclines.Insert(i + 1, "نقدا");
        }
        else
        {
            i = reclines.FindIndex(k, x => x.Contains("Card"));
            if (i >= 0)
            {
                reclines.Insert(i + 1, "بطاقة");
            }
            else
            {
                i = reclines.FindIndex(k, x => x.Contains("Credit Card"));
                if (i >= 0)
                    reclines.Insert(i + 1, "بطاقة الائتمان");
            }
        }
            if (k < i)
            {
                k = i + 1;
                k += 1;
            }
            //Total Tendered
            i = reclines.FindIndex(k, x => x.Contains("Total Tendered"));
        if (i >= 0)
        {
            reclines.Insert(i + 1, "القيمة النهائية");
            //sTemp = reclines[i + 1].Replace("Total2: Tendered", "");
            //sTemp = sTemp.Trim();
            //reclines[i + 1] = reclines[i + 1].Replace(sTemp, "");
            //reclines[i + 1] = reclines[i + 1].Replace("Total2: Tendered", "القيمة النهائية");
            //reclines[i] = reclines[i].Replace("Total2: Tendered", "Total Tendered");
        }
        return reclines;
    }
    public static string En_Ar(string Content)
    {
        if (Content == null)
            return null;
        string[] arr = Regex.Split(Content, "\r\n");
        List<string> reclines = En_Ar_List(arr.ToList());
         string sTemp = "";
                   
        for (int j = 0; j < reclines.Count; j++)
        {
            sTemp += reclines[j].Replace("  $","SAR") + "\r\n";
        }


        return sTemp;
    }
        //Added by Tony 07/29/2019---End
   
        private static readonly char[] ShiftNumLookup = { ')', '!', '@', '#', '$', '%', '^', '&', '*', '(' };

        internal async Task<List<int>> GetTillsOffline()
        {
            var folderContainingTillsFile = await GetLocalFolder();

            var tillsFile = await folderContainingTillsFile.GetFileAsync(ApplicationConstants.TillsFileName);

            var fileContent = await FileIO.ReadLinesAsync(tillsFile);
            var offlineResponse = 0;
            foreach (var line in fileContent)
            {
                if (line.StartsWith("Till="))
                {
                    var tills = line.Split('=');
                    int.TryParse(tills[1], out offlineResponse);
                    break;
                }
            }

            return new List<int> { offlineResponse };
        }
        public static async Task<XDocument> SockeRequest(
            string ip,
            string port,
            string strReq
            )
        {
            XDocument doc = null;
            try
            {
                var socket = new Windows.Networking.Sockets.StreamSocket();
                var serverHost = new Windows.Networking.HostName(ip);
                await socket.ConnectAsync(serverHost, port);

                //Send request
                DataWriter writer = new DataWriter(socket.OutputStream);

                writer.WriteUInt32(writer.MeasureString(strReq));
                writer.WriteString(strReq);
                await writer.StoreAsync();
                await writer.FlushAsync();

                writer.DetachStream();
                writer.Dispose();


                //Read response
                Stream streamIn = socket.InputStream.AsStreamForRead();
                int bsize = 4096;
                byte[] buffer = new byte[bsize];
                int readByte = await streamIn.ReadAsync(
                        buffer,
                        0,
                        bsize
                        );
                if (readByte <= 0)
                {
                    //return null if there is no response
                    return doc;
                }
                byte[] rdata = new byte[readByte];
                Array.Copy(buffer, rdata, readByte);
                streamIn.Dispose();
                socket.Dispose();


                doc = XDocument.Parse(Encoding.ASCII.GetString(rdata));
            }
            catch (Exception ex)
            {

            }
            return doc;
        }
        public static async Task<bool> IfStorageItemExist(StorageFolder folder, string itemName)
        {
            try
            {
                IStorageItem item = await folder.TryGetItemAsync(itemName);
                return (item != null);
            }
            catch (Exception ex)
            {
                // Should never get here 
                return false;
            }
        }
        internal async Task<byte> GetRegisterNumber()
        {
            var folderContainingTillsFile = await GetLocalFolder();

            var tillsFile = await folderContainingTillsFile.GetFileAsync(ApplicationConstants.TillsFileName);

            var fileContent = await FileIO.ReadLinesAsync(tillsFile);
            byte offlineResponse = 0;
            foreach (var line in fileContent)
            {
                if (line.StartsWith("RegisterNumber="))
                {
                    var registerNumber = line.Split('=');
                    byte.TryParse(registerNumber[1], out offlineResponse);
                    break;
                }
            }

            return offlineResponse;
        }

        internal async Task<bool> DeleteAllSignatures()
        {
            var folder = await GetLocalFolder();

            try
            {
                var files = await folder.GetFilesAsync();
                foreach (var file in files)
                {
                    if (file.FileType.ToUpper().Contains("JPG") || file.FileType.ToUpper().Contains("PNG") || file.FileType.ToUpper().Contains("BMP"))
                    {
                        await file.DeleteAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal static string SelectDecimalValue(string inputString, string previousValue)
        {
            if (inputString.Count(x => x == '.') > 1)
            {
                return previousValue;
            }

            var filteredString = new StringBuilder("");
            var decimalPointsCount = 0;

            foreach (var ch in inputString)
            {
                if (ch >= '0' && ch <= '9')
                {
                    filteredString.Append(ch);
                }
                if (ch == '.' && decimalPointsCount == 0)
                {
                    filteredString.Append(ch);
                    decimalPointsCount++;
                }
            }

            return filteredString.ToString();
        }

        internal static string SelectAllDecimalValue(string inputString, string previousValue)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return string.Empty;
            }

            if (inputString.Count(x => x == '.') > 1)
            {
                return previousValue;
            }

            var filteredString = new StringBuilder("");
            var decimalPointsCount = 0;
            var minusCount = 0;

            foreach (var ch in inputString)
            {
                if (ch >= '0' && ch <= '9')
                {
                    filteredString.Append(ch);
                }
                else if (ch == '.' && decimalPointsCount == 0)
                {
                    filteredString.Append(ch);
                    decimalPointsCount++;
                }
                else if (ch == '-' && minusCount == 0)
                {
                    filteredString.Append(ch);
                    minusCount++;
                }
            }

            return filteredString.ToString();
        }

        internal static string SelectIntegers(string inputString)
        {
            var filteredString = new StringBuilder("");
            foreach (var ch in inputString)
            {
                if (ch >= '0' && ch <= '9')
                {
                    filteredString.Append(ch);
                }
            }
            return filteredString.ToString();
        }

        internal static bool IsEnterKey(dynamic args)
        {
            VirtualKey pressedKey;

            // Look if Enter is pressed
            return Enum.TryParse<VirtualKey>(args.OriginalKey.ToString(), out pressedKey)
                   && pressedKey == VirtualKey.Enter;
        }

        internal static bool IsValidURI(string uri)
        {
            Uri tmp;
            if (Uri.TryCreate(uri, UriKind.Absolute, out tmp) && !string.IsNullOrEmpty(uri))
                return true;
            return false;
        }

        internal static string GetResourceString(string resourceName, string languageTag)
        {
            try
            {
                var language = new UtilityHelper().LanguageToLanguageTag(languageTag);
                if (ApplicationLanguages.PrimaryLanguageOverride != language)
                {
                    ApplicationLanguages.PrimaryLanguageOverride = language;
                    ResourceContext.GetForViewIndependentUse().Reset();
                }
                var resourceContext = new ResourceContext();
                ResourceMap libmap = ResourceManager.Current.MainResourceMap.GetSubtree("ApplicationConstants.Resources");
                var resourceString = libmap.GetValue(resourceName, resourceContext).ValueAsString;
                return resourceString;
                //var language = new UtilityHelper().LanguageToLanguageTag(languageTag);
                //var resourceManager = new System.Resources.ResourceManager("ApplicationConstants.Resources", typeof(Helper).GetTypeInfo().Assembly);


                //CultureInfo ci = new CultureInfo(language);

                //var resourceString = resourceManager.GetString(resourceName,ci);

                //return resourceString;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        internal async void EnsureDataFileExists()
        {
            var folderContainingTillsFile = await GetLocalFolder();

            StorageFile file;
            try
            {
                file = await folderContainingTillsFile.GetFileAsync(ApplicationConstants.TillsFileName);
            }
            catch (Exception)
            {
                var fileContent = new List<string>
                {
                    "Till=1",
                    "RegisterNumber=1"
                };
                file = await folderContainingTillsFile.CreateFileAsync(ApplicationConstants.TillsFileName);
                await FileIO.WriteLinesAsync(file, fileContent);
            }
            await FileIO.ReadTextAsync(file);
        }

        public static async Task<StorageFolder> GetLocalFolder()
        {
            var localCacheFolder = ApplicationData.Current.LocalFolder;
            var folderContainingTillsFile = await localCacheFolder.CreateFolderAsync(ApplicationConstants.CacheFolderName,
                CreationCollisionOption.OpenIfExists);
            return folderContainingTillsFile;
        }

        public static bool IfFileExists(string fileName)
        {
            var folder = ApplicationData.Current.LocalFolder;
            string filePath = Path.Combine(folder.Path, fileName);

            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Function to convert virtual keys to character
        /// </summary>
        /// <param name="key"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static char? ToChar(VirtualKey key, bool shift)
        {// convert virtual key to char

            if (32 == (int)key)
                return ' ';

            VirtualKey search;

            // look for simple letter
            foreach (var letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                if (Enum.TryParse<VirtualKey>(letter.ToString(), out search) && search.Equals(key))
                    return (shift) ? letter : letter.ToString()[0];
            }

            // look for simple number
            foreach (var number in "1234567890")
            {
                if (Enum.TryParse<VirtualKey>("Number" + number.ToString(), out search) && search.Equals(key))
                    return (shift) ? ShiftNumLookup[int.Parse((number).ToString(), NumberStyles.Any, CultureInfo.InvariantCulture)] : number;
            }

            if (key == VirtualKey.Enter)
            {
                return '\n';
            }

            // look for ?
            if (191 == (int)key)
            {
                return (shift) ? '?' : '/';
            }

            // look for '=' & '+'
            if (187 == (int)key)
            {
                return (shift) ? '+' : '=';
            }
            // look for ';' & ':'
            if (186 == (int)key)
            {
                return (shift) ? ':' : ';';
            }
            // not found
            return null;
        }

        public static string EncodeToBase64(string data)
        {
            var paymentDataEncyption = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(paymentDataEncyption);
        }

        public async static Task<Uri> Base64StringToBitmap(string source)
        {
            var ims = new InMemoryRandomAccessStream();
            var bytes = Convert.FromBase64String(source);

            var localFolder = await GetLocalFolder();
            var fileName = Guid.NewGuid().ToString();
            StorageFile jpegFile = await localFolder.CreateFileAsync(fileName + ".jpg");
            await FileIO.WriteBytesAsync(jpegFile, bytes);

            return new Uri(jpegFile.Path);
        }

        internal string GetTrack2Data(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }

            string cardNumber = string.Empty;
            var firstIndex = data.LastIndexOf(';');
            var secondIndex = data.LastIndexOf('?');
            if (firstIndex > -1 && secondIndex > firstIndex)
            {
                return data.Substring(firstIndex, secondIndex - firstIndex + 1).Replace(" ", "");
            }
            else if (firstIndex == -1 || secondIndex == -1)
            {
                return data;
            }
            return string.Empty;
        }

        internal static bool IsKickBackCardNumber(string kickBackNumber)
        {
            return kickBackNumber.Contains("?") && kickBackNumber.Contains(";");
        }

        internal static double GetDoubleValue(string value)
        {
            double balance = 0D;
            double.TryParse(value, out balance);
            return balance;
        }
    }
}
