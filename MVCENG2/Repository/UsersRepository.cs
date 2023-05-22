using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Models.Hoffman;
using MVCENG2.Models.ViewModel;

namespace MVCENG2.Repository
{
    public class UsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(User user)
        {
            _context.Add(user);
            return Save();

        }

        public bool Delete(User user)
        {
            _context.Remove(user);
            return Save();
        }


        public User GetUserByAuthModel(LoginModel loginModel)
        {
            return _context.users.Include(k=> k.Role).FirstOrDefault(k=> k.ULogin==loginModel.Email && k.UPassword==loginModel.Password);
            
        }
        public IEnumerable<User> GetAll()
        {
            return _context.users.ToList();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
