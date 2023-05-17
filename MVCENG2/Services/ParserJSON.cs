using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MVCENG2.Interfaces;
using MVCENG2.Models.Hoffman;
using MVCENG2.Repository;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using static MVCENG2.Models.SerializerModels.JSONSerializeModel;

namespace MVCENG2.Services
{

    public class ParserJSON
    {
        private readonly ITestJsonRepository _testJsonRepository;
        private readonly IStandRepository _standRepository;
        private readonly OperatorsRepository _operatorRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;
        private readonly JsonTestsRepository _jsonTestsRepository;
        private readonly JsonValuesRepository _jsonValuesRepository;
        //private readonly DbContext _context;

        private ITestJsonRepository testJsonRepository;


        public ParserJSON(ITestJsonRepository testJsonRepository, IStandRepository standRepository, OperatorsRepository operatorRepository, JsonHeadersRepository jsonHeadersRepository, JsonTestsRepository jsonTestsRepository,JsonValuesRepository jsonValuesRepository  )
        {
            _testJsonRepository = testJsonRepository;
            _standRepository = standRepository;
            _operatorRepository = operatorRepository;
            _jsonHeadersRepository = jsonHeadersRepository;
            _jsonTestsRepository = jsonTestsRepository;
            _jsonValuesRepository = jsonValuesRepository;
            //_context = context;
        }

        public void ParsingJsonFiles()
        {
            int count = 0;
            string URL = "C:\\STAND_Results";
            string ERROR_URL = "";

           
            //string URL = "C:\\TestJSON";
            try
            {
                foreach (var file in Directory.EnumerateFiles(URL, "*", SearchOption.AllDirectories))
                {
                    if (file.Contains(".json"))
                    {
                        
                        try
                        {
                            using (FileStream fs = new FileStream(file, FileMode.Open))
                            {
                                ERROR_URL = fs.Name;
                                
                                Rootobject deserializeJSONObject = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(fs);

                                if (deserializeJSONObject.header.VIN.Length >= 18)
                                {
                                    File.Copy(file, "C:\\STAND_Results\\ERRORS\\Incorrect_VIN\\" + fs.Name.Split("\\")[^1], true);
                                    continue;
                                }
                                if (deserializeJSONObject.header.orderNum.Length >= 21)
                                {
                                    File.Copy(file, "C:\\STAND_Results\\ERRORS\\Incorrect_ProductionNumber\\" + fs.Name.Split("\\")[^1], true);
                                    continue;
                                }
                                if (fs.Name.Split("\\")[^1].Contains("ЧЕС"))
                                {
                                    File.Copy(file, "C:\\STAND_Results\\ERRORS\\IncorrectFileName\\" + fs.Name.Split("\\")[^1], true);
                                    continue;
                                }
                                ResultsJsonHeader jsonHeaderModel = new ResultsJsonHeader();
                                jsonHeaderModel.VIN = deserializeJSONObject.header.VIN;
                                jsonHeaderModel.Ordernum = deserializeJSONObject.header.orderNum;
                                jsonHeaderModel.JsonFilename = fs.Name.Split("\\")[^1];
                                jsonHeaderModel.StandId = _standRepository.GetStandIDbyName(deserializeJSONObject.header.standName);
                                jsonHeaderModel.Created = DateTime.ParseExact(deserializeJSONObject.header.date, "yyyy.MM.dd HH-mm-ss", CultureInfo.InvariantCulture);
                                jsonHeaderModel.OperatorId = _operatorRepository.GetOperatorIDbyName(deserializeJSONObject.header.@operator);
                                _jsonHeadersRepository.Add(jsonHeaderModel);

                                if (deserializeJSONObject.tests!=null)
                                    foreach (var jsonTestObject in deserializeJSONObject.tests)
                                    {
                                        ResultsJsonTest jsonTestsModel = new ResultsJsonTest();
                                        jsonTestsModel.TName = jsonTestObject.nameTest;
                                        jsonTestsModel.TSpecname = jsonTestObject.testID;
                                        jsonTestsModel.ResId = (byte)(jsonTestObject.testRes.Contains("NOK") ? 2 : 1);
                                        jsonTestsModel.Created = DateTime.ParseExact(deserializeJSONObject.header.date, "yyyy.MM.dd HH-mm-ss", CultureInfo.InvariantCulture);
                                        jsonTestsModel.HeaderId = jsonHeaderModel.Id;//_jsonHeadersRepository.GetJsonHeaderIDbyFileName(jsonHeaderModel.JsonFilename);
                                        _jsonTestsRepository.Add(jsonTestsModel);

                                        if (jsonTestObject.values!=null)
                                            foreach (var jsonValueObject in jsonTestObject.values)
                                            {
                                                ResultsJsonValue jsonValuesModel = new ResultsJsonValue();
                                                jsonValuesModel.VName = jsonValueObject.varName;
                                                jsonValuesModel.VValue = jsonValueObject.varValue;
                                                jsonValuesModel.TestId = jsonTestsModel.Id;
                                                _jsonValuesRepository.Add(jsonValuesModel);

                                            }
                                }
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            count++;
                            using (StreamWriter writer = new StreamWriter("C:\\Users\\BikovDI\\source\\repos\\MVCENG2\\MVCENG2\\Logs\\MysteryStands.txt", true, System.Text.Encoding.Default))
                            {
                                writer.WriteLine(ex.Message);
                            }
                            if (File.Exists(file))
                                //System.IO.File.Copy(file, "C:\\STAND_Results\\ERRORS\\Other\\" + file.Split("\\")[^1], true);
                            continue;
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Count: " + count);

        }
    }

}