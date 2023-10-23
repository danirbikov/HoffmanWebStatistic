using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using HoffmanWebstatistic.ComfortModules;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using HoffmanWebstatistic.Services;
using Microsoft.EntityFrameworkCore;
using ServicesWebAPI.Services;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using static HoffmanWebstatistic.Models.SerializerModels.JSONSerializeModel;

namespace HoffmanWebstatistic.Services.Job
{

    public class ParserJSON
    {
        //private readonly ILogger<ParserJSON> _logger;
        // public ParserJSON(ILogger<ParserJSON> logger)
        // {
        //      _logger = logger;
        //  }

        public void AddAllJsonFiles(ApplicationDbContext _dbContext)
        {

            try
            {

                string sourceFilePath;
                string destFilePath = @"C:\\WebStatistic\\ReportsBackup\\";
                string fileName;
                string reportException = "";
                NetworkCredential credentials = new NetworkCredential();

                DirectoryInfo dirInfo = new DirectoryInfo(destFilePath);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }


                var stands = _dbContext.stands.ToList().Where(k => k.StandName != "UNKNOWN");

                foreach (JsonsPath jsons_path in _dbContext.jsons_paths.ToList())
                {
                    Stand stand = _dbContext.stands.Where(k=>k.Id ==jsons_path.StandId).FirstOrDefault();
                    credentials = new NetworkCredential(jsons_path.CLogin, jsons_path.CPassword);

                    sourceFilePath = @"\\" + stand.IpAdress + jsons_path.CPath;

                    LoggerTXT.LogError("sourceFile " + sourceFilePath);

                    CmdOperations cmdOperations = new CmdOperations();
                    cmdOperations.DeleteCredentialForFolder(sourceFilePath);

                    try
                    {

                        using (new NetworkConnection(sourceFilePath, credentials))
                        {
                            foreach (var fileInStand in Directory.GetFileSystemEntries(sourceFilePath, "*.json", SearchOption.AllDirectories))
                            //foreach (var file in Directory.GetFileSystemEntries(sourceFilePath, "*.json", SearchOption.AllDirectories))
                            {
                                fileName = new FileInfo(fileInStand).Name;

                                if (!_dbContext.results_json_headers.Where(k => k.JsonFilename == fileName).Any())
                                {
                                    try
                                    {
                                        File.Copy(fileInStand, destFilePath + fileName, true);
                                        reportException = AddSingleFile(destFilePath + fileName, _dbContext);
                                        _dbContext.SaveChanges();

                                        if (reportException != "")
                                        {
                                            File.Copy(fileInStand, "C:\\WebStatistic\\ErrorBackups\\" + fileName, true);
                                            LoggerTXT.LogWarning(destFilePath + fileName + " (" + reportException + ")");
                                            File.Delete(fileInStand);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        File.Copy(fileInStand, "C:\\WebStatistic\\ErrorBackups\\" + fileName, true);
                                        LoggerTXT.LogWarning(destFilePath + fileName + " (" + reportException + "" + ex.InnerException + ")");
                                        File.Delete(fileInStand);
                                    }


                                }
                                else
                                {
                                    File.Delete(fileInStand);
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        LoggerTXT.LogError("ERROR! Stand's IP " + stand.IpAdress + " " + sourceFilePath + "\n" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerTXT.LogError("Error in parser \n" + ex);

            }

        }


        public string AddSingleFile(string filePath, ApplicationDbContext _dbContext)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        #region Check correctness file
                        Rootobject deserializeJSONObject = JsonSerializer.Deserialize<Rootobject>(fs);

                        if (deserializeJSONObject.header.VIN.Length >= 18)
                        {
                            File.Copy(filePath, "C:\\WebStatistic\\ReportsBackup\\Errors\\" + fs.Name.Split("\\")[^1], true);

                        }
                        if (deserializeJSONObject.header.orderNum.Length >= 21)
                        {
                            File.Copy(filePath, "C:\\WebStatistic\\ReportsBackup\\Errors\\" + fs.Name.Split("\\")[^1], true);

                        }
                        if (fs.Name.Split("\\")[^1].Contains("ЧЕС"))
                        {
                            File.Copy(filePath, "C:\\WebStatistic\\ReportsBackup\\Errors\\" + fs.Name.Split("\\")[^1], true);
                        }

                        if (deserializeJSONObject.header.standName.Contains("line"))
                        {
                            if (deserializeJSONObject.header.standName.Contains("7"))
                            {
                                deserializeJSONObject.header.standName = "rts7";
                            }
                            if (deserializeJSONObject.header.standName.Contains("6"))
                            {
                                deserializeJSONObject.header.standName = "bts6";
                            }
                        }
                        #endregion
                        #region Add json header value
                        ResultsJsonHeader jsonHeaderModel = new ResultsJsonHeader();
                        jsonHeaderModel.VIN = deserializeJSONObject.header.VIN;
                        jsonHeaderModel.Ordernum = deserializeJSONObject.header.orderNum;
                        jsonHeaderModel.JsonFilename = fs.Name.Split("\\")[^1];

                        var standObject = _dbContext.stands.Where(k => k.StandName == deserializeJSONObject.header.standName.Replace("_QNX", "")).FirstOrDefault();
                        if (standObject != null)
                        {
                            jsonHeaderModel.StandId = standObject.Id;
                        }
                        else
                        {
                            jsonHeaderModel.StandId = _dbContext.stands.Where(k => k.StandName == "UNKNOWN").FirstOrDefault().Id;
                        }

                        jsonHeaderModel.Created = DateTime.ParseExact(deserializeJSONObject.header.date, "yyyy.MM.dd HH-mm-ss", CultureInfo.InvariantCulture);

                        var operatorObject = _dbContext.operators.Where(k => k.OLogin == deserializeJSONObject.header.@operator).FirstOrDefault();
                        if (operatorObject != null)
                        {
                            jsonHeaderModel.OperatorId = operatorObject.Id;
                        }
                        else
                        {
                            jsonHeaderModel.OperatorId = _dbContext.operators.Where(k => k.OLogin == "UNKNOWN").FirstOrDefault().Id;
                        }

                        _dbContext.results_json_headers.Add(jsonHeaderModel);

                        _dbContext.SaveChanges();
                        #endregion
                        #region Add tests value
                        if (deserializeJSONObject.tests != null)
                            foreach (var jsonTestObject in deserializeJSONObject.tests)
                            {
                                ResultsJsonTest jsonTestsModel = new ResultsJsonTest();
                                jsonTestsModel.TName = jsonTestObject.nameTest;
                                jsonTestsModel.TSpecname = jsonTestObject.testID;
                                jsonTestsModel.ResId = (byte)(jsonTestObject.testRes.Contains("NOK") ? 2 : 1);
                                jsonTestsModel.Created = DateTime.ParseExact(jsonTestObject.date, "yyyy.MM.dd HH-mm-ss", CultureInfo.InvariantCulture);
                                jsonTestsModel.HeaderId = jsonHeaderModel.Id;//_jsonHeadersRepository.GetJsonHeaderIDbyFileName(jsonHeaderModel.JsonFilename);

                                _dbContext.results_json_tests.Add(jsonTestsModel);
                                _dbContext.SaveChanges();

                                #endregion
                                #region Add values from json file
                                if (jsonTestObject.values != null)
                                    foreach (var jsonValueObject in jsonTestObject.values)
                                    {
                                        ResultsJsonValue jsonValuesModel = new ResultsJsonValue();
                                        jsonValuesModel.VName = jsonValueObject.varName;
                                        jsonValuesModel.VValue = jsonValueObject.varValue;
                                        jsonValuesModel.TestId = jsonTestsModel.Id;

                                        _dbContext.results_json_values.Add(jsonValuesModel);
                                        _dbContext.SaveChanges();

                                    }

                            }
                        transaction.Commit();


                        #endregion
                    }
                    return "";
                }


                catch (Exception ex)
                {

                    transaction.Rollback();
                    return ex.Message;
                    //_logger.LogError(ex.Message);

                }
            }

        }
    }


}