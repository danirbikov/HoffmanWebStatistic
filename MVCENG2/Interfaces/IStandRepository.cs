using MVCENG2.Models.General;

namespace MVCENG2.Interfaces
{
    public interface IStandRepository
    {
        IEnumerable<Stand> GetAll();
        int GetStandIDbyName(string standName);
        bool Add(Stand stand);
        bool Update(Stand stand);
        bool Save();



    }
}
