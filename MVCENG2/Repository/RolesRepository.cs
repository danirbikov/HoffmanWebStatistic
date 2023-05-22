using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Models.Hoffman;
using MVCENG2.Models.ViewModel;

namespace MVCENG2.Repository
{
    public class RolesRepository
    {
        private readonly ApplicationDbContext _context;
        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Role role)
        {
            _context.Add(role);
            return Save();

        }

        public bool Delete(Role role)
        {
            _context.Remove(role);
            return Save();
        }


        public Role GetByRoleName(string roleName)
        {
            return _context.roles.FirstOrDefault(k=> k.RName==roleName);
            
        }
        public IEnumerable<Role> GetAll()
        {
            return _context.roles.ToList();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Role role)
        {
            _context.Update(role);
            return Save();
        }
    }
}
