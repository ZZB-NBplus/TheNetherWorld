using System.Windows.Controls;
using TheNetherWorldClient.ViewModel;

namespace TheNetherWorldClient.View
{
    /// <summary>
    /// LogoutView.xaml 的交互逻辑
    /// </summary>
    public partial class LogoutView : UserControl
    {
        public LogoutView()
        {
            InitializeComponent();
            this.DataContext = new LogoutViewModel();
        }
    }
}
