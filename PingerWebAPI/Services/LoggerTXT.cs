using System.Text;

namespace ServicesWebAPI.Services
{
    public static class LoggerTXT
    {
        private static readonly string parserTxtFileLocation = @"Logs//Parser.txt";
        private static readonly string pingerTxtFileLocation = @"Logs//Pinger.txt";
        private static readonly string servicesTxtFileLocation = @"Logs//Services.txt";

        public static void LogPinger(string logText)
        {
            Log(pingerTxtFileLocation, logText);
        }

        public static void LogParser(string logText)
        {
            Log(parserTxtFileLocation, logText);
        }
        public static void LogServices(string logText)
        {
            Log(servicesTxtFileLocation, logText);
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
