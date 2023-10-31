using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using HoffmanWebstatistic.ComfortModules;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using ServicesWebAPI.Services;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Xml;

namespace HoffmanWebstatistic.Services.InteractionStand
{
    public class SendDeleteFileOnStand
    {
        public void SendImageOnStand(Image image, string destinationFilePath)
        {
            image.Save(destinationFilePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        public void DeleteFileFromStand(string destinationFilePath)
        {
            File.Delete(destinationFilePath);
        }

        public void SaveXMLOnStand(XmlDocument xmlDoc, string destinationFilePath)
        {
            xmlDoc.Save(destinationFilePath);
        }

        public void SendFileToStandWithAuth(string sourcePath, string destinationPath, string username, string password)
        {
            string folderPath = Path.GetDirectoryName(destinationPath);

            CmdOperations cmdOperations = new CmdOperations();
            cmdOperations.DeleteCredentialForFolder(folderPath);

            NetworkCredential credentials = new NetworkCredential(username, password);

            using (new NetworkConnection(folderPath, credentials))
            {
                FileStream fileStream = new FileStream(sourcePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

                var destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write);

                fileStream.CopyTo(destinationStream);

                destinationStream.Close();
                fileStream.Close();
            }
        }
    }
}
