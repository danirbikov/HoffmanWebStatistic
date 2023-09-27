using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Models.General;

namespace HoffmanWebstatistic.Repository
{
    public class TranslatePathRepository
    {
        private readonly ApplicationDbContext _context;
        public TranslatePathRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<TranslatesPath> GetAll()
        {
            return _context.translates_paths.ToList();           
        }

        public TranslatesPath GetTranslatePathByStandID(int standId)
        {

            return _context.translates_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

      
    }
}
