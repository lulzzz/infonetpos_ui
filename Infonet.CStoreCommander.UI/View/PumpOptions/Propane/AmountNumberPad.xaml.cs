using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions.PropaneGrade;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.PumpOptions.Propane
{
    public sealed partial class AmountNumberPad : Page
    {
        public PropaneGradeVM PropaneGradeVM { get; set; }
        = SimpleIoc.Default.GetInstance<PropaneGradeVM>();

        public AmountNumberPad()
        {
            this.InitializeComponent();
        }
    }
}
