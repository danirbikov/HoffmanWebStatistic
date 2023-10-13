using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Models.General;

namespace HoffmanWebstatistic.Repository
{
    public class PicturePathRepository
    {
        private readonly ApplicationDbContext _context;
        public PicturePathRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public PicturesPath GePicturesPathByStandID(int standId)
        {

            return _context.pictures_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }
      
    }
}
