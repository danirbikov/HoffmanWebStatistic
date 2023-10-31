using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using HoffmanWebstatistic.ComfortModules;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using ServicesWebAPI.Services;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Xml;

namespace HoffmanWebstatistic.Services.InteractionStand
{
    public class StandOperationOLD
    {


        public readonly string operatorFilePathInProject = @"C:\WebStatistic\FormationFiles\lp.xml";
        public readonly string translationFilePathInProject = @"C:\WebStatistic\FormationFiles\translations.xml";

        private readonly StandRepository _standRepository;
        private readonly TranslatePathRepository _translatePathRepository;
        private readonly PicturePathRepository _picturePathRepository;
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;
        private readonly PictureRepository _pictureRepository;
        private readonly OperatorPathRepository _operatorPathRepository;
        private readonly DTCContentRepository _dtcContentRepository;
        private readonly DTCPathRepository _dtcPathRepository;

        #region Constructors
        public StandOperationOLD()
        {
        }

        public StandOperationOLD(StandRepository standRepository)
        {
            _standRepository = standRepository;
        }
        public StandOperationOLD(SendingStatusLogRepository sendingStatusLogRepository)
        {
            _sendingStatusLogRepository = sendingStatusLogRepository;
        }
        public StandOperationOLD(StandRepository standRepository, SendingStatusLogRepository sendingStatusLogRepository, OperatorPathRepository operatorPathRepository)
        {
            _standRepository = standRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
            _operatorPathRepository = operatorPathRepository;
        }
        public StandOperationOLD(StandRepository standRepository, TranslatePathRepository translatePathRepository, SendingStatusLogRepository sendingStatusLogRepository)
        {
            _standRepository = standRepository;
            _translatePathRepository = translatePathRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
        }
        public StandOperationOLD(StandRepository standRepository, PictureRepository pictureRepository, SendingStatusLogRepository sendingStatusLogRepository, PicturePathRepository picturePathRepository)
        {
            _standRepository = standRepository;
            _pictureRepository = pictureRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
            _picturePathRepository = picturePathRepository;
        }
        public StandOperationOLD(StandRepository standRepository, DTCContentRepository dtcContentRepository, SendingStatusLogRepository sendingStatusLogRepository, DTCPathRepository dtcPathRepository)
        {
            _standRepository = standRepository;
            _dtcContentRepository = dtcContentRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
            _dtcPathRepository = dtcPathRepository;
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
                    foreach (var picturePathObject in _picturePathRepository.GetAll())
                    {

                        Stand standObject = _standRepository.GetStandByID(picturePathObject.StandId);
                        destinationFilePath = @"\\" + standObject.IpAdress + picturePathObject.CPath + "\\" + picture.PName;
                        string fileDirectory = Path.GetDirectoryName(destinationFilePath);

                        try
                        {
                            CmdOperations cmdOperations = new CmdOperations();
                            cmdOperations.DeleteCredentialForFolder(fileDirectory);

                            NetworkCredential credentials = new NetworkCredential(picturePathObject.CLogin, picturePathObject.CPassword);

                            using (new NetworkConnection(fileDirectory, credentials))
                            {
                                image.Save(destinationFilePath, System.Drawing.Imaging.ImageFormat.Png);
                                AddSendStatusLogInDB("Add all images in stand", destinationFilePath, "DATABASE", userId, standObject, "Ok", "");
                            }

                        }
                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB("Add all images in stand", destinationFilePath, "DATABASE", userId, standObject, "Error", ex.Message);
                            LoggerTXT.LogError("Error in connection by path: " + destinationFilePath);
                        }
                    }
                }
            }

        }

        public void DeletePictureFromStands(string pictureName, int userId = 15)
        {

            string destinationFilePath = "";
            foreach (var picturePathObject in _picturePathRepository.GetAll())
            {
                Stand standObject = _standRepository.GetStandByID(picturePathObject.StandId);

                destinationFilePath = @"\\" + standObject.IpAdress + picturePathObject.CPath + "\\" + pictureName;

                try
                {
                    CmdOperations cmdOperations = new CmdOperations();
                    cmdOperations.DeleteCredentialForFolder(Path.GetDirectoryName(destinationFilePath));

                    NetworkCredential credentials = new NetworkCredential(picturePathObject.CLogin, picturePathObject.CPassword);

                    using (new NetworkConnection(Path.GetDirectoryName(destinationFilePath), credentials))
                    {
                        File.Delete(destinationFilePath);
                        AddSendStatusLogInDB("Delete all images in stand", destinationFilePath, destinationFilePath, userId, standObject, "Ok", "");
                    }
                }
                catch (Exception ex)
                {
                    AddSendStatusLogInDB("Delete all images in stand", destinationFilePath, destinationFilePath, userId, standObject, "Error", ex.Message);
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

        #region DTC operations

        public void AddDTCForStands(DtcContent dtc, int userId = 15)
        {
            string destinationFilePath = "";
            foreach (var dtcPathObject in _dtcPathRepository.GetAll())
            {
                var standObject = _standRepository.GetStandByID(dtcPathObject.StandId);

                destinationFilePath = @"\\" + standObject.IpAdress + dtcPathObject.CPath + "\\" + dtc.Fname;
                string fileDirectory = Path.GetDirectoryName(destinationFilePath);

                try
                {
                    CmdOperations cmdOperations = new CmdOperations();
                    cmdOperations.DeleteCredentialForFolder(fileDirectory);

                    NetworkCredential credentials = new NetworkCredential(dtcPathObject.CLogin, dtcPathObject.CPassword);

                    using (new NetworkConnection(fileDirectory, credentials))
                    {
                        string xmlString = dtc.Fdata;

                        if (xmlString.Contains("encoding=\"UTF-16\"?"))
                        {
                            xmlString = xmlString.Replace("encoding=\"UTF-16\"?", "encoding=\"UTF-8\"?");
                        }

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlString);
                        xmlDoc.Save(destinationFilePath);

                        AddSendStatusLogInDB("Add DTC in stand", destinationFilePath, "DATABASE", userId, standObject, "Ok", "");
                    }

                }
                catch (Exception ex)
                {
                    AddSendStatusLogInDB("Add DTC in stand", destinationFilePath, "DATABASE", userId, standObject, "Error", ex.Message);
                    LoggerTXT.LogError("Error in connection by path: " + destinationFilePath);
                }
            }
        }

        public void DeleteDTCFromStands(string dtcName, int userId = 15)
        {

            string destinationFilePath = "";
            foreach (var dtcPathObject in _dtcPathRepository.GetAll())
            {
                var standObject = _standRepository.GetStandByID(dtcPathObject.StandId);

                destinationFilePath = @"\\" + standObject.IpAdress + dtcPathObject.CPath + "\\" + dtcName;
                try
                {
                    CmdOperations cmdOperations = new CmdOperations();
                    cmdOperations.DeleteCredentialForFolder(Path.GetDirectoryName(destinationFilePath));

                    NetworkCredential credentials = new NetworkCredential(dtcPathObject.CLogin, dtcPathObject.CPassword);

                    using (new NetworkConnection(Path.GetDirectoryName(destinationFilePath), credentials))
                    {
                        File.Delete(destinationFilePath);

                        AddSendStatusLogInDB("Delete DTC in stand", destinationFilePath, "DATABASE", userId, standObject, "Ok", "");
                    }

                }
                catch (Exception ex)
                {
                    AddSendStatusLogInDB("Delete DTC in stand", destinationFilePath, "DATABASE", userId, standObject, "Error", ex.Message);
                    LoggerTXT.LogError("Error in connection by path: " + destinationFilePath);
                }
            }
        }

        public void EditDTCFromStands(DtcContent dtc, int userId = 15)
        {
            DeleteDTCFromStands(dtc.Fname, userId);
            AddDTCForStands(dtc, userId);
        }

        #endregion

        #region Send single file to stands

        public void SendSingleFileToStandWithAuth(string sourcePath, string destinationPath, string username, string password)
        {
            string folderPath = Path.GetDirectoryName(destinationPath);

            CmdOperations cmdOperations = new CmdOperations();
            cmdOperations.DeleteCredentialForFolder(folderPath);

            NetworkCredential credentials = new NetworkCredential(username, password);


            using (new NetworkConnection(folderPath, credentials))
            {

                FileStream fileStream = new FileStream(sourcePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                var destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write);

                try
                {
                    fileStream.CopyTo(destinationStream);
                }
                catch (Exception ex)
                {
                    LoggerTXT.LogError(ex.Message);
                }
                finally
                {
                    destinationStream.Close();
                    fileStream.Close();
                }               
            }
        }



        public void SendFileOnStands(string purposeFile, int userId = 15)
        {

            string destinationFilePath = "";

            switch (purposeFile)
            {
                case "Operator":

                    foreach (var operatorPathObject in _operatorPathRepository.GetAll())
                    {
                        Stand standObject = _standRepository.GetStandByID(operatorPathObject.StandId);

                        destinationFilePath = @"\\" + standObject.IpAdress + operatorPathObject.CPath;

                        try
                        {
                            SendSingleFileToStandWithAuth(operatorFilePathInProject, destinationFilePath, operatorPathObject.CLogin, operatorPathObject.CPassword);
                            AddSendStatusLogInDB(destinationFilePath, operatorFilePathInProject, userId, standObject, "Ok", "");
                        }

                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB(destinationFilePath, operatorFilePathInProject, userId, standObject, "Error", ex.Message);
                            LoggerTXT.LogError(ex.Message);
                        }
                    }
                    break;

                case "Translate":
                    foreach (var translatePathObject in _translatePathRepository.GetAll())
                    {
                        Stand standObject = _standRepository.GetStandByID(translatePathObject.StandId);

                        destinationFilePath = @"\\" + standObject.IpAdress + translatePathObject.CPath;

                        try
                        {
                            SendSingleFileToStandWithAuth(translationFilePathInProject, destinationFilePath, translatePathObject.CLogin, translatePathObject.CPassword);
                            AddSendStatusLogInDB(destinationFilePath, translationFilePathInProject, userId, standObject, "Ok", "");
                        }

                        catch (Exception ex)
                        {
                            AddSendStatusLogInDB(destinationFilePath, translationFilePathInProject, userId, standObject, "Error", ex.Message);
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
