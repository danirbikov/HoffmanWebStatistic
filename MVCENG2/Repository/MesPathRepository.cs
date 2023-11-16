using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class MesPathRepository
    {
        private readonly ApplicationDbContext _context;
        public MesPathRepository(ApplicationDbContext context)
        {
            _context = context;
        }      

        public MesPathsCredential GetMesPathsCredentialByXSDName(string xsdName)
        {

            return _context.mes_paths_credentials.Include(k=>k.XsdPurpose).Where(k => k.XsdPurpose.XsdPurpose == xsdName).FirstOrDefault() ;
        }
        public List<MesPathsCredential> GetAll()
        {

            return _context.mes_paths_credentials.ToList();
        }



    }
}
