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

        public bool AddOrEdit(Translate translate)
        {
            if (!_context.translates.Any(m => m.EngVariant == translate.EngVariant))
            {
                if (translate.EngVariant!=null && translate.EngVariant != "")
                {
                    _context.Add(translate);
                }
                
                return Save();
            }
            else
            {
                return EditTranslate(translate);
            }            
        }

        
        public bool EditTranslate(Translate translate)
        {            
            Translate oldTranslate = _context.translates.Where(k => k.EngVariant == translate.EngVariant).FirstOrDefault();
            oldTranslate.RusVariant = translate.RusVariant;
            
            return Save();           
        }

       
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

      
    }
}
