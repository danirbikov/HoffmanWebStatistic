using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using ServicesWebAPI.Services;
using System.Diagnostics;
using System.Net;

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

        #region Constructors
        public InteractionStand()
        {
        }

        public InteractionStand(StandRepository standRepository)
        {
           _standRepository = standRepository;
        }
        public InteractionStand(StandRepository standRepository, TranslatePathRepository translatePathRepository)
        {
            _standRepository = standRepository;
            _translatePathRepository = translatePathRepository;
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
        public void AddPictureForStands(Picture picture)
        {
            using (MemoryStream stream = new MemoryStream(picture.PictureBytes))
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                {
                    
                    var onlineStands = Pinger.standsPingResult.Where(k => k.Value == true).Select(k=>k.Key).ToList();

                    foreach (string standIP in _standRepository.GetAll().Where(k=>onlineStands.Contains(k.StandName)).Select(k => k.IpAdress))
                    {
                        try
                        {
#if DEBUG
                            image.Save(standsPicturePath + @"\\" + picture.PName, System.Drawing.Imaging.ImageFormat.Png);
#else
                            image.Save(GetPictureFolderFullPath(standIP) + @"\\" + picture.PName, System.Drawing.Imaging.ImageFormat.Png);
#endif
                        }
                        catch
                        {
                            LoggerTXT.LogServices("Error in connection by path: "+ standsPicturePath + @"\\" + picture.PName);
                        }
                    }
                } 
            }
            
        }

        public void DeletePictureFromStands(string pictureName)
        {
            var onlineStands = Pinger.standsPingResult.Where(k => k.Value == true).Select(k => k.Key).ToList();
            foreach (string standIP in _standRepository.GetAll().Where(k => onlineStands.Contains(k.StandName)).Select(k=>k.IpAdress).ToList())
            {
                try
                {
#if DEBUG
                    
                    File.Delete(standsPicturePath + "\\" + pictureName);
#else
                    File.Delete(GetPictureFolderFullPath(standIP)+@"\\" + pictureName);
#endif
                }
                catch
                {
                    LoggerTXT.LogServices("Error in connection by path: " + standsPicturePath + @"\\" + pictureName);
                }
            }
        }

        public void EditPictureFromStands(Picture picture)
        {
            DeletePictureFromStands(picture.PName);
            AddPictureForStands(picture);
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
            }

            using (new NetworkConnection(folderPath, credentials))
            {
                System.IO.File.Copy(sourcePath, destinationPath, true);
            }      
        }

            

        public void SendFileOnStands(string purposeFile)
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
            switch (purposeFile)
            {
                case "Operator":
                    foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandType == "QNX"))
                    {
                        try
                        {
                            File.Copy(operatorFilePathInProject, GetOperatorFolderFullPath(stand.IpAdress), true);
                        }
                        catch (Exception ex)
                        {
                            LoggerTXT.LogServices(ex.Message);
                        }
                    }
                    break;

                case "Translate":
                    foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandType != "QNX")) 
                    {
                        try
                        {
                            TranslatesPath translatePathObject = _translatePathRepository.GetTranslatePathByStandID(stand.Id);

                            string destiantionPath = @"\\" + stand.IpAdress + "\\" + translatePathObject.CPath + "\\" + "translations.xml";

                            SendSingleFileToStandWithAuth(translationFilePathInProject, destiantionPath, translatePathObject.CLogin, translatePathObject.CPassword);
                        
                        }
                        catch (Exception ex) 
                        {
                            LoggerTXT.LogServices(ex.Message);
                        }

                    }
                    break;
            }
#endif
        }
        #endregion
    }
}
