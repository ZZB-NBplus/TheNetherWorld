using System.Windows.Controls;
using TheNetherWorldClient.ViewModel;

namespace TheNetherWorldClient.View
{
    /// <summary>
    /// OperatorView.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorView : UserControl
    {
        public OperatorView()
        {
            InitializeComponent();
            this.DataContext = new OperatorViewModel();
        }
    }
}
