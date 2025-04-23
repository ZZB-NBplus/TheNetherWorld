using Common.Model;
using logisticsms.DAL.Helper;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheNetherWorldClient.View;

namespace TheNetherWorldClient.ViewModel
{
    public class JudgmentViewModel:ViewModelBase
    {
        private ObservableCollection<JudgmentInfoData> _judgmentList=new ObservableCollection<JudgmentInfoData>();

        public ObservableCollection<JudgmentInfoData> JudgmentList
        {
            get { return _judgmentList; }
            set 
            {
                _judgmentList= value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(async() => {
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            HttpResponseMessage response = await client.GetAsync("https://localhost/TheNetherWorld/GetNotJudgmentInfo");

                            response.EnsureSuccessStatusCode();

                            string responseBody = await response.Content.ReadAsStringAsync();

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            ObservableCollection<string> uniquelist = JsonSerializer.Deserialize<ObservableCollection<string>>(responseBody, options);

                            foreach (var item in uniquelist)
                            {
                                JudgmentInfoData judgmentInfoData = new JudgmentInfoData();
                                judgmentInfoData.UniqueCode = item;

                                JudgmentList.Add(judgmentInfoData);
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

        public RelayCommand<JudgmentView> UpdateCommand
        {
            get 
            {
                return new RelayCommand<JudgmentView>((judgmentView) => {

                    DataGrid dataGrid= judgmentView.entityDataGrid;

                    if (dataGrid == null) return;

                    if (dataGrid.SelectedIndex < 0) return;

                    DataGridRow selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);

                    List<TextBox> childTextBoxs = FindVisualChildrenHelper.FindVisualChildren<TextBox>(selectedRow);

                    for (int i = 1; i < childTextBoxs.Count; i++)
                    {
                        childTextBoxs[i].IsReadOnly = false;
                    }

                    List<DatePicker> childDatePickers = FindVisualChildrenHelper.FindVisualChildren<DatePicker>(selectedRow);

                    for (int i = 0; i < childDatePickers.Count; i++)
                    {
                        childDatePickers[i].IsEnabled = true;
                    }

                    selectedRow.BorderBrush = Brushes.Black;
                    selectedRow.BorderThickness = new Thickness(2);
                });
            }
        }

        public RelayCommand<JudgmentView> SaveCommand
        {
            get
            {
                return new RelayCommand<JudgmentView>(async(judgmentView) => {

                    DataGrid dataGrid = judgmentView.entityDataGrid;

                    if (dataGrid == null) return;

                    if (dataGrid.SelectedIndex < 0) return;

                    DataGridRow selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);

                    List<TextBox> childs = FindVisualChildrenHelper.FindVisualChildren<TextBox>(selectedRow);

                    for (int i = 1; i < childs.Count; i++)
                    {
                        childs[i].IsReadOnly = true;
                    }

                    List<DatePicker> childDatePickers = FindVisualChildrenHelper.FindVisualChildren<DatePicker>(selectedRow);

                    for (int i = 0; i < childDatePickers.Count; i++)
                    {
                        childDatePickers[i].IsEnabled = true;
                    }


                    selectedRow.BorderBrush = Brushes.Black;
                    selectedRow.BorderThickness = new Thickness(0);

                    var judgmentInfoData = dataGrid.SelectedItem as JudgmentInfoData;

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string baseUrl = "https://localhost/TheNetherWorld/AddJudgmentInfo";

                            var parameters = HttpUtility.ParseQueryString(string.Empty);
                            parameters["operatorName"] = "包拯";

                            string url = $"{baseUrl}?{parameters}";


                            string jsonData = JsonSerializer.Serialize(judgmentInfoData);
                            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                            HttpResponseMessage response = await client.PostAsync(url, httpContent);

                            response.EnsureSuccessStatusCode();

                            string responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("Response: " + responseContent);

                            if (responseContent!=null&& responseContent.Equals("0"))
                            {
                                JudgmentList.Remove(judgmentInfoData);
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
