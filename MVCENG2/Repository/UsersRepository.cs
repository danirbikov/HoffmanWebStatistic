using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Models.ViewModel;

namespace HoffmanWebstatistic.Repository
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
            return _context.users.Include(k=> k.Role).Where(k => k.InactiveMark == "FALSE").FirstOrDefault(k=> k.ULogin==loginModel.Email && k.UPassword==loginModel.Password);
            
        }
        public IEnumerable<User> GetAll()
        {
            return _context.users.Where(k=>k.InactiveMark=="FALSE").Include(k=>k.Role).ToList();

        }
        public bool UnactiveUser(int userId)
        {
            User user = _context.users.Where(k => k.Id == userId).FirstOrDefault();
            user.InactiveMark = "TRUE";

            return Save();

        }

        public bool EditUser(User userObject)
        {
            User user = _context.users.Where(k => k.Id == userObject.Id).FirstOrDefault();

            user.ULogin = userObject.ULogin;
            user.UPassword = userObject.UPassword;
            user.RoleId = userObject.RoleId;           

            return Save();

        }

        public User GetUserByID(int userID)
        {
            var userObject = _context.users.Where(k => k.Id == userID).Include(k=>k.Role).FirstOrDefault();
            return userObject;

        }

        public User GetUserByName(string userName)
        {
            var userObject = _context.users.Where(k => k.ULogin == userName).FirstOrDefault();
            return userObject;

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
