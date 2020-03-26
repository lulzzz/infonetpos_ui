//-----------------------------------------------------------------------
// <copyright file="DataGrid.cs" company="MyToolkit">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/MyToolkit/MyToolkit/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

#if WINRT

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using MyToolkit.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using MyToolkit.UI;
using MyToolkit.Utilities;
using System.Windows.Input;

namespace MyToolkit.Controls
{
    /// <summary>A data grid control. </summary>
    public class DataGrid : Control
    {
        private Grid _titleRowControl;
        private MtListBox _listControl;
        private bool _initialized = false;
        private object _initialSelectedItem;
        private object _initialFilter;

        internal bool IsFocusSet;

        /// <summary>Initializes a new instance of the <see cref="DataGrid"/> class. </summary>
        public DataGrid()
        {
            DefaultStyleKey = typeof(DataGrid);
            Columns = new DataGridColumnCollection(); // Initialize collection so that columns can be defined in XAML

            if (!Designer.IsInDesignMode)
                Loaded += OnLoaded;
        }

        private void ScrolledToEnd(object sender, ScrolledToEndEventArgs e)
        {
            if (ScrolledTillEnd != null)
            {
                ScrolledTillEnd(sender, e);
            }
        }

        public event EventHandler<ScrolledToEndEventArgs> ScrolledTillEnd;

        /// <summary>Occurs when the selected item (row) has changed. </summary>
        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        //public event EventHandler<>

        /// <summary>Occurs when the order of the grid has changed (column or direction). </summary>
        public event EventHandler<DataGridOrderChangedEventArgs> OrderChanged;

        /// <summary>Occurs when the user clicked on an item and wants to navigate to its detail page. </summary>
        public event EventHandler<NavigationListEventArgs> Navigate;

        /// <summary>Gets the list of currently selected items. </summary>
        public IList<object> SelectedData
        {
            get { return _listControl?.SelectedItems; }
        }

        /// <summary>Gets the currently displayed items. </summary>
        public IObservableCollectionView Items
        {
            get { return _listControl == null ? null : _listControl.ItemsSource as IObservableCollectionView; }
        }

        /// <summary>Gets the selected column. </summary>
        public DataGridColumnBase SelectedColumn { get; private set; }

        public bool SelectionAllowed
        {
            get { return (bool)GetValue(SelectionAllowedProperty); }
            set { SetValue(SelectionAllowedProperty, value); }
        }

        public static readonly DependencyProperty SelectionAllowedProperty = DependencyProperty.Register(
            "SelectionAllowed", typeof(bool), typeof(DataGrid), new PropertyMetadata(true));

        public bool IsDeleteEnabled
        {
            get { return (bool)GetValue(IsDeleteEnabledProperty); }
            set { SetValue(IsDeleteEnabledProperty, value); }
        }


        public static readonly DependencyProperty IsDeleteEnabledProperty = DependencyProperty.Register(
            "IsDeleteEnabled", typeof(bool), typeof(DataGrid), new PropertyMetadata(false));

        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set
            {
                if (value != null)
                {
                    SetValue(DeleteCommandProperty, value);
                }
            }
        }

        public bool UpdateOnlyOnEnter
        {
            get { return (bool)GetValue(UpdateOnlyOnEnterProperty); }
            set { SetValue(UpdateOnlyOnEnterProperty, value); }
        }

        public static readonly DependencyProperty UpdateOnlyOnEnterProperty =
             DependencyProperty.Register(nameof(UpdateOnlyOnEnter),
                 typeof(bool),
                 typeof(DataGrid),
                 new PropertyMetadata(true));


        // Using a DependencyProperty as the backing store for DeleteCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand), typeof(ICommand), typeof(DataGrid), new PropertyMetadata(null));


