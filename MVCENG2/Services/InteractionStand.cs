using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using ServicesWebAPI.Services;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Claims;

namespace HoffmanWebstatistic.Services
{   
    public class InteractionStand
    {

#if DEBUG
        public readonly string standsReportPath = @"\PAtools\vsp0\data\log_data";

        public readonly string standsPicturePath = @"C:\WebStatistic\Pictures";
        public readonly string operatorFilePathInStand = @"C:\\PAtools\\lp.xml";
        public readonly string translationFilePathInStand = @"C:\\PAtools\\translations.xml";
#else
        public readonly string standsReportPath = @"\PAtools\vsp0\data\log_data";

        public readonly string standsPicturePath = @"\PAtools\pictures" ;
        public readonly string operatorFilePathInStand = @"\PAtools\vsp0\data\lp.xml";
#endif

        public readonly string operatorFilePathInProject = @"C:\WebStatistic\FormationFiles\lp.xml";
        public readonly string translationFilePathInProject = @"C:\WebStatistic\FormationFiles\translations.xml";

        private readonly StandRepository _standRepository;
        private readonly TranslatePathRepository _translatePathRepository;
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;
        private readonly PictureRepository _pictureRepository;

        #region Constructors
        public InteractionStand()
        {
        }

        public InteractionStand(StandRepository standRepository)
        {
           _standRepository = standRepository;
        }
        public InteractionStand(StandRepository standRepository, SendingStatusLogRepository sendingStatusLogRepository)
        {
            _standRepository = standRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
        }
        public InteractionStand(StandRepository standRepository, TranslatePathRepository translatePathRepository, SendingStatusLogRepository sendingStatusLogRepository)
        {
            _standRepository = standRepository;
            _translatePathRepository = translatePathRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
        }
        public InteractionStand(StandRepository standRepository, PictureRepository pictureRepository, SendingStatusLogRepository sendingStatusLogRepository)
        {
            _standRepository = standRepository;
            _pictureRepository = pictureRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
        }
        #endregion

        #region GetFullPaths
        public string GetReporFoldertFullPath(string IpAdress)
        {
            return @"\\" + IpAdress + standsReportPath;
        }

        public string GetPictureFolderFullPath(string IpAdress)
        {
            return @"\\" + IpAdress + standsPicturePath;
        }

        public string GetOperatorFolderFullPath(string IpAdress)
        {
            return @"\\" + IpAdress + operatorFilePathInStand;
        }
        #endregion

