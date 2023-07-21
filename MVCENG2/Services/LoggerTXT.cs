

using System.Text;

namespace ServicesWebAPI.Services
{
    public static class LoggerTXT
    {
        private static readonly string webApiClientTxtFileLocation = @"Logs//WebApiClient.txt";


        public static void LogWebApiClient(string logText)
        {
            Log(webApiClientTxtFileLocation, logText);
        }


        private static void Log(string txtFileLocation, string logText)
        {
            using (StreamWriter writer = new StreamWriter(txtFileLocation, true, Encoding.Default))
            {
                writer.WriteLine(logText);
            }
        }
    }
}


