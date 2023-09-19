using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class TranslateRepository
    {
        private readonly ApplicationDbContext _context;
        public TranslateRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Translate translate)
        {
            if (!_context.translates.Any(m => m.EngVariant == translate.EngVariant))
            {
                _context.Add(translate);
            }
            
            
            return Save();

        }

        
        public bool Delete(string translateName)
        {
            _context.Remove(_context.translates.Where(k => k.EngVariant == translateName).FirstOrDefault());
            return Save();
        }
        

        public IEnumerable<Translate> GetAll()
        {
            return _context.translates.ToList();
            
        }
        /*
        public Translate EditTranslate(string oldTranslateName, string newTranslateName, byte[] fileBytes)
        {
            
            Translate translate = _context.translates.Where(k => k.PName == oldTranslateName).FirstOrDefault();
            
            translate.PName=newTranslateName;

            if (fileBytes!=null)
            {
                translate.TranslateBytes=fileBytes;
            }

            Save();

            return translate;
            

        }

        public Translate GetTranslateByName(string translateName)
        {
           
            return _context.translates.Where(k => k.PName == translateName).FirstOrDefault();

        }
       */
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

      
    }
}
