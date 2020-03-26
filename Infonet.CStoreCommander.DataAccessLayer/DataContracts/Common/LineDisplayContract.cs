using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common
{
    public class LineDisplayContract
    {
        public string oposText1 { get; set; }
        public string oposText2 { get; set; }
        public List<string> nonOposTexts { get; set; }
    }
}
