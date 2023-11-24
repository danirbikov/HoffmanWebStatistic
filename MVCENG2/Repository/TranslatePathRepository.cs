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

        public List<TranslatesPath> GetAllWithInclude()
        {
            return _context.translates_paths.Include(k => k.Stand).ToList();
        }
        public bool Add(TranslatesPath addObject)
        {
            _context.Add(addObject);
            return Save();

        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
