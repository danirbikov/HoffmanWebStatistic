using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using HoffmanWebstatistic.ComfortModules;
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


        public readonly string operatorFilePathInProject = @"C:\WebStatistic\FormationFiles\lp.xml";
        public readonly string translationFilePathInProject = @"C:\WebStatistic\FormationFiles\translations.xml";

        private readonly StandRepository _standRepository;
        private readonly TranslatePathRepository _translatePathRepository;
        private readonly PicturePathRepository _picturePathRepository;
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;
        private readonly PictureRepository _pictureRepository;
        private readonly OperatorPathRepository _operatorPathRepository;

        #region Constructors
        public InteractionStand()
        {
        }

        public InteractionStand(StandRepository standRepository)
        {
           _standRepository = standRepository;
        }
        public InteractionStand(StandRepository standRepository, SendingStatusLogRepository sendingStatusLogRepository, OperatorPathRepository operatorPathRepository)
        {
            _standRepository = standRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
            _operatorPathRepository = operatorPathRepository;
        }
        public InteractionStand(StandRepository standRepository, TranslatePathRepository translatePathRepository, SendingStatusLogRepository sendingStatusLogRepository)
        {
            _standRepository = standRepository;
            _translatePathRepository = translatePathRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
        }
        public InteractionStand(StandRepository standRepository, PictureRepository pictureRepository, SendingStatusLogRepository sendingStatusLogRepository, PicturePathRepository picturePathRepository)
        {
            _standRepository = standRepository;
            _pictureRepository = pictureRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
            _picturePathRepository = picturePathRepository;
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
                        PicturesPath picturePathObject = _picturePathRepository.GePicturesPathByStandID(stand.Id);

                        destinationFilePath = @"\\" + stand.IpAdress + picturePathObject.CPath + "\\" + picture.PName;

                        try
                        {

                            CmdOperations cmdOperations = new CmdOperations();
                            cmdOperations.DeleteCredentialForFolder(Path.GetDirectoryName(destinationFilePath));

                            NetworkCredential credentials = new NetworkCredential(picturePathObject.CLogin, picturePathObject.CPassword);

                            using (new NetworkConnection(Path.GetDirectoryName(destinationFilePath), credentials))
                            {
                                image.Save(destinationFilePath, System.Drawing.Imaging.ImageFormat.Png);
                                AddSendStatusLogInDB("Add all images in stand", destinationFilePath, "DATABASE", userId, stand, "Ok", "");
                            }
                                                 
                        }
                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB("Add all images in stand", destinationFilePath, "DATABASE", userId, stand, "Error", ex.Message);
                            LoggerTXT.LogError("Error in connection by path: "+ destinationFilePath);
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
                PicturesPath picturePathObject = _picturePathRepository.GePicturesPathByStandID(stand.Id);

                destinationFilePath = @"\\" + stand.IpAdress + picturePathObject.CPath + "\\" + pictureName;

                try
                {
                    CmdOperations cmdOperations = new CmdOperations();
                    cmdOperations.DeleteCredentialForFolder(Path.GetDirectoryName(destinationFilePath));
                   
                    NetworkCredential credentials = new NetworkCredential(picturePathObject.CLogin, picturePathObject.CPassword);

                    using (new NetworkConnection(Path.GetDirectoryName(destinationFilePath), credentials))
                    {
                        File.Delete(destinationFilePath);
                        AddSendStatusLogInDB("Delete all images in stand", destinationFilePath, destinationFilePath, userId, stand, "Ok", "");
                    }                   
                }
                catch (Exception ex)
                {
                    AddSendStatusLogInDB("Delete all images in stand", destinationFilePath, destinationFilePath, userId, stand, "Error", ex.Message);
                    LoggerTXT.LogError("Error in connection by path: " + destinationFilePath);
                }
            }
        }

        public void EditPictureFromStands(Picture picture, int userId = 15)
        {
            DeletePictureFromStands(picture.PName, userId);
            AddPictureForStands(picture, userId);
        }


        /*
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
        */

            #endregion

        #region Send single file to stands

        public void SendSingleFileToStandWithAuth(string sourcePath , string destinationPath, string username, string password)
        {          
            string folderPath = Path.GetDirectoryName(destinationPath);           

            CmdOperations cmdOperations = new CmdOperations();
            cmdOperations.DeleteCredentialForFolder(folderPath);

            NetworkCredential credentials = new NetworkCredential(username, password);

            using (new NetworkConnection(folderPath, credentials))
            {
                File.Copy(sourcePath, destinationPath, true);
            }      
        }

            

        public void SendFileOnStands(string purposeFile, int userId=15)
        {

            string destinationFilePath = "";

            switch (purposeFile)
            {
                case "Operator":
                                        
                    foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandType == "QNX"))
                    {
                        OperatorsPath operatorPathObject = _operatorPathRepository.GetOperatorPathByStandID(stand.Id);

                        destinationFilePath = @"\\" + stand.IpAdress + operatorPathObject.CPath;      
                        
                        try
                        {
                            SendSingleFileToStandWithAuth(operatorFilePathInProject, destinationFilePath, operatorPathObject.CLogin, operatorPathObject.CPassword);
                            AddSendStatusLogInDB(destinationFilePath, operatorFilePathInProject, userId, stand, "Ok", "");                   
                        }

                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB(destinationFilePath, operatorFilePathInProject, userId, stand, "Error", ex.Message);
                            LoggerTXT.LogError(ex.Message);
                        }
                    }
                    break;

                case "Translate":                   
                    foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandType != "QNX")) 
                    {
                        TranslatesPath translatePathObject = _translatePathRepository.GetTranslatePathByStandID(stand.Id);

                        destinationFilePath = @"\\" + stand.IpAdress + translatePathObject.CPath;

                        try
                        {
                            SendSingleFileToStandWithAuth(translationFilePathInProject, destinationFilePath, translatePathObject.CLogin, translatePathObject.CPassword);
                            AddSendStatusLogInDB(destinationFilePath,translationFilePathInProject, userId, stand, "Ok", "");
                        }

                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB(destinationFilePath, translationFilePathInProject, userId, stand, "Error", ex.Message);
                            LoggerTXT.LogError(ex.Message);
                        }

                    }
                    break;
            }
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
