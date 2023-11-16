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
