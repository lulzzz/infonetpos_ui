using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class OverLimitDetailsContract
    {
        public bool isGasReasons;
        public bool isTobaccoReasons;
        public bool isPropaneReasons;
        public List<ExplanationContract> gasReasons;
        public List<ExplanationContract> tobaccoReasons;
        public List<ExplanationContract> propaneReasons;
        public List<TaxExemptSaleLineContract> taxExemptSale;
    }

    public class ExplanationContract
    {
        public int explanationCode;
        public string reason;
    }

    public class TaxExemptSaleLineContract
    {
        public string type;
        public string product;
        public string quantity;
        public string regularPrice;
        public string taxFreePrice;
        public string exemptedTax;
        public string quotaUsed;
        public string quotaLimit;
    }
}
