using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Models.Hoffman;

namespace MVCENG2.Repository
{
    public class JsonHeadersRepository
    {
        private readonly ApplicationDbContext _context;
        public JsonHeadersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(ResultsJsonHeader resultsJsonHeader)
        {
            _context.Add(resultsJsonHeader);
            return Save();

        }

        public bool Delete(ResultsJsonHeader resultsJsonHeader)
        {
            _context.Remove(resultsJsonHeader);
            return Save();
        }

        public long GetJsonHeaderIDbyFileName(string jsonHeaderFileName)
        {
            var jsonHeaderObject = _context.results_json_headers.Where(k => k.JsonFilename == jsonHeaderFileName).FirstOrDefault();
            if (jsonHeaderObject != null)
            {
                return jsonHeaderObject.Id;
            }
            else
            {
                return _context.results_json_headers.Where(k => k.JsonFilename == jsonHeaderFileName).FirstOrDefault().Id;
            }
        }
        public IQueryable<ResultsJsonHeader> Include()
        {
            
            return _context.results_json_headers.Include(x => x.Stand).Include(x => x.Operator);
                   
                    
        }

        public IQueryable<ResultsJsonHeader> GetJsonHeadersByStandsIdentidier(string standsIdentifier, IQueryable<ResultsJsonHeader> resultsJsonHeaders)
        {
            if (standsIdentifier == "Hoffman")
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.Project == standsIdentifier);
            else if (_context.stands.Select(k=>k.StandType).Distinct().Contains(standsIdentifier))
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.StandType == standsIdentifier);
            else
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.StandName == standsIdentifier);

            return resultsJsonHeaders;
        }

        

        public IEnumerable<ResultsJsonHeader> GetAll()
        {
            return _context.results_json_headers.ToList();
            
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ResultsJsonHeader ResultsJsonHeader)
        {
            _context.Update(ResultsJsonHeader);
            return Save();
        }
    }
}
