

using NLog;
using System.Text;

namespace ServicesWebAPI.Services
{
    public static class LoggerTXT
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogPinger(string logText)
        {
            Log(logText);
        }

        public static void LogParser(string logText)
        {
            Log(logText);
        }
        public static void LogServices(string logText)
        {
            Log(logText);
        }
        public static void LogWebApiClient(string logText)
        {
            Log(logText);
        }

        private static void Log(string logText)
        {
           
            try
            {
                logger.Error(logText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex "+ex);
            }
            
            
        }
    }
}


