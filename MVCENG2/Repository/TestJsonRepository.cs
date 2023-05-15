using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.Hoffman;

namespace MVCENG2.Repository
{
    public class TestJsonRepository : ITestJsonRepository
    {
        private readonly ApplicationDbContext _context;
        public TestJsonRepository(ApplicationDbContext context)
        {
            
            _context = context;
        }
        public bool Add(ResultDataJSON_OLDTEST testJson)
        {
            _context.TestJson.Add(testJson);
            return Save();

        }

        public bool Delete(ResultDataJSON_OLDTEST testJson)
        {
            
            _context.Remove(testJson);
            return Save();
        }

        

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return true;
        }

        public bool Update(ResultDataJSON_OLDTEST testJson)
        {
            _context.Update(testJson);
            return Save();
        }
    }
}
