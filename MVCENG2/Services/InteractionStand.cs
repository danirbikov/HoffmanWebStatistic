using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using ServicesWebAPI.Services;
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
        public readonly string translationFilePathInStand = @"\c\ProgramData\Kratzer Automation AG\PAtools TX\Helix\translations.xml";
#endif

        public readonly string operatorFilePathInProject = @"C:\WebStatistic\FormationFiles\lp.xml";
        public readonly string translationFilePathInProject = @"C:\WebStatistic\FormationFiles\translations.xml";

        public readonly StandRepository _standRepository;

        public InteractionStand()
        {
        }

        public InteractionStand(StandRepository standRepository)
        {
           _standRepository = standRepository;
        }

        #region GetFullPaths
        public string GetReporFoldertFullPath(string IpAdress)
        {
            return @"\\" + IpAdress + standsReportPath;
        }

        public string GetPictureFolderFullPath(string IpAdress)
        {
            return @"\\" + IpAdress + standsPicturePath;
        }

        public string GetTranslateFolderFullPath(string IpAdress)
        {
            var a = @"\\" + IpAdress + translationFilePathInStand;
            return @"\\" + IpAdress + translationFilePathInStand;
        }

        public string GetOperatorFolderFullPath(string IpAdress)
        {
            var a = @"\\" + IpAdress + operatorFilePathInStand;
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

        public void SendSingleFile(string sourcePath , string IPAddress)
        {
            
            string remoteFolderPath = @"\\10.194.100.18\c\ProgramData\Kratzer Automation AG\PAtools TX\Helix";

            try
            {
                using (NetworkManager network = new NetworkManager(@"\\" + IPAddress))
                {
                    File.Copy(@"C:\WebStatistic\FormationFiles\translations.xml", @"\\10.194.100.18\c\ProgramData\Kratzer Automation AG\PAtools TX\Helix\translations2.xml", true);
                }

                    LoggerTXT.LogServices("Файл успешно передан в удаленную папку.");
            }
            catch (Exception ex)
            {
                LoggerTXT.LogServices("Ошибка при передаче файла: " + ex.Message);
            }




            /*
            try
            {
                string username = "admin";
                string password = "Kamaz2019";

                NetworkCredential credentials = new NetworkCredential(username, password);
                CredentialCache credentialCache = new CredentialCache();
                credentialCache.Add(new Uri(GetTranslateFolderFullPath(IPAddress)), "Basic", credentials);

                File.Copy(sourcePath, GetTranslateFolderFullPath(IPAddress), true);
                
            }
            catch (Exception ex)
            {
                LoggerTXT.LogServices(ex.Message);
            }
            */
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
                    foreach (string IP in _standRepository.GetAll().Where(k => k.StandType == "QNX").Select(k => k.IpAdress))
                    {
                        SendSingleFile(operatorFilePathInProject, IP);
                        //File.Copy(operatorFilePathInProject, GetOperatorFolderFullPath(IP), true);
                    }
                    break;
                case "Translate":
                    foreach (string IP in _standRepository.GetAll().Where(k => k.StandType != "QNX").Select(k => k.IpAdress))
                    {
                        SendSingleFile(translationFilePathInProject, IP);
                        //File.Copy(translationFilePathInProject, GetTranslateFolderFullPath(IP), true);
                    }
                    break;
            }
#endif
        }

    }
}
