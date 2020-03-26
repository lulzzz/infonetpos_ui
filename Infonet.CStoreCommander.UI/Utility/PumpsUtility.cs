using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Infonet.CStoreCommander.UI.Utility
{
    public class PumpsUtility
    {
        private static readonly InfonetLog _log = InfonetLogManager.GetLogger<PumpsUtility>();
        
        private static InitalizeFuelPumpContract DeseralizePumpStatus(string pumpStatus)
        {
            _log.Info("Deseralizing pump message");
            InitalizeFuelPumpContract fuelPumpsContract;
            var bytes = Encoding.Unicode.GetBytes(pumpStatus);
            using (var stream = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(InitalizeFuelPumpContract));
                fuelPumpsContract = (InitalizeFuelPumpContract)serializer.ReadObject(stream);
            }
            return fuelPumpsContract;
        }

        internal static InitializeFuelPump GetPumpStatus(string pumpStatus)
        {
            var fuelPumps = new InitializeFuelPump();

            var fuelPumpsContract = DeseralizePumpStatus(pumpStatus);

            if (fuelPumpsContract != null)
            {
                fuelPumps = new InitializeFuelPump
                {
                    IsCurrentEnabled = fuelPumpsContract.isCurrentEnabled,
                    IsErrorEnabled = fuelPumpsContract.isErrorEnabled,
                    IsFinishEnabled = fuelPumpsContract.isFinishEnabled,
                    IsFuelPriceEnabled = fuelPumpsContract.isFuelPriceEnabled,
                    IsManualEnabled = fuelPumpsContract.isManualEnabled,
                    IsPrepayEnabled = fuelPumpsContract.isPrepayEnabled,
                    IsPropaneEnabled = fuelPumpsContract.isPropaneEnabled,
                    IsResumeButtonEnabled = fuelPumpsContract.isResumeButtonEnabled,
                    IsStopButtonEnabled = fuelPumpsContract.isStopButtonEnabled,
                    IsTierLevelEnabled = fuelPumpsContract.isTierLevelEnabled,
                    BigPumps = new List<BigPumps>(),
                    Pumps = new List<PumpStatus>()
                };

                if (fuelPumpsContract.bigPumps != null)
                {
                    fuelPumps.BigPumps = (from b in fuelPumpsContract.bigPumps
                                          select new BigPumps
                                          {
                                              Amount = b.amount,
                                              IsPumpVisible = b.isPumpVisible,
                                              PumpId = b.pumpId,
                                              PumpLabel = b.pumpLabel,
                                              PumpMessage = b.pumpMessage
                                          }).ToList();
                }

                if (fuelPumpsContract.pumps != null)
                {
                    fuelPumps.Pumps = (from f in fuelPumpsContract.pumps
                                       select new PumpStatus
                                       {
                                           BasketButtonCaption = f.basketButtonCaption,
                                           BasketButtonVisible = f.basketButtonVisible,
                                           BasketLabelCaption = f.basketLabelCaption,
                                           PrepayText = f.prepayText,
                                           PumpButtonCaption = f.pumpButtonCaption,
                                           PumpId = f.pumpId,
                                           Status = f.status != null ? f.status : string.Empty,
                                           PayPumporPrepay = f.payPumporPrepay,
                                           EnableBasketButton = f.enableBasketBotton,
                                           EnableStackBasketButton = f.enableStackBasketBotton
                                       }).ToList();
                }
            }

            return fuelPumps;
        }
    }

    #region Pumps Contract
    public class InitalizeFuelPumpContract
    {
        public bool isPrepayEnabled { get; set; }
        public bool isFinishEnabled { get; set; }
        public bool isManualEnabled { get; set; }
        public bool isCurrentEnabled { get; set; }
        public bool isFuelPriceEnabled { get; set; }
        public bool isTierLevelEnabled { get; set; }
        public bool isPropaneEnabled { get; set; }
        public bool isStopButtonEnabled { get; set; }
        public bool isResumeButtonEnabled { get; set; }
        public bool isErrorEnabled { get; set; }
        public List<BigPumpsContract> bigPumps { get; set; }
        public List<PumpStatusContract> pumps { get; set; }

    }

    public class BigPumpsContract
    {
        public string pumpId { get; set; }
        public string isPumpVisible { get; set; }
        public string pumpLabel { get; set; }
        public string pumpMessage { get; set; }
        public string amount { get; set; }
    }

    public class PumpStatusContract
    {
        public int pumpId { get; set; }
        public string status { get; set; }
        public string pumpButtonCaption { get; set; }
        public string basketButtonCaption { get; set; }
        public int basketButtonVisible { get; set; }
        public string basketLabelCaption { get; set; }
        public bool payPumporPrepay { get; set; }
        public string prepayText { get; set; }
        public bool enableBasketBotton { get; set; }
        public bool enableStackBasketBotton { get; set; }
    }
    #endregion
}
