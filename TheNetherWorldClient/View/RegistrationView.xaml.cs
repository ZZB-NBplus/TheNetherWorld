using System.Windows.Controls;
using TheNetherWorldClient.ViewModel;

namespace TheNetherWorldClient.View
{
    /// <summary>
    /// RegistrationView.xaml 的交互逻辑
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
            this.DataContext = new RegistrationViewModel();
        }
    }
}
