using HoffmanWebstatistic.Services;
using NLog;
using System.Text;

namespace ServicesWebAPI.Services
{
    public static class LoggerNLOG
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogTrace(string logText)
        {      
            logger.Error(logText);                       
        }
        public static void LogWarning(string logText)
        {
            logger.Warn(logText);
        }
        public static void LogFatalError(string errorTarget,string logText)
        {
            logger.Fatal(logText);
            EmailService emailService = new EmailService();
            emailService.SendEmail("BikovDI@kamaz.ru", errorTarget, logText);
        }
    }
}


