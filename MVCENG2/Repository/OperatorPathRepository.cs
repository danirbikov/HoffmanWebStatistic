using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class OperatorPathRepository
    {
        private readonly ApplicationDbContext _context;
        public OperatorPathRepository(ApplicationDbContext context)
        {
            _context = context;
        }      

        public OperatorsPath GetOperatorPathByStandID(int standId)
        {

            return _context.operators_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }
      
    }
}
