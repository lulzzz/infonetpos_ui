//-----------------------------------------------------------------------
// <copyright file="DataGridRow.cs" company="MyToolkit">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/MyToolkit/MyToolkit/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

#if WINRT

using MyToolkit.Extended.Controls;
using MyToolkit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace MyToolkit.Controls
{
    /// <summary>A <see cref="DataGrid"/> row. </summary>
    public class DataGridRow : Grid
    {
        private ContentControl _detailsControl = null;
        private bool _isSelected = false;
        private static ContentPresenter _previousCellControl = null;
        private static object _cellContent = null;
        private DataGridCellBase _cellToFocus;

        private bool _isFocusInProgress;

        internal DataGridRow(DataGrid dataGrid, object item)
        {
            Item = item;
            DataGrid = dataGrid;

            var hasStar = CreateCells(item);
            // CreateColumnAndRowDefinitions(hasStar);
            dataGrid.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            dataGrid.VerticalContentAlignment = VerticalAlignment.Stretch;
            dataGrid.Padding = new Thickness(0);
            dataGrid.Margin = new Thickness(0);

            Loaded -= OnLoaded;
            Loaded += OnLoaded;
            LayoutUpdated -= SetFocusOnField;
            LayoutUpdated += SetFocusOnField;
            Tapped -= DataGridRow_Holding;
            Tapped += DataGridRow_Holding;

            // TODO: Added this to simulate the hold feature
            RightTapped -= Right_Tapped;
            RightTapped += Right_Tapped;
        }

        private async void SetFocusOnField(object sender, object e)
        {
            if (!DataGrid.IsFocusSet && _cellToFocus != null)
            {
                var textBox = _cellToFocus.Control as CustomTextBox;
                if (textBox != null && !_isFocusInProgress)
                {
                    var maxTries = 1000;
                    while (maxTries-- > 0 && !textBox.Focus(FocusState.Keyboard))
                    {
                        _isFocusInProgress = true;
                        await Task.Delay(10);
                    }
                    _isFocusInProgress = false;
                    DataGrid.IsFocusSet = true;
                    DataGrid.ScrollTillEnd();
                }
            }
        }

        private void DataGridRow_Holding(object sender, TappedRoutedEventArgs e)
        {
            if (DataGrid.DeleteCommand != null)
            {
                ChangeItemSelectionOnHold(sender, true);
            }
        }

        private void Right_Tapped(object sender, RightTappedRoutedEventArgs e)
        {
            if (DataGrid.DeleteCommand != null)
            {
                ChangeItemSelectionOnHold(sender, true);
            }
        }

        private void ChangeItemSelectionOnHold(object item, bool isSelected)
        {
            var index = DataGrid.Columns.Count - 1;
            var column = DataGrid.Columns[index];
            var row = (DataGridRow)item;
            if (DataGrid.Items.IndexOf(row.Item) != DataGrid.Items.Count - 1)
            {
                if (_previousCellControl != null)
                {
                    _previousCellControl.Content = _cellContent;
                }
                var cell = CreateDeleteCell(DataGrid, Item);
                var cellControl = (ContentPresenter)row.Children[index];
                _previousCellControl = cellControl;
                _cellContent = cellControl.Content;
                cell.Control.Tag = cell;
                cell.Control.DataContext = item;
                cellControl.Content = cell;
            }
        }

        public DataGridCellBase CreateDeleteCell(DataGrid dataGrid, object dataItem)
        {
            var deleteButton = new Button
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 209, 52, 56)),
                Content = "\xEA39",
                FontSize = 20,
                FontFamily = new FontFamily("Segoe MDL2 Assets"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                IsEnabled = dataGrid.IsDeleteEnabled
            };

            if (dataGrid.IsDeleteEnabled)
            {
                deleteButton.Click -= DeleteButton_Click;
                deleteButton.Click += DeleteButton_Click;
            }

            return new DefaultDataGridCell(deleteButton);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = ((DataGridRow)((FrameworkElement)sender).DataContext).Item;
            if (DataGrid.DeleteCommand != null)
            {
                DataGrid.DeleteCommand.Execute(item);
            }
        }

        /// <summary>Gets the parent <see cref="DataGrid"/>. </summary>
        public DataGrid DataGrid { get; private set; }

        /// <summary>Gets or sets the associated item. </summary>
        public object Item { get; private set; }

        /// <summary>Gets a value indicating whether the row is selected. </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            internal set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;

                    _cellToFocus = UpdateItemDetails();
                    UpdateRowBackgroundBrush();
                }
            }
        }

        /// <summary>Gets the list of <see cref="DataGridCellBase"/>. </summary>
        public IList<DataGridCellBase> Cells
        {
            get
            {
                return Children
                    .OfType<FrameworkElement>()
                    .Where(c => c.Tag is DataGridCellBase)
                    .Select(c => (DataGridCellBase)c.Tag)
                    .ToList();
            }
        }

        public bool CreateCells(object item)
        {
            var columnIndex = 0;
            var hasStar = false;
            foreach (var column in DataGrid.Columns)
            {
                var cell = column.CreateCell(DataGrid, item);
                cell.Control.Tag = cell;
                cell.Control.DataContext = item;

                var cellControl = new ContentPresenter();
                cellControl.Content = cell;
                cellControl.ContentTemplate = DataGrid.CellTemplate;
                cellControl.Padding = new Thickness(0);
                cellControl.Margin = new Thickness(0);
                cellControl.Height = 43;
                cellControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                cellControl.VerticalContentAlignment = VerticalAlignment.Stretch;
                SetColumn(cellControl, columnIndex++);
                Children.Add(cellControl);

                var columnDefinition = column.CreateGridColumnDefinition();
                hasStar = hasStar || columnDefinition.Width.IsStar;

                ColumnDefinitions.Add(columnDefinition);
            }
            return hasStar;
        }

        private void CreateColumnAndRowDefinitions(bool hasStar)
        {
            if (!hasStar)
                ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // second row used for details view
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            if (DataGrid.SelectionMode == SelectionMode.Single)
            {
                if (DataGrid.SelectedItem != null)
                    IsSelected = DataGrid.SelectedItem.Equals(Item);
            }
            else
            {
                if (DataGrid.SelectedData != null)
                    IsSelected = DataGrid.SelectedData.Contains(Item);
            }

            UpdateRowBackgroundBrush();
        }

        private void UpdateRowBackgroundBrush()
        {
            // TODO: Update row backgrounds when DataGrid.Items changed
            var listBoxItem = this.GetVisualParentOfType<ListBoxItem>();
            if (listBoxItem != null)
            {
                if (IsSelected)
                {
                    var cells = this.Cells;
                    var row = listBoxItem.Content as DataGridRow;
                    if (row != null)
                    {
                        foreach (var rowChild in row.Children)
                        {
                            var contentPresenter = rowChild as ContentPresenter;
                            if (contentPresenter != null)
                            {
                                contentPresenter.Background = new SolidColorBrush(Color.FromArgb(255, 105, 121, 126));
                                var cell = contentPresenter.Content as DefaultDataGridCell;
                                var textblock = cell?.Control as TextBox;
                                if (textblock != null)
                                {
                                    textblock.Foreground =
                                        new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                }
                            }
                        }
                    }

                    listBoxItem.Background = null;
                }
                else
                {
                    listBoxItem.Background = DataGrid.Items.IndexOf(Item) % 2 == 0 ?
                        DataGrid.RowBackgroundOddBrush : DataGrid.RowBackgroundEvenBrush;

                    var row = listBoxItem.Content as DataGridRow;
                    if (row != null)
                    {
                        foreach (var rowChild in row.Children)
                        {
                            var contentPresenter = rowChild as ContentPresenter;
                            if (contentPresenter != null)
                            {
                                contentPresenter.Background = null;
                                var cell = contentPresenter.Content as DefaultDataGridCell;
                                var textblock = cell?.Control as TextBox;
                                if (textblock != null)
                                {
                                    var column = DataGrid.Columns[row.Children.IndexOf(rowChild)] as DataGridTextColumn;
                                    if (column != null)
                                    {
                                        textblock.Foreground = column.Foreground;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private DataGridCellBase UpdateItemDetails()
        {
            var columnIndex = 0;

            DataGridCellBase _cell = null;
            foreach (var column in DataGrid.Columns)
            {
                //Children.
                if (((DataGridTextColumn)column).IsEdit)
                {
                    //item.CreateCell
                    var cell = column.CreateEditableCell(DataGrid, Item, (((DataGridTextColumn)column).FocusOnEditableRow));

                    //   var cellControl= GetContentPreseneterFromItem(column);
                    cell.Control.Tag = cell;
                    cell.Control.DataContext = Item;

                    //  var cellControl = new ContentPresenter();
                    var cellControl = (ContentPresenter)Children[columnIndex];
                    cellControl.Content = cell;
                    cellControl.ContentTemplate = DataGrid.CellTemplate;

                    if (((DataGridTextColumn)column).FocusOnEditableRow)
                    {
                        _cell = cell;
                    }
                }
                columnIndex++;
            }

            return _cell;

        }

        public ContentPresenter GetContentPreseneterFromItem(object item)
        {
            //return (ContentPresenter)ItemsControl.g.ContainerFromItem(item);
            return (ContentPresenter)item;
        }
    }
}

#endif