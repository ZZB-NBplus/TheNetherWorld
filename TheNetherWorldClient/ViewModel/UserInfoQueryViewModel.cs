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
    public class UserInfoQueryViewModel:ViewModelBase
    {
        private ObservableCollection<UserInfoData> _userInfoList = new ObservableCollection<UserInfoData>();

        public ObservableCollection<UserInfoData> UserInfoList
        {
            get { return _userInfoList; }
            set
            {
                _userInfoList = value;
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
                            HttpResponseMessage response = await client.GetAsync($"{AppData.Api}/GetAllUserInfo");

                            response.EnsureSuccessStatusCode();

                            string responseBody = await response.Content.ReadAsStringAsync();

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            UserInfoList = JsonSerializer.Deserialize<ObservableCollection<UserInfoData>>(responseBody, options);

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
