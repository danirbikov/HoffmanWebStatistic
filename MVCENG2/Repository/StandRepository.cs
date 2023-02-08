using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models;

namespace MVCENG2.Repository
{
    public class StandRepository : IStandRepository
    {
        private readonly ApplicationDbContext _context;
        public StandRepository (ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Stand stand)
        {
            _context.Add(stand);
            return Save();

        }

        public bool Delete(Stand stand)
        {
            _context.Remove(stand);
            return Save();
        }


        public async Task<IEnumerable<Stand>> GetAll()
        {
            return await _context.Stand.ToListAsync();
            
        }

        public async Task<Stand> GetByIdAsync(int id)
        {
            return await _context.Stand.FirstOrDefaultAsync(i => i.Id==id); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Stand stand)
        {
            _context.Update(stand);
            return Save();
        }
    }
}
