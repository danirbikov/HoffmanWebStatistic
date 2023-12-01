using HoffmanWebstatistic.ComfortModules;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Models.SerializerModels;
using HoffmanWebstatistic.Repository;
using HoffmanWebstatistic.Services.InteractionStand;
using ServicesWebAPI.Services;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HoffmanWebstatistic.Services.Job
{
    public class ParserMes2Sup
    {
        private readonly XSDSchemasRepository _xsdSchemasRepository;
        private readonly Mes2SupPathRepository _mes2SupPathRepository;
        private readonly Mes2SupTelegramsRepository _mes2SupTelegramsRepository;
        private readonly MesPathRepository _mesPathRepository;
        private readonly Mes2SupTelegramsStandRepository _mes2SupTelegramsStandRepository;

       public ParserMes2Sup(ApplicationDbContext dbContext) 
        {
            _xsdSchemasRepository = new XSDSchemasRepository(dbContext);
            _mes2SupPathRepository = new Mes2SupPathRepository(dbContext);
            _mes2SupTelegramsRepository = new Mes2SupTelegramsRepository(dbContext);
            _mesPathRepository = new MesPathRepository(dbContext);
            _mes2SupTelegramsStandRepository = new Mes2SupTelegramsStandRepository(dbContext);
        }




        public void CheckMes2SupFolder()
        {
            MesPathsCredential mesCredential = _mesPathRepository.GetMesPathsCredentialByXSDName("mes2sup");
            XsdSchema xsdSchema = _xsdSchemasRepository.GetXSDSchemaByPurposeId(mesCredential.XsdPurposeId);

            CmdOperations cmdOperations = new CmdOperations();
            cmdOperations.DeleteCredentialForFolder(mesCredential.CPath);

            NetworkCredential credentials = new NetworkCredential(mesCredential.CLogin, mesCredential.CPassword);
            try
            {
                using (new NetworkConnection(mesCredential.CPath, credentials))
                {

                    foreach (string filePath in Directory.GetFiles(mesCredential.CPath, "*", SearchOption.AllDirectories))
                    {
                        XSDValidator xsdValidator = new XSDValidator();
                        bool validateResult = xsdValidator.validateXSD(filePath, xsdSchema.XsdSchemaFile);

                        Mes2supTelegram mes2SupTelegram = FormationTelegramObject(filePath);

                        if (validateResult)
                        {
                            bool addedResult = _mes2SupTelegramsRepository.Add(mes2SupTelegram);
                            if (addedResult)
                            {
                                var fileDumpPath = "C:\\WebStatistic\\Telegrams\\Mes2Sup\\Dump\\" + mes2SupTelegram.TgFilename;

                                File.Copy(filePath, fileDumpPath, true);
                                SendTelegramFileOnStands(fileDumpPath);

                                File.Delete(filePath);
                            }
                        }
                        else
                        {
                            File.Copy(filePath, "C:\\WebStatistic\\Telegrams\\Mes2Sup\\Errors\\" + mes2SupTelegram.TgFilename, true);
                            File.Delete(filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerNLOG.LogWarning("Error in connection: "+ mesCredential.CPath+"\n"+ex.Message);
            }
        }

        private void SendTelegramFileOnStands(string fileDumpPath)
        {
            string destinationFilePath = "";
            string telegramName = Path.GetFileName(fileDumpPath);
            SendDeleteFileOnStand sendDeleteFileOnStand = new SendDeleteFileOnStand();

            foreach (Mes2supPath mes2SupPath in _mes2SupPathRepository.GetAllWithInclude())
            {
                Mes2supTelegramsStand mes2SupStandStatus = new Mes2supTelegramsStand()
                {
                    TgId = _mes2SupTelegramsRepository.GetTelegramByTgName(telegramName).Id,
                    StandId = mes2SupPath.StandId,
                    Transfered = DateTime.Now,
                    TransferStatus = "OK"
                };

                try
                {
                    destinationFilePath = @"\\" + mes2SupPath.Stand.IpAdress + mes2SupPath.CPath + "\\" + telegramName;
                    sendDeleteFileOnStand.SendFileToStandWithAuth(fileDumpPath, destinationFilePath, mes2SupPath.CLogin, mes2SupPath.CPassword);
                    _mes2SupTelegramsStandRepository.Add(mes2SupStandStatus);
                }

                catch (Exception ex)
                {
                    mes2SupStandStatus.TransferStatus="NOK";
                    _mes2SupTelegramsStandRepository.Add(mes2SupStandStatus);
                    File.Copy(fileDumpPath, "C:\\WebStatistic\\Telegrams\\Mes2Sup\\Unsending\\" + Path.GetFileName(fileDumpPath), true);
                }
            }                             
        }

        private Mes2supTelegram FormationTelegramObject(string filePath)
        {
            StringBuilder xmlString = new StringBuilder();

            using (XmlReader reader = XmlReader.Create(filePath))
            {   
                while (reader.Read())
                {
                    xmlString.Append(reader.ReadOuterXml());
                }
            }

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(MES_VEHICLE_STATUS_DESCRIPTOR));
                MES_VEHICLE_STATUS_DESCRIPTOR deserializeMesObject = (MES_VEHICLE_STATUS_DESCRIPTOR)xmlSerializer.Deserialize(reader);              

                return new Mes2supTelegram() {
                    Vin = deserializeMesObject.POSECTION.VIN,
                    Ordernum = deserializeMesObject.PRODUCTIONORDER,
                    TgFilename = FormationTelegramFileName(deserializeMesObject.PRODUCTIONORDER),
                    Created = DateTime.Now,
                    TgContent = xmlString.ToString()
                };
            }
        }

        private string FormationTelegramFileName(string productionOrder)
        {
            string documentTemplateName = "ELTProgramming";
            string physicalAddressName = "Hofmann";

            var prodName = productionOrder + "$$$" + documentTemplateName + "$$$" + physicalAddressName + "$$$" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var finalName = prodName + "$$$" + "hash" + prodName.GetHashCode() + ".xml";

            return finalName;
        }
    }
}
