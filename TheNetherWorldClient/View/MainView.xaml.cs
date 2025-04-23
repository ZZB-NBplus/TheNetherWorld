using System.Windows.Controls;
using TheNetherWorldClient.ViewModel;

namespace TheNetherWorldClient.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}
