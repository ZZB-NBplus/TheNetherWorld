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
using System.Web;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using TheNetherWorldClient.View;

namespace TheNetherWorldClient.ViewModel
{
    public class LogoutViewModel:ViewModelBase
    {
        private ObservableCollection<LogoutInfoData> _logoutList = new ObservableCollection<LogoutInfoData>();

        public ObservableCollection<LogoutInfoData> LogoutList
        {
            get { return _logoutList; }
            set
            {
                _logoutList = value;
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
                            HttpResponseMessage response = await client.GetAsync($"{AppData.Api}/GetNotLogoutInfo");

                            response.EnsureSuccessStatusCode();

                            string responseBody = await response.Content.ReadAsStringAsync();

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            ObservableCollection<string> uniquelist = JsonSerializer.Deserialize<ObservableCollection<string>>(responseBody, options);

                            foreach (var item in uniquelist)
                            {
                                LogoutInfoData logoutInfoData = new LogoutInfoData();
                                logoutInfoData.UniqueCode = item;

                                LogoutList.Add(logoutInfoData);
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

        public RelayCommand<LogoutView> SaveCommand
        {
            get
            {
                return new RelayCommand<LogoutView>(async (judgmentView) => {

                    DataGrid dataGrid = judgmentView.entityDataGrid;

                    if (dataGrid == null) return;

                    if (dataGrid.SelectedIndex < 0) return;

                    DataGridRow selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);

                    List<TextBox> childs = FindVisualChildrenHelper.FindVisualChildren<TextBox>(selectedRow);

                    for (int i = 1; i < childs.Count; i++)
                    {
                        childs[i].IsReadOnly = true;
                    }

                    selectedRow.BorderBrush = Brushes.Black;
                    selectedRow.BorderThickness = new Thickness(0);

                    var logoutInfoData = dataGrid.SelectedItem as LogoutInfoData;

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string baseUrl = $"{AppData.Api}/AddLogoutInfo";

                            var parameters = HttpUtility.ParseQueryString(string.Empty);
                            parameters["uniqueCode"] = logoutInfoData.UniqueCode;
                            parameters["operatorName"] = "包拯";

                            string url = $"{baseUrl}?{parameters}";


                            HttpResponseMessage response = await client.GetAsync(url);

                            response.EnsureSuccessStatusCode();

                            string responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("Response: " + responseContent);

                            if (responseContent != null && responseContent.Equals("0"))
                            {
                                LogoutList.Remove(logoutInfoData);
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
    }
}
