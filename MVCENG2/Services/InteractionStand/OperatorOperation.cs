using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using HoffmanWebstatistic.ComfortModules;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServicesWebAPI.Services;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Xml;

namespace HoffmanWebstatistic.Services.InteractionStand
{
    public class OperatorOperation
    {

        public readonly string operatorFilePathInProject = @"C:\WebStatistic\FormationFiles\lp.xml";

        public SendingStatusLog SendOperatorFileOnStand(OperatorsPath operatorPath, Stand stand, int userId = 15)
        {
            LoggingStandOperation loggingStandOperation = new LoggingStandOperation();

            string destinationFilePath = @"\\" + stand.IpAdress + operatorPath.CPath;

            try
            {
                SendDeleteFileOnStand sendFileOnStand = new SendDeleteFileOnStand();
                sendFileOnStand.SendFileToStandWithAuth(operatorFilePathInProject, destinationFilePath, operatorPath.CLogin, operatorPath.CPassword);
                
                return loggingStandOperation.FormationSendStatusLog(destinationFilePath, operatorFilePathInProject, userId, stand, "Ok", "");
            }

            catch (Exception ex)
            {
                UnsendingFileBackup unsendingFileBackup = new UnsendingFileBackup();
                unsendingFileBackup.SaveBackupFile(stand.StandName, "Operator", operatorFilePathInProject);
                return loggingStandOperation.FormationSendStatusLog(destinationFilePath, operatorFilePathInProject, userId, stand, "Error", ex.Message);
            }
        }    
    }
}
