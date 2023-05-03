using MVCENG2.Models;

namespace MVCENG2.Interfaces
{
    public interface ITestJsonRepository
    {           
        bool Add(TestJSON testJSON);
        bool Update(TestJSON testJSON);
        bool Delete(TestJSON testJSON);
        bool Save();
    }
}
