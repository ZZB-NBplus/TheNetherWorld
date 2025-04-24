using Common.Model;
using logisticsms.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using TheNetherWorldClient.View;
using System.Web;

namespace TheNetherWorldClient.ViewModel
{
    public class OperatorViewModel : ViewModelBase
    {
        private ObservableCollection<OperatorInfo> _operatorInfoList = new ObservableCollection<OperatorInfo>();

        public ObservableCollection<OperatorInfo> OperatorInfoList
        {
            get { return _operatorInfoList; }
            set
            {
                _operatorInfoList = value;
                RaisePropertyChanged();
            }
        }


        private OperatorInfo _operatorInfo;

        public OperatorInfo OperatorInfoData
        {
            get { return _operatorInfo; }
            set
            {
                _operatorInfo = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            HttpResponseMessage response = await client.GetAsync($"{AppData.Api}/GetAllOperatorInfo");

                            response.EnsureSuccessStatusCode();

                            string responseBody = await response.Content.ReadAsStringAsync();

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            OperatorInfoList = JsonSerializer.Deserialize<ObservableCollection<OperatorInfo>>(responseBody, options);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }
                });
            }
        }

        public RelayCommand<OperatorView> UpdateCommand
        {
            get
            {
                return new RelayCommand<OperatorView>((operatorView) =>
                {

                    DataGrid dataGrid = operatorView.entityDataGrid;

                    if (dataGrid == null) return;

                    if (dataGrid.SelectedIndex < 0) return;

                    DataGridRow selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);

                    List<TextBox> childTextBoxs = FindVisualChildrenHelper.FindVisualChildren<TextBox>(selectedRow);

                    for (int i = 1; i < childTextBoxs.Count; i++)
                    {
                        childTextBoxs[i].IsReadOnly = false;
                    }

                    List<ComboBox> childcomboxs = FindVisualChildrenHelper.FindVisualChildren<ComboBox>(selectedRow);

                    for (int i = 0; i < childcomboxs.Count; i++)
                    {
                        childcomboxs[i].IsEnabled = true;
                    }

                    selectedRow.BorderBrush = Brushes.Black;
                    selectedRow.BorderThickness = new Thickness(2);
                });
            }
        }

        public RelayCommand<OperatorView> SaveCommand
        {
            get
            {
                return new RelayCommand<OperatorView>(async (operatorView) => {

                    DataGrid dataGrid = operatorView.entityDataGrid;

                    if (dataGrid == null) return;

                    if (dataGrid.SelectedIndex < 0) return;

                    DataGridRow selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);

                    List<TextBox> childs = FindVisualChildrenHelper.FindVisualChildren<TextBox>(selectedRow);

                    for (int i = 0; i < childs.Count; i++)
                    {
                        childs[i].IsReadOnly = true;
                    }

                    List<ComboBox> childcomboxs = FindVisualChildrenHelper.FindVisualChildren<ComboBox>(selectedRow);

                    for (int i = 0; i < childcomboxs.Count; i++)
                    {
                        childcomboxs[i].IsEnabled = false;
                    }

                    selectedRow.BorderBrush = Brushes.Black;
                    selectedRow.BorderThickness = new Thickness(0);

                    var operatorInfo = dataGrid.SelectedItem as OperatorInfo;

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string baseUrl = $"{AppData.Api}/UpdateOperatorInfo";

                            var parameters = HttpUtility.ParseQueryString(string.Empty);
                            parameters["operatorName"] = "包拯";

                            string url = $"{baseUrl}?{parameters}";


                            string jsonData = JsonSerializer.Serialize(operatorInfo);
                            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                            HttpResponseMessage response = await client.PostAsync(url, httpContent);

                            response.EnsureSuccessStatusCode();

                            string responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("Response: " + responseContent);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }


                });
            }
        }

        public RelayCommand<OperatorView> InsertCommand
        {
            get
            {
                return new RelayCommand<OperatorView>((operatorView) =>
                {
                    operatorView.InsertStackPanel.Visibility = Visibility.Visible;
                    operatorView.ComboxSelect.IsEnabled = true;
                    OperatorInfoData = new OperatorInfo();
                });
            }
        }

        public RelayCommand<OperatorView> CancelCommand
        {
            get
            {
                return new RelayCommand<OperatorView>((operatorView) =>
                {
                    operatorView.InsertStackPanel.Visibility = Visibility.Hidden;
                });
            }
        }

        public RelayCommand<OperatorView> SubmitCommand
        {
            get
            {
                return new RelayCommand<OperatorView>(async(operatorView) =>
                {
                    OperatorInfo operatorInfo = new OperatorInfo();
                    operatorInfo.Name = OperatorInfoData.Name;
                    operatorInfo.RoleName = OperatorInfoData.RoleName;
                    operatorInfo.Password = OperatorInfoData.Password;


                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string baseUrl = $"{AppData.Api}/AddOperatorInfo";

                            var parameters = HttpUtility.ParseQueryString(string.Empty);
                            parameters["operatorName"] = "包拯";

                            string url = $"{baseUrl}?{parameters}";


                            string jsonData = JsonSerializer.Serialize(operatorInfo);
                            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                            HttpResponseMessage response = await client.PostAsync(url, httpContent);

                            response.EnsureSuccessStatusCode();

                            string responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("Response: " + responseContent);
                            if (responseContent != null && responseContent.Equals("0"))
                            {
                                OperatorInfoData = new OperatorInfo();
                                operatorView.InsertStackPanel.Visibility = Visibility.Hidden;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }

                    OperatorInfoList.Add(operatorInfo);


                });
            }
        }

        public RelayCommand<OperatorView> DeleteCommand
        {
            get
            {
                return new RelayCommand<OperatorView>(async (operatorView) => {

                    DataGrid dataGrid = operatorView.entityDataGrid;

                    if (dataGrid == null) return;

                    if (dataGrid.SelectedIndex < 0) return;

                    var operatorInfo = dataGrid.SelectedItem as OperatorInfo;

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string baseUrl = $"{AppData.Api}/DeleteOperatorInfo";

                            var parameters = HttpUtility.ParseQueryString(string.Empty);
                            parameters["targetName"] = operatorInfo.Name;
                            parameters["operatorName"] = "包拯";

                            string url = $"{baseUrl}?{parameters}";

                            HttpResponseMessage response = await client.GetAsync(url);

                            response.EnsureSuccessStatusCode();

                            string responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("Response: " + responseContent);
                            if (responseContent!=null&&responseContent.Equals("0"))
                            {
                                OperatorInfoList.Remove(operatorInfo);
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }


                });
            }
        }


        private List<string> _options = new List<string> { "阎王爷", "黑白无常", "牛头马面", "判官" , "孟婆", "鬼差" };

        public List<string> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                RaisePropertyChanged();
            }
        }
    }
}
