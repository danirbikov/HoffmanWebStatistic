using MVCENG2.Models;

namespace MVCENG2.Interfaces
{
    public interface IStandRepository
    {
        IEnumerable<Stand> GetAll();
        //Task<Stand> GetByStandNameAsync(string standName);
        //Task<Stand> GetByProjectNameAsync(string projectName);
        //Task<Stand> GetByStandTypeAsync(string standType);
        bool Add(Stand stand);
        bool Update(Stand stand);
        bool Delete(Stand stand);
        bool Save();



    }
}
