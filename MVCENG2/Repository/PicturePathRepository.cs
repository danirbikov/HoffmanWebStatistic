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
       

        public DTCPaths GetPicturesPathByStandID(int standId)
        {

            return _context.pictures_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }

        public List<DTCPaths> GetAll()
        {

            return _context.pictures_paths.ToList();
        }

        public List<DTCPaths> GetAllWithInclude()
        {

            return _context.pictures_paths.Include(k=>k.Stand).ToList();
        }
    }
}
