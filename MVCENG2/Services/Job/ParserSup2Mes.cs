using HoffmanWebstatistic.ComfortModules;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using ServicesWebAPI.Services;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HoffmanWebstatistic.Services.Job
{
    public class ParserSup2Mes
    {
        private readonly XSDSchemasRepository _xsdSchemasRepository;
        private readonly Sup2MesPathRepository _sup2MesPathRepository;
        private readonly Sup2MesTelegramsRepository _sup2MesTelegramsRepository;
        private readonly MesPathRepository _mesPathRepository;

        public ParserSup2Mes(ApplicationDbContext dbContext)
        {
            _xsdSchemasRepository = new XSDSchemasRepository(dbContext);
            _sup2MesPathRepository = new Sup2MesPathRepository(dbContext);
            _sup2MesTelegramsRepository = new Sup2MesTelegramsRepository(dbContext);
            _mesPathRepository = new MesPathRepository(dbContext);
        }

        public void CheckSup2MesFolder()
        {
            MesPathsCredential mesCredential = _mesPathRepository.GetMesPathsCredentialByXSDName("sup2mes");
            XsdSchema xsdSchema = _xsdSchemasRepository.GetXSDSchemaByPurposeId(mesCredential.XsdPurposeId);
            string destinationFolderPath = "";

            foreach (Sup2mesPath sup2MesPath in _sup2MesPathRepository.GetAllWithInclude())
            {
                destinationFolderPath = @"\\" + sup2MesPath.Stand.IpAdress + sup2MesPath.CPath;

                CmdOperations cmdOperations = new CmdOperations();
                cmdOperations.DeleteCredentialForFolder(destinationFolderPath);

                NetworkCredential credentials = new NetworkCredential(sup2MesPath.CLogin, sup2MesPath.CPassword);

                try
                {
                    using (new NetworkConnection(destinationFolderPath, credentials))
                    {
                        foreach (string filePath in Directory.GetFiles(destinationFolderPath, "*", SearchOption.AllDirectories))
                        {
                            XSDValidator xsdValidator = new XSDValidator();
                            bool validateResult = xsdValidator.validateXSD(filePath, xsdSchema.XsdSchemaFile);

                            Sup2mesTelegram sup2MesTelegram = FormationTelegramObject(filePath, sup2MesPath.StandId);

                            if (validateResult && sup2MesTelegram!=null)
                            {
                                bool addedResult = _sup2MesTelegramsRepository.Add(sup2MesTelegram);
                                if (addedResult)
                                {
                                    var fileDumpPath = "C:\\WebStatistic\\Telegrams\\Sup2Mes\\Dump\\" + sup2MesTelegram.TgFilename;

                                    File.Copy(filePath, fileDumpPath, true);
                                    SendTelegramFileOnMes(fileDumpPath, mesCredential);

                                    File.Delete(filePath);
                                }
                            }
                            else
                            {
                                File.Copy(filePath, "C:\\WebStatistic\\Telegrams\\Sup2Mes\\Errors\\" + Path.GetFileName(filePath), true);
                                File.Delete(filePath);
                            }
                        }
                    }
                } 
                catch (Exception ex) 
                {
                    LoggerNLOG.LogWarning("Error in connection "+ destinationFolderPath+" "+ex.Message+" innerException: "+ex.InnerException);
                }
            }
        }

        private void SendTelegramFileOnMes(string fileDumpPath, MesPathsCredential mesCredential)
        {
            string destinationFilePath = "";
            string telegramName = Path.GetFileName(fileDumpPath);

            CmdOperations cmdOperations = new CmdOperations();
            cmdOperations.DeleteCredentialForFolder(mesCredential.CPath);

            NetworkCredential credentials = new NetworkCredential(mesCredential.CLogin, mesCredential.CPassword);
            destinationFilePath = mesCredential.CPath + "\\" + telegramName;
            try
            {
                using (new NetworkConnection(mesCredential.CPath, credentials))
                {                  
                    File.Copy(fileDumpPath, destinationFilePath, true);
                }
            }
            catch (Exception ex)
            {
                File.Copy(fileDumpPath, "C:\\WebStatistic\\Telegrams\\Sup2Mes\\Unsending\\" + Path.GetFileName(fileDumpPath), true);
            }
        }
    
                                   
        

        private Sup2mesTelegram FormationTelegramObject(string filePath, int standId)
        {
            try
            {
                StringBuilder xmlString = new StringBuilder();

                XDocument doc = XDocument.Load(filePath);
                XElement shopFloorDataElement = doc.Element("SHOP_FLOOR_DATA");

                if (shopFloorDataElement != null)
                {
                    shopFloorDataElement.AddBeforeSelf(Environment.NewLine);
                }
                doc.Save(filePath);

                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    while (reader.Read())
                    {
                        xmlString.Append(reader.ReadOuterXml());
                    }
                }

                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SHOP_FLOOR_DATA));
                    SHOP_FLOOR_DATA deserializeMesObject = (SHOP_FLOOR_DATA)xmlSerializer.Deserialize(reader);

                    return new Sup2mesTelegram()
                    {
                        Vin = filePath.Split(new string[] { "$$$" }, StringSplitOptions.None)[1],
                        Ordernum = deserializeMesObject.PRODUCTIONORDER,
                        StandId = standId,
                        TgFilename = Path.GetFileName(filePath),
                        Created = DateTime.Now,
                        TgContent = xmlString.ToString()
                    };
                }
            }
            catch (Exception ex) 
            {
                LoggerNLOG.LogWarning("Error in formation TelegramObject: "+filePath+"\n"+ex.Message);
                return null;
            }
            
        }
    }
}
