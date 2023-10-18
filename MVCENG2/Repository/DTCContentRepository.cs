using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class DTCContentRepository
    {
        private readonly ApplicationDbContext _context;
        public DTCContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(DtcContent dtcContent)
        {
            if (_context.dtc_content.Where(k=>k.Fname==dtcContent.Fname).Count()!=0)
            {
                Delete(dtcContent.Fname);
            }
            string extension = Path.GetExtension(dtcContent.Fname).ToLower();
            string[] imageExtensions = { ".xml"};

            if (imageExtensions.Contains(extension))
            {
                if (dtcContent.Fdata.Contains("encoding=\"UTF-8\"?"))
                {
                    dtcContent.Fdata = dtcContent.Fdata.Replace("encoding=\"UTF-8\"?", "encoding=\"UTF-16\"?");
                }

                _context.Add(dtcContent);
            }
            
            return Save();

        }

        public bool Delete(DtcContent dtcContent)
        {
            _context.Remove(dtcContent);
            return Save();
        }
        public bool Delete(string dtcName)
        {
            _context.Remove(_context.dtc_content.Where(k => k.Fname == dtcName).FirstOrDefault());
            return Save();
        }
        public bool Delete(int dtcId)
        {
            _context.Remove(_context.dtc_content.Where(k=>k.Id==dtcId).FirstOrDefault());
            
            return Save();
        }


        public IEnumerable<DtcContent> GetAll()
        {
            return _context.dtc_content.ToList();
            
        }

        public DtcContent EditDTC(string oldDTCName, string newDTCName, string xmlDtc)
        {
            DtcContent dtcContent = _context.dtc_content.Where(k => k.Fname == oldDTCName).FirstOrDefault();
            
            dtcContent.Fname =newDTCName;

            if (xmlDtc != null)
            {
                if (xmlDtc.Contains("encoding=\"UTF-8\"?"))
                {
                    xmlDtc = xmlDtc.Replace("encoding=\"UTF-8\"?", "encoding=\"UTF-16\"?");
                }
                dtcContent.Fdata = xmlDtc;
                dtcContent.Created = DateTime.Now;
            }
            else
            {
                dtcContent.Fdata = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + dtcContent.Fdata;
            }

            Save();

            return dtcContent;

        }

        public DtcContent GetDTCByName(string dtcName)
        {
            var dtcObject = _context.dtc_content.Where(k => k.Fname == dtcName).FirstOrDefault();

            dtcObject.Fdata = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + dtcObject.Fdata;

            return dtcObject;

        }
        public DtcContent GetDTCById(int dtcId)
        {
            var dtcObject = _context.dtc_content.Where(k => k.Id == dtcId).FirstOrDefault();
            dtcObject.Fdata = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + dtcObject.Fdata;

            return dtcObject;

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

      
    }
}
