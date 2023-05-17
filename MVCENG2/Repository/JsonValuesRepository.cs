using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Models.Hoffman;

namespace MVCENG2.Repository
{
    public class JsonValuesRepository
    {
        private readonly ApplicationDbContext _context;
        public JsonValuesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(ResultsJsonValue resultsJsonValue)
        {
            _context.Add(resultsJsonValue);
            return Save();

        }

        public bool Delete(ResultsJsonValue resultsJsonValue)
        {
            _context.Remove(resultsJsonValue);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ResultsJsonValue resultsJsonValue)
        {
            _context.Update(resultsJsonValue);
            return Save();
        }
    }
}
