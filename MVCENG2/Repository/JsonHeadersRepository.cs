using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Interfaces;
using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
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
            var jsonHeaderObject = GetAllElementsForRead().Where(k => k.JsonFilename == jsonHeaderFileName).FirstOrDefault();
            if (jsonHeaderObject != null)
            {
                return jsonHeaderObject.Id;
            }
            else
            {
                return _context.results_json_headers.Where(k => k.JsonFilename == jsonHeaderFileName).FirstOrDefault().Id;
            }
        }
        public IQueryable<ResultsJsonHeader> GetJsonHeadersBySearchIdentidier(string searchIdentifier)
        {
            IQueryable<ResultsJsonHeader> resultsJsonHeaders = GetAllElementsForRead()
                .Include(k => k.Stand)
                .Include(k => k.ResultsJsonTests).ThenInclude(k => k.Res)
                .Include(k => k.ResultsJsonTests).ThenInclude(k => k.ResultsJsonValues)
                .Include(k => k.Operator).Take(7);
            resultsJsonHeaders = resultsJsonHeaders.Where(k => 
            k.Stand.StandName == searchIdentifier || k.Stand.StandType == searchIdentifier || k.Stand.Project == searchIdentifier
            || k.ResultsJsonTests.Select(p=>p.TName).Contains(searchIdentifier) || k.ResultsJsonTests.Select(p => p.TSpecname).Contains(searchIdentifier) || k.ResultsJsonTests.Select(p => p.Res.Val).Contains(searchIdentifier));
            
            /*if (standsIdentifier == "HOFFMAN")
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.Project == standsIdentifier);
            else if (_context.stands.Select(k=>k.StandType).Distinct().Contains(standsIdentifier))
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.StandType == standsIdentifier);
            else
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.StandName == standsIdentifier);
            */
            return resultsJsonHeaders;
        }


        public IQueryable<ResultsJsonHeader> GetJsonHeadersByStandsIdentidier(string standsIdentifier)
        {
            IQueryable<ResultsJsonHeader> resultsJsonHeaders = GetAllElementsForRead().Include(k=>k.Stand);

            if (standsIdentifier == "HOFFMAN")
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.Project == standsIdentifier);
            else if (_context.stands.Select(k=>k.StandType).Distinct().Contains(standsIdentifier))
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.StandType == standsIdentifier);
            else
                resultsJsonHeaders = resultsJsonHeaders.Where(p => p.Stand.StandName == standsIdentifier);

            return resultsJsonHeaders;
        }

        public ResultsJsonHeader GetJsonHeadersById(long id)
        {

            

            var returnObject = GetAllElementsForRead().Where(k => k.Id == id)
                .Include(k => k.ResultsJsonTests).ThenInclude(k => k.ResultsJsonValues)
                .Include(k => k.ResultsJsonTests).ThenInclude(k => k.Res)
                .Include(k => k.Stand)
                .Include(k => k.Operator)
                .AsNoTracking()
                .FirstOrDefault();
            return (returnObject);
        }
        public IEnumerable<ResultsJsonHeader> GetJsonHeadersById(List<long>id)
        {
            

            var returnObject = GetAllElementsForRead().Where(k => id.Contains(k.Id))
                .Include(k => k.Stand)
                .Include(k => k.Operator)
                .Include(k => k.ResultsJsonTests)
                .Include(k => k.ResultsJsonTests).ThenInclude(k => k.Res)
                .AsNoTracking()
                .ToList().OrderBy(x => id.IndexOf(x.Id)).AsEnumerable();
            
            return (returnObject);
        }
        public IQueryable<ResultsJsonHeader> GetAllElementsForRead()
        {
            return _context.results_json_headers.AsNoTracking();
            
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
