using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class DTCPathRepository
    {
        private readonly ApplicationDbContext _context;
        public DTCPathRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public DtcsPath GetDtcsPathByStandID(int standId)
        {
            return _context.dtcs_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }
        public List<DtcsPath> GetAll()
        {
            return _context.dtcs_paths.ToList();
        }

    }
}
