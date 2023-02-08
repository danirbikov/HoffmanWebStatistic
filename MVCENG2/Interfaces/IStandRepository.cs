using MVCENG2.Models;

namespace MVCENG2.Interfaces
{
    public interface IStandRepository
    {
        Task<IEnumerable<Stand>> GetAll();
        Task<Stand> GetByIdAsync(int id);
        bool Add(Stand stand);
        bool Update(Stand stand);
        bool Delete(Stand stand);
        bool Save();



    }
}
