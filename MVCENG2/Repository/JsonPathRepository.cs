using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class JsonPathRepository
    {
        private readonly ApplicationDbContext _context;
        public JsonPathRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public JsonsPath GetJsonpPathByStandID(int standId)
        {

            return _context.jsons_paths.Where(k => k.StandId == standId).FirstOrDefault();
        }
        public List<JsonsPath> GetAll()
        {

            return _context.jsons_paths.ToList();
        }

        public List<JsonsPath> GetAllWithInclude()
        {
            return _context.jsons_paths.Include(k => k.Stand).ToList();
        }
        public bool Add(JsonsPath addObject)
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
