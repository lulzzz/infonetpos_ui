using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Common
{
    public class ReasonsList
    {
        public string ReasonTitle { get; set; }
        public List<Reasons> Reasons { get; set; }
    }

    public class Reasons
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; } = string.Empty;
    }
}
