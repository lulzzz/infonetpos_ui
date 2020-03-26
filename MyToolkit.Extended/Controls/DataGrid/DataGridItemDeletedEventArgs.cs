using System;

namespace MyToolkit.Extended.Controls.DataGrid
{
    public class DataGridItemDeletedEventArgs : EventArgs
    {
        internal DataGridItemDeletedEventArgs(object item)
        {
            Item = item;
        }

        /// <summary>Gets ordering column. </summary>
        public object Item { private set; get; }
    }
}
