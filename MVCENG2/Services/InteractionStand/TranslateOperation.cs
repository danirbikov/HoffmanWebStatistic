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
    public class TranslateOperation
    {
        public readonly string translationFilePathInProject = @"C:\WebStatistic\FormationFiles\translations.xml";

        public SendingStatusLog SendTranslateFileOnStands(Stand stand, TranslatesPath translatesPath, int userId = 15)
        {
            LoggingStandOperation loggingStandOperation = new LoggingStandOperation();

            SendDeleteFileOnStand sendFileOnStand = new SendDeleteFileOnStand();
            string destinationFilePath = @"\\" + stand.IpAdress + translatesPath.CPath;

            try
            {
                sendFileOnStand.SendFileToStandWithAuth(translationFilePathInProject, destinationFilePath, translatesPath.CLogin, translatesPath.CPassword);
                return loggingStandOperation.FormationSendStatusLog(destinationFilePath, translationFilePathInProject, userId, stand, "Ok", "");
            }

            catch (Exception ex)
            {
                UnsendingFileBackup unsendingFileBackup = new UnsendingFileBackup();
                unsendingFileBackup.SaveBackupFile(stand.StandName, "Translate", translationFilePathInProject);
                return loggingStandOperation.FormationSendStatusLog(destinationFilePath, translationFilePathInProject, userId, stand, "Error", ex.Message);
                    
            }
            
        }
    }
}
