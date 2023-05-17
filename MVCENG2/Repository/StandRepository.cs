using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using System.IO;

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
            return _context.stands.ToList();
            
        }
        /*
        public Stand GetByStandNameAsync(string standName)
        {
            return _context.Stand.Where(g => g.Stand_name == standName);
        }
        public async Task<Stand> GetByStandTypeAsync(string standType)
        {
            return await _context.Stand.FirstOrDefaultAsync(i => i.Stand_type == standType);
        }
        public async Task<Stand> GetByProjectNameAsync(string projectName)
        {
            return await _context.Stand.FirstOrDefaultAsync(i => i.Project == projectName);
        }
        */
        public int GetStandIDbyName(string standName)
        {
            var standObject = _context.stands.Where(k => k.StandName == standName).FirstOrDefault();
            if (standObject!=null)
            {
                return standObject.Id;
            }
            else
            {
                using (StreamWriter writer = new StreamWriter("C:\\Users\\BikovDI\\source\\repos\\MVCENG2\\MVCENG2\\Logs\\MysteryStands.txt", true, System.Text.Encoding.Default))
                {
                    writer.WriteLine(standName);
                }
                return _context.stands.Where(k => k.StandName == "UNKNOWN").FirstOrDefault().Id;
            }
              
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
