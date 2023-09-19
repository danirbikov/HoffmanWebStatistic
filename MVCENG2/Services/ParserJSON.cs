using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;
using System.Globalization;
using System.Text;
using System.Text.Json;
using static HoffmanWebstatistic.Models.SerializerModels.JSONSerializeModel;

namespace HoffmanWebstatistic.Services
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

                DirectoryInfo dirInfo = new DirectoryInfo(destFilePath);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }


                var stands = _dbContext.stands.ToList();

                foreach (Stand stand in stands)
                {
                    if (stand.StandType == "QNX")
                    {
                        InteractionStand interactionStand = new InteractionStand();
                        sourceFilePath = interactionStand.GetReporFoldertFullPath(stand.IpAdress);
                    }
                    else
                    {
                        // НУЖНО РЕАЛИЗОВАТЬ!!!!
                        sourceFilePath = "";
                        //sourceFilePath = @"\\" + stand.IpAdress + @"\c\PressureMeaKAMAZ\mes\out";                                                
                    }
                              
                    try
                    {
                        foreach (var fileInStand in Directory.GetFileSystemEntries(sourceFilePath, "*.json", SearchOption.AllDirectories))
                        //foreach (var file in Directory.GetFileSystemEntries(sourceFilePath, "*.json", SearchOption.AllDirectories))
                        {
                            fileName = new FileInfo(fileInStand).Name;

                            if (!File.Exists(destFilePath + fileName))
                            {
                                File.Copy(fileInStand, destFilePath + fileName, true);
                                AddSingleFile(destFilePath + fileName, _dbContext);
                                //LoggerTXT.LogParser("File " + fileInStand + " parsed!");
                                _dbContext.SaveChanges();
                            }
                            else
                            {
                                File.Delete(fileInStand);
                            }
                        }                      
                    }

                    catch (Exception ex)
                    {

                        //_logger.LogError("ERROR! Stand's IP " + stand.IpAdress + "\n" + ex);

                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error in parser \n" + ex);
                
            }

        }


        public void AddSingleFile(string file, ApplicationDbContext _dbContext)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        #region Check correctness file
                        Rootobject deserializeJSONObject = JsonSerializer.Deserialize<Rootobject>(fs);

                        if (deserializeJSONObject.header.VIN.Length >= 18)
                        {
                            File.Copy(file, "Incorrect_VIN\\" + fs.Name.Split("\\")[^1], true);

                        }
                        if (deserializeJSONObject.header.orderNum.Length >= 21)
                        {
                            File.Copy(file, "Incorrect_ProductionNumber\\" + fs.Name.Split("\\")[^1], true);

                        }
                        if (fs.Name.Split("\\")[^1].Contains("ЧЕС"))
                        {
                            File.Copy(file, "IncorrectFileName\\" + fs.Name.Split("\\")[^1], true);
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
                            using (StreamWriter writer = new StreamWriter("Logs\\MysteryStands.txt", true, Encoding.Default))
                            {
                                writer.WriteLine(deserializeJSONObject.header.standName);
                            }
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
                }


                catch (Exception ex)
                {
                    transaction.Rollback();
                    //_logger.LogError(ex.Message);
                    
                }
            }

        }
    }


}