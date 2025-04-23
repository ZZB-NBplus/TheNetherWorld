using System.Windows.Controls;
using TheNetherWorldClient.ViewModel;

namespace TheNetherWorldClient.View
{
    /// <summary>
    /// JudgmentView.xaml 的交互逻辑
    /// </summary>
    public partial class JudgmentView : UserControl
    {
        public JudgmentView()
        {
            InitializeComponent();
            this.DataContext = new JudgmentViewModel();
            
        }
    }
}
