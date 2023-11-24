using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
{
    public class Sup2MesTelegramsRepository
    {
        private readonly ApplicationDbContext _context;
        public Sup2MesTelegramsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public List<Sup2mesTelegram> GetAll()
        {
            return _context.sup2mes_telegrams.ToList();
        }
        public IQueryable<Sup2mesTelegram> GetAllQuery()
        {
            return _context.sup2mes_telegrams;
        }

        public IEnumerable<Sup2mesTelegram> GetSup2mesTelegramById(List<long> id)
        {
            var returnObject = GetAllQuery().Where(k => id.Contains(k.Id))
                .Include(k => k.Stand)
                .AsNoTracking()
                .ToList().OrderBy(x => id.IndexOf(x.Id)).AsEnumerable();

            return (returnObject);
        }

        public Sup2mesTelegram GetSup2MesTelegramById(long id)
        {
            var returnObject = GetAllQuery().Where(k => k.Id == id).FirstOrDefault();

            return (returnObject);
        }

        public int GetTelegramsCount()
        {
            return _context.sup2mes_telegrams.Count();
        }
        public bool Add(Sup2mesTelegram sup2mesTelegram)
        {
            try
            {
                _context.Add(sup2mesTelegram);
                return Save();
            }
            catch
            {
                return false;
            }          
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
