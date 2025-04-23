using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TheNetherWorldClient.ViewModel
{
    public class RegistrationViewModel: ViewModelBase
    {
        private string _uniqueCode="";

        public string UniqueCode
        {
            get { return _uniqueCode; }
            set
            {
                _uniqueCode = value;
                RaisePropertyChanged();
            }
        }

        private string _info="";
        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddRegistrationInfoCommand
        {
            get
            {
                return new RelayCommand( async() => {

                    if (string.IsNullOrEmpty(UniqueCode))
                    {
                        Info = "生灵唯一编码不能为空";
                    }
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string baseUrl = "https://localhost/TheNetherWorld/AddRegistrationInfo";

                            var parameters = HttpUtility.ParseQueryString(string.Empty);
                            parameters["uniqueCode"] = UniqueCode;
                            parameters["operatorName"] = "小黑";

                            string url = $"{baseUrl}?{parameters}";

                            HttpResponseMessage response = await client.GetAsync(url);

                            response.EnsureSuccessStatusCode();

                            string responseBody = await response.Content.ReadAsStringAsync();

                            UniqueCode = "";

                            if (responseBody.Equals("0"))
                            {
                                Info = "操作成功";
                            }
                            else
                            {
                                Info = "操作失败";
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
