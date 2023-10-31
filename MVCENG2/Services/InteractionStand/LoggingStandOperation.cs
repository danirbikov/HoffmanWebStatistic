
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;


namespace HoffmanWebstatistic.Services.InteractionStand
{
    public class LoggingStandOperation
    {

        public SendingStatusLog FormationSendStatusLog(string destinationFilePath, string sourceFilePath, int userId, Stand stand, string status, string exceptonMessage)
        {
            SendingStatusLog sendingStatusLog = new SendingStatusLog()
            {
                FileName = Path.GetFileName(destinationFilePath),
                FileSize = (int)new FileInfo(sourceFilePath).Length / 1024,
                SourceFilePath = sourceFilePath,
                TargetFilePath = destinationFilePath,
                UserId = userId,
                Stand = stand,
                StandId = stand.Id,
                Date = DateTime.Now,
                Status = status,
                ErrorMessage = exceptonMessage
            };

            return sendingStatusLog;

        }

        public SendingStatusLog FormationSendStatusLog(string fileName, string destinationFilePath, string sourceFilePath, int userId, Stand stand, string status, string exceptonMessage)
        {
            SendingStatusLog sendingStatusLog = new SendingStatusLog()
            {
                FileName = fileName,
                FileSize = 0,
                SourceFilePath = sourceFilePath,
                TargetFilePath = destinationFilePath,
                UserId = userId,
                Stand = stand,
                StandId = stand.Id,
                Date = DateTime.Now,
                Status = status,
                ErrorMessage = exceptonMessage
            };

            return sendingStatusLog;
        }
    }
}
