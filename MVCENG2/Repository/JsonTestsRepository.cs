using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
{
    public class JsonTestsRepository
    {
        private readonly ApplicationDbContext _context;
        public JsonTestsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(ResultsJsonTest resultsJsonTest)
        {
            _context.Add(resultsJsonTest);
            return Save();

        }
        //public ResultsJsonHeader Add(ResultsJsonTest resultsJsonTest)
        //{
        //    _context.Add(resultsJsonTest);
        //     return Save();
        //
       // }
        public bool Delete(ResultsJsonTest resultsJsonTest)
        {
            _context.Remove(resultsJsonTest);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ResultsJsonTest resultsJsonTest)
        {
            _context.Update(resultsJsonTest);
            return Save();
        }
    }
}
