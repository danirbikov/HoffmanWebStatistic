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

        public List<OperatorsPath> GetAll()
        {

            return _context.operators_paths.ToList();
        }
        public List<OperatorsPath> GetAllWithInclude()
        {

            return _context.operators_paths.Include(k=>k.Stand).ToList();
        }

        public bool Add(OperatorsPath addObject)
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