        #region Picture operations
        public void AddPictureForStands(Picture picture, int userId = 15)
        {
            using (MemoryStream stream = new MemoryStream(picture.PictureBytes))
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                {
                    string destinationFilePath = "";

                    foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandType == "QNX"))
                    {
                        destinationFilePath = GetPictureFolderFullPath(stand.IpAdress) + "\\" + picture.PName;

                        try
                        {
#if DEBUG
                            
                            image.Save(standsPicturePath + @"\\" + picture.PName, System.Drawing.Imaging.ImageFormat.Png);
                            
#else
                            
                            image.Save(destinationFilePath, System.Drawing.Imaging.ImageFormat.Png);
                            AddSendStatusLogInDB("Add all images in stand", destinationFilePath, "DATABASE", userId, stand, "Ok", "");                       
#endif
                        }
                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB("Add all images in stand", destinationFilePath, "DATABASE", userId, stand, "Error", ex.Message);
                            LoggerTXT.LogServices("Error in connection by path: "+ standsPicturePath + "\\" + picture.PName);
                        }
                    }
                } 
            }
            
        }

        public void DeletePictureFromStands(string pictureName, int userId = 15)
        {
           
            string destinationFilePath ="";

            foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandType == "QNX"))
            {
                destinationFilePath = GetPictureFolderFullPath(stand.IpAdress) + "\\" + pictureName;

                try
                {
#if DEBUG                    
                    File.Delete(standsPicturePath + "\\" + pictureName);

#else
                    File.Delete(destinationFilePath);
                    AddSendStatusLogInDB("Delete all images in stand", destinationFilePath, destinationFilePath, userId, stand, "Ok", "");
#endif
                }
                catch (Exception ex)
                {
                    AddSendStatusLogInDB("Delete all images in stand", destinationFilePath, destinationFilePath, userId, stand, "Error", ex.Message);
                    LoggerTXT.LogServices("Error in connection by path: " + standsPicturePath + @"\\" + pictureName);
                }
            }
        }

        public void EditPictureFromStands(Picture picture, int userId = 15)
        {
            DeletePictureFromStands(picture.PName, userId);
            AddPictureForStands(picture, userId);
        }



        public bool ComparePictureFilesOnStand(PictureRepository _pictureRepository)
        {
            List<byte[]> picturesInDBHashCodes = new List<byte[]>();
            List<byte[]> picturesInFileHashCodes = new List<byte[]>();
            string[] files = Directory.GetFiles("C:\\WebStatistic\\Pictures", "*.png");

            foreach (byte[] byteArr in _pictureRepository.GetAll().Select(k => k.PictureBytes).Take(1))
            {
                foreach (string file in files)
                {

                    byte[] imageBytes = File.ReadAllBytes(file);
                    picturesInFileHashCodes.Add(Hashing.CalculateFileHash(file));

                }
                picturesInDBHashCodes.Add(Hashing.CalculateImageHash(byteArr));
            }

            



            

            if (picturesInDBHashCodes.OrderBy(x => x).SequenceEqual(picturesInFileHashCodes.OrderBy(x => x)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            #endregion

        #region Send single file to stands
            public void SendSingleFileToStandWithAuth(string sourcePath , string destinationPath, string username, string password)
        {          

            string folderPath = Path.GetDirectoryName(destinationPath);

            NetworkCredential credentials = new NetworkCredential(username, password);

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"net use {folderPath} /delete";
                process.Start();                

                if (!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }

            using (new NetworkConnection(folderPath, credentials))
            {
                System.IO.File.Copy(sourcePath, destinationPath, true);
            }      
        }

            

        public void SendFileOnStands(string purposeFile, int userId=15)
        {
#if DEBUG
            switch (purposeFile)
            {
                case "Operator":
                    foreach (string IP in _standRepository.GetAll().Where(k => k.StandType == "QNX").Select(k => k.IpAdress))
                    {                        
                        File.Copy(operatorFilePathInProject, operatorFilePathInStand, true);
                    }
                    break;
                case "Translate":                    
                    foreach (string IP in _standRepository.GetAll().Where(k => k.StandType != "QNX").Select(k => k.IpAdress))
                    {                       
                       File.Copy(translationFilePathInProject, translationFilePathInStand, true);                        
                    }
                    
                    break;
            }
            
#else
            string destinationFilePath = "";

            switch (purposeFile)
            {
                case "Operator":
                                        
                    foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandType == "QNX"))
                    {
                        destinationFilePath = GetOperatorFolderFullPath(stand.IpAdress);
                        

                        try
                        {
                            File.Copy(operatorFilePathInProject, destinationFilePath, true);
                            AddSendStatusLogInDB(destinationFilePath, operatorFilePathInProject, userId, stand, "Ok", "");
                     
                        }

                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB(destinationFilePath, operatorFilePathInProject, userId, stand, "Error", ex.Message);

                            LoggerTXT.LogServices(ex.Message);
                        }
                    }
                    break;

                case "Translate":                   
                    foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandType != "QNX")) 
                    {
                        TranslatesPath translatePathObject = _translatePathRepository.GetTranslatePathByStandID(stand.Id);

                        destinationFilePath = @"\\" + stand.IpAdress + "\\" + translatePathObject.CPath + "\\" + "translations.xml";

                        try
                        {
                            SendSingleFileToStandWithAuth(translationFilePathInProject, destinationFilePath, translatePathObject.CLogin, translatePathObject.CPassword);
                            AddSendStatusLogInDB(destinationFilePath,translationFilePathInProject, userId, stand, "Ok", "");

                        }

                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB(destinationFilePath, translationFilePathInProject, userId, stand, "Error", ex.Message);
                            LoggerTXT.LogServices(ex.Message);
                        }

                    }
                    break;
            }
#endif
        }
        #endregion

        #region Add send status log
        public void AddSendStatusLogInDB(string destinationFilePath, string sourceFilePath, int userId, Stand stand, string status, string exceptonMessage)
        {
            SendingStatusLog sendingStatusLog = new SendingStatusLog()
            {
                FileName = Path.GetFileName(destinationFilePath),
                FileSize = (int)(new FileInfo(sourceFilePath)).Length / 1024,
                SourceFilePath = sourceFilePath,
                TargetFilePath = destinationFilePath,
                UserId = userId,
                Stand = stand,
                StandId = stand.Id,
                Date = DateTime.Now,
                Status = status,
                ErrorMessage = exceptonMessage
            };

            _sendingStatusLogRepository.AddOrUpdate(sendingStatusLog);
            
        }

        public void AddSendStatusLogInDB(string fileName, string destinationFilePath, string sourceFilePath, int userId, Stand stand, string status, string exceptonMessage)
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

            _sendingStatusLogRepository.AddOrUpdate(sendingStatusLog);

        }
        #endregion
    }
}
