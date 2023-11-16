using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using HoffmanWebstatistic.Services.InteractionStand;
using System.IO;

namespace HoffmanWebstatistic.Services.Job
{
    public class ActualizeFilesInStand
    {
        private readonly string unsendingFilesFolderPath = "C:\\WebStatistic\\UnsendingFileBackup";

        public void CheckFolderWithUnsendingFiles(ApplicationDbContext dbContext)
        {
            StandRepository _standRepository = new StandRepository(dbContext);
            SendingStatusLogRepository _sendingStatusLogRepository = new SendingStatusLogRepository(dbContext);
            SendingStatusLog statusLog = new SendingStatusLog();

            foreach (string filePath in Directory.GetFiles(unsendingFilesFolderPath, "*", SearchOption.AllDirectories))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileType = Path.GetFileName(Path.GetDirectoryName(filePath));
                string standName = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(filePath)));

                if (Pinger.standsPingResult.Where(k => k.Key == standName).FirstOrDefault().Value)
                {
                    Stand stand = _standRepository.GetStandbyName(standName);

                    switch (fileType)
                    {
                        case "Operator":
                            OperatorPathRepository operatorPathRepository = new OperatorPathRepository(dbContext);
                            OperatorOperation operatorOperation = new OperatorOperation();

                            statusLog = operatorOperation.SendOperatorFileOnStand(operatorPathRepository.GetOperatorPathByStandID(stand.Id), stand);
                            _sendingStatusLogRepository.AddOrUpdate(statusLog);
                            break;

                        case "Translate":
                            TranslatePathRepository _translatePathRepository = new TranslatePathRepository(dbContext);
                            TranslateOperation translateOperation = new TranslateOperation();

                            statusLog = translateOperation.SendTranslateFileOnStands(stand, _translatePathRepository.GetTranslatePathByStandID(stand.Id));
                            _sendingStatusLogRepository.AddOrUpdate(statusLog);
                            break;

                        case "Picture":
                            PicturePathRepository _picturePathRepository = new PicturePathRepository(dbContext);
                            PictureRepository _pictureRepository = new PictureRepository(dbContext);
                            PictureOperation pictureOperation = new PictureOperation();

                            statusLog = pictureOperation.AddPictureForStand(_pictureRepository.GetPictureByName(fileName), stand, _picturePathRepository.GetPicturesPathByStandID(stand.Id));
                            _sendingStatusLogRepository.AddOrUpdate(statusLog);
                            break;

                        case "DTC":
                            DTCPathRepository _dtcPathRepository = new DTCPathRepository(dbContext);
                            DTCContentRepository _dtcContentRepository = new DTCContentRepository(dbContext);
                            DTCOperation dtcOperation = new DTCOperation();

                            statusLog = dtcOperation.AddDTCForStand(_dtcContentRepository.GetDTCByName(fileName), stand, _dtcPathRepository.GetDtcsPathByStandID(stand.Id));
                            _sendingStatusLogRepository.AddOrUpdate(statusLog);
                            break;
                    }

                    if (statusLog.Status.ToUpper()=="OK")
                        File.Delete(filePath);

                }
            }           
        }
    }
}
