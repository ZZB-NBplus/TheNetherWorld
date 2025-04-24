using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheNetherWorldClient.ViewModel
{
    public class OperatorLogViewModel:ViewModelBase
    {
        private ObservableCollection<OperationLogInfo> _operatorList = new ObservableCollection<OperationLogInfo>();

        public ObservableCollection<OperationLogInfo> OperatorList
        {
            get { return _operatorList; }
            set
            {
                _operatorList = value;
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
                            HttpResponseMessage response = await client.GetAsync($"{AppData.Api}/GetOperationLogInfo");

                            response.EnsureSuccessStatusCode();

                            string responseBody = await response.Content.ReadAsStringAsync();

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            OperatorList = JsonSerializer.Deserialize<ObservableCollection<OperationLogInfo>>(responseBody, options);

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
