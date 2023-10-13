

using NLog;
using System.Text;

namespace ServicesWebAPI.Services
{
    public static class LoggerTXT
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogError(string logText)
        {      
            logger.Error(logText);                       
        }
        public static void LogWarning(string logText)
        {
            logger.Warn(logText);
        }
    }
}


