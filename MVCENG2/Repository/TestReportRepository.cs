using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.Siemens;

namespace MVCENG2.Repository
{
    public class TestReportRepository : ITestReportRepository
    {
        private readonly ApplicationDbContext _context;
        public TestReportRepository(ApplicationDbContext context)
        {
            
            _context = context;
        }
        public Task<bool> Add(TestReport testReport)
        {
            _context.TestReport.AddAsync(testReport);
            return Save();

        }

        public Task<bool> Delete(TestReport testReport)
        {
            

            _context.Remove(testReport);
            return Save();
        }

        public async Task<IEnumerable<TestReport>> GetAll()
        {
            return await _context.TestReport.ToListAsync();
            
        }

        public async Task<TestReport> GetByVINAsync(string VIN)
        {
            return await _context.TestReport.FirstOrDefaultAsync(i => i.VIN==VIN); 
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> Update(TestReport testReport)
        {
            _context.Update(testReport);
            return Save();
        }
    }
}
