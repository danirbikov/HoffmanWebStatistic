using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using System.Linq;

namespace HoffmanWebstatistic.ComfortModules
{
    public class SearchInDB
    {
        public ApplicationDbContext _context;

        public SearchInDB(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<ResultsJsonHeader> SearchInAllDB(string searchIdentifier)
        {


            var headersIdList = _context.results_json_headers.Where(k => k.VIN == searchIdentifier || k.Ordernum == searchIdentifier || k.Created.ToString() == searchIdentifier).Select(k=>k.Id).ToList();

            var standsId = _context.stands.Where(k => k.StandName == searchIdentifier || k.Project == searchIdentifier || k.StandType==searchIdentifier).Select(k=>k.Id).ToList();
            var operatorsId = _context.operators.Where(k => k.OLogin == searchIdentifier).Select(k => k.Id).ToList(); ;
           
            var tests = _context.results_json_tests.Where(k => k.TName == searchIdentifier || k.TSpecname == searchIdentifier || k.Created.ToString() == searchIdentifier); 
            var testIdInValues = _context.results_json_values.Where(k=>k.VName==searchIdentifier || k.VValue==searchIdentifier).Select(k=>k.TestId).ToList(); 
            var okNokValsId = _context.ok_nok_val.Where(k=>k.Val==searchIdentifier).Select(k=>k.Id).ToList(); 

            if (standsId != null)
            {
                headersIdList = headersIdList.Union(_context.results_json_headers.Where(k=>standsId.Contains(k.StandId)).Select(k => k.Id).ToList()).ToList();
            }
            if (operatorsId!=null)
            {
                headersIdList = headersIdList.Union(_context.results_json_headers.Where(k => operatorsId.Contains(k.OperatorId)).Select(k => k.Id).ToList()).ToList();
            }
            if (tests != null)
                headersIdList = headersIdList.Union(tests.Select(k => k.HeaderId)).ToList();
            if (testIdInValues != null)
            {
                headersIdList = headersIdList.Union(_context.results_json_tests.Where(k=> testIdInValues.Contains(k.Id)).Select(k=>k.HeaderId).ToList()).ToList();
            }
            if (okNokValsId != null)
            {

                headersIdList = headersIdList.Union(_context.results_json_tests.Where(k => okNokValsId.Contains(k.ResId)).Select(k => k.HeaderId).ToList()).ToList();
            }
            

            return _context.results_json_headers.Where(k=>headersIdList.Contains(k.Id));
        }

    }
}
