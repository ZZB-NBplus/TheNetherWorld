using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using TheNetherWorldClient.View;

namespace TheNetherWorldClient.ViewModel
{
    public class LoginViewModel:ViewModelBase
    {


        public RelayCommand<LoginView> LoginCommand
        {
            get
            {
                return new RelayCommand<LoginView>(async (loginView) =>
                {
                    if (string.IsNullOrEmpty(loginView.Name.Text)|| string.IsNullOrEmpty(loginView.Password.Password))
                    {
                        loginView.Info.Text = "用户名或者密码为空";
                        return ;
                    }

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string baseUrl = $"{AppData.Api}/Login";

                            var parameters = HttpUtility.ParseQueryString(string.Empty);
                            parameters["name"] = loginView.Name.Text;
                            parameters["password"] = loginView.Password.Password;

                            string url = $"{baseUrl}?{parameters}";


                            HttpResponseMessage response = await client.GetAsync(url);

                            response.EnsureSuccessStatusCode();

                            string responseBody = await response.Content.ReadAsStringAsync();
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            var roleinfo = JsonSerializer.Deserialize<RoleInfo>(responseBody, options);

                            if (string.IsNullOrEmpty(roleinfo.Name))
                            {
                                loginView.Info.Text= "用户不存在或密码错误";
                                return ;
                            }

                            AppData.Name = loginView.Name.Text;
                            AppData.Role = roleinfo;
                            loginView.DialogResult = true;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }



                });
            }
        }

        public RelayCommand<LoginView> ExitCommand
        {
            get
            {
                return new RelayCommand<LoginView>(async (loginView) =>
                {
                    loginView.Close();

                });
            }
        }
    }
}
