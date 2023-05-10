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


        public IEnumerable<Stand> GetAll()
        {
            return _context.Stand.ToList();
            
        }

        public async Task<Stand> GetByStandNameAsync(string standName)
        {
            return await _context.Stand.FirstOrDefaultAsync(i => i.Stand_name==standName); 
        }
        public async Task<Stand> GetByStandTypeAsync(string standType)
        {
            return await _context.Stand.FirstOrDefaultAsync(i => i.Stand_type == standType);
        }
        public async Task<Stand> GetByProjectNameAsync(string projectName)
        {
            return await _context.Stand.FirstOrDefaultAsync(i => i.Project == projectName);
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
