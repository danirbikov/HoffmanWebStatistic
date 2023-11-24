using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class Sup2MesPathRepository
    {
        private readonly ApplicationDbContext _context;
        public Sup2MesPathRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public Sup2mesPath GetSup2mesPathByStandID(int standId)
        {

            return _context.sup2mes_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }
        public List<Sup2mesPath> GetAll()
        {

            return _context.sup2mes_paths.ToList();
        }

        public List<Sup2mesPath> GetAllWithInclude()
        {
            return _context.sup2mes_paths.Include(k => k.Stand).ToList();
        }

        public bool Add(Sup2mesPath addObject)
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
