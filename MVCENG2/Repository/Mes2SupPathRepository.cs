using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class Mes2SupPathRepository
    {
        private readonly ApplicationDbContext _context;
        public Mes2SupPathRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public Mes2supPath GetMes2supPathByStandID(int standId)
        {

            return _context.mes2sup_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }
        public List<Mes2supPath> GetAll()
        {

            return _context.mes2sup_paths.ToList();
        }

        public List<Mes2supPath> GetAllWithInclude()
        {
            return _context.mes2sup_paths.Include(k => k.Stand).ToList();
        }
        public bool Add(Mes2supPath addObject)
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
