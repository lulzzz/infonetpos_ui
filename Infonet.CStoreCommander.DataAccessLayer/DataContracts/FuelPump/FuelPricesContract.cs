using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class FuelPricesContract
    {
        public List<FuelPriceContract> fuelPrices { get; set; }
        public ReportContract report { get; set; }
        public bool isTaxExemptionVisible { get; set; }
        public bool isReadTotalizerEnabled { get; set; }
        public bool isReadTotalizerChecked { get; set; }
        public bool isPricesToDisplayEnabled { get; set; }
        public bool isPricesToDisplayChecked { get; set; }
        public bool isReadTankDipEnabled { get; set; }
        public bool isReadTankDipChecked { get; set; }
        public bool canReadTotalizer { get; set; }
        public bool canSelectPricesToDisplay { get; set; }
        public bool isExitEnabled { get; set; }
        public bool isErrorEnabled { get; set; }
        public string caption { get; set; }
        public bool isCashPriceEnabled { get; set; }
        public bool isCreditPriceEnabled { get; set; }
        public bool isTaxExemptedCashPriceEnabled { get; set; }
        public bool isTaxExemptedCreditPriceEnabled { get; set; }
        public bool isIncrementEnabled { get; set; }
        public int tillNumber { get; set; }
    }

    public class FuelPriceContract
    {
        public int row { get; set; }
        public string grade { get; set; }
        public short gradeId { get; set; }
        public string tier { get; set; }
        public short tierId { get; set; }
        public string level { get; set; }
        public short levelId { get; set; }
        public string cashPrice { get; set; }
        public string creditPrice { get; set; }
        public string taxExemptedCashPrice { get; set; }
        public string taxExemptedCreditPrice { get; set; }
    }
}