        #region Dependency Properties

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(DataGrid), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty HeaderForegroundProperty =
        DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(DataGrid), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty ScrolledTillEndProperty =
     DependencyProperty.Register("ScrolledTillEnd", typeof(object), typeof(DataGrid), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(SelectionMode), typeof(DataGrid), new PropertyMetadata(SelectionMode.Single));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(DataGrid), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemToAddProperty =
            DependencyProperty.Register("ItemToAdd", typeof(object), typeof(DataGrid), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(List<object>), typeof(DataGrid), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(DataGrid),
                new PropertyMetadata(null, (obj, args) => ((DataGrid)obj).UpdateItemsSource()));

        public static readonly DependencyProperty DefaultOrderIndexProperty =
            DependencyProperty.Register("DefaultOrderIndex", typeof(int), typeof(DataGrid), new PropertyMetadata(-1));

        public static readonly DependencyProperty RowStyleProperty =
            DependencyProperty.Register("RowStyle", typeof(Style), typeof(DataGrid), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty ItemDetailsTemplateProperty =
            DependencyProperty.Register("ItemDetailsTemplate", typeof(DataTemplate), typeof(DataGrid), new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(DataGrid), new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.RegisterAttached("Columns",
            typeof(ObservableCollection<DataGridColumnBase>), typeof(DataGrid), new PropertyMetadata(null, OnColumnsPropertyChanged));

        public static readonly DependencyProperty CellTemplateProperty =
            DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(DataGrid), new PropertyMetadata(default(DataTemplate)));

        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register(nameof(HeaderHeight), typeof(double), typeof(DataGrid), new PropertyMetadata(40D));

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register(nameof(HeaderFontSize), typeof(double), typeof(DataGrid), new PropertyMetadata(12D));

        public static readonly DependencyProperty FocusOnEditableRowProperty = DependencyProperty.Register(
            "FocusOnEditableRow", typeof(bool), typeof(DataGrid), new PropertyMetadata(false));

        public bool FocusOnEditableRow
        {
            get { return (bool)GetValue(FocusOnEditableRowProperty); }
            set
            {
                SetValue(FocusOnEditableRowProperty, value);
                if (value)
                {
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items[Items.Count - 1];
                        IsFocusSet = false;
                    }
                }
            }
        }

