using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheNetherWorldClient.View;

namespace TheNetherWorldClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginView loginView = new LoginView();
            loginView.ShowDialog();
            if (loginView.DialogResult != true)
            {
                Close();
            }
            InitializeComponent();

            Name.Text = AppData.Name;
            Role.Text=AppData.Role.Name;
        }

        private void RadioButton_Click_SwitchPage(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton button))
            {
                return;
            }

            if (string.IsNullOrEmpty(button.Name))
            {
                return;
            }

            switch (button.Name)
            {
                case "MainViewButton":
                    container.Content = new MainView();
                    break;

                case "RegistrationButton":
                    if (AppData.Role.Registration!=1)
                    {
                        break;
                    }
                    container.Content = new RegistrationView();
                    break;
                case "JudgmentViewButton":
                    if (AppData.Role.Judgment != 1)
                    {
                        break;
                    }
                    container.Content = new JudgmentView();
                    break;

                case "LogoutViewButton":
                    if (AppData.Role.Logout != 1)
                    {
                        break;
                    }
                    container.Content = new LogoutView();
                    break;

                case "UserInfoQueryViewButton":
                    if (AppData.Role.Query != 1)
                    {
                        break;
                    }
                    container.Content = new UserInfoQueryView();
                    break;

                case "OperatorLogViewButton":
                    if (AppData.Role.OperatorLog != 1)
                    {
                        break;
                    }
                    container.Content = new OperatorLogView();
                    break;

                case "RoleViewButton":
                    if (AppData.Role.Name!="阎王爷")
                    {
                        break;
                    }
                    container.Content = new RoleView();
                    break;
                
                case "OperatorViewButton":
                    if (AppData.Role.Name != "阎王爷")
                    {
                        break;
                    }
                    container.Content = new OperatorView();
                    break;


                default:
                    break;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.ShowDialog();
            if (loginView.DialogResult != true)
            {
                loginView.Close();
            }
            Name.Text = AppData.Name;
            Role.Text = AppData.Role.Name;
        }
    }
}