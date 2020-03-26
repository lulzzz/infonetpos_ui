using MyToolkit.Controls;
using System;

namespace MyToolkit.Extended.Controls.DataGrid
{
    class DataGridSelectedColumn : DataGridBoundColumn
    {
        public override DataGridCellBase CreateCell(MyToolkit.Controls.DataGrid dataGrid, object dataItem)
        {
            throw new NotImplementedException();
        }

        public override DataGridCellBase CreateEditableCell(MyToolkit.Controls.DataGrid dataGrid, object dataItem, bool setFocus)
        {
            throw new NotImplementedException();
        }
    }
}
