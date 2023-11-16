using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
{
    public class XSDSchemasPurposeRepository
    {
        private readonly ApplicationDbContext _context;
        public XSDSchemasPurposeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public List<XsdSchemasPurpose> GetAll()
        {
            return _context.xsd_schemas_purpose.ToList();
        }

    }
}
