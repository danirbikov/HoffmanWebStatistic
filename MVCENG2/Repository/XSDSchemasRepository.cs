using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
{
    public class XSDSchemasRepository
    {
        private readonly ApplicationDbContext _context;
        public XSDSchemasRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public List<XsdSchema> GetAll()
        {
            return _context.xsd_schemas.ToList();
        }
        public List<XsdSchema> GetAllWithInclude ()
        {
            return _context.xsd_schemas.Include(k=>k.Purpose).ToList();
        }
        public bool Add(XsdSchema xsdSchema)
        {
            _context.Add(xsdSchema);
            return Save();
        }

        public XsdSchema GetXSDSchemaByID(int xsdSchemaId)
        {
            var xsdSchemaObject = _context.xsd_schemas.Where(k => k.Id == xsdSchemaId).FirstOrDefault();
            return xsdSchemaObject;

        }

        public XsdSchema GetXSDSchemaByPurposeId(int purposeId)
        {
            return _context.xsd_schemas.Where(k => k.PurposeId == purposeId).FirstOrDefault();


        }

        public bool DeleteXSDSchema(int xsdSchemaId)
        {
            _context.xsd_schemas.Remove(new XsdSchema() { Id = xsdSchemaId});

            return Save();

        }

        public bool EditXSDSchema(XsdSchema xsdSchemaObject)
        {
            XsdSchema xsdSchema = _context.xsd_schemas.Where(k => k.Id == xsdSchemaObject.Id).FirstOrDefault();
            
            xsdSchema.XsdDescription = xsdSchemaObject.XsdDescription;
            xsdSchema.PurposeId = xsdSchemaObject.PurposeId;

            if (xsdSchemaObject.XsdSchemaFile!=null)
            {
                xsdSchema.XsdSchemaFile = xsdSchemaObject.XsdSchemaFile;
            }
            

            return Save();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
