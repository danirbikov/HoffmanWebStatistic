using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using ServicesWebAPI.Services;


namespace HoffmanWebstatistic.Services
{   
    public class InteractionStand
    {
#if DEBUG
        public readonly string standsReportPath = @"\PAtools\vsp0\data\log_data";
        public readonly string standsPicturePath = @"C:\WebStatistic\Pictures";
#else
        public readonly string standsReportPath = @"\PAtools\vsp0\data\log_data";
        public readonly string standsPicturePath = @"\PAtools\pictures" ;
#endif

        public readonly StandRepository _standRepository;

        public InteractionStand()
        {
        }
        public InteractionStand(StandRepository standRepository)
        {
           _standRepository = standRepository;
        }

        public string GetReporFoldertFullPath(string IpAdress)
        {
            return @"\\" + IpAdress + standsReportPath;
        }

        public string GetPictureFolderFullPath(string IpAdress)
        {
            return @"\\" + IpAdress + standsPicturePath;
        }
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
    }
}
