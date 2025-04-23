using Common.Model;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace BLLTest
{
    [TestClass]
    public class HttpClientTest
    {
        [TestMethod]
        public async Task TestGetRegistrationCount()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("http://localhost:5000/TheNetherWorld/GetRegistrationCount");

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }


        }

        [TestMethod]
        public async Task TestAddRegistrationInfo()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string baseUrl = "http://localhost:5000/TheNetherWorld/AddRegistrationInfo";

                    var parameters = HttpUtility.ParseQueryString(string.Empty);
                    parameters["uniqueCode"] = "202504010002";
                    parameters["operatorName"] = "小黑";

                    string url = $"{baseUrl}?{parameters}";

                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }


        [TestMethod]
        public async Task TestAddJudgmentInfo()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string baseUrl = "https://localhost/TheNetherWorld/AddJudgmentInfo";

                    var parameters = HttpUtility.ParseQueryString(string.Empty);
                    parameters["operatorName"] = "包拯";

                    string url = $"{baseUrl}?{parameters}";

                    JudgmentInfoData judgmentInfoData = new JudgmentInfoData
                    {
                        JudgmentTime = DateTime.Now,
                        UniqueCode = "0013", 
                        Name = "dog",
                        Sex = "Male",
                        Lifespan = "70",
                        BirthTime = new DateTime(1950, 1, 1),
                        DeathTime = new DateTime(2020, 1, 1),
                        DeathCause = "常州",
                        PreDeathBehaviorRecord = "罪大恶极",
                        JudgmentInfo = "打入地狱"
                    };

                    string jsonData = JsonConvert.SerializeObject(judgmentInfoData);
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
        }

       

    }
}
