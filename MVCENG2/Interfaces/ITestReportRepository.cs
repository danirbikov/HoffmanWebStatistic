using HoffmanWebstatistic.Models.Siemens;

namespace HoffmanWebstatistic.Interfaces
{
    public interface ITestReportRepository
    {
        Task<IEnumerable<TestReport>> GetAll();
        Task<TestReport> GetByVINAsync(string VIN);
        Task<bool> Add(TestReport testReport);
        Task<bool> Update(TestReport testReport);
        Task<bool> Delete(TestReport testReport);
        Task<bool> Save();



    }
}
