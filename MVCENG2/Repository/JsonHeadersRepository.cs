using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
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

        public DateTime GetLastTestDateByStandId(int standId)
        {
            var lastTestDatesElements = GetAllElementsForRead().Where(k => k.StandId == standId).OrderByDescending(k => k.Created);

            if (lastTestDatesElements.Any())
            {
                return lastTestDatesElements.FirstOrDefault().Created;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public int GetAllTestsCountByStandId(int standId)
        {
            return GetAllElementsForRead().Where(k => k.StandId == standId).Count();
        }

        public int GetCarsCountLastmonth(int standId)
        {
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            return GetAllElementsForRead()
                .Where(k => k.StandId == standId)
                .Where(x => x.Created >= lastMonth)
                .GroupBy(t => t.VIN)
                .Select(t => t.FirstOrDefault()).AsEnumerable()
                .Count();

        }
        public IQueryable<ResultsJsonHeader> SearchInAllDB(string searchIdentifier)
        {


            var headersIdList = _context.results_json_headers.Where(k => k.VIN == searchIdentifier || k.Ordernum == searchIdentifier || k.Created.ToString() == searchIdentifier).Select(k => k.Id).ToList();

            var standsId = _context.stands.Where(k => k.StandName == searchIdentifier || k.Project == searchIdentifier || k.StandType == searchIdentifier).Select(k => k.Id).ToList();
            var operatorsId = _context.operators.Where(k => k.OLogin == searchIdentifier).Select(k => k.Id).ToList(); ;

            var tests = _context.results_json_tests.Where(k => k.TName == searchIdentifier || k.TSpecname == searchIdentifier || k.Created.ToString() == searchIdentifier);
            var testIdInValues = _context.results_json_values.Where(k => k.VName == searchIdentifier || k.VValue == searchIdentifier).Select(k => k.TestId).ToList();
            var okNokValsId = _context.ok_nok_val.Where(k => k.Val == searchIdentifier).Select(k => k.Id).ToList();

            if (standsId != null)
            {
                headersIdList = headersIdList.Union(_context.results_json_headers.Where(k => standsId.Contains(k.StandId)).Select(k => k.Id).ToList()).ToList();
            }
            if (operatorsId != null)
            {
                headersIdList = headersIdList.Union(_context.results_json_headers.Where(k => operatorsId.Contains(k.OperatorId)).Select(k => k.Id).ToList()).ToList();
            }
            if (tests != null)
                headersIdList = headersIdList.Union(tests.Select(k => k.HeaderId)).ToList();
            if (testIdInValues != null)
            {
                headersIdList = headersIdList.Union(_context.results_json_tests.Where(k => testIdInValues.Contains(k.Id)).Select(k => k.HeaderId).ToList()).ToList();
            }
            if (okNokValsId != null)
            {

                headersIdList = headersIdList.Union(_context.results_json_tests.Where(k => okNokValsId.Contains(k.ResId)).Select(k => k.HeaderId).ToList()).ToList();
            }


            return _context.results_json_headers.Where(k => headersIdList.Contains(k.Id));
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
