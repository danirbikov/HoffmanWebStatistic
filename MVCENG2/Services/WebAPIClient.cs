
using Newtonsoft.Json;
using ServicesWebAPI.Services;

namespace HoffmanWebstatistic
{
    static class WebAPIClient
    {
        private static HttpClient client = new HttpClient() {BaseAddress = new Uri("https://localhost:7191/") };
        private static Dictionary<string, bool> pingResultsDict = new Dictionary<string, bool>() {{ "test", false }};

        public static Dictionary<string, bool> GetPingResult()
        {
            try
            {
                string json = GetAPI("api/GetPingResult");
                pingResultsDict = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);
                

            }
            catch
            {
                return pingResultsDict;
            }

            return pingResultsDict;

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
                LoggerTXT.LogError("Error: " + response.StatusCode);
                return null;
            }            
        }        
    }
}
