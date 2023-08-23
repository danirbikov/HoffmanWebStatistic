using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Interfaces
{
    public interface ITestJsonRepository
    {           
        bool Add(ResultDataJSON_OLDTEST testJSON);
        bool Update(ResultDataJSON_OLDTEST testJSON);
        bool Delete(ResultDataJSON_OLDTEST testJSON);
        bool Save();
    }
}
