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
    public class DTCOperation
    {

        public SendingStatusLog AddDTCForStand(DtcContent dtc, Stand stand, DtcsPath dtcsPath,  int userId = 15)
        {
            LoggingStandOperation loggingStandOperation = new LoggingStandOperation();

            string destinationFilePath = @"\\" + stand.IpAdress + dtcsPath.CPath + "\\" + dtc.Fname;
            string fileDirectory = Path.GetDirectoryName(destinationFilePath);
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                CmdOperations cmdOperations = new CmdOperations();
                cmdOperations.DeleteCredentialForFolder(fileDirectory);

                string xmlString = dtc.Fdata;

                if (xmlString.Contains("encoding=\"UTF-16\"?"))
                {
                    xmlString = xmlString.Replace("encoding=\"UTF-16\"?", "encoding=\"UTF-8\"?");
                }

                xmlDoc.LoadXml(xmlString);

                NetworkCredential credentials = new NetworkCredential(dtcsPath.CLogin, dtcsPath.CPassword);
                using (new NetworkConnection(fileDirectory, credentials))
                {                  
                    SendDeleteFileOnStand sendDeleteFileOnStand = new SendDeleteFileOnStand();
                    sendDeleteFileOnStand.SaveXMLOnStand(xmlDoc, destinationFilePath);                

                    return loggingStandOperation.FormationSendStatusLog("Add DTC in stand", destinationFilePath, "DATABASE", userId, stand, "Ok", "");
                }

            }
            catch (Exception ex)
            {
                UnsendingFileBackup unsendingFileBackup = new UnsendingFileBackup();
                unsendingFileBackup.SaveBackupFile(stand.StandName, "DTC", xmlDoc, dtc.Fname);
                return loggingStandOperation.FormationSendStatusLog("Add DTC in stand", destinationFilePath, "DATABASE", userId, stand, "Error", ex.Message);
            }
            
        }

        public SendingStatusLog DeleteDTCFromStand(string dtcName, Stand stand, DtcsPath dtcsPath, int userId = 15)
        {
            LoggingStandOperation loggingStandOperation = new LoggingStandOperation();

            string destinationFilePath = @"\\" + stand.IpAdress + dtcsPath.CPath + "\\" + dtcName;
            try
            {
                CmdOperations cmdOperations = new CmdOperations();
                cmdOperations.DeleteCredentialForFolder(Path.GetDirectoryName(destinationFilePath));

                NetworkCredential credentials = new NetworkCredential(dtcsPath.CLogin, dtcsPath.CPassword);

                using (new NetworkConnection(Path.GetDirectoryName(destinationFilePath), credentials))
                {
                    SendDeleteFileOnStand sendDeleteFileOnStand = new SendDeleteFileOnStand();
                    sendDeleteFileOnStand.DeleteFileFromStand(destinationFilePath);

                    return loggingStandOperation.FormationSendStatusLog("Delete DTC in stand", destinationFilePath, "DATABASE", userId, stand, "Ok", "");
                }

            }
            catch (Exception ex)
            {
                return loggingStandOperation.FormationSendStatusLog("Delete DTC in stand", destinationFilePath, "DATABASE", userId, stand, "Error", ex.Message);
            }
            
        }

        public void EditDTCFromStands(DtcContent dtc, Stand stand, DtcsPath dtcsPath, int userId = 15)
        {
            DeleteDTCFromStand(dtc.Fname, stand, dtcsPath, userId);
            AddDTCForStand(dtc, stand, dtcsPath, userId);
        }

    }

}
