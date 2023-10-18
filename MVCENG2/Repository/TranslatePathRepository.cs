using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class TranslatePathRepository
    {
        private readonly ApplicationDbContext _context;
        public TranslatePathRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public TranslatesPath GetTranslatePathByStandID(int standId)
        {

            return _context.translates_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }
        public List<TranslatesPath> GetAll()
        {

            return _context.translates_paths.ToList();
        }

    }
}
