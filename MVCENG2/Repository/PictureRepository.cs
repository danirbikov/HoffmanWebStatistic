using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class PictureRepository
    {
        private readonly ApplicationDbContext _context;
        public PictureRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Picture picture)
        {
            if (_context.pictures.Where(k=>k.PName==picture.PName).Count()!=0)
            {
                Delete(picture.PName);
            }
            string extension = Path.GetExtension(picture.PName).ToLower();
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".ico",".tiff",".webp",".eps" };

            if (imageExtensions.Contains(extension))
            {
                picture.PName = picture.PName;
                _context.Add(picture);
            }
            
            return Save();

        }

        public bool Delete(Picture picture)
        {
            _context.Remove(picture);
            return Save();
        }
        public bool Delete(string pictureName)
        {
            _context.Remove(_context.pictures.Where(k => k.PName == pictureName).FirstOrDefault());
            return Save();
        }
        public bool Delete(int pictureId)
        {
            _context.Remove(_context.pictures.Where(k=>k.Id==pictureId).FirstOrDefault());
            
            return Save();
        }


        public IEnumerable<Picture> GetAll()
        {
            return _context.pictures.ToList();
            
        }

        public Picture EditPicture(string oldPictureName, string newPictureName, byte[] fileBytes)
        {
            Picture picture = _context.pictures.Where(k => k.PName == oldPictureName).FirstOrDefault();
            
            picture.PName=newPictureName;

            if (fileBytes!=null)
            {
                picture.PictureBytes=fileBytes;
            }

            Save();

            return picture;

        }

        public Picture GetPictureByName(string pictureName)
        {
           
            return _context.pictures.Where(k => k.PName == pictureName).FirstOrDefault();

        }
        public Picture GetPictureById(int pictureId)
        {

            return _context.pictures.Where(k => k.Id == pictureId).FirstOrDefault();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

      
    }
}
