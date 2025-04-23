using System.Net.Http;
using System.Windows.Input;

namespace TheNetherWorldClient.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private string _registrationCount="0";

        public string RegistrationCount
        { 
            get { return _registrationCount; }
            set
            {
                _registrationCount = value;
                RaisePropertyChanged();
            }
        }


        private string _judgmentInfoCount = "0";

        public string JudgmentInfoCount
        {
            get { return _judgmentInfoCount; }
            set
            {
                _judgmentInfoCount = value;
                RaisePropertyChanged();
            }
        }


        private string _logoutCount = "0";
        public string LogoutCount
        {
            get { return _logoutCount; }
            set
            {
                _logoutCount = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() => {
                    Task.Run(() =>{ GetRegistrationCount(); });
                    Task.Run(() =>{ GetJudgmentInfoCount(); });
                    Task.Run(() =>{ GetLogoutCount(); });

                });
            }
            
        }

        /// <summary>
        /// 刷新登记数量
        /// </summary>
        private async void GetRegistrationCount()
        {
            while (true)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync("https://localhost/TheNetherWorld/GetRegistrationCount");

                        response.EnsureSuccessStatusCode();

                        RegistrationCount = await response.Content.ReadAsStringAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }

                Thread.Sleep(2000);
            }
            
        }

        /// <summary>
        /// 刷新审判数量
        /// </summary>
        private async void GetJudgmentInfoCount()
        {
            while (true)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync("https://localhost/TheNetherWorld/GetJudgmentInfoCount");

                        response.EnsureSuccessStatusCode();

                        JudgmentInfoCount = await response.Content.ReadAsStringAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }

                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// 刷新登出数量
        /// </summary>
        private async void GetLogoutCount()
        {
            while (true)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync("https://localhost/TheNetherWorld/GetLogoutCount");

                        response.EnsureSuccessStatusCode();

                        LogoutCount = await response.Content.ReadAsStringAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }

                Thread.Sleep(2000);
            }
        }

        
    }
}
