using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common
{
    public class SoundContract
    {
        public List<SoundEntityContract> pumpSounds { get; set; }
        public List<SoundEntityContract> deviceSounds { get; set; }
        public List<SoundEntityContract> systemSounds { get; set; }
    }

    public class SoundEntityContract
    {
        public string name { get; set; }
        public string file { get; set; }
    }

}