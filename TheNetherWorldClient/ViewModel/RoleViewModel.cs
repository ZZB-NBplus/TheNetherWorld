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
    public class RoleViewModel:ViewModelBase
    {
        private ObservableCollection<RoleInfo> _roleInfoList = new ObservableCollection<RoleInfo>();

        public ObservableCollection<RoleInfo> RoleInfoList
        {
            get { return _roleInfoList; }
            set
            {
                _roleInfoList = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(async () => {
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            HttpResponseMessage response = await client.GetAsync($"{AppData.Api}/GetAllRoleInfo");

                            response.EnsureSuccessStatusCode();

                            string responseBody = await response.Content.ReadAsStringAsync();

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            RoleInfoList = JsonSerializer.Deserialize<ObservableCollection<RoleInfo>>(responseBody, options);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }
                });
            }
        }

        public RelayCommand<RoleView> UpdateCommand
        {
            get
            {
                return new RelayCommand<RoleView>((roleView) => {

                    DataGrid dataGrid = roleView.entityDataGrid;

                    if (dataGrid == null) return;

                    if (dataGrid.SelectedIndex < 0) return;

                    DataGridRow selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);

                    List<ComboBox> childTextBoxs = FindVisualChildrenHelper.FindVisualChildren<ComboBox>(selectedRow);

                    for (int i = 1; i < childTextBoxs.Count; i++)
                    {
                        childTextBoxs[i].IsEnabled = true;
                    }

                    selectedRow.BorderBrush = Brushes.Black;
                    selectedRow.BorderThickness = new Thickness(2);
                });
            }
        }

        public RelayCommand<RoleView> SaveCommand
        {
            get
            {
                return new RelayCommand<RoleView>(async (roleView) => {

                    DataGrid dataGrid = roleView.entityDataGrid;

                    if (dataGrid == null) return;

                    if (dataGrid.SelectedIndex < 0) return;

                    DataGridRow selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);

                    List<ComboBox> childTextBoxs = FindVisualChildrenHelper.FindVisualChildren<ComboBox>(selectedRow);

                    for (int i = 1; i < childTextBoxs.Count; i++)
                    {
                        childTextBoxs[i].IsEnabled = false;
                    }

                    selectedRow.BorderBrush = Brushes.Black;
                    selectedRow.BorderThickness = new Thickness(0);

                    var roleInfo = dataGrid.SelectedItem as RoleInfo;

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string baseUrl = $"{AppData.Api}/UpdateRoleInfo";

                            var parameters = HttpUtility.ParseQueryString(string.Empty);
                            parameters["operatorName"] = "包拯";

                            string url = $"{baseUrl}?{parameters}";


                            string jsonData = JsonSerializer.Serialize(roleInfo);
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


        private List<int> _options= new List<int> {0,1 };

        public List<int> Options
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
