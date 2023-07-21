using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Models.SerializerModels;
using MVCENG2.Models.Siemens;
using MVCENG2.Repository;
using System.IO;
using System.Xml.Serialization;

namespace MVCENG2.Services
{
    public class ParserSiemens
    {
        private readonly IStandRepository _standRepository;
        private readonly IStandsStatisticRepository _statisticRepository;
        private readonly ITestReportRepository _testReportRepository;

        public XmlSerializer xmlSerializer = new XmlSerializer(typeof(Dashboard));

        public ParserSiemens(IStandRepository standRepository, IStandsStatisticRepository statisticRepository, ITestReportRepository testReportRepository)
        {
            _standRepository = standRepository;
            _statisticRepository = statisticRepository;
            _testReportRepository = testReportRepository;
            //Parsing_XML_files();

        }


        public string GetStatisticObjectFromXml(Dashboard TestReportFile)
        {
            Statistic statistic = new Statistic();
            statistic.ProductionNumber = TestReportFile.TestReport.TestHeader.ProductionNumber.ToString();
            statistic.VIN = TestReportFile.TestReport.TestHeader.VehicleIdentificationNumber;
            statistic.TestStart = TestReportFile.TestReport.TestHeader.StartOfTest;
            DateTime TimeStamp = TestReportFile.TestReport.TestHeader.TimeStamp;
            statistic.TestEnd = TimeStamp.ToString();

            DateTime myDateStart = DateTime.ParseExact(statistic.TestStart, "yyyy-MM-dd HH:mm:ss",
                                               System.Globalization.CultureInfo.InvariantCulture);
            DateTime myDateEnd = new DateTime(TimeStamp.Year, TimeStamp.Month, TimeStamp.Day, TimeStamp.Hour, TimeStamp.Minute, TimeStamp.Second);

            statistic.TotalDuration = (myDateEnd - myDateStart).ToString();
            statistic.Result = TestReportFile.TestReport.TestEnd.TestResultState;
            statistic.NotOks = TestReportFile.TestReport.TestEnd.NotOkResults.ToString();
            statistic.Client = TestReportFile.TestReport.TestHeader.TestunitName;
            statistic.TestType = "";

            _statisticRepository.Add(statistic);

            return statistic.VIN;

        }
        public void GetTestReportbjectFromXml(Dashboard TestReportFile, string VIN)
        {
            List<TestReport> TestReportObjects = new List<TestReport>();
            foreach (var result in TestReportFile.TestReport.Result)
            {
                TestReport report = new TestReport();
                report.Component = result.Component;
                report.Result = result.ResultState;
                report.VIN = VIN;
                if (result.Type_Tolerance is not null)
                {
                    report.LowerLimit = result.Type_Tolerance.LowerLimit.ToString();
                    report.UpperLimit = result.Type_Tolerance.UpperLimit.ToString();
                    report.MeasureValue = result.Type_Tolerance.ActualValue.ToString();
                    report.Unit = result.Type_Tolerance.PhysicalUnit.ToString();
                }

                if (result.Type_State is not null)
                {
                    report.ResultValue = result.Type_State.ActualString + "/n" + result.Type_State.ReferenceString;
                }
                _testReportRepository.Add(report);

            }

        }
        public async Task<List<string>> GetURLsFromStands()
        {
            List<string> stands_URL = new List<string>();
            IEnumerable<Stand> stands = _standRepository.GetAll();
            foreach (Stand stand in stands)
            {
                stands_URL.Add(stand.Mes2supTelegramsStands.ToString());
            }
            return stands_URL;
        }
        public async Task Parsing_XML_files()
        {
            List<string> stands_URL = await GetURLsFromStands();
            try
            {
                foreach (string URL in stands_URL.Where(e => e is not null))
                {
                    foreach (var file in Directory.EnumerateFiles(URL, "*", SearchOption.AllDirectories))
                    {
                        using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
                        {
                            Dashboard TestReportFile = (Dashboard)xmlSerializer.Deserialize(fs);
                            string VIN = GetStatisticObjectFromXml(TestReportFile);
                            GetTestReportbjectFromXml(TestReportFile, VIN);
                            fs.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DateTime date = DateTime.Now;
                using (StreamWriter writer = new StreamWriter("Logs/ParserLogs.txt"))
                {
                    writer.WriteLine(date);
                    writer.WriteLine(ex.Message);
                    writer.WriteLine();
                    
                }
                
                

            }
            
        }
    }
}

