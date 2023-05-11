using MVCENG2.Models;

namespace MVCENG2.Interfaces
{
    public interface IStandsStatisticRepository
    {
        IEnumerable<Statistic> GetAllElementsThatStand(string standsIdentifier);
        Task<Statistic> GetByStandNameAsync(string statistic);
        Task<bool> Add(Statistic stand);
        Task<bool> Update(Statistic stand);
        Task<bool> Delete(Statistic stand);
        List<string> GetAllVINs();
        Task<bool> Save();



    }
}
