using MVCENG2.Models.Siemens;

namespace MVCENG2.Interfaces
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
