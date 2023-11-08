using System.Drawing;
using System.Xml;

namespace HoffmanWebstatistic.Services
{
    public class UnsendingFileBackup
    {
        private readonly string fileBackupPath = "C:\\WebStatistic\\UnsendingFileBackup\\";

        public void SaveBackupFile(string standName, string fileTypeName, string sourceFilePath)
        {
            string fullBackupFilePath = fileBackupPath+standName+"\\"+fileTypeName+"\\";
            string destinationDirectory = Path.GetDirectoryName(fullBackupFilePath);
            Directory.CreateDirectory(destinationDirectory);

            File.Copy(sourceFilePath, fullBackupFilePath + Path.GetFileName(sourceFilePath), true) ;
        }

        public void SaveBackupFile(string standName, string fileTypeName, XmlDocument xmlDoc, string DTCName)
        {
            string fullBackupFilePath = fileBackupPath + standName + "\\" + fileTypeName + "\\"+ DTCName;
            string destinationDirectory = Path.GetDirectoryName(fullBackupFilePath);
            Directory.CreateDirectory(destinationDirectory);

            xmlDoc.Save(fullBackupFilePath);
        }
        public void SaveBackupFile(string standName, string fileTypeName, Image image, string imageName)
        {
            string fullBackupFilePath = fileBackupPath + standName + "\\" + fileTypeName + "\\"+imageName;
            string destinationDirectory = Path.GetDirectoryName(fullBackupFilePath);
            Directory.CreateDirectory(destinationDirectory);

            image.Save(fullBackupFilePath, System.Drawing.Imaging.ImageFormat.Png);
        }

    }
}
