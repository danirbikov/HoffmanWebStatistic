using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
{
    public class Mes2SupTelegramsStandRepository
    {
        private readonly ApplicationDbContext _context;
        public Mes2SupTelegramsStandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Mes2supTelegramsStand mes2supTelegramsStand)
        {
            _context.Add(mes2supTelegramsStand);
            return Save();

        }

        public List<Mes2supTelegramsStand> GetAll()
        {

            return _context.mes2sup_telegrams_stands.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
