using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common
{
    public class ReasonListContract
    {
        public string reasonTitle { get; set; }
        public List<Reason> reasons { get; set; }
    }

    public class Reason
    {
        public string code { get; set; }
        public string description { get; set; }
    }

}

