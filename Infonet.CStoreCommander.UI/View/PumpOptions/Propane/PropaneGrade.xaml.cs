using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions.PropaneGrade;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.PumpOptions.Propane
{
    public sealed partial class PropaneGrade : Page
    {
        public PropaneGradeVM PropaneGradeVM { get; set; } =
      SimpleIoc.Default.GetInstance<PropaneGradeVM>();

        public PropaneGrade()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                PropaneGradeVM.ResetVM();
            }
        }
    }
}
