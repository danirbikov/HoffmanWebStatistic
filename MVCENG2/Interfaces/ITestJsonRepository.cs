using MVCENG2.Models.Hoffman;

namespace MVCENG2.Interfaces
{
    public interface ITestJsonRepository
    {           
        bool Add(ResultDataJSON testJSON);
        bool Update(ResultDataJSON testJSON);
        bool Delete(ResultDataJSON testJSON);
        bool Save();
    }
}
