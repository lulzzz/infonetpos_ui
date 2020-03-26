using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Login
{
    /// <summary>
    /// Data Contract for Active shifts
    /// </summary>
    public class ActiveShiftsContract
    {
        /// <summary>
        /// List of Shifts
        /// </summary>
        public List<Shift> shifts { get; set; }

        /// <summary>
        /// Are shifts used for the Day
        /// </summary>
        public bool shiftsUsedForDay { get; set; }

        /// <summary>
        /// Cash float
        /// </summary>
        public string cashFloat { get; set; }

        /// <summary>
        /// Forcefully login after shifts
        /// </summary>
        public bool forceShift { get; set; }
    }

    /// <summary>
    /// Data Contract for a Shift
    /// </summary>
    public class Shift
    {
        public string shiftDate;

        /// <summary>
        /// Shift Number
        /// </summary>
        public int shiftNumber { get; set; }

        /// <summary>
        /// Display Format of the Shift
        /// </summary>
        public string displayFormat { get; set; }
    }
}
