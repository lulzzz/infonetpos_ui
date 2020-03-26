using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPrice
{
    public class PricesToDisplayContract
    {
        public List<string> grades { get; set; }
        public List<string> tiers { get; set; }
        public List<string> levels { get; set; }
        public List<ComboboxStateContract> gradesState { get; set; }
        public List<ComboboxStateContract> tiersState { get; set; }
        public List<ComboboxStateContract> levelsState { get; set; }
    }

    public class ComboboxStateContract
    {
        public bool isEnabled { get; set; }
        public string selectedValue { get; set; }
    }
}
