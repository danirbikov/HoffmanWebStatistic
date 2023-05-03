using Microsoft.AspNetCore.Mvc.Diagnostics;
using MVCENG2.Interfaces;
using MVCENG2.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using static MVCENG2.Models.JSONSerializeModel;

namespace MVCENG2.Services
{

    public class ParserJSON
    {
        private readonly ITestJsonRepository _testJsonRepository;

        public ParserJSON(ITestJsonRepository testJsonRepository)
        {
            _testJsonRepository = testJsonRepository;


        }

        public void TestParsingJsonFile()
        {
            int count = 0;
            string URL = "C:\\STAND_Results";
            string ERROR_ULR = "";
            //string URL = "C:\\TestJSON";
            try
            {
                foreach (var file in Directory.EnumerateFiles(URL, "*", SearchOption.AllDirectories))
                {
                    if (file.Contains(".json"))
                    {
                        TestJSON testJSON = new TestJSON();
                        try
                        {
                            using (FileStream fs = new FileStream(file, FileMode.Open))
                            {


                                ERROR_ULR = fs.Name;
                                DateTime date1 = new DateTime(2015, 7, 20);
                                Rootobject deserializeJSONObject = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(fs);
                                if (deserializeJSONObject.header.VIN.Length >= 18)
                                {
                                    System.IO.File.Copy(file, "C:\\STAND_Results\\ERRORS\\Incorrect_VIN\\" + fs.Name.Split("\\")[^1], true);
                                    continue;
                                }
                                if (deserializeJSONObject.header.orderNum.Length >= 21)
                                {
                                    System.IO.File.Copy(file, "C:\\STAND_Results\\ERRORS\\Incorrect_ProductionNumber\\" + fs.Name.Split("\\")[^1], true);
                                    continue;
                                }
                                if (fs.Name.Split("\\")[^1].Contains("ЧЕС"))
                                {
                                    System.IO.File.Copy(file, "C:\\STAND_Results\\ERRORS\\IncorrectFileName\\" + fs.Name.Split("\\")[^1], true);
                                    continue;
                                }

                                testJSON.vin = deserializeJSONObject.header.VIN;
                                testJSON.ordernum = deserializeJSONObject.header.orderNum;
                                testJSON.json_filename = fs.Name.Split("\\")[^1];
                                testJSON.stand_id = 1;
                                testJSON.created = date1;
                                testJSON.operator_id = 2;
                                testJSON.tg_content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(deserializeJSONObject));
                                Console.WriteLine(fs.Name);
                            }
                            _testJsonRepository.Add(testJSON);
                        }
                        catch (Exception ex)
                        {
                            count++;
                            System.IO.File.Copy(file, "C:\\STAND_Results\\ERRORS\\Other\\" + file.Split("\\")[^1], true);
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