        /// <summary>Gets or sets the header background. </summary>
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        /// <summary>Gets or sets the selection mode (default: single). </summary>
        public SelectionMode SelectionMode
        {
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        /// <summary>Gets or sets the selected item. </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public List<object> SelectedItems
        {
            get { return SelectedData.ToList(); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public List<object> SelectedValues
        {
            get { return _listControl.SelectedItems.ToList(); }
            set { SetValue(SelectedValuesProperty, value); }
        }

        public object ItemToAdd
        {
            get { return (object)GetValue(ItemToAddProperty); }
            set
            {
                SetValue(ItemToAddProperty, value);
                if (value != null)
                {
                    SelectedData.Add(value);
                }
            }
        }

        /// <summary>Gets or sets the items collection to show in the <see cref="DataGrid"/>.</summary>
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>Gets or sets the index of the column which is initially ordered.</summary>
        public int DefaultOrderIndex
        {
            get { return (int)GetValue(DefaultOrderIndexProperty); }
            set { SetValue(DefaultOrderIndexProperty, value); }
        }

        /// <summary>Used to change the row style, the ItemContainerStyle of the internal ListBox; use ListBoxItem as style target type.</summary>
        public Style RowStyle
        {
            get { return (Style)GetValue(RowStyleProperty); }
            set { SetValue(RowStyleProperty, value); }
        }

        /// <summary>Gets or sets the data template for item details (shown when an item is selected). When null then no details are shown.</summary>
        public DataTemplate ItemDetailsTemplate
        {
            get { return (DataTemplate)GetValue(ItemDetailsTemplateProperty); }
            set { SetValue(ItemDetailsTemplateProperty, value); }
        }

        /// <summary>Gets or sets the header data template (styling of column container).</summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>Gets or sets the cell data template (styling of cell container).</summary>
        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }

        /// <summary>Gets the column description of the <see cref="DataGrid"/>. </summary>
        public DataGridColumnCollection Columns
        {
            get { return (DataGridColumnCollection)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public IList<object> SelectedRows
        {
            get { return (IList<object>)GetValue(SelectedRowsProperty); }
            set { SetValue(SelectedRowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedRows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedRowsProperty =
            DependencyProperty.Register("SelectedRows", typeof(IList<object>), typeof(DataGrid), new PropertyMetadata(new List<object>()));



        public static readonly DependencyProperty RowBackgroundOddBrushProperty = DependencyProperty.Register(
            "RowBackgroundOddBrush", typeof(Brush), typeof(DataGrid), new PropertyMetadata(default(Brush)));

        public Brush RowBackgroundOddBrush
        {
            get { return (Brush)GetValue(RowBackgroundOddBrushProperty); }
            set { SetValue(RowBackgroundOddBrushProperty, value); }
        }

        public static readonly DependencyProperty RowBackgroundEvenBrushProperty = DependencyProperty.Register(
            "RowBackgroundEvenBrush", typeof(Brush), typeof(DataGrid), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty SelectedValuesProperty = DependencyProperty.Register(
        "SelectedValues", typeof(List<object>), typeof(DataGrid), new PropertyMetadata(null));

        public Brush RowBackgroundEvenBrush
        {
            get { return (Brush)GetValue(RowBackgroundEvenBrushProperty); }
            set { SetValue(RowBackgroundEvenBrushProperty, value); }
        }

        #endregion

        /// <summary>Selects a column for ordering. 
        /// If the column is not selected the the default ordering is used (IsAscendingDefault property). 
        /// If the column is already selected then the ordering direction will be inverted. </summary>
        /// <param name="column">The column. </param>
        /// <returns>Returns true if column could be changed. </returns>
        public bool SelectColumn(DataGridColumnBase column)
        {
            return SelectColumn(column, column == SelectedColumn ? !column.IsAscending : column.IsAscendingDefault);
        }

        /// <summary>Selects a column for ordering. </summary>
        /// <param name="column">The column. </param>
        /// <param name="ascending">The value indicating whether to sort the column ascending; otherwise descending. </param>
        /// <returns>Returns true if column could be changed. </returns>
        public bool SelectColumn(DataGridColumnBase column, bool ascending)
        {
            if (column.CanSort)
            {
                var oldColumn = SelectedColumn;
                if (oldColumn != null)
                    oldColumn.IsSelected = false;

                SelectedColumn = column;
                SelectedColumn.IsSelected = true;
                SelectedColumn.IsAscending = ascending;

                UpdateItemsOrder();
                OnOrderChanged();

                return true;
            }
            return false;
        }

        /// <summary>Sets the filter on the items source. </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filter"></param>
        public void SetFilter<TItem>(Func<TItem, bool> filter)
        {
            if (_listControl == null)
                _initialFilter = filter;
            else
            {
                if (Items != null)
                    Items.Filter = filter;
            }
        }

        /// <summary>Removes the current filter. </summary>
        public void RemoveFilter()
        {
            if (Items != null)
                Items.Filter = null;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _titleRowControl = (Grid)GetTemplateChild("ColumnHeaders");

            _listControl = (MtListBox)GetTemplateChild("Rows");
            _listControl.PrepareContainerForItem += OnPrepareContainerForItem;

            _listControl.SelectionChanged -= OnSelectionChanged;
            _listControl.SelectionChanged += OnSelectionChanged;

            _listControl.SetBinding(Selector.SelectedItemProperty,
                new Binding { Source = this, Path = new PropertyPath("SelectedItem"), Mode = BindingMode.TwoWay });
            _listControl.SetBinding(ListBox.SelectionModeProperty,
                new Binding { Source = this, Path = new PropertyPath("SelectionMode"), Mode = BindingMode.TwoWay });

            _initialSelectedItem = SelectedItem;

            _listControl.ScrolledToEnd -= ScrolledToEnd;
            _listControl.ScrolledToEnd += ScrolledToEnd;

            if (DefaultOrderIndex == -1)
            {
                var currentOrdered = Columns.FirstOrDefault(c => c.CanSort);
                if (currentOrdered != null)
                    DefaultOrderIndex = Columns.IndexOf(currentOrdered);
                else
                    DefaultOrderIndex = 0;
            }

            Initialize();
        }



        private static void OnColumnsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var dataGrid = (DataGrid)obj;

            var oldList = args.OldValue as INotifyCollectionChanged;
            var newList = args.NewValue as INotifyCollectionChanged;

            if (oldList != null)
                oldList.CollectionChanged -= dataGrid.OnColumnsChanged;
            if (newList != null)
                newList.CollectionChanged += dataGrid.OnColumnsChanged;

            dataGrid.OnColumnsChanged(null, null);
        }

        private void OnColumnsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (_initialized)
            {
                RunWithSelectedItemRestore(() =>
                {
                    UpdateColumnHeaders();

                    var itemsSource = _listControl.ItemsSource;
                    _listControl.ItemsSource = null;
                    _listControl.ItemsSource = itemsSource;
                });
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!SelectionAllowed)
                return;

            foreach (var item in e.RemovedItems)
                ChangeItemSelection(item, false);

            foreach (var item in e.AddedItems)
                ChangeItemSelection(item, true);

            var copy = SelectionChanged;
            if (copy != null)
                copy(this, e);
        }

        private void OnOrderChanged()
        {
            var copy = OrderChanged;
            if (copy != null)
                copy(this, new DataGridOrderChangedEventArgs(SelectedColumn));
        }

        private void ChangeItemSelection(object item, bool isSelected)
        {
            var listBoxItem = _listControl.GetListBoxItemFromItem(item);
            if (listBoxItem != null && listBoxItem.Content != null)
                ((DataGridRow)listBoxItem.Content).IsSelected = isSelected;

            var row = (DataGridRow)listBoxItem?.Content;

            if (SelectionMode == SelectionMode.Multiple)
            {
                if (isSelected)
                {
                    SelectedRows.Add(item);
                }
                else
                {
                    SelectedRows.Remove(item);
                }
            }

            var columnIndex = 0;
            if (!isSelected && row != null)
            {
                foreach (var column in Columns)
                {
                    var cell = column.CreateCell(this, item);

                    //   var cellControl= GetContentPreseneterFromItem(column);
                    cell.Control.Tag = cell;
                    cell.Control.DataContext = item;

                    // var cellControl = new ContentPresenter();
                    var cellControl = (ContentPresenter)row.Children[columnIndex];
                    cellControl.Content = cell;
                    cellControl.ContentTemplate = this.CellTemplate;
                    cellControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    cellControl.VerticalContentAlignment = VerticalAlignment.Stretch;
                    columnIndex++;
                }
            }

            SelectedValues = _listControl.SelectedItems.ToList();
        }



        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            SelectedRows?.Clear();

            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Initialize);
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, delegate
            {
                if (_initialSelectedItem != null)
                {
                    _listControl.SelectedItem = _initialSelectedItem;
                    _initialSelectedItem = null;
                }
            });
        }

        private void OnPrepareContainerForItem(object sender, PrepareContainerForItemEventArgs args)
        {
            var item = (ListBoxItem)args.Element;
            item.Padding = new Thickness(0); // remove padding (use CellMargin instead)
            item.Content = new DataGridRow(this, args.Item);
            item.ContentTemplate = null;
            item.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            item.VerticalContentAlignment = VerticalAlignment.Stretch;
            item.Tapped += OnTapped;
        }

        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            var copy = Navigate;
            if (copy != null)
                copy(this, new NavigationListEventArgs(element.DataContext));
        }

        private void Initialize()
        {
            if (!_initialized)
            {
                if (_titleRowControl == null)
                    return;

                _initialized = true;

                UpdateColumnHeaders();
                if (_listControl.ItemsSource == null)
                    UpdateItemsSource();

                UpdateItemsOrder();
            }
        }

        private void UpdateColumnHeaders()
        {
            var columnIndex = 0;
            var hasStar = false;

            // Deregister old events
            foreach (var c in _titleRowControl.Children)
                c.Tapped -= OnColumnTapped;

            _titleRowControl.Children.Clear();
            _titleRowControl.ColumnDefinitions.Clear();

            foreach (var column in Columns)
            {
                // Create header element
                var title = new ContentPresenter();
                title.Content = column;
                title.ContentTemplate = HeaderTemplate;
                title.Tapped += OnColumnTapped;
                title.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(title, columnIndex++);
                _titleRowControl.Children.Add(title);

                // Create grid column definition
                var columnDefinition = column.CreateGridColumnDefinition();
                hasStar = hasStar || columnDefinition.Width.IsStar;
                _titleRowControl.ColumnDefinitions.Add(columnDefinition);
            }

            if (!hasStar)
                _titleRowControl.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Update selected column
            if (SelectedColumn == null || !Columns.Contains(SelectedColumn))
            {
                if (Columns.Count > DefaultOrderIndex)
                    SelectColumn(Columns[DefaultOrderIndex]);
                else if (Columns.Any(c => c.CanSort))
                    SelectColumn(Columns.First(c => c.CanSort));
                else
                    SelectedColumn = null;
            }
        }

        private void OnColumnTapped(object sender, TappedRoutedEventArgs e)
        {
            var column = (DataGridColumnBase)((ContentPresenter)sender).Content;
            if (column != null && column.CanSort)
                SelectColumn(column);
        }

        private void UpdateItemsOrder()
        {
            if (Items != null)
            {
                RunWithSelectedItemRestore(() =>
                {
                    if (SelectedColumn != null)
                    {
                        Items.IsTracking = false;
                        Items.Order = new Func<object, object>(o => PropertyPathHelper.Evaluate(o, SelectedColumn.OrderPropertyPath));
                        Items.Ascending = SelectedColumn.IsAscending;
                        Items.IsTracking = true;
                    }
                });
            }
        }

        private void RunWithSelectedItemRestore(Action action)
        {
            if (SelectionMode == SelectionMode.Single)
            {
                var previouslySelectedItem = SelectedItem;
                action();
                SelectedItem = previouslySelectedItem;
            }
            else
            {
                var previouslySelectedItems = SelectedData.ToList();
                action();
                var currentlySelectedItems = SelectedData.ToList();
                foreach (var item in previouslySelectedItems.Where(i => !currentlySelectedItems.Contains(i)))
                    SelectedData.Add(item);
            }
        }

        private void UpdateItemsSource()
        {
            if (_listControl != null)
            {
                if (ItemsSource != null && !(ItemsSource is IObservableCollectionView))
                {
                    // TODO: Call dispose on ObservableView (currently using weak events)

                    // Wrap original items source with ObservableView for sorting and filtering
                    var types = ItemsSource.GetType().GenericTypeArguments;
                    var observableView = typeof(ObservableCollectionView<>).CreateGenericObject(types.Any() ?
                        types[0] : ItemsSource.GetType().GetElementType(), ItemsSource);

                    _listControl.ItemsSource = observableView;
                }
                else
                    _listControl.ItemsSource = ItemsSource;

                if (_initialFilter != null)
                {
                    Items.Filter = _initialFilter;
                    _initialFilter = null;
                }
            }
        }

        public void ScrollTillEnd()
        {
            _listControl?.ScrollTillEnd();
        }
    }
}

#endif