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
    public class PictureOperation
    {
        public SendingStatusLog AddPictureForStand(Picture picture, Stand stand, PicturesPath picturesPath, int userId = 15)
        {
            LoggingStandOperation loggingStandOperation = new LoggingStandOperation();

            string destinationFilePath = @"\\" + stand.IpAdress + picturesPath.CPath + "\\" + picture.PName;
            string fileDirectory = Path.GetDirectoryName(destinationFilePath);

            try
            {
                CmdOperations cmdOperations = new CmdOperations();
                cmdOperations.DeleteCredentialForFolder(fileDirectory);

                NetworkCredential credentials = new NetworkCredential(picturesPath.CLogin, picturesPath.CPassword);

                using (new NetworkConnection(fileDirectory, credentials))
                {
                    using (MemoryStream stream = new MemoryStream(picture.PictureBytes))
                    {
                        using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                        {
                            SendDeleteFileOnStand sendDeleteFileOnStand = new SendDeleteFileOnStand();
                            sendDeleteFileOnStand.SendImageOnStand(image, destinationFilePath);

                            return loggingStandOperation.FormationSendStatusLog("Add all images in stand", destinationFilePath, "DATABASE", userId, stand, "Ok", "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return loggingStandOperation.FormationSendStatusLog("Add all images in stand", destinationFilePath, "DATABASE", userId, stand, "Error", ex.Message);
            }
                    
        }
                  

        public SendingStatusLog DeletePictureFromStand(string pictureName, Stand stand, PicturesPath picturesPath, int userId = 15)
        {
            LoggingStandOperation loggingStandOperation = new LoggingStandOperation();
            string destinationFilePath = @"\\" + stand.IpAdress + picturesPath.CPath + "\\" + pictureName;

            try
            {
                CmdOperations cmdOperations = new CmdOperations();
                cmdOperations.DeleteCredentialForFolder(Path.GetDirectoryName(destinationFilePath));

                NetworkCredential credentials = new NetworkCredential(picturesPath.CLogin, picturesPath.CPassword);

                using (new NetworkConnection(Path.GetDirectoryName(destinationFilePath), credentials))
                {
                    SendDeleteFileOnStand sendDeleteFileOnStand = new SendDeleteFileOnStand();
                    sendDeleteFileOnStand.DeleteFileFromStand(destinationFilePath);

                    return loggingStandOperation.FormationSendStatusLog("Delete all images in stand", destinationFilePath, destinationFilePath, userId, stand, "Ok", "");
                }
            }

            catch (Exception ex)
            {
                return loggingStandOperation.FormationSendStatusLog("Delete all images in stand", destinationFilePath, destinationFilePath, userId, stand, "Error", ex.Message);
            }
        }


        public void EditPictureFromStands(Picture picture, Stand stand, PicturesPath picturesPath, int userId = 15)
        {
            DeletePictureFromStand(picture.PName, stand, picturesPath, userId);
            AddPictureForStand(picture, stand, picturesPath, userId);
        }     
    }
}
