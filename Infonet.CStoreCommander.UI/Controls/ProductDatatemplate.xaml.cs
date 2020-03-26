using Infonet.CStoreCommander.UI.Model.Stock;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class ProductDatatemplate : UserControl
    {
        #region Dependency Properties
        public ICommand DecreaseQuantityCommand
        {
            get { return (ICommand)GetValue(DecreaseQuantityCommandProperty); }
            set { SetValue(DecreaseQuantityCommandProperty, value); }
        }

        public ICommand IncreaseQuantityCommand
        {
            get { return (ICommand)GetValue(IncreaseQuantityCommandProperty); }
            set { SetValue(IncreaseQuantityCommandProperty, value); }
        }

        public ICommand SetQuantityCommand
        {
            get { return (ICommand)GetValue(SetQuantityCommandProperty); }
            set { SetValue(SetQuantityCommandProperty, value); }
        }

        public static readonly DependencyProperty SetQuantityCommandProperty =
          DependencyProperty.Register(nameof(SetQuantityCommand),
              typeof(ICommand),
              typeof(ProductDatatemplate),
              new PropertyMetadata(null));


        public static readonly DependencyProperty DecreaseQuantityCommandProperty =
            DependencyProperty.Register(nameof(DecreaseQuantityCommand),
                typeof(ICommand),
                typeof(ProductDatatemplate),
                new PropertyMetadata(null));


        public static readonly DependencyProperty IncreaseQuantityCommandProperty =
               DependencyProperty.Register(nameof(IncreaseQuantityCommand),
                   typeof(ICommand),
                   typeof(ProductDatatemplate),
                   new PropertyMetadata(null));
        #endregion
        public ProductDataModel ProductDetails { get { return this.DataContext as ProductDataModel; } }

        public ProductDatatemplate()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}