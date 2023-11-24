using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class PicturePathRepository
    {
        private readonly ApplicationDbContext _context;
        public PicturePathRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public PicturesPath GetPicturesPathByStandID(int standId)
        {

            return _context.pictures_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }

        public List<PicturesPath> GetAll()
        {

            return _context.pictures_paths.ToList();
        }

        public List<PicturesPath> GetAllWithInclude()
        {

            return _context.pictures_paths.Include(k=>k.Stand).ToList();
        }

        public bool Add(PicturesPath addObject)
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
