//-----------------------------------------------------------------------
// <copyright file="DataGridTextColumn.cs" company="MyToolkit">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/MyToolkit/MyToolkit/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

#if WINRT

using MyToolkit.Extended.Controls;
using MyToolkit.Extended.Controls.DataGrid;
using System;
using System.Windows.Input;
using Windows.System;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace MyToolkit.Controls
{
    public class DataGridTextColumn : DataGridBoundColumn
    {
        public static readonly DependencyProperty StyleProperty = DependencyProperty.Register(
            "Style", typeof(Style), typeof(DataGridTextColumn), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize", typeof(double), typeof(DataGridTextColumn), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register(
            "FontStyle", typeof(FontStyle), typeof(DataGridTextColumn), new PropertyMetadata(default(FontStyle)));

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
            "Foreground", typeof(Brush), typeof(DataGridTextColumn), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
            "TextAlignment", typeof(TextAlignment), typeof(DataGridTextColumn), new PropertyMetadata(default(TextAlignment)));


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEditProperty =
            DependencyProperty.Register("IsEdit", typeof(bool), typeof(DataGridTextColumn), new PropertyMetadata(false));

        public static readonly DependencyProperty EnterPressedProperty = DependencyProperty.Register(
            "EnterPressed", typeof(EventHandler), typeof(DataGridTextColumn), new PropertyMetadata(null));


        /// <summary>Gets or sets the style.</summary>
        public ICommand KeyUpCommand
        {
            get { return (ICommand)GetValue(KeyUpCommandProperty); }
            set { SetValue(KeyUpCommandProperty, value); }
        }

        public int MaxCharacterLength
        {
            get { return (int)GetValue(MaxCharacterLengthProperty); }
            set { SetValue(MaxCharacterLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxCharacterLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxCharacterLengthProperty =
            DependencyProperty.Register(nameof(MaxCharacterLength), typeof(int), typeof(DataGridTextColumn), new PropertyMetadata(int.MaxValue));


        public static readonly DependencyProperty KeyUpCommandProperty = DependencyProperty.Register(
       "KeyUpCommand", typeof(ICommand), typeof(DataGridTextColumn), new PropertyMetadata(null));

        //public event EventHandler<>
        public bool IsEdit
        {
            get { return (bool)GetValue(IsEditProperty); }
            set { SetValue(IsEditProperty, value); }
        }

        public static readonly DependencyProperty FocusOnEditableRowProperty = DependencyProperty.Register(
            "FocusOnEditableRow", typeof(bool), typeof(DataGrid), new PropertyMetadata(false));

        public bool FocusOnEditableRow
        {
            get { return (bool)GetValue(FocusOnEditableRowProperty); }
            set
            {
                SetValue(FocusOnEditableRowProperty, value);
            }
        }

        public event EventHandler<DataGridItemDeletedEventArgs> ItemDeleted;
        private void OnItemDeleted(object item)
        {
            var copy = ItemDeleted;
            if (copy != null)
                copy(this, new DataGridItemDeletedEventArgs(item));
        }


        /// <summary>Gets or sets the style.</summary>
        public Style Style
        {
            get { return (Style)GetValue(StyleProperty); }
            set { SetValue(StyleProperty, value); }
        }

        /// <summary>Gets or sets the size of the font.</summary>
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>Gets or sets the font style. </summary>
        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        /// <summary>Gets or sets the foreground brush.</summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        /// <summary>Gets or sets the text alignment.</summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public InputScopeNameValue InputScope
        {
            get { return (InputScopeNameValue)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register(nameof(InputScope), typeof(InputScopeNameValue), typeof(DataGridTextColumn),
                new PropertyMetadata(InputScopeNameValue.AlphanumericFullWidth));

        public NumericKeyType NumericKeyType
        {
            get { return (NumericKeyType)GetValue(NumericKeyTypeProperty); }
            set
            {
                SetValue(NumericKeyTypeProperty, value);
            }
        }

        public static readonly DependencyProperty NumericKeyTypeProperty =
            DependencyProperty.Register(nameof(NumericKeyType),
                typeof(bool), typeof(CustomTextBox),
                new PropertyMetadata(NumericKeyType.None));



        /// <summary>Generates the cell for the given item. </summary>
        /// <param name="dataGrid"></param>
        /// <param name="dataItem">The item to generate the cell for. </param>
        /// <returns>The <see cref="DataGridCellBase"/>. </returns>
        public override DataGridCellBase CreateCell(DataGrid dataGrid, object dataItem)
        {
            var block = new CustomTextBox();
            block.VerticalAlignment = VerticalAlignment.Center;
            block.Background = new SolidColorBrush(Colors.Transparent);
            block.Foreground = Foreground;
            block.BorderBrush = new SolidColorBrush(Colors.Transparent);
            block.BorderThickness = new Thickness(0);
            block.Margin = new Thickness(2, 0, 2, 0);
            block.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            block.VerticalContentAlignment = VerticalAlignment.Stretch;
            block.IsEnabled = false;
            block.UpdateOnlyOnEnter = dataGrid.UpdateOnlyOnEnter;
            block.NumericKeyType = NumericKeyType;

            if (MaxCharacterLength != 0)
            {
                block.MaxLength = MaxCharacterLength;
            }
            if (FontSize <= 0)
                FontSize = dataGrid.FontSize;

            CreateBinding(StyleProperty, "Style", block, CustomTextBox.StyleProperty);
            CreateBinding(FontStyleProperty, "FontStyle", block, CustomTextBox.FontStyleProperty);
            CreateBinding(FontSizeProperty, "FontSize", block, CustomTextBox.FontSizeProperty);
            CreateBinding(ForegroundProperty, "Foreground", block, CustomTextBox.ForegroundProperty);
            CreateBinding(TextAlignmentProperty, "TextAlignment", block, CustomTextBox.TextAlignmentProperty);

            if (Binding != null)
                block.SetBinding(CustomTextBox.TextProperty, Binding);

            return new DefaultDataGridCell(block);
        }

        private void Block_KeyDown(dynamic sender, KeyRoutedEventArgs e)
        {
            if (e.OriginalKey == VirtualKey.Enter)
            {
                var item = sender.DataContext;
                KeyUpCommand?.Execute(item);
                IsSelected = false;
            }
        }

        public override DataGridCellBase CreateEditableCell(DataGrid dataGrid, object dataItem, bool setFocus)
        {
            var block = new CustomTextBox();
            block.VerticalAlignment = VerticalAlignment.Center;
            block.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            block.VerticalContentAlignment = VerticalAlignment.Stretch;
            block.Background = new SolidColorBrush(Colors.Transparent);
            block.Foreground = Foreground;
            block.BorderBrush = new SolidColorBrush(Colors.Transparent);
            block.Padding = new Thickness(0);
            block.Margin = new Thickness(0);
            block.BorderThickness = new Thickness(0);
            block.UpdateOnlyOnEnter = dataGrid.UpdateOnlyOnEnter;
            block.PreventKeyboardDisplayOnProgrammaticFocus = true;
            block.NumericKeyType = NumericKeyType;

            block.InputScope = new InputScope
            {
                Names = {
                    new InputScopeName()
                    {
                        NameValue =  InputScope
                    }
                    }
            };

            if (MaxCharacterLength != 0)
            {
                block.MaxLength = MaxCharacterLength;
            }
            if (FontSize <= 0)
                FontSize = dataGrid.FontSize;

            CreateBinding(StyleProperty, "Style", block, CustomTextBox.StyleProperty);
            CreateBinding(FontStyleProperty, "FontStyle", block, CustomTextBox.FontStyleProperty);
            CreateBinding(FontSizeProperty, "FontSize", block, CustomTextBox.FontSizeProperty);
            CreateBinding(ForegroundProperty, "Foreground", block, CustomTextBox.ForegroundProperty);
            CreateBinding(TextAlignmentProperty, "TextAlignment", block, CustomTextBox.TextAlignmentProperty);

            if (Binding != null)
                block.SetBinding(CustomTextBox.TextProperty, Binding);

            if (Edit != null)
                block.SetBinding(CustomTextBox.IsEnabledProperty, Edit);

            KeyEventHandler keyeventHandler = new KeyEventHandler(Block_KeyDown);

            block.RemoveHandler(TextBox.KeyDownEvent, keyeventHandler);
            block.AddHandler(TextBox.KeyDownEvent, keyeventHandler, false);

            block.GotFocus -= BlockGotFocus;
            block.GotFocus += BlockGotFocus;

            return new DefaultDataGridCell(block);
        }

        private void BlockGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as CustomTextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                var SelectionStart = 0;
                var SelectionLength = textBox.Text.Length;
                textBox.Select(SelectionStart, SelectionLength);
            }
        }
    }
}

#endif