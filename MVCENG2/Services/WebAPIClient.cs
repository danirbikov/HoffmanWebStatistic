
using Newtonsoft.Json;
using ServicesWebAPI.Services;

namespace MVCENG2
{
    static class WebAPIClient
    {
        private static HttpClient client = new HttpClient() {BaseAddress = new Uri("https://localhost:7191/") };

      
        public static Dictionary<string, bool> GetPingResult()
        {
            try
            {
                string json = GetAPI("api/GetPingResult");
                Dictionary<string, bool> pingResultsDict = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);
                return pingResultsDict; 

            }
            catch
            {
                return null;
            }

        }

        public static bool StartWebServices()
        {
            string json = GetAPI("api/WebServices");
            string werbServicesStartCondition = JsonConvert.DeserializeObject<string>(json);

            if (werbServicesStartCondition == "Started")
            {
                return true;
            }
            else
            {
                return false;

            }       
        }

        private static string GetAPI(string apiAddress)
        {
            var response = client.GetAsync(apiAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
            else
            {
                LoggerTXT.LogWebApiClient("Error: " + response.StatusCode);
                return null;
            }            
        }        
    }
}
