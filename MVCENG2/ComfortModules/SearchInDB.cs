using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Models.General;
using MVCENG2.Models.Hoffman;
using System.Linq;

namespace MVCENG2.ComfortModules
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
            /*
            var headersIdList = _context.results_json_headers.Where(k => searchIdentifier.Contains(k.VIN) || searchIdentifier.Contains(k.Ordernum) || searchIdentifier.Contains(k.Created.ToString())).Select(k => k.Id).ToList();

            var standsId = _context.stands.Where(k => searchIdentifier.Contains(k.StandName) || searchIdentifier.Contains(k.Project) || searchIdentifier.Contains(k.StandType)).Select(k => k.Id).ToList();
            var operatorsId = _context.operators.Where(k => searchIdentifier.Contains(k.OLogin)).Select(k => k.Id).ToList();

            var tests = _context.results_json_tests.Where(k => searchIdentifier.Contains(k.TName) || searchIdentifier.Contains(k.TSpecname) || searchIdentifier.Contains(k.Created.ToString()));
            var testIdInValues = _context.results_json_values.Where(k => searchIdentifier.Contains(k.VName) || searchIdentifier.Contains(k.VValue)).Select(k => k.TestId).ToList();
            var okNokValsId = _context.ok_nok_val.Where(k => searchIdentifier.Contains(k.Val)).Select(k => k.Id).ToList();

            var headersIdList = _context.results_json_headers.Where(k => searchIdentifier.Contains(k.VIN) ||   searchIdentifier.Contains(k.Ordernum) ||  searchIdentifier.Contains(k.Created.ToString())).Select(k=>k.Id).ToList();

            var standsId = _context.stands.Where(k =>  searchIdentifier.Contains(k.StandName) || searchIdentifier.Contains(k.Project) || searchIdentifier.Contains(k.StandType)).Select(k=>k.Id).ToList();
            var operatorsId = _context.operators.Where(k => searchIdentifier.Contains(k.OLogin)).Select(k => k.Id).ToList(); 
           
            var tests = _context.results_json_tests.Where(k => searchIdentifier.Contains(k.TName) || searchIdentifier.Contains(k.TSpecname) || searchIdentifier.Contains(k.Created.ToString())); 
            var testIdInValues = _context.results_json_values.Where(k=> searchIdentifier.Contains(k.VName) || searchIdentifier.Contains(k.VValue)).Select(k=>k.TestId).ToList(); 
            var okNokValsId = _context.ok_nok_val.Where(k=>searchIdentifier.Contains(k.Val)).Select(k=>k.Id).ToList(); 
            */
            var lol = _context.results_json_headers.Select(k=> k.Created).First().ToString()==searchIdentifier;
            var headersIdList = _context.results_json_headers.Where(k => k.VIN == searchIdentifier || k.Ordernum == searchIdentifier || k.Created.ToString().Contains(searchIdentifier)).Select(k=>k.Id).ToList();

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